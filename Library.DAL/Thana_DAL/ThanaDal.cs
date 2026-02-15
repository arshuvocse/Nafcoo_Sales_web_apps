using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;
using Library.DAO.Thana_DAO;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Library.DAL.DataManager;

namespace SalesSolution.Web.DataLayer
{
    public class ThanaDal
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable Get_Thana_All_List()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_ALl_Thana_List");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultInfo SaveThanaInfo(ThanaDao thana, string sessionUser)
        {
            int pk = 0;

            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
                gSqlParameterList.Add(new SqlParameter("@id", thana.ThanaId));
                gSqlParameterList.Add(new SqlParameter("@district_id", thana.district_id));
                gSqlParameterList.Add(new SqlParameter("@ThanaName", thana.ThanaName));
                gSqlParameterList.Add(new SqlParameter("@Lat", thana.Lat));
                gSqlParameterList.Add(new SqlParameter("@Long", thana.Long));
   

                if (thana.ThanaId > 0)
                {
                    //aSqlParameterList.Add(new SqlParameter("@id", thana.district_id));
                    //aSqlParameterList.Add(new SqlParameter("@Name", thana.ThanaName));
                    //DataTable dt = accessManager.GetDataTable("sp_check_ThanaInfo", aSqlParameterList);
                    //if (dt.Rows.Count == 0)
                    //{
                        gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser));
                        aInformation.isSuccess = accessManager.UpdateData("sp_UD_ThanaInfo", gSqlParameterList);
                    //}
                    //else
                    //{
                    //    aInformation.isSuccess = false;
                    //}

                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@CreatedBy", sessionUser));
                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_ThanaInfo", gSqlParameterList);

                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
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

        public void LoadDivision(DropDownList groupDropDownList)
        {
            string query = @"SELECT DivisionId,DivisionName FROM dbo.tbl_Division WHERE IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(groupDropDownList, "DivisionName", "DivisionId", query, "SSIDB");
        }
        public DataTable GetDivision_All_DDL()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_Division_Active_DDL");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        
       public DataTable Get_Thana_By_Id_Dal(string id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));
                DataTable dt = accessManager.GetDataTable("sp_GET_ThanaInfo_ById", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable GetDistrict_All_DDL(string id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));
                DataTable dt = accessManager.GetDataTable("sp_Get_District_Active_DDL", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public DataTable Get_District_All_List()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_ALl_District_List");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public DataTable GetDivision_All_List()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_ALl_Division_List");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable GetDistrictById(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));
                DataTable dt = accessManager.GetDataTable("sp_District_GetById",aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultInfo SaveDistrict(DistrictDao aMasterDao)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@DistrictId", aMasterDao.DistrictId));
                masterParameters.Add(new SqlParameter("@DivisionId", aMasterDao.DivisionId));
                masterParameters.Add(new SqlParameter("@DistrictName", aMasterDao.DistrictName));
                masterParameters.Add(new SqlParameter("@Lat", aMasterDao.Lat));
                masterParameters.Add(new SqlParameter("@Long", aMasterDao.Long));

                if (aMasterDao.DistrictId > 0)
                {

                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_District_Update", masterParameters);
                    pk = aMasterDao.DistrictId;
                }
                //else
                //{
                //    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                //    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                //    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_TopSheetMaster", masterParameters);

                //}


                if (pk > 0)
                {

                    aInformation.isSuccess = true;

                }
                else
                {
                    aInformation.isSuccess = false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }

        public DataTable GetDivisionById(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));
                DataTable dt = accessManager.GetDataTable("sp_Division_GetById", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultInfo SaveDivision(DivisionDao aMasterDao)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@DivisionId", aMasterDao.DivisionId));
                masterParameters.Add(new SqlParameter("@DistrictName", aMasterDao.DivisionName));
                masterParameters.Add(new SqlParameter("@Lat", aMasterDao.Lat));
                masterParameters.Add(new SqlParameter("@Long", aMasterDao.Long));

                if (aMasterDao.DivisionId > 0)
                {

                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_Division_Update", masterParameters);
                    pk = aMasterDao.DivisionId;
                }
                //else
                //{
                //    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                //    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                //    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_TopSheetMaster", masterParameters);

                //}


                if (pk > 0)
                {

                    aInformation.isSuccess = true;

                }
                else
                {
                    aInformation.isSuccess = false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }
    }
}