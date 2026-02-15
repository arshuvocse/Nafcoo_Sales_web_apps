using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class MultiCustomerEditDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool UpdateDataForCustomer(string parameter,string customerId)
        {
            string insertQuery = @"UPDATE dbo.tblCustMaster SET "+parameter+" WHERE CustomerMasterId='"+customerId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(insertQuery, "SSIDB");
        }
        public DataTable LoadCusteomer(string parameter)
        {
            string query = @"SELECT * FROM dbo.View_CustomerMaster " + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable CHeckInvice(string custimerId)
        {
            string query = @"SELECT * FROM dbo.tblInvoice WHERE CustomerMasterId= '" + custimerId + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

    }
}
