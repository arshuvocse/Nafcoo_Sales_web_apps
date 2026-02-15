using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAL.MAIN_FUNCTION;

namespace Library.DAL.SInventory_DAL
{

    public class GrossDiscountAdjustmentDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();
        DB_Manager aDbManager = new DB_Manager();


        public void CreateConnection_DAL()
        {
            aDbManager.CreateConnection("SalesDisDB_New3");
        }
        public void CloseAllConnection_DAL()
        {
            aDbManager.CloseConnection();
        }

        public int UpdateInvoice(string deliveryInvoiceNo,decimal grossDiscountAmount, int updateby)
        {
            
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@DelivaryInvoiceNo", deliveryInvoiceNo));
            aSqlParameterList.Add(new SqlParameter("@TotalDiscountAmount", grossDiscountAmount));
            aSqlParameterList.Add(new SqlParameter("@UpdateBy", updateby));

            return aCommonInternalDal.RunStoreProcedure("sp_GrossDiscount_Update", aSqlParameterList, "SSIDB");
        }


    }
}
