using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.Models;

namespace Library.DAL.MasterSetup_DAL
{
   public class PriceGroupDal
    {

        private DataAccessManager accessManager = new DataAccessManager();

       public ResultInfo SaveInfo(priceSetupDao aSetupDao)
       {
           int pk = 0;
           ResultInfo aInformation = new ResultInfo();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.SalesDB);
               List<SqlParameter> gSqlParameterList = new List<SqlParameter>();

               gSqlParameterList.Add(new SqlParameter("@id", aSetupDao.PriceGroupId));
               gSqlParameterList.Add(new SqlParameter("@PriceGroupName", aSetupDao.PriceGroupName ?? (object)DBNull.Value));
               gSqlParameterList.Add(new SqlParameter("@CheckPriceGroupName", aSetupDao.PriceGroupName ?? (object)DBNull.Value));
               
               if (aSetupDao.PriceGroupId > 0)
               {
                   
                   DataTable dt = accessManager.GetDataTable("sp_Save_priceGroup_existCheck", gSqlParameterList);

                   if (dt.Rows.Count == 0)
                   {
                       gSqlParameterList.Add(new SqlParameter("@UpdateBy", aSetupDao.EntryBy));
                       aInformation.isSuccess = accessManager.DeleteData("sp_Update_priceGroup", gSqlParameterList);
                       pk = aSetupDao.PriceGroupId;
                   }
                   else
                   {
                       aInformation.isSuccess = false;
                   }

              
               }
               else
               {
                   gSqlParameterList.Add(new SqlParameter("@EntryBy", aSetupDao.EntryBy));
                   pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_priceGroup_Setup", gSqlParameterList);
                   if (pk > 0)
                   {
                       aInformation.isSuccess = true;
                   }
                   else
                   {
                       aInformation.isSuccess = false;
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





       public DataTable PriceListById(string prm)
       {
           try
           {
               accessManager.SqlConnectionOpen(DataBase.SalesDB);
               List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
               aSqlParameterlist.Add(new SqlParameter("@PriceGroupId", prm));
               DataTable dt = accessManager.GetDataTable("sp_GET_RriceGroupInfo_ById", aSqlParameterlist);
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



        public DataTable PriceList(string prm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", prm));
                DataTable dt = accessManager.GetDataTable("sp_GET_RriceGroupInfo", aSqlParameterlist);
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
    }
}
