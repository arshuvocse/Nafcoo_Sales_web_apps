using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Library.DAL.DataManager;
using SalesSolution.Web.Models;

namespace SalesSolution.Web.DataLayer
{
    public class MenuDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public dynamic LoadMenu(string roleid,string typeId)
        {
            
            try
            {
                try
                {
                    accessManager.SqlConnectionOpen(DataBase.SalesDB);
                    List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                    aSqlParameterlist.Add(new SqlParameter("@RoleId", (object)(roleid ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@TypeId", (object)(typeId ?? (object)DBNull.Value)));
                    SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GET_MainMenuRole2",aSqlParameterlist, false);
                    var list = accessManager.Serialize(dr);

                    return list;
                }
                catch (Exception exception)
                {
                    throw;
                }
            }
            finally
            {
                accessManager.SqlConnectionClose(false);
            }
            
        }


        public dynamic LoadMenuIsApp(string roleid, string typeId)
        {

            try
            {
                try
                {
                    accessManager.SqlConnectionOpen(DataBase.SalesDB);
                    List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                    bool? isapp =true;
                    aSqlParameterlist.Add(new SqlParameter("@RoleId", (object)(roleid ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@TypeId", (object)(isapp ?? (object)DBNull.Value)));
                    SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GET_MainMenuRoleIsApp", aSqlParameterlist, false);
                    var list = accessManager.Serialize(dr);

                    return list;
                }
                catch (Exception exception)
                {
                    throw;
                }
            }
            finally
            {
                accessManager.SqlConnectionClose(false);
            }

        }
        public dynamic GetUserRoleDropDown()
        {

            try
            {
                try
                {
                    accessManager.SqlConnectionOpen(DataBase.SalesDB);
                    //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                    //aSqlParameterlist.Add(new SqlParameter("@RoleId", (object)(roleid ?? (object)DBNull.Value)));
                    SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GET_UserRoleDropDown", false);
                    var list = accessManager.Serialize(dr);

                    return list;
                }
                catch (Exception exception )
                {
                    throw ;
                }
            }
            finally
            {
                accessManager.SqlConnectionClose(false);
            }

        }
        public int SaveMenuRole(MenuRoleDAO aInformation)
        {
            
            try
            {
                try
                {
                    this.accessManager.SqlConnectionOpen(DataBase.SalesDB);
                    List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                    
                    aSqlParameterlist.Add(new SqlParameter("@SL", (object)(aInformation.SL ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@RoleId", (object)(aInformation.RoleId ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@Add", (object)(aInformation.Add ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@View", (object)(aInformation.View ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@Delete", (object)(aInformation.Delete ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@Edit", (object)(aInformation.Edit ?? (object)DBNull.Value)));
                    aSqlParameterlist.Add(new SqlParameter("@Permission", (object)(aInformation.Permission ?? (object)DBNull.Value)));
                    
                    return accessManager.SaveDataReturnPrimaryKey("sp_Save_MenuRole", aSqlParameterlist, true);
                }
                catch (Exception exception )
                {
                    throw ;
                }
            }
            finally
            {
                this.accessManager.SqlConnectionClose(false);
            }
            
        }



        public ResultInfo Delete_Prevoiusmenu(int? iud)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
               
                gSqlParameterList.Add(new SqlParameter("@id", iud));
                bool result = accessManager.DeleteData("sp_Delete_PreviousmenuByRoleID", gSqlParameterList);
                
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
                aInformation.isSuccess = true;
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }


        public ResultInfo Delete_PrevoiusmenuAll(string roleSelectid, string typeSelectId)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@RoleId", roleSelectid));
                gSqlParameterList.Add(new SqlParameter("@TypeId", typeSelectId));
                bool result = accessManager.DeleteData("sp_Delete_ByRoleIDTypeId", gSqlParameterList);

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
                aInformation.isSuccess = true;
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }

    }
}