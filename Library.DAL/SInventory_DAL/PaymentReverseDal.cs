using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class PaymentReverseDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetOrderInformationByOrderNo(string orderNo)
        {
            string query = @"SELECT INV.InvoiceId FROM dbo.tblOrder AS ODR 
                             LEFT JOIN dbo.tblInvoice AS INV ON ODR.OrderId = INV.OrderId
                             WHERE (INV.DeliveryInvoiceStatus IS NOT NULL AND INV.DeliveryInvoiceStatus NOT IN ('Reject')) AND ODR.OrderCode = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable subdepoGetOrderInformationByOrderNo(string orderNo)
        {
            string query = @"SELECT INV.InvoiceId FROM dbo.tblOrder AS ODR 
                             LEFT JOIN dbo.tblSubInvoiceMaster AS INV ON ODR.OrderId = INV.OrderId
                             WHERE (INV.DeliveryInvoiceStatus IS NOT NULL AND INV.DeliveryInvoiceStatus NOT IN ('Reject')) AND ODR.OrderCode = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public bool subResetInvoicePaymentStatus(int inoiceId)
        {
            string query = @"UPDATE dbo.tblSubInvoiceMaster SET PaymentStatus = NULL,PaymentAmount = NULL WHERE InvoiceId = " + inoiceId;
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }




        public bool ResetInvoicePaymentStatus(int inoiceId)
        {
            string query = @"UPDATE dbo.tblInvoice SET PaymentStatus = NULL,PaymentAmount = NULL WHERE InvoiceId = " + inoiceId;
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public DataTable GetPaymentInfoByInvoiceId(int inoiceId)
        {
            string query = @"SELECT PD.CustPayDetailId,PD.CustPayId FROM dbo.tblCustPayDetail AS PD WHERE PD.InvoiceId = " + inoiceId;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable subdGetPaymentInfoByInvoiceId(int inoiceId)
        {
            string query = @"SELECT PD.CustPayDetailId,PD.CustPayId FROM dbo.tblCustPayDetail AS PD WHERE PD.SubDeportInvoiceId = " + inoiceId;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public DataTable SubdepoGetPaymentInfoByInvoiceId(int inoiceId)
        {
            string query = @"SELECT PD.CustPayDetailId,PD.CustPayId FROM dbo.tblCustPayDetail AS PD WHERE PD.InvoiceId = " + inoiceId;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool DeletePaymentDetail(int payId)
        {
            string query = @"DELETE FROM dbo.tblCustPayDetail WHERE CustPayId = " + payId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }

        public bool DeletePaymentMaster(int payId)
        {
            string query = @"DELETE FROM dbo.tblCustomerPay WHERE CustPayId = " + payId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }
    }
}
