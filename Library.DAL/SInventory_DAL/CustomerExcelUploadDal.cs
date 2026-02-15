using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class CustomerExcelUploadDal
    {

        DataAccessManager accessManager = new DataAccessManager();


        public DataTable CheckMarketCode(string param)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@MarketCode", param));
                DataTable dt = accessManager.GetDataTable("sp_check_MarketCode", aSqlParameters);
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




        public ResultInfo Save_CustomerInfoByExcel(List<CustomerMaster> employeeLeave)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                aInformation.isValiCheck = false;
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

              //  List<SqlParameter> gSqlParameterList = new List<SqlParameter>();

                foreach (var listData in employeeLeave)
                {

                    List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                    gSqlParameterList.Add(new SqlParameter("@CustomerMasterId", listData.CustomerMasterId));
                    gSqlParameterList.Add(new SqlParameter("@MarketCode", listData.MarketCode));
                    gSqlParameterList.Add(new SqlParameter("@CustomerCode", listData.CustomerCode));
                    gSqlParameterList.Add(new SqlParameter("@CustomerName", listData.CustomerName));
                    gSqlParameterList.Add(new SqlParameter("@Address", (object)listData.Address??DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@OwnerName", (object)listData.Addrees2 ?? DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@CellNo", (object)listData.CellNo??DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@TermOfPayment", (object)listData.TermOfPayment??DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString())));
                    aInformation.Id = accessManager.SaveDataReturnPrimaryKey("sp_Save_EXcel_CustomerMaster", gSqlParameterList);

                    if (aInformation.Id > 0)
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

    }
}
