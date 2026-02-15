using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Library.DAL.InternalCls
{
    public class ClsCommonInternalDAL
    {


        internal void LoadDropDownValueWithoutDataBase(DropDownList ddl, string displayField, string valueField, string queryString)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 1200000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataDDL = db.ExecuteDataSet(dbCommand).Tables[0];
                ddl.DataTextField = displayField;
                ddl.DataValueField = valueField;
                ddl.DataSource = dataDDL;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select from list", String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        internal bool DeleteAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            try
            {
                ActionStatus =
                    Convert.ToBoolean(ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, string.Empty, false));
            }
            catch (Exception ex)
            {
                ActionStatus = false;
            }
            return ActionStatus;
        }
        internal DataTable GetDataTableAction(string StoreProcedureName, string dataBaseName)
        {
            DataTable dt = new DataTable();

            try
            {
                Database db;
                DbCommand dbCommand;

                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 199000000;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal IDataReader DataContainerDataReader(string queryString)
        {
            try
            {
                IDataReader dataReader;
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataReader = db.ExecuteReader(dbCommand);
                return dataReader;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        internal DataTable DataContainerDataTable(string queryString)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                return dataContain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal int SaveDataByInsertCommandWithIdentity(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            int pk = 0;
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                using (dbCommand = db.GetSqlStringCommand(queryString + ";select @ID=SCOPE_IDENTITY()"))
                {
                    dbCommand.CommandTimeout = 199000000;
                    dbCommand.Parameters.Clear();
                    dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                    db.AddOutParameter(dbCommand, "@ID", DbType.Int32, 20);
                    if (db.ExecuteNonQuery(dbCommand) > 0)
                    {
                        pk = Convert.ToInt32(dbCommand.Parameters["@ID"].Value.ToString());
                        dbCommand.Parameters.Clear();
                        return pk;
                    }
                    else
                    {
                        dbCommand.Parameters.Clear();
                        return pk;
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool SaveDataByInsertCommand(string queryString)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool UpdateDataByUpdateCommandNew(string queryString, List<SqlParameter> aSqlParameterlist)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal bool UpdateDataByUpdateCommand(string queryString)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal void LoadDropDownValue(DropDownList ddl, string displayField, string valueField, string queryString, string dataBaseName)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataDDL = db.ExecuteDataSet(dbCommand).Tables[0];
                ddl.DataTextField = displayField;
                ddl.DataValueField = valueField;
                ddl.DataSource = dataDDL;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select from list", String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        internal IDataReader DataContainerDataReader(string queryString, string dataBaseName)
        {
            try
            {
                IDataReader dataReader;
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataReader = db.ExecuteReader(dbCommand);
                return dataReader;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal DataTable DataContainerDataTable(string queryString, string dataBaseName)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 1990000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                return dataContain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal bool SaveDataByInsertCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal bool UpdateAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            try
            {
                ActionStatus =
                    Convert.ToBoolean(ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, string.Empty, false));
            }
            catch (Exception ex)
            {

                ActionStatus = false;
            }

            return ActionStatus;
        }
        internal bool UpdateDataByUpdateCommand(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal bool UpdateDataByUpdateCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
               // throw ex;
            }
        }

        internal bool DeleteDataByDeleteCommand(string queryString)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionStringSSIDB");
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal bool DeleteDataByDeleteCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                //dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand = db.GetStoredProcCommand("ExecuteAllSqlQueryByStoreProcedure");
                dbCommand.CommandTimeout = 199000000;
                db.AddInParameter(dbCommand, "Query", DbType.String, queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int RunStoreProcedure(string sp, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            int pk = 0;
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                using (dbCommand = db.GetStoredProcCommand(sp))
                {
                    dbCommand.CommandTimeout = 199000000;
                    dbCommand.Parameters.Clear();
                    dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());

                    if (db.ExecuteNonQuery(dbCommand) > 0)
                    {
                        pk = 1;
                        return pk;
                    }
                    else
                    {
                        dbCommand.Parameters.Clear();
                        return pk;
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // SP Insert Method
        bool ActionStatus;
        internal int SaveAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist,
            string PrimaryKeyParameter)
        {
            int pk = 0;
            try
            {
                pk = ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, PrimaryKeyParameter, true);
            }
            catch (Exception ex)
            {
                pk = 0;
            }
            return pk;
        }
        private int ExecuteNonQueryAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist,
            string PrimaryKeyParameter, bool IsPrimaryKey)
        {

            int pk = 0;
            try
            {
                Database db;
                DbCommand dbCommand;

                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + "SSIDB");
                dbCommand = db.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 199000000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                if (IsPrimaryKey)
                {
                    db.AddOutParameter(dbCommand, PrimaryKeyParameter, DbType.Int32, 10);
                    if (db.ExecuteNonQuery(dbCommand) > 0)
                    {
                        pk = int.Parse(dbCommand.Parameters[PrimaryKeyParameter].Value.ToString());
                        dbCommand.Parameters.Clear();
                    }
                    else
                    {
                        pk = 0;
                        dbCommand.Parameters.Clear();
                    }
                }
                else
                {

                    if (db.ExecuteNonQuery(dbCommand) > 0)
                    {
                        pk = 1;
                        dbCommand.Parameters.Clear();
                    }
                    else
                    {
                        pk = 0;
                        dbCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pk;
        }


        internal DataTable GetDataTableAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist, string dataBaseName)
        {
            DataTable dt = new DataTable();
            Database db;
            DbCommand dbCommand = null;

            try
            {

                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 199000000;
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                dbCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                if (dbCommand != null) dbCommand.Parameters.Clear();
                throw;
            }
            return dt;
        }

    }
}
