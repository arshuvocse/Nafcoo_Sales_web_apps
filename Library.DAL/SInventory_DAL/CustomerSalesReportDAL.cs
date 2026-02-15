
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class CustomerSalesReportDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadComUnit(DropDownList dropDownList)
        {
            string query = @"SELECT * from tblCompanyUnit ";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "ComUnitName", "ComUnitId", query, "SSIDB");
        }
        public void LoadComUnit(DropDownList dropDownList,string comUnitId)
        {
            string query = @"SELECT * from tblCompanyUnit where ComUnitId='" + comUnitId + "' ";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "ComUnitName", "ComUnitId", query, "SSIDB");
        }

        public void LoadDistrict(DropDownList dropDownList, string comUnitId)
        {
            string query = @"SELECT DISTINCT DistrictName,DistrictId FROM dbo.View_CustomerMaster WHERE DistrictId IN (SELECT DistrictId FROM dbo.tblDistrict) AND dbo.View_CustomerMaster.ComUnitId='" + comUnitId.Trim() + "'";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "DistrictName", "DistrictId", query, "SSIDB");
        }

        public void LoadArea(DropDownList dropDownList, string districtId)
        {
            string query = @"SELECT DISTINCT AreaId,AreaName FROM dbo.View_CustomerMaster WHERE AreaId IN (SELECT AreaId FROM dbo.tblArea) AND dbo.View_CustomerMaster.DistrictId='" + districtId.Trim() + "'";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "AreaName", "AreaId", query, "SSIDB");
        }

        public void LoadMarket(DropDownList dropDownList, string areaId)
        {
            string query = @"SELECT DISTINCT MarketId,MarketName FROM dbo.View_CustomerMaster WHERE MarketId IN (SELECT MarketId FROM dbo.tblMarket) AND dbo.View_CustomerMaster.AreaId='" + areaId.Trim() + "'";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "MarketName", "MarketId", query, "SSIDB");
        }

        public void LoadCustomer(DropDownList dropDownList, string areaId)
        {
            string query = @"SELECT DISTINCT CustomerMasterId,CustomerName FROM dbo.View_CustomerMaster WHERE CustomerMasterId IN (SELECT CustomerMasterId FROM dbo.tblCustMaster) AND dbo.View_CustomerMaster.MarketId='" + areaId.Trim() + "'";
            aCommonInternalDal.LoadDropDownValue(dropDownList, "CustomerName", "CustomerMasterId", query, "SSIDB");
        }

        public DataTable CustomerReportMainDataDAL(string custId, DateTime fromDate, DateTime toDate)
        {
            string query = @"select (CustomerCode+':'+CustomerName) as CustDetail,Address,MarketName,AreaName,DistrictName,ComUnitName, '" + fromDate + "' as FromDate, '" + toDate + "' as ToDate  from View_CustomerMaster where CustomerMasterId ='" + custId + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable CustomerReportDetailDataDAL(string custId, DateTime fromDate, DateTime toDate)
        {
            string query =
                @"select ProductCode,Product,sum(TotalQuantity) as Total, sum(Price) as TotalPrice from  View_MIAWiseSalesReport  where CustomerMasterId ='" + custId + "'  and InvoiceDate between '" + fromDate + "' and '" + toDate +"' group by ProductCode,Product order by ProductCode";


            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
