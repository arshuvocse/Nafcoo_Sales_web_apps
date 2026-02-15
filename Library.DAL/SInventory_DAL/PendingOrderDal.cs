using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class PendingOrderDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetPendingOrderInformation(string SC, string fromdate, string todate)
        {
            DataTable aDataTable = new DataTable();

            string query = @"SELECT DISTINCT OrderMasterID,OrderCode,SubmissionDate,SalesCentre,CustomerID,CustomerName
            FROM dbo.tblOrderListDetail  with (nolock)  
inner join tblCompanyUnit on tblOrderListDetail.SalesCentre = tblCompanyUnit.ComUnitCode
WHERE 
            OrderCode NOT IN (SELECT OrderCode FROM dbo.tblOrder nolock WHERE SubmissionDate BETWEEN '" + fromdate + "' and '" + todate +  "') AND  ComUnitId = '" + SC + "' and (SubmissionDate BETWEEN '" + fromdate + "' and '" + todate + "') ";
            return aDataTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetOrderExistOrNotInfo(string orderCode)
        {
            DataTable aDataTable = new DataTable();

            string query = @"SELECT OrderCode FROM dbo.tblOrder WITH(NOLOCK) WHERE OrderCode = '" + orderCode.Trim() + "'";
            return aDataTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetOrderDetailsInfo(string orderCode){
            
            DataTable aDataTable = new DataTable();

            string query = @"SELECT OrderMasterID,OrderCode FROM dbo.tblOrderListDetail WITH(NOLOCK) WHERE OrderCode = '" + orderCode.Trim() + "'";
            return aDataTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public bool SaveOrderGenerationInfo(Int32 OrderMasterID, string orderCode){
        
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@OrderMasterID_In", OrderMasterID));
            aSqlParameterlist.Add(new SqlParameter("@OrderCode", orderCode));
            aSqlParameterlist.Add(new SqlParameter("@IsApiData", 1));

            return aCommonInternalDal.DeleteAction("sp_OrderGenerationFromUploadOrder_SingleOrder", aSqlParameterlist);

        }
    }
}
