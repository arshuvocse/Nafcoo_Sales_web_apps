using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class StockBatchUpdateDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable LoadStockByDcId(Int32 dcId)
        {
            string query = @"SELECT DCStoreId,ProductCode,ProductName,BatchNo,TotalQuantity,StockQty,FORMAT(MfgDate,'dd MMMM, yyyy') MfgDate, FORMAT(ExpDate,'dd MMMM, yyyy') ExpDate FROM tblDCStore with (nolock) WHERE StockQty > 0 AND ComUnitId = " + dcId + " Order by ProductCode";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public bool UpdateStockBatch(int dcStoreId, string batch, string mfgdate, string expDate,string updateBy, decimal  StockQty, string productCode, int comUnitId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@dcStoreId", dcStoreId));
            aSqlParameterlist.Add(new SqlParameter("@batch", batch));
            aSqlParameterlist.Add(new SqlParameter("@mfgdate", mfgdate));
            aSqlParameterlist.Add(new SqlParameter("@expDate", expDate));
            aSqlParameterlist.Add(new SqlParameter("@updateBy", updateBy));
            aSqlParameterlist.Add(new SqlParameter("@productCode", productCode));
            aSqlParameterlist.Add(new SqlParameter("@ComUnitId", comUnitId));
            aSqlParameterlist.Add(new SqlParameter("@StockQty",  StockQty));

            return aCommonInternalDal.UpdateAction("sp_UD_StockBatch_new", aSqlParameterlist);
        }
    }
}
