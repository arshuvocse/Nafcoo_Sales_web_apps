using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class OrderStatusDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetOrderStatusInfo(string orderNumber)
        {
            string query = @"SELECT *, ORDRD.CampaignName as CampaignName2 from tblOrder AS ORDR WITH(NOLOCK)
                            INNER JOIN tblOrderDetail AS ORDRD ON ORDR.OrderId = ORDRD.OrderId
                            WHERE ORDR.OrderCode = '" + orderNumber + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadOrderStatusInfoTesting(string orderNumber)
        {
            string query = @"SELECT *, (GrossValue*DiscountPercent)/100 as DisAmt from SystemTest_Testing..OrderListDetail O
                            WHERE O.OrderCode = '" + orderNumber + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable GetInvoiceExistOrNot(string orderNo)
        {
            string query = @"SELECT * FROM tblInvoice AS INV WITH(NOLOCK)
                            WHERE INV.OrderNo = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetProformaInvoiceInfo(string orderNo)
        {
            string query = @"SELECT * FROM tblInvoice AS INV WITH(NOLOCK)
                           INNER JOIN tblInvoiceDetail INVD ON INV.InvoiceId = INVD.InvoiceId
                           WHERE INV.OrderNo = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetCheckDeliveryInvoiceExistOrNot(string orderNo)
        {
            string query = @"SELECT * FROM tblInvoice AS INV WITH(NOLOCK)
                            INNER JOIN tblInvoiceDetail INVD ON INV.InvoiceId = INVD.InvoiceId
                            WHERE INV.DeliveryInvoiceStatus IS NOT NULL AND
                            INV.OrderNo = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetDeliveryInvoiceInfo(string orderNo)
        {
            string query = @"SELECT * FROM tblInvoice AS INV WITH(NOLOCK)
                           INNER JOIN tblInvoiceDetail INVD ON INV.InvoiceId = INVD.InvoiceId
                           WHERE INV.OrderNo = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetCheckPaymentStatus(string orderNo)
        {
            string query = @"SELECT  [PaymentAmount],[PaymentStatus] FROM tblInvoice AS INV WITH(NOLOCK)
                             WHERE INV.PaymentAmount IS NOT NULL AND INV.PaymentStatus IS NOT NULL AND INV.OrderNo = '" + orderNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetDeliveryReturnInvoiceInfo(string orderNo)
        {
            string query = @"SELECT DelivaryInvoiceNo,TotalPrice,TotalPrice - ISNULL(DeliveryTotalPrice,0) ReturnTotalPrice ,ProductCode,ProductName,
BatchNo,TotalPrice - DeliveryTotalPrice AS ReturnTotalPrice, TotalPriceVatAmount - DeliveryTotalPriceVatAmount ReturnTotaleVatAmount,DeliveryDiscountPercentage,DiscountAmount - DeliveryDiscountAmount ReturnDiscount,NetAmount - DeliveryNetAmount ReturnNetAmount FROM tblInvoice AS INV WITH(NOLOCK)
INNER JOIN tblInvoiceDetail INVD ON INV.InvoiceId = INVD.InvoiceId
WHERE INVD.DeliveryQuantity IS NOT NULL AND (TotalPrice - ISNULL(DeliveryTotalPrice,0)) > 0 AND INV.OrderNo = '" + orderNo + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
