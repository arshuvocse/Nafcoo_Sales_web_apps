using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http; 
using System.Threading.Tasks;
 
using ListItem = System.Web.UI.WebControls.ListItem;
namespace Library.DAL.DataManager
{
    public class DataAccessManagerAsync
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        private SqlDataReader sqlDataReader = null;
        private SqlTransaction sqlTransaction = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private const int CommandTimeout = 120000;
        private bool isException;
        private bool returnValue = true;
        private string ConnectionString(string database)
        {
            return @"data source=" + SqlUserAccess.DataSource + ";Initial Catalog=" + database +
                   ";Integrated Security=false; User Id=" +
                   SqlUserAccess.UserName + "; password=" + SqlUserAccess.PassWord + ";";

            //return @"data source=" + SqlUserAccess.DataSource + ";Initial Catalog=" + database +
            //    ";Integrated Security=true;";

        }
        public async Task<bool> SqlConnectionOpen(string database)
        {
            try
            {
                sqlConnection = new SqlConnection(ConnectionString(database));

                if (sqlConnection.State != ConnectionState.Open)
                {
                   await sqlConnection.OpenAsync();
                    sqlTransaction = (SqlTransaction)sqlConnection.BeginTransaction();

                }
            }
            catch (SqlException sqlException)
            {
                isException = true;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    returnValue = false;
                }

            }
            return returnValue;
        }

        public bool SqlConnectionClose(bool IsRollBack = false)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    if (sqlTransaction != null)
                    {
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();

                        }
                        if (IsRollBack)
                        {
                            sqlTransaction.Rollback();
                        }
                        else
                        {
                            sqlTransaction.Commit();
                        }
                        sqlTransaction.Dispose();
                    }
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }

            }
            catch (SqlException sqlException)
            {
                isException = true;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    returnValue = false;
                }

            }

            return returnValue;
        }
        public async Task<SqlDataReader> GetSqlDataReaderAsync(string StoreProcedure, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            }
            catch (SqlException sqlException)
            {
                isException = true;
                sqlDataReader = null;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return sqlDataReader;
        }
        public async Task<SqlDataReader> GetSqlDataReaderAsync(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }



                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                sqlCommand.Parameters.Clear();
            }
            catch (SqlException sqlException)
            {
                isException = true;
                sqlDataReader = null;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Dispose();
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return sqlDataReader;
        }
        public async Task<DataTable> GetDataTableAsync(string StoreProcedure, bool IsBigData = false)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = new DataSet();
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                dt = ds.Tables[0];
            }
            catch (SqlException sqlException)
            {
                isException = true;
                dt = null;
                throw sqlException;
            }
            finally
            {
                if (isException)
                {
                    SqlConnectionClose(true);
                }

            }
            return dt;
        }
        public async Task<DataTable> GetDataTableAsync(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = new DataSet();
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                dt = ds.Tables[0];

            }
            catch (SqlException sqlException)
            {
                isException = true;
                dt = null;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }
            return dt;
        }

        private int ExecuteNonQueryData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsPrimaryKey = true, bool IsBigData = false)
        {
            int primaryKey = 0;
            try
            {
                sqlCommand = new SqlCommand();

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = StoreProcedure;
                sqlCommand.Transaction = sqlTransaction;

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                primaryKey = Convert.ToInt32(sqlCommand.ExecuteScalar());

            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                isException = true;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }
            return primaryKey;
        }
        /// <summary>
        /// After Saving Data Only Identity Value will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>
        private bool ExecuteNonQueryData(string StoreProcedure, List<SqlParameter> SqlParameterList, bool IsBigData = false)
        {
            try
            {
                sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = StoreProcedure,
                    Transaction = sqlTransaction
                };
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddRange(SqlParameterList.ToArray());
                if (IsBigData)
                {
                    sqlCommand.CommandTimeout = CommandTimeout;
                }
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
                returnValue = false;
                isException = true;
                throw sqlException;
            }
            finally
            {
                sqlCommand.Parameters.Clear();
                if (isException)
                {
                    SqlConnectionClose(true);
                }
            }

            return returnValue;
        }
        /// <summary>
        /// After Saving Data a Boolean will be Returned
        /// </summary>
        /// <param name="StoreProcedure"></param>
        /// <param name="SqlParameterList"></param>
        /// <returns></returns>

    }
}