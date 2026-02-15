using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class CountryStockReportDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable CountryReportMainDataDAL(string productCode)
        {
            string query = @"select '"+productCode+"' as ProductCode";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable CountryReportDetailDataDAL(string productCode)
        {
            string query =
                @"select * from  View_TotalCurrentStockofCompanyWithStockInTransfar where ProductCode='"+productCode+"'";


            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable CountryReportWithoutProductCodeDetailDataDAL()
        {
            string query =
                @"select * from View_CentralStoreCurrentStock";


            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
