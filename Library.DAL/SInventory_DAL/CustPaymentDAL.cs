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

namespace Library.DAL.SInventory_DAL
{
    public class CustPaymentDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveCustPayment(CustPayment aCustPayment)
        {
            string insertQuery = @"INSERT INTO dbo.tblCustomerPay
        ( CustPayId ,
          MarketId ,
          CustomerMasterId ,
            ComUnitId,
          PaymentDate ,
          PaymentAmount ,
          PayType ,
          RefNo ,
          RefDate ,
          CreateBy ,
          CreateDate 
          
        )

            values (" + aCustPayment.CustPayId + "," +
                   "'" + aCustPayment.MarketId + "'," +
                    "" + aCustPayment.CustomerMasterId + "," +
                    "" + aCustPayment.ComUnitId + "," +
                    "'" + aCustPayment.PaymentDate + "'," +
                    "'" + aCustPayment.PaymentAmount + "'," +
                    "'" + aCustPayment.PayType + "'," +
                    "'" + aCustPayment.RefNo + "'," +
                    "'" + (object)(aCustPayment.RefDate ?? (object)DBNull.Value) + "'," +
                    "'" + aCustPayment.CreateBy + "'," +
                    "'" + aCustPayment.CreateDate + "'" +
                    //"'" + aCustPayment.UpdateBy + "'," +
                    //"'" + aCustPayment.UpdateDate + "'" +
                                 
                    ")";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool SaveCustDetail(CustPaymentDetail aCustPaymentDetail)
        {
            string insertQuery = @"INSERT INTO dbo.tblCustPayDetail
        ( CustPayDetailId ,
          InvoiceId ,
          PaymentAmount ,
          DiscountAmount ,
          CustPayId
        )

          
        

            values (" + aCustPaymentDetail.CustPayDetailId+ "," +
                   "'" + aCustPaymentDetail.InvoiceId + "'," +
                    "" + aCustPaymentDetail.PaymentAmount + "," +
                    "" + aCustPaymentDetail.DiscountAmount + "," +
                    "'" + aCustPaymentDetail.CustPayId + "'" +
                    

                    ")";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool SubdeportSaveCustDetail(CustPaymentDetail aCustPaymentDetail)
        {
            string insertQuery = @"INSERT INTO dbo.tblCustPayDetail
        ( CustPayDetailId ,
          SubDeportInvoiceId ,
          PaymentAmount ,
          CustPayId
        )

          
        

            values (" + aCustPaymentDetail.CustPayDetailId + "," +
                   "'" + aCustPaymentDetail.InvoiceId + "'," +
                    "" + aCustPaymentDetail.PaymentAmount + "," +
                    "'" + aCustPaymentDetail.CustPayId + "'" +


                    ")";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public void LoadSC(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblCompanyUnit";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }
        public void LoadSC(DropDownList ddl, string userId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            //            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
            //                                LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
            //                                WHERE UserId='"+userId+"'";


            string queryStr = "select ComUnitId, ComUnitName  from tblCompanyUnit WHERE " +
                               " ComUnitId IN (SELECT CompanyUnitId FROM dbo.tblUserCompanyUnit WHERE UserId='" + userId.Trim() + "')";


            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }
        public void LoadManufac(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblManufacturer";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }
        public bool UpdateInvoicePaymentAmount(string amount,string status,string id)
        {
            string query = @"UPDATE dbo.tblInvoice SET PaymentAmount='" + amount + "',PaymentStatus='"+status+"' WHERE InvoiceId='" + id + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool SubdeportUpdateInvoicePaymentAmount(string amount, string status, string id)
        {
            string query = @"UPDATE dbo.tblSubInvoiceMaster SET PaymentAmount='" + amount + "',PaymentStatus='" + status + "' WHERE InvoiceId='" + id + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }


        public DataTable Load_CustomerPayment(string Param)
        {
            string query = @"SELECT  CPD.CustPayDetailId, CP.CustPayId ,CU.ComUnitCode , CU.ComUnitName, CM.CustomerCode, CM.CustomerName, INV.InvoiceNo, INV.InvoiceDate, CP.PaymentAmount, CP.CreateBy, CP.PayType, CP.PaymentDate FROM tblCustPayDetail  CPD
LEFT JOIN tblCustomerPay CP ON CP.CustPayId= CPD.CustPayId
LEFT JOIN tblInvoice INV ON  CPD.InvoiceId = INV.InvoiceId
LEFT JOIN tblCompanyUnit CU ON INV.ComUnitId = CU.ComUnitId
LEFT JOIN tblCustMaster AS CM ON INV.CustomerMasterId = CM.CustomerMasterId
WHERE CP.CustPayId IS NOT NULL " + Param + "";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public void LoadCustomerMaster(DropDownList ddl, string marketId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblCustMaster WHERE CustomerMasterId IN (SELECT DISTINCT CustomerMasterId FROM dbo.View_CustomerMaster WHERE MarketId='" + marketId + "')";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "CustomerName", "CustomerMasterId", queryStr);
        }
        public void PaymentTypeLoad(DropDownList aDropDownList)
        {
            string query = @"select * from tblPaymentType";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "PaymentTypeName", "PaymentTypeId", query, "SSIDB");
        }

        public void LoadMarket(DropDownList ddl, string comunitId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = @"SELECT DISTINCT CM.MarketId,CM.MarketCode,CM.MarketName FROM dbo.tblCompanyUnit Cu
                inner JOIN dbo.View_CustomerMaster CM ON CM.ComUnitCode = Cu.ComUnitCode
                inner JOIN dbo.tblMarket ON tblMarket.MarketCode = CM.MarketCode
                WHERE CU.ComUnitId='" + comunitId + "'";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MarketName", "MarketId", queryStr);
        }

        public DataTable LoadInvoice(string comUnitId,string customerId,string marketId)
        {
            //LEFT JOIN (SELECT CustomerMasterId,SUM(DiscountAmount) AS AdjustableAmount FROM tbl_Campaign_DiscountProbationInsert WHERE IsAdjusted = 0 GROUP BY CustomerMasterId) AS DS ON View_CustomerMaster.CustomerMasterId = DS.CustomerMasterId 
            string query = @"SELECT INV.InvoiceId,InvoiceNo,InvoiceDate,DelivaryInvoiceNo,INV.UpdateDate,TotalDelivery,ISNULL(PP,0) PaymentAmount, P.DiscountAmount AS PreviousDiscount,(ISNULL(TotalDelivery,0) - (ISNULL(PP,0) + ISNULL(P.DiscountAmount,0))) AS Due,ISNULL(ReturnTotal,0) AdjustableAmount FROM tblInvoice AS INV WITH(NOLOCK)
LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) AS PP, SUM(ISNULL(DiscountAmount,0)) DiscountAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS P ON INV.InvoiceId = P.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(DeliveryNetAmount) - ISNULL(SUM(AdjustmentAmount),0) AS TotalDelivery FROM tblInvoiceDetail AS IVD WITH(NOLOCK) GROUP BY InvoiceId) AS TD ON INV.InvoiceId = TD.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(TPGrandTotal) ReturnTotal FROM tblReturnInvoice WHERE ApprovalStatus = 'Approved' GROUP BY InvoiceId) AS RTN ON INV.InvoiceId= RTN.InvoiceId
WHERE DelivaryInvoiceNo IS NOT NULL AND INV.DeliveryInvoiceStatus IN ('Full','Partial') 
AND (ISNULL(TotalDelivery,0) - (ISNULL(PP,0) + ISNULL(P.DiscountAmount,0))) > 0  AND INV.ComUnitId=" + comUnitId + " AND INV.CustomerMasterId=" + customerId;

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadInvoice(string comUnitId, string marketId)
        {
//            string query = @"SELECT ISNULL(PaymentAmount,0)PaymentAmount,*,(DeliveryTpGrandTotal-(isnull(PaymentAmount,0)+ISNULL(AdjustAmount,0))) AS Due,ISNULL(AdjustAmount,0)AjAmt FROM dbo.tblInvoice
//            
//            inner JOIN dbo.View_CustomerMaster ON dbo.tblInvoice.CustomerMasterId=dbo.View_CustomerMaster.CustomerMasterId
//            WHERE  DeliveryTpGrandTotal > 0 AND MarketId='" + marketId + "' AND dbo.tblInvoice.ComUnitId='" + comUnitId + "'  AND (PaymentStatus IS NULL OR PaymentStatus='Partial') AND (DelivaryInvoiceNo IS NOT NULL) AND (DeliveryInvoiceStatus ='Partial' or DeliveryInvoiceStatus ='Full')";


            string query = @"SELECT ISNULL(PaymentAmount,0)PaymentAmount,*,(DeliveryTpGrandTotal-(isnull(PaymentAmount,0)+ISNULL(AdjustAmount,0))) AS Due,ISNULL(AdjustAmount,0)AjAmt FROM dbo.tblInvoice
            
            inner JOIN dbo.View_CustomerMaster ON dbo.tblInvoice.CustomerMasterId=dbo.View_CustomerMaster.CustomerMasterId
            WHERE  DeliveryTpGrandTotal > 0  AND dbo.tblInvoice.ComUnitId='" + comUnitId + "'  AND (PaymentStatus IS NULL OR PaymentStatus='Partial') AND (DelivaryInvoiceNo IS NOT NULL) AND (DeliveryInvoiceStatus ='Partial' or DeliveryInvoiceStatus ='Full')";
            
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadSubDeportInvoice(string comUnitId, string marketId)
        {
            string query = @"SELECT ISNULL(PaymentAmount,0)PaymentAmount,*,(DeliveryTpGrandTotal-isnull(PaymentAmount,0)) AS Due FROM dbo.tblSubInvoiceMaster

            inner JOIN dbo.View_CustomerMaster ON dbo.tblSubInvoiceMaster.CustomerMasterId=dbo.View_CustomerMaster.CustomerMasterId
            WHERE  DeliveryTpGrandTotal > 0 AND MarketId='" + marketId + "' AND dbo.tblSubInvoiceMaster.ComUnitId='" + comUnitId + "'  AND (PaymentStatus IS NULL OR PaymentStatus='Partial') AND (DelivaryInvoiceNo IS NOT NULL) AND (DeliveryInvoiceStatus ='Partial' or DeliveryInvoiceStatus ='Full')";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable GetPrevAmount(string invoiceId)
        {
            string query = @"SELECT PaymentAmount, 
         FROM dbo.tblInvoice WHERE InvoiceId='" + invoiceId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable Existence(string invoiceId, string Amount)
        {
            string query = @"SELECT *
            FROM dbo.tblCustPayDetail
            WHERE  dbo.tblCustPayDetail.InvoiceId='" + invoiceId + "' AND dbo.tblCustPayDetail.PaymentAmount='" + Amount + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public CustomerMaster CustomerLoad(string CustomerCode)
        {
            string query = @"SELECT CustomerMasterId FROM dbo.tblCustMaster WHERE CustomerCode= '" + CustomerCode + "'";
                             
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            CustomerMaster aCustomerMaster = new CustomerMaster();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCustomerMaster.CustomerMasterId = Int32.Parse(dataReader["CustomerMasterId"].ToString());
                }
            }
            return aCustomerMaster;
        }
        public CustomerMaster DetailCustomerLoad(string CustomerCode)
        {
            string query = @"SELECT * FROM dbo.tblCustMaster WHERE CustomerCode= '" + CustomerCode + "'";

            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            CustomerMaster aCustomerMaster = new CustomerMaster();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCustomerMaster.CustomerMasterId = Int32.Parse(dataReader["CustomerMasterId"].ToString());
                    aCustomerMaster.CustomerCode = (dataReader["CustomerCode"].ToString());
                    aCustomerMaster.CustomerName = (dataReader["CustomerName"].ToString());
                }
            }
            return aCustomerMaster;
        }
        public Product DetailProductLoad(string ProductCode)
        {
            string query = @"SELECT * FROM dbo.tblProduct WHERE ProductCode= '" + ProductCode + "'";

            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            Product aProduct = new Product();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aProduct.ProductId = Int32.Parse(dataReader["ProductId"].ToString());
                    aProduct.ProductCode = (dataReader["ProductCode"].ToString());
                    aProduct.ProductName = (dataReader["ProductName"].ToString());
                }
            }
            return aProduct;
        }

        private DataAccessManager accessManager = new DataAccessManager();
        public bool UpdateAdjustment(int invoiceId)
        {
            bool status = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@InvoiceId", invoiceId));
                status = accessManager.UpdateData("sp_Campaign_SetIsAdjustedProbationDiscount", aSqlParameters);
                return status;
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

        public DataTable CheckCustomerPaymentExistOrNot(CustPaymentDetail aCustPayment)
        {
            string query = @"SELECT CONVERT(DATE,PaymentDate) FROM dbo.tblCustPayDetail AS PD
            LEFT JOIN tblCustomerPay AS PM ON PD.CustPayId = PM.CustPayId
            WHERE  CONVERT(DATE,PaymentDate) = CONVERT(DATE,GETDATE()) AND PD.InvoiceId='" + aCustPayment.InvoiceId + "' AND PD.PaymentAmount='" + aCustPayment.PaymentAmount + "'";
            
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
