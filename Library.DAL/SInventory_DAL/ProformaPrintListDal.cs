using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class ProformaPrintListDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable LoadInvoice(string pram)
        {
            string query = @"SELECT  * 				
        FROM tblInvoice I
        INNER JOIN (SELECT DISTINCT D.InvoiceId, ManufacId FROM dbo.tblInvoice I
                    INNER JOIN dbo.tblInvoiceDetail D ON I.InvoiceId = D.InvoiceId
                    INNER JOIN dbo.tblProduct P ON D.ProductCode = P.ProductCode
                    ) as tblD ON I.InvoiceId = tblD.InvoiceId  
         INNER JOIN dbo.View_CustomerMaster C ON I.CustomerMasterId = C.CustomerMasterId
         INNER JOIN dbo.tblMarket ON C.MarketCode=dbo.tblMarket.MarketCode " + pram + " order by OrderNo";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable LoadInvoiceSubdeport(string pram)
        {
            string query = @"SELECT  * 				
        FROM tblSubInvoiceMaster I
        INNER JOIN (SELECT DISTINCT D.InvoiceId, ManufacId FROM dbo.tblSubInvoiceMaster I
                    INNER JOIN dbo.tblSubInvoiceDetail D ON I.InvoiceId = D.InvoiceId
                    INNER JOIN dbo.tblProduct P ON D.ProductCode = P.ProductCode
                    ) as tblD ON I.InvoiceId = tblD.InvoiceId  
         INNER JOIN dbo.View_CustomerMaster C ON I.CustomerMasterId = C.CustomerMasterId
         INNER JOIN dbo.tblMarket ON C.MarketCode=dbo.tblMarket.MarketCode " + pram + " order by OrderNo";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
