using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class RegionSalesReportDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadRegion(DropDownList dropDownList)
        {
            string query = @"SELECT * from tblRegion ";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "RegionName", "RegionId", query, "SSIDB");
        }

        public void LoadRegion(DropDownList dropDownList, string RegionId)
        {
            string query = @"SELECT * from tblRegion where RegionId='" + RegionId + "'";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "RegionName", "RegionId", query, "SSIDB");
        }
        
        public DataTable RegionReportMainDataDAL(string RegionId, DateTime fromDate, DateTime toDate)
        {
            string query = @"select (RegionCode+':'+RegionName) as RegionDetail,'" + fromDate + "' as FromDate, '" +
                           toDate + "' as ToDate from tblRegion where RegionId='" + RegionId.Trim() + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable RegionReportDetailDataDAL(string RegionId, DateTime fromDate, DateTime toDate)
        {
            string query =
                @"select ProductCode,Product, sum(TotalQuantity) as TotalQty ,sum(Price) as TotalAmount from View_MIAWiseSalesReport  where MiaId in (select MiaId from View_CustomerMaster where RegionId='" + RegionId.Trim() + "') and InvoiceDate between '" + fromDate + "' and '" + toDate + "' group by ProductCode,Product";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
