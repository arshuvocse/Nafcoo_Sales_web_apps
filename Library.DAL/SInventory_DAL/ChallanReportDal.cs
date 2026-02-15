using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;

namespace Library.DAL.SInventory_DAL
{
    public class ChallanReportDal
    {

        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable GetChallanReport(string id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parameter", id));
                DataTable dt = accessManager.GetDataTable("sp_GET_ChallanInfo_Report", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

    }
}
