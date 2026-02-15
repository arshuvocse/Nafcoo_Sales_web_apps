using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class DcOpeningStockReportDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void DCLoad(DropDownList aDownList)
        {
            string dc = "SELECT [ComUnitId],(ComUnitCode+':'+ComUnitName) AS Unit FROM [dbo].[tblCompanyUnit]";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Unit", "ComUnitId", dc, "SSIDB");
        }

        public DataTable GetAllDcOpeningStockReport(string date)
        {
            string query = @"SELECT [ComUnitCode],[ComUnitName],[ProductCode],[ProductName],[PackSize],[BatchNo],[MfgDate],[ExpDate],[StockQty] FROM tblDCStore_OpeningBalance 
                             INNER JOIN tblCompanyUnit ON tblDCStore_OpeningBalance.ComUnitId = tblCompanyUnit.ComUnitId
                             WHERE tblDCStore_OpeningBalance.DCOpeningBalanceDate = '" + date + "'" ;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetDcOpeningStockReport(int dcId, string date)
        {
            string query = @"SELECT [ComUnitCode],[ComUnitName],[ProductCode],[ProductName],[PackSize],[BatchNo],[MfgDate],[ExpDate],[StockQty] FROM tblDCStore_OpeningBalance 
                             INNER JOIN tblCompanyUnit ON tblDCStore_OpeningBalance.ComUnitId = tblCompanyUnit.ComUnitId
                             WHERE tblDCStore_OpeningBalance.ComUnitId = '" + dcId + "' AND tblDCStore_OpeningBalance.DCOpeningBalanceDate = '" + date + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
