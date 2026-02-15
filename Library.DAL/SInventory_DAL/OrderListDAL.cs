using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class OrderListDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public bool SaveOrderMaster(OrderInfoMaster aListMasterDao)
        {
            string insertQuery = @"INSERT
                             INTO dbo.tblOrder
                                     ( OrderCode ,
 TerritoryCode ,
                                       ComUnitId ,
                                       ComUnitCode ,
                                       ComUnitName ,
                                       MIOCode ,
                                       MIOName ,
                                       ManufacId ,
                                       CustomerCode ,
                                       CustomerName ,
                                       GrossValue ,
                                       SubmissionDate ,FixedCustomer,
                                       IsManual,IsInvoice
                                     ) VALUES  ( 
                                    '" + aListMasterDao.OrderCode+"'," +
                                         "'" + aListMasterDao.teritory + "'," +
                                 "'"+aListMasterDao.ComUnitId+"',"+
                                 "'"+aListMasterDao.ComUnitCode+"',"+
                                 "'"+aListMasterDao.ComUnitName+"',"+
                                 "'"+aListMasterDao.MIOCode+"',"+
                                 "'"+aListMasterDao.MIOName+"',"+
                                 "'"+aListMasterDao.ManufacId+"',"+
                                 "'"+aListMasterDao.CustomerCode+"',"+
                                 "'"+aListMasterDao.CustomerName+"',"+
                                 "'"+aListMasterDao.GrossValue+"',"+
                                 "'"+aListMasterDao.SubmissionDate+"',"+
                                  "'" + aListMasterDao.FCB + "'," +
                                 "'" + aListMasterDao.IsManual + "','False' " +

                                     ")";
                                        return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool SaveOrderDetail(OrderInfoDetail aOrderListDetailDao)
        {
            string insertQuery = @"INSERT INTO dbo.tblOrderDetail
                                ( ProductId ,
                                  ProductCode ,
                                  ProductName ,
                                  Quantity ,
                                  TradePrice ,
                                  TotalTradePrice ,
ISGiftProduct,
IsCampaignProduct,
                                  OrderId
                                  
                                  
                                ) VALUES  ( 
                                    '" + aOrderListDetailDao.ProductId+"'," +
                                 "'"+aOrderListDetailDao.ProductCode+"',"+
                                 "'"+aOrderListDetailDao.ProductName+"',"+
                                 "'"+aOrderListDetailDao.Quantity+"',"+
                                 "'"+aOrderListDetailDao.TradePrice+"',"+
                                 "'"+aOrderListDetailDao.TotalTradePrice+"',"+
                                   "'" + aOrderListDetailDao.IsgiftProduct + "'," +
                                          "'" + aOrderListDetailDao.IsCampaignProduct + "'," +


                                 "'"+aOrderListDetailDao.OrderId+"'"+
                                 
                                 
                                ")";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public void LoadmanufacturerName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblManufacturer";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }
        public DataTable CustomerInfo(string custCode)
        {
            DataTable aDataTable = new DataTable();
            string query = @"SELECT * FROM dbo.View_CustomerMaster WHERE CustomerCode='"+custCode+"'";
            aDataTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            return aDataTable;
        }
        public int OrderManualId()
        {
            DataTable aDataTable = new DataTable();
            string query = @"SELECT (isnull(MAX(SUBSTRING(OrderCode,5,15)),0)+1) as PKMaxNo FROM dbo.tblOrder WHERE IsManual='True'";
            return Convert.ToInt32(aCommonInternalDal.DataContainerDataTable(query, "SSIDB").Rows[0][0].ToString());

        }
        public void DCLoad(DropDownList aDownList)
        {
            string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
        }
        public bool UpdateCompanyInfo(OrderInfoMaster aOrderInfoMaster)
        {
            string query = @"UPDATE tblOrder SET ComUnitId='" + aOrderInfoMaster.ComUnitId + "',ComUnitCode='" + aOrderInfoMaster.ComUnitCode + "',ComUnitName='" + aOrderInfoMaster.ComUnitName + "' WHERE OrderId=" + aOrderInfoMaster.OrderId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public ResultInfo SaveOrder(OrderInfoMaster aMasterDao, List<OrderInfoDetail> aList)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@OrderId", aMasterDao.OrderId));
                masterParameters.Add(new SqlParameter("@CustomerMasterId", aMasterDao.CustomerMasterId));
                
                masterParameters.Add(new SqlParameter("@DateOfDelivery", aMasterDao.DateOfDelivery));
                masterParameters.Add(new SqlParameter("@Remarks", aMasterDao.Remarks));
                masterParameters.Add(new SqlParameter("@IsManual", aMasterDao.IsManual));

                if (aMasterDao.OrderId > 0)
                {

                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_Update_ManualOrderMaster", masterParameters);
                    pk = aMasterDao.OrderId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@SubmissionDate", aMasterDao.SubmissionDate));
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_ManualOrderMaster", masterParameters);

                }


                if (pk > 0)
                {
                    List<SqlParameter> deleteId = new List<SqlParameter>();

                    deleteId.Add(new SqlParameter("@OrderId", aMasterDao.OrderId));
                    accessManager.DeleteData("sp_Delete_ManualOrderDetailsById", deleteId);

                    foreach (var item in aList)
                    {
                        List<SqlParameter> aSQL = new List<SqlParameter>();

                        aSQL.Add(new SqlParameter("@OrderId", pk));
                        aSQL.Add(new SqlParameter("@ProductId", item.ProductId));
                        aSQL.Add(new SqlParameter("@ProductCode", item.ProductCode));
                        aSQL.Add(new SqlParameter("@ProductName", item.ProductName));
                        aSQL.Add(new SqlParameter("@TradePrice", item.TradePrice));
                        aSQL.Add(new SqlParameter("@Quantity", item.Quantity));
                        aSQL.Add(new SqlParameter("@Vat", item.Vat));
                        aSQL.Add(new SqlParameter("@TotalTradePrice", item.TotalTradePrice));
                        aSQL.Add(new SqlParameter("@TotalVat", item.TotalVat));
                        aSQL.Add(new SqlParameter("@GrossValue", item.GrossValue));
                        aSQL.Add(new SqlParameter("@DiscountValue", item.DiscountValue));
                        aSQL.Add(new SqlParameter("@IsCampaignProduct", item.IsCampaignProduct));
                        aSQL.Add(new SqlParameter("@DiscountPercent", item.DiscountPercent));
                        aSQL.Add(new SqlParameter("@DiscountAmount", item.DiscountAmount));
                        aSQL.Add(new SqlParameter("@CampaignType", (object)item.CampaignType??DBNull.Value));
                        aSQL.Add(new SqlParameter("@CampaignName", item.CampaignName));

                        

                        aInformation.isSuccess = accessManager.SaveData("sp_Save_ManualOrderDetails", aSQL);

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

        public DataTable LoadOrderDetailById(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_GET_OrderInfoById", aSqlParameters);
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

        public DataTable GetTradePolicyInfo(int customerMasterId, int customerTypeId,decimal restamount)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@CustomerMasterId", customerMasterId));
                aSqlParameters.Add(new SqlParameter("@CustomerTypeId", customerTypeId));
                aSqlParameters.Add(new SqlParameter("@Restamount", restamount));

                DataTable dt = accessManager.GetDataTable("sp_Get_TradePolicyForManualOrder", aSqlParameters);
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

        public DataTable GetCampaignInfo(int productId, int customerMasterId, int customerTypeId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@ProductId", productId));
                aSqlParameters.Add(new SqlParameter("@CustomerMasterId", customerMasterId));
                aSqlParameters.Add(new SqlParameter("@CustomerTypeId", customerTypeId));

                DataTable dt = accessManager.GetDataTable("sp_Get_CampaignInfoManualOrder", aSqlParameters);
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
