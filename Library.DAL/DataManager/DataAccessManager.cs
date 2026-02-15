
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Library.DAL.DataManager
{
    

    public class DataAccessManager
    {
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        private SqlDataReader sqlDataReader;
        private const int DefaultCommandTimeout = 180; // seconds

        private bool isException;
        private bool returnValue;

        private string ConnectionString(string database)
        {
            // প্রয়োজনে Encrypt/TrustServerCertificate যোগ করো (ইন্টারনাল নেটওয়ার্কে সাধারণ)
            return "Data Source=" + SqlUserAccess.DataSource +
                   ";Initial Catalog=" + database +
                   ";Integrated Security=false;User ID=" + SqlUserAccess.UserId +
                   ";Password=" + SqlUserAccess.PassWord +
                   ";Encrypt=True;TrustServerCertificate=True;";
            // বিকল্প:
            // ";Integrated Security=true;"
        }

        // ---------- Core guards ----------
        private void EnsureOpen(string database)
        {
            var cs = ConnectionString(database);

            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(cs);
            }
            else if (!string.Equals(sqlConnection.ConnectionString, cs, StringComparison.Ordinal))
            {
                SafeDispose(sqlConnection);
                sqlConnection = new SqlConnection(cs);
            }

            if (sqlConnection.State == ConnectionState.Broken)
            {
                try { sqlConnection.Close(); } catch { }
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            // completed transaction হলে null করে দাও
            if (sqlTransaction != null && sqlTransaction.Connection == null)
                sqlTransaction = null;
        }

        private static void SafeDispose(IDisposable d)
        {
            try { if (d != null) d.Dispose(); } catch { }
        }
 

private static int SafeScalarToInt(object scalar)
{
    if (scalar == null || scalar == DBNull.Value) return 0;

    if (scalar is int)     return (int)scalar;
    if (scalar is short)   return (short)scalar;
    if (scalar is byte)    return (byte)scalar;

    if (scalar is long)    return checked((int)(long)scalar);
    if (scalar is decimal) return decimal.ToInt32((decimal)scalar);
    if (scalar is double)  return checked((int)Convert.ToDouble(scalar, CultureInfo.InvariantCulture));
    if (scalar is float)   return checked((int)Convert.ToSingle(scalar, CultureInfo.InvariantCulture));

    if (scalar is string)
    {
        int tmp;
        if (int.TryParse((string)scalar, NumberStyles.Integer, CultureInfo.InvariantCulture, out tmp))
            return tmp;
        // fall through to throw below
    }

    try
    {
        // General fallback for IConvertible types
        return Convert.ToInt32(scalar, CultureInfo.InvariantCulture);
    }
    catch (Exception)
    {
        throw new InvalidCastException(
            "ExecuteScalar returned a non-integer value of type " +
            scalar.GetType().FullName);
    }
}

        // ---------- Connection open/close ----------
        public bool SqlConnectionOpen(string database)
        {
            isException = false;
            returnValue = false;

            try
            {
                EnsureOpen(database);        // শুধু ওপেন; ট্রানজ্যাকশন এখানে শুরু করছি না
                returnValue = true;
                return true;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public bool SqlConnectionClose(bool isRollBack = false)
        {
            isException = false;
            returnValue = true;

            try
            {
                // reader থাকলে আগে বন্ধ
                if (sqlDataReader != null)
                {
                    try { if (!sqlDataReader.IsClosed) sqlDataReader.Close(); } catch { }
                    sqlDataReader = null;
                }

                // ট্রানজ্যাকশন alive থাকলে কমিট/রোলব্যাক
                if (sqlTransaction != null && sqlTransaction.Connection != null)
                {
                    try
                    {
                        if (isRollBack) sqlTransaction.Rollback();
                        else sqlTransaction.Commit();
                    }
                    finally
                    {
                        SafeDispose(sqlTransaction);
                        sqlTransaction = null;
                    }
                }

                // কানেকশন dispose
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    try { sqlConnection.Close(); } catch { }
                }
                SafeDispose(sqlConnection);
                sqlConnection = null;

                return true;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
            finally
            {
                if (isException) returnValue = false;
            }
        }

        // ---------- READ (DataReader) ----------
        // নোট: Reader-এ ট্রানজ্যাকশন attach করা হয়নি; CommandBehavior.CloseConnection দেওয়া আছে।
        public SqlDataReader GetSqlDataReader(string storeProcedure, bool isBigData = false, string database = null)
        {
            isException = false;

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                var cmd = new SqlCommand(storeProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout
                };

                // Reader নিলে command/txn dispose করা যাবে না—CloseConnection ব্যবহার করি।
                sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return sqlDataReader;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public SqlDataReader GetSqlDataReader(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                var cmd = new SqlCommand(storeProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout
                };

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return sqlDataReader;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        // ---------- READ (DataTable / DataSet) ----------
        public DataTable GetDataTable(string storeProcedure, bool isBigData = false, string database = null)
        {
            isException = false;
            var dt = new DataTable();

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                using (var cmd = new SqlCommand(storeProcedure, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public DataTable GetDataTable(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            var dt = new DataTable();

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                using (var cmd = new SqlCommand(storeProcedure, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                    if (parameters != null && parameters.Count > 0)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public DataSet GetDataSet(string storeProcedure, bool isBigData = false, string database = null)
        {
            isException = false;
            var ds = new DataSet();

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                using (var cmd = new SqlCommand(storeProcedure, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public DataSet GetDataSet(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            var ds = new DataSet();

            try
            {
                if (database != null) EnsureOpen(database);
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");

                using (var cmd = new SqlCommand(storeProcedure, sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                    if (parameters != null && parameters.Count > 0)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        // ---------- WRITE ----------
        private int ExecuteNonQueryScalarInt(string storeProcedure, List<SqlParameter> parameters, bool isBigData)
        {
            // writes-এর আগে tx নিশ্চিত
            if (sqlTransaction == null || sqlTransaction.Connection == null)
            {
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");
                sqlTransaction = sqlConnection.BeginTransaction();
            }

            using (var cmd = new SqlCommand(storeProcedure, sqlConnection, sqlTransaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                var scalar = cmd.ExecuteScalar();
                return SafeScalarToInt(scalar);
            }
        }

        private bool ExecuteNonQueryVoid(string storeProcedure, List<SqlParameter> parameters, bool isBigData)
        {
            if (sqlTransaction == null || sqlTransaction.Connection == null)
            {
                if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection is not open.");
                sqlTransaction = sqlConnection.BeginTransaction();
            }

            using (var cmd = new SqlCommand(storeProcedure, sqlConnection, sqlTransaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = isBigData ? Math.Max(DefaultCommandTimeout, 600) : DefaultCommandTimeout;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        /// After Saving Data Only Identity Value will be Returned
        public int SaveDataReturnPrimaryKey(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            try
            {
                if (database != null) EnsureOpen(database);
                return ExecuteNonQueryScalarInt(storeProcedure, parameters, isBigData);
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        /// After Saving Data a Boolean will be Returned
        public bool SaveData(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            try
            {
                if (database != null) EnsureOpen(database);
                return ExecuteNonQueryVoid(storeProcedure, parameters, isBigData);
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public bool UpdateData(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            try
            {
                if (database != null) EnsureOpen(database);
                return ExecuteNonQueryVoid(storeProcedure, parameters, isBigData);
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        public bool DeleteData(string storeProcedure, List<SqlParameter> parameters, bool isBigData = false, string database = null)
        {
            isException = false;
            try
            {
                if (database != null) EnsureOpen(database);
                return ExecuteNonQueryVoid(storeProcedure, parameters, isBigData);
            }
            catch (SqlException)
            {
                isException = true;
                throw;
            }
        }

        // ---------- Utility (serialize) ----------
        public IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
            {
                var row = new Dictionary<string, object>(cols.Count);
                foreach (var col in cols)
                    row.Add(col, reader[col]);
                results.Add(row);
            }
            return results;
        }
    }

}