using Library.DAL.DataManager;
using Library.DAO.DoctorModule_DAO;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Library.DAL.DoctorModule_DAL
{
  public  class UserInfoDAL
    {

        private DataAccessManager accessManager = new DataAccessManager();
        public DataTable GetUserSetupById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_UserMaster_ById", aSqlParameterlist);
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
        public DataTable GetUserSetupByEmpId(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_UserMaster_ByEmpId", aSqlParameterlist);
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

        
        public DataTable GetUserDetailMarketById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_UserDetailMarket_ById", aSqlParameterlist);
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

        public DataTable check_UserInfoByEmpCode(string MasterId, string PageName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@MasterId", MasterId));
                aSqlParameterlist.Add(new SqlParameter("@PageName", PageName));

                DataTable dt = accessManager.GetDataTable("sp_check_Vali_EmployeeInfoEntry", aSqlParameterlist);
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

        public DataTable check_UserInfoByEmpCodeUpdate(string MasterId, string EmpCode)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@EmpId", MasterId));
                aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", EmpCode));

                DataTable dt = accessManager.GetDataTable("sp_check_Vali_EmployeeInfoUpdate", aSqlParameterlist);
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
        public ResultInfo SaveUserInfo(UserDAO master, List<BonusCampaignMarketDetailDAO> _MarketDtls,  String _ProLineArray,string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@UserId", master.UserId));
                gSqlParameterList.Add(new SqlParameter("@UserType", master.UserType ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@UserTypeId", master.UserTypeId ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@EmpInfoId", master.EmpInfoId ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@UserName", master.UserName ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@LoginName", master.LoginName ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@Password", master.Password ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@IsAppsUser", master.IsAppsUser ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IMEI_One", master.IMEI_One ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IMEI_Two", master.IMEI_Two ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@UserRoleID", master.UserRoleID ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@UserStatus", master.UserStatus ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@ActiveInActiveDate", master.ActiveInActiveDate ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IsMainDashboard", master.IsMainDashboard ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IsDepotDashboard", master.IsDepotDashboard ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser));
                if (master.UserId > 0)
                {

                    List<SqlParameter> aSqlprm = new List<SqlParameter>();
                    aSqlprm.Add(new SqlParameter("@UserId", master.UserId));
                    //aSqlprm.Add(new SqlParameter("@UserName", master.UserName));
                    aSqlprm.Add(new SqlParameter("@LoginName", master.LoginName));
                    aSqlprm.Add(new SqlParameter("@EmpInfoId", master.EmpInfoId));
                    DataTable dt = accessManager.GetDataTable("sp_check_UserInfo", aSqlprm);
                    if (dt.Rows.Count == 0)
                    {
                        aInformation.isSuccess = accessManager.UpdateData("sp_Update_UserMaster_new", gSqlParameterList);

                        pk = master.UserId;
                    }
                    else
                    {
                        aInformation.isValiCheck = true;

                    }


                }
                else
                {
                    List<SqlParameter> aSqlprm = new List<SqlParameter>();
                    aSqlprm.Add(new SqlParameter("@UserId", master.UserId));
                    //aSqlprm.Add(new SqlParameter("@UserName", master.UserName));
                    aSqlprm.Add(new SqlParameter("@LoginName", master.LoginName));
                    aSqlprm.Add(new SqlParameter("@EmpInfoId", master.EmpInfoId));
                    DataTable dt = accessManager.GetDataTable("sp_check_UserInfo_Save", aSqlprm);
                    if (dt.Rows.Count == 0)
                    {
                        pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_UserMaster_New", gSqlParameterList);
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

                if (pk > 0)
                {

                    if(aInformation.isSuccess)
                    {
                        foreach (var item in _MarketDtls)
                        {

                            List<SqlParameter> aSQL = new List<SqlParameter>();
                            aSQL.Add(new SqlParameter("@UserId", pk));
                            aSQL.Add(new SqlParameter("@GroupId", item.GroupId ?? (object)DBNull.Value));
                            aSQL.Add(new SqlParameter("@RegionId", item.RegionId ?? (object)DBNull.Value));
                            aSQL.Add(new SqlParameter("@AreaId", item.AreaId ?? (object)DBNull.Value));
                            aSQL.Add(new SqlParameter("@TerritoryId", item.TerritoryId ?? (object)DBNull.Value));
                            aSQL.Add(new SqlParameter("@SubTerritoryId", item.SubTerritoryId ?? (object)DBNull.Value));
                            aSQL.Add(new SqlParameter("@MarketId", item.MarketId ?? (object)DBNull.Value));

                            aInformation.isSuccess = accessManager.SaveData("sp_Save_UserMarketDetail", aSQL);

                        }


                        if (_ProLineArray != "")
                        {
                            string[] ProLine = _ProLineArray.Split(',');

                            foreach (String item in ProLine)
                            {
                                if (item != "")
                                {
                                    List<SqlParameter> aSQL = new List<SqlParameter>();
                                    aSQL.Add(new SqlParameter("@UserId", pk));
                                    aSQL.Add(new SqlParameter("@ComUnitId", item ?? (object)DBNull.Value));
                                    aInformation.isSuccess = accessManager.SaveData("sp_Save_UserCompanyUnit", aSQL);
                                }
                            }



                        }
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

    }
}
