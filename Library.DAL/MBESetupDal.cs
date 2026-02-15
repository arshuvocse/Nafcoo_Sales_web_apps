using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.DoctorModule_DAO;
using SalesSolution.Web.Models;

namespace Library.DAL
{
    public class MBESetupDal
    {
        private DataAccessManager accessManager = new DataAccessManager();


        public DataTable Get_MBEInfo(string parameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parameter", parameter));
                DataTable dt = accessManager.GetDataTable("sp_GET_MBEInfo", aSqlParameterlist);
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


        public ResultInfo Save_MBEInfo(MBEInfoDao aMIOInfo, int sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();

                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@MBEInfoId", aMIOInfo.MBEInfoId == 0 ? DBNull.Value : (object)aMIOInfo.MBEInfoId));
                gSqlParameterList.Add(new SqlParameter("@SubTerritoryId", aMIOInfo.SubTerritoryId == 0 ? DBNull.Value : (object)aMIOInfo.SubTerritoryId));
                gSqlParameterList.Add(new SqlParameter("@EmployeeId", aMIOInfo.EmployeeId == 0 ? DBNull.Value : (object)aMIOInfo.EmployeeId));
                gSqlParameterList.Add(new SqlParameter("@entryBy", sessionUser == 0 ? DBNull.Value : (object)sessionUser));
                gSqlParameterList.Add(new SqlParameter("@isActive", aMIOInfo.IsActive));
                gSqlParameterList.Add(new SqlParameter("@ActiveDate", aMIOInfo.ActiveDate == DateTime.MinValue ? DBNull.Value : (object)aMIOInfo.ActiveDate));


                if (aMIOInfo.MBEInfoId > 0)
                {

                    aInformation.isSuccess = accessManager.UpdateData("sp_UD_MBEInfo", gSqlParameterList);
                    pk = aMIOInfo.MBEInfoId;

                    //List<SqlParameter> aSqlprm = new List<SqlParameter>();
                    //aSqlprm.Add(new SqlParameter("@TerritoryId", aMIOInfo.TerritoryId));
                    //aSqlprm.Add(new SqlParameter("@MIOId", aMIOInfo.MIOId));
                    //DataTable dt = accessManager.GetDataTable("sp_check_MIOInfo", aSqlprm);
                    //if (dt.Rows.Count == 0)
                    //{
                    //    aInformation.isSuccess = accessManager.UpdateData("sp_UD_MIOInfo", gSqlParameterList);
                    //    pk = aMIOInfo.MIOId;
                    //}
                    //else
                    //{
                    //    aInformation.isValiCheck = true;

                    //}
                }
                else
                {

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_MBEInfo", gSqlParameterList);
                }

                if (pk > 0)
                {
                    aInformation.isSuccess = true;
                }
                else
                {
                    aInformation.isValiCheck = true;

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


        // Group

        public DataTable GetGroupInfo_Active()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_CS_Group_Active");
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


        // Zone


        public DataTable GetZone_byGroupId_Active(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@GroupId", id));
                DataTable dt = accessManager.GetDataTable("sp_Get_Zone_All_Active", aSqlParameterlist);
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

        // Area

        public DataTable GetArea_ByZoneId_Active(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));

                DataTable dt = accessManager.GetDataTable("sp_CS_GetArea_ByZoneId_Active", aSqlParameterlist);
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


        
        

        // Territory


        public DataTable Get_VacentTerritory(int areaId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", areaId));
                DataTable dt = accessManager.GetDataTable("sp_CS_GetTerritory_ByAreaId_Active", aSqlParameterlist);
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

        // Subterritory

        public DataTable GetSubTerritory_ByTerritoryId_Active(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", id));

                DataTable dt = accessManager.GetDataTable("sp_CS_GetSubTerritory_ByTerritoryId_Active", aSqlParameterlist);
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



        // MBE

        public DataTable GetEmployee_AllFieldForceEmployeeList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_EmployeeListFieldForce", aSqlParameterlist);
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


        // Edit Data


        public DataTable GetEMBEditDataById(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


                aSqlParameterlist.Add(new SqlParameter("@id", id));

                DataTable dt = accessManager.GetDataTable("sp_GET_MBEInfo_ById", aSqlParameterlist);
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

        public MBEInfoDao GetEMBEditDataDAL(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                MBEInfoDao master = new MBEInfoDao();
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GET_MBEInfo_ById", aSqlParameters);
                while (dr.Read())
                {
                    master.MBEInfoId = (int)dr["MBEInfoId"];
                    master.GroupId = (int)dr["GroupId"];
                    master.RegionId = (int)dr["RegionId"];
                    master.AreaId = (int)dr["AreaId"];
                    master.TerritoryId = (int)dr["TerritoryId"];
                    master.EmployeeId = DBNull.Value.Equals(dr["EmployeeId"]) ? 0 : (int)dr["EmployeeId"];
                    master.SubTerritoryId = (int)dr["SubTerritoryId"];
                    master.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    master.ActiveDateStr = (dr["ActiveDateStr"].ToString());

                }
                return master;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

    }
}
