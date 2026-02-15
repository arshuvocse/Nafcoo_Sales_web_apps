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
    public class NewSalesReturnDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable LoadCustomerMaster(string CustomerMasterId)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT * FROM dbo.View_CustomerMaster WHERE CustomerCode='" + CustomerMasterId.Trim() + "' ";
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }

        public void DCLoad(DropDownList aDownList)
        {
            string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
        }

        public ResultInfo SaveSalesReturn(Invoice aMasterDao, List<InvoiceDetail> aList)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@ReturnInvoiceid", aMasterDao.ReturnInvoiceid));
                masterParameters.Add(new SqlParameter("@InvoiceDate", aMasterDao.InvoiceDate));
                masterParameters.Add(new SqlParameter("@CustomerMasterId", aMasterDao.CustomerMasterId));
                masterParameters.Add(new SqlParameter("@ComUnitId", aMasterDao.ComUnitId));
                masterParameters.Add(new SqlParameter("@TpTotal", aMasterDao.TpTotal));
                masterParameters.Add(new SqlParameter("@TpDiscount", aMasterDao.TpDiscount));
                masterParameters.Add(new SqlParameter("@TpVat", aMasterDao.TpVat));
                masterParameters.Add(new SqlParameter("@TpGrandTotal", aMasterDao.TpGrandTotal));
                masterParameters.Add(new SqlParameter("@UserId", aMasterDao.UserId));
                masterParameters.Add(new SqlParameter("@InvoiceId", aMasterDao.InvoiceId));

                if (aMasterDao.ReturnInvoiceid > 0)
                {

                    //masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    //masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    //accessManager.UpdateData("sp_Update_TopSheetMaster", masterParameters);
                    //pk = aMasterDao.ReturnInvoiceid;
                }
                else
                {
                    //masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    //masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_SalesReturn_MasterInsert", masterParameters);

                }


                if (pk > 0)
                {
                    List<SqlParameter> deleteId = new List<SqlParameter>();
                    deleteId.Add(new SqlParameter("@TopSheetGenReportId", aMasterDao.ReturnInvoiceid));
                    accessManager.DeleteData("sp_Delete_TopSheetDetailsById", deleteId);

                    foreach (var item in aList)
                    {
                        List<SqlParameter> aSQL = new List<SqlParameter>();

                        aSQL.Add(new SqlParameter("@ReturnInvoiceId", pk));
                        aSQL.Add(new SqlParameter("@ProductCode", item.ProductCode));
                        aSQL.Add(new SqlParameter("@ProductName", item.ProductName));
                        //aSQL.Add(new SqlParameter("@PackSize", item.PackSize));
                        aSQL.Add(new SqlParameter("@BatchNo", item.BatchNo));
                        aSQL.Add(new SqlParameter("@ExpDate", item.ExpDate));
                        aSQL.Add(new SqlParameter("@UnitPrice", item.UnitPrice));
                        aSQL.Add(new SqlParameter("@UnitVatAmount", item.UnitVatAmount));
                        aSQL.Add(new SqlParameter("@Quantity", item.Quantity));
                        aSQL.Add(new SqlParameter("@TotalQuantity", item.TotalQuantity));
                        aSQL.Add(new SqlParameter("@TotalPrice", item.TotalPrice));
                        aSQL.Add(new SqlParameter("@TotalPriceVatAmount", item.TotalPriceVatAmount));
                        aSQL.Add(new SqlParameter("@NetAmount", item.NetAmount));
                        aSQL.Add(new SqlParameter("@IsFOC", item.IsCampaignProductforInv));

                        aInformation.isSuccess = accessManager.SaveData("sp_SalesReturn_DetailInsert", aSQL);

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

        public DataTable LoadInvoiceDetail(int invoiceId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@invoiceId", invoiceId));
                DataTable dt = accessManager.GetDataTable("sp_ReturnInvoice_GetDeliveryInvoiceId", aSqlParameters);
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

        public DataTable GetReturnData(string getPram)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@getPram", getPram));
                DataTable dt = accessManager.GetDataTable("sp_ReturnInvoice_GetReturnInvoiceList", aSqlParameters);
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


        public bool AppReturnInvoice(string userName, string returnInvoiceId,string approvalStatus)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@ReturnInvoiceId", returnInvoiceId));
                aSqlParameters.Add(new SqlParameter("@ApprovalStatus", approvalStatus.Trim()));
                aSqlParameters.Add(new SqlParameter("@ApproveBy", userName));

                bool dt = accessManager.UpdateData("sp_ReturnInvoice_Approval", aSqlParameters);
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
