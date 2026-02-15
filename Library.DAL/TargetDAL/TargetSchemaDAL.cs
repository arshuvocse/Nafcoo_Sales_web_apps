using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.Target_DAO;
using SalesSolution.Web.Models;

namespace Library.DAL.TargetDAL
{
    public class TargetSchemaDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public ResultInfo SaveMasterDetals(int masterId, string schemaName,Decimal schemaAmount, List<TargetSchemaDetailDAO> _Dtls, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                var SchemaMasterId = masterId;

                gSqlParameterList.Add(new SqlParameter("@SchemaAmount", schemaAmount));
                gSqlParameterList.Add(new SqlParameter("@SchemaName", schemaName));

                if (SchemaMasterId > 0)
                {
                    gSqlParameterList.Add(new SqlParameter("@SchemaMasterId", SchemaMasterId));
                    gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@UpdateDate", entryDtae));

                    bool result = accessManager.UpdateData("sp_UD_TargetSchema_UpdateMasterInfo", gSqlParameterList);
                    aInformation.isSuccess = result;
                    //pk = _Master.SchemaMasterId;
                    pk = SchemaMasterId;
                    aInformation.Id = pk;
                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@EntryDate", entryDtae));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_TargetSchema_SaveMasterInfo", gSqlParameterList);

                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                        aInformation.Id = pk;
                    }
                }

                //Deleted Previous
                List<SqlParameter> aDeleteParameters = new List<SqlParameter>();

                aDeleteParameters.Add(new SqlParameter("@SchemaMasterId", pk));
                accessManager.DeleteData("sp_TargetSchema_DeleteDetailInfoById", aDeleteParameters);

                foreach (var item in _Dtls)
                {

                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@SchemaMasterId", pk));
                    aParameters.Add(new SqlParameter("@ProductId", item.ProductId));
                    aParameters.Add(new SqlParameter("@Percentage", item.Percentage));

                    accessManager.SaveData("sp_TargetSchema_SaveDetailsInfo", aParameters);
                }
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                aInformation.isSuccess = false;
                aInformation.ErrorMessage = exception.Message;

                throw exception;
            }
            finally
            {

                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }



        public ResultInfo SaveTargetDeclarationData(int TargetDeclarationId,int schemaMasterId,int subTerritoryId, int Year, string month, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@SchemaMasterId", schemaMasterId));
                gSqlParameterList.Add(new SqlParameter("@Year", Year));
                gSqlParameterList.Add(new SqlParameter("@MonthName", month));
                gSqlParameterList.Add(new SqlParameter("@SubTerritoryId", subTerritoryId));

                if (TargetDeclarationId > 0)
                {
                    gSqlParameterList.Add(new SqlParameter("@TargetDeclarationId", TargetDeclarationId));
                    gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@UpdateDate", entryDtae));

                    bool result = accessManager.UpdateData("sp_UD_TargetDeclaration_UpdateInfo", gSqlParameterList);
                    aInformation.isSuccess = result;
                    //pk = _Master.SchemaMasterId;
                    pk = TargetDeclarationId;
                    aInformation.Id = pk;
                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@EntryDate", entryDtae));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_I_TargetDeclaration_SaveInfo", gSqlParameterList);

                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                        aInformation.Id = pk;
                    }
                }
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                aInformation.isSuccess = false;
                aInformation.ErrorMessage = exception.Message;

                throw exception;
            }
            finally
            {

                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }

        public void GetSchemaDropDown(object schemaDropdown)
        {
            throw new NotImplementedException();
        }

        public void GetSchemaDropDown(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();

            string queryStr = "Select TRG.SchemaMasterId,TRG.SchemaName From tbl_TargetSchemaMaster AS TRG";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "SchemaName", "SchemaMasterId", queryStr);
        }

        public DataTable GetTargetSchemaData(string pram)
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            try
            {

                string query = @"SELECT TRM.SchemaMasterId,TRM.SchemaAmount,TRM.EntryDate,TRM.UpdateDate,TRD.SchemaDetailId,TRD.ProductId,TRD.Percentage,PD.ProductName,usr.UserName AS EntryBy,usr2.UserName AS UpdateBy FROM tbl_TargetSchemaMaster AS TRM
                                LEFT JOIN tbl_TargetSchemaDetail AS TRD ON TRM.SchemaMasterId = TRD.SchemaMasterId
                                LEFT JOIN tblUser AS usr ON TRM.EntryBy = usr.UserId
								LEFT JOIN tblUser AS usr2 ON TRM.UpdateBy = usr2.UserId
                                LEFT JOIN tblProduct AS PD ON TRD.ProductId = PD.ProductId  WHERE  " + pram + " ORDER BY SchemaName ";


                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                
            }
        }


        public DataTable GetTagetDeclarationData()
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            try
            {

                string query = @"SELECT TD.*, urs.UserName, TSM.SchemaName,TSM.SchemaCode
                                FROM tbl_TargetDeclaration AS TD
                                LEFT JOIN tbl_TargetSchemaMaster AS TSM ON TD.SchemaMasterId = TSM.SchemaMasterId
                                LEFT JOIN tblUser AS urs ON TD.EntryBy = urs.UserId";


                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {

            }
        }

        public DataTable GetDataById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@SchemaMasterId", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_TargetSchemaData_ById", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public DataTable Get_SubterritoryIdByCode(string subterritoryCode)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@SubterritoryCode", subterritoryCode));

                DataTable dt = accessManager.GetDataTable("sp_Get_SubterritoryId_ByCode", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable Get_SchemaIdByCode(string schemaCode)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@SchemaCode", schemaCode));

                DataTable dt = accessManager.GetDataTable("sp_Get_SchemaMasterId_BySchemaCode", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable GetTargetDeclarationDataById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@TargetDeclarationId", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_TargetDeclarationData_ById", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }



        public DataTable GetTargetVsAchievement_ReportData(int Year,string Month)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@year", Year));
                aSqlParameterlist.Add(new SqlParameter("@month", Month));

                DataTable dt = accessManager.GetDataTable("sp_Get_TargetVsAchievement_ReportData", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
    }
}
