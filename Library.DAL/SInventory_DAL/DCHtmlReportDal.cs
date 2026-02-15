using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;

namespace Library.DAL.SInventory_DAL
{
    public class DCHtmlReportDal
    {

        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable LoadDepotWiseStock(DateTime fromDate, DateTime toDate, int branchId, string ProCode,string BranchName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@fromDate", fromDate));
                aSqlParameterList.Add(new SqlParameter("@toDate", toDate));
                aSqlParameterList.Add(new SqlParameter("@CiD", branchId));
                aSqlParameterList.Add(new SqlParameter("@ProCode", ProCode));
              

                DataTable dt =new DataTable();

                if (branchId == 0)
                {
                    dt = accessManager.GetDataTable("sp_Stock_StockReportByDepotNational", aSqlParameterList);
                }
                else
                {
                    aSqlParameterList.Add(new SqlParameter("@BranchName", BranchName));
                    dt = accessManager.GetDataTable("sp_Stock_StockReportByDepot", aSqlParameterList);
                }

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

        public DataTable LoadStockmovementReport(DateTime fromDate, DateTime toDate, int branchId, string ProCode, string BranchName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@fromDate", fromDate));
                aSqlParameterList.Add(new SqlParameter("@toDate", toDate));
                aSqlParameterList.Add(new SqlParameter("@CiD", branchId));
                aSqlParameterList.Add(new SqlParameter("@ProCode", ProCode));
                //aSqlParameterList.Add(new SqlParameter("@BranchName", BranchName));

                DataTable dt = new DataTable();

                if (ProCode == "" || ProCode == "0")
                {
                    dt = accessManager.GetDataTable("sp_Stock_StockMoveMentReport", aSqlParameterList);
                }else
                {
                     dt = accessManager.GetDataTable("sp_Stock_StockMoveMentReportByProduct", aSqlParameterList);
                }

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

        public DataTable LoadDepotWiseStock_New_WithoutBatch(DateTime fromDate, DateTime toDate, int branchId, string ProCode, string BranchName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@fromDate", fromDate));
                aSqlParameterList.Add(new SqlParameter("@toDate", toDate));
                aSqlParameterList.Add(new SqlParameter("@CiD", branchId));
                aSqlParameterList.Add(new SqlParameter("@ProCode", ProCode));

                DataTable dt = new DataTable();

                if (branchId == 0)
                {
                    dt = accessManager.GetDataTable("sp_NasaBoss_StockErrorQueryNational", aSqlParameterList);
                }
                else
                {
                    aSqlParameterList.Add(new SqlParameter("@BranchName", BranchName));
                    dt = accessManager.GetDataTable("sp_StockReport_Depot", aSqlParameterList);
                }

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


        public DataTable LoadDepotWiseStock_New(DateTime fromDate, DateTime toDate, int branchId, string ProCode, string BranchName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@fromDate", fromDate));
                aSqlParameterList.Add(new SqlParameter("@toDate", toDate));
                aSqlParameterList.Add(new SqlParameter("@CiD", branchId));
                aSqlParameterList.Add(new SqlParameter("@ProCode", ProCode));

                DataTable dt = new DataTable();

                if (branchId == 0)
                {
                    dt = accessManager.GetDataTable("sp_Stock_StockReportByDepotNational_New", aSqlParameterList);
                }
                else
                {
                    aSqlParameterList.Add(new SqlParameter("@BranchName", BranchName));
                    dt = accessManager.GetDataTable("sp_Stock_StockReportByDepot_New", aSqlParameterList);
                }

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


        public DataTable LoadNationaleStock(DateTime fromDate, DateTime toDate)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@fromDate", fromDate));
                aSqlParameterList.Add(new SqlParameter("@toDate", toDate));

                DataTable dt = accessManager.GetDataTable("sp_Stock_StockReportByDepot", aSqlParameterList);
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
