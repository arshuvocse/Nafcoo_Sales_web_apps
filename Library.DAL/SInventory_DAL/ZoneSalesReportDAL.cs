using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class ZoneSalesReportDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadComUnit(DropDownList dropDownList)
        {
            string query = @"SELECT * from tblCompanyUnit ";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "ComUnitName", "ComUnitId", query, "SSIDB");
        }
        public void LoadComUnit(DropDownList dropDownList,string comUnitId)
        {
            string query = @"SELECT * from tblCompanyUnit  where ComUnitId='"+comUnitId+"'" ;
            aCommonInternalDal.LoadDropDownValue(dropDownList, "ComUnitName", "ComUnitId", query, "SSIDB");
        }

        public void LoadZone(DropDownList dropDownList,string ComUnitId)
        {
            string query = @"SELECT * from tblZone where ComUnitId='"+ComUnitId.Trim()+"' ";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "ZoneName", "ZoneId", query, "SSIDB");
        }

        public DataTable ZoneReportMainDataDAL(string zoneId, DateTime fromDate, DateTime toDate)
        {
            string query = @"select (ZoneCode+':'+ZoneName) as ZoneDetail,ComUnitName,'" + fromDate + "' as FromDate, '" +
                           toDate + "' as ToDate from tblZone where ZoneId='"+zoneId.Trim()+"'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable ZoneReportDetailDataDAL(string zoneId, DateTime fromDate, DateTime toDate)
        {
            string query =
                @"select ProductCode,Product, sum(TotalQuantity) as TotalQty ,sum(Price) as TotalAmount from View_MIAWiseSalesReport  where MiaId in (select MiaId from tblMIAInfo where ZoneId='" + zoneId.Trim() + "') and InvoiceDate between '" + fromDate + "' and '" + toDate + "' group by ProductCode,Product";


            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
