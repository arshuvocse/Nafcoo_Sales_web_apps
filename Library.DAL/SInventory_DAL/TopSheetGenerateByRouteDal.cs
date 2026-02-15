using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class TopSheetGenerateByRouteDal
    {
        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public void LoadDisRouteforInvoice(DropDownList ddl, int dis)
        {

            string queryStr = @"select distinct tblOrder.DistributionRouteId ,tblRouteInformationMaster.RouteName DistributionRouteName
            from tblOrder  with (nolock)
            inner join tblRouteInformationMaster  with (nolock) on tblRouteInformationMaster.RouteInformationMasterId=tblOrder.DistributionRouteId
            where IsInvoice=1 and  tblOrder.ActionStatus='2'  and ComUnitId=" + dis + "  order by tblRouteInformationMaster.RouteName asc";

            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "DistributionRouteName", "DistributionRouteId", queryStr);
        }

        public DataTable LoadOrderForOrderCreation(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_LoadInvoiceForTopSheet", aSqlParameters);
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

        public void LoadDA(DropDownList ddl, string SalesCenterId)
        {
            string queryStr = @"SELECT DAId,Name + ' (' + DACode + ')' AS Da FROM tblDAInfo  where SalesCenterId="+ SalesCenterId
+ " ORDER BY LTRIM(Name)";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "Da", "DAId", queryStr);
        }

        public ResultInfo SaveTopSheet(TopSheetMasterDao aMasterDao, List<TopSheetDetaildao> aList)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                
                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@TopSheetGenReportId", aMasterDao.TopSheetGenReportId));
                masterParameters.Add(new SqlParameter("@DAId", aMasterDao.DAId));

                if (aMasterDao.TopSheetGenReportId > 0)
                {
                    
                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_Update_TopSheetMaster", masterParameters);
                    pk = aMasterDao.TopSheetGenReportId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_TopSheetMaster", masterParameters);
                    
                }


                if (pk > 0)
                {
                    List<SqlParameter> deleteId = new List<SqlParameter>();
                    deleteId.Add(new SqlParameter("@TopSheetGenReportId", aMasterDao.TopSheetGenReportId));
                    accessManager.DeleteData("sp_Delete_TopSheetDetailsById", deleteId);

                    foreach (var item in aList)
                    {
                        List<SqlParameter> aSQL = new List<SqlParameter>();

                        aSQL.Add(new SqlParameter("@TopSheetMasterId", pk));
                        aSQL.Add(new SqlParameter("@InvoiceId", item.InvoiceId));

                        aInformation.isSuccess = accessManager.SaveData("sp_Save_TopSheetDetails", aSQL);

                    }

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

        public DataTable GetTopSheetList(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_GET_TopSheet", aSqlParameters);

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

        public DataTable GetTopSheetById(int masterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@masterId", masterId));
                DataTable dt = accessManager.GetDataTable("sp_GET_TopSheetById", aSqlParameters);

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
