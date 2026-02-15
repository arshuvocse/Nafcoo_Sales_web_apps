using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using Microsoft.VisualBasic;

public partial class SInventory_UI_Dashboard : System.Web.UI.Page
{
    static SalesDashboardDal aDashboardDal = new SalesDashboardDal();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //[WebMethod(EnableSession = true)]
    //public static CompanyWiseSalesDao[] GetCompanyWiseSales(string companyId, string year, string month)
    //{

    //    List<CompanyWiseSalesDao> aList = new List<CompanyWiseSalesDao>();

    //    DateTime date = DateTime.Today;
    //    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
    //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

    //    DataTable aTable = aDashboardDal.GetMioWiseInvoice(companyId == "" ? "1" : companyId, fromDate == "" ? firstDayOfMonth : Convert.ToDateTime(fromDate), toDate == "" ? lastDayOfMonth : Convert.ToDateTime(toDate), mioId == null ? "0" : mioId);


    //    if (aTable.Rows.Count > 0)
    //    {

    //        foreach (DataRow DR in aTable.Rows)
    //        {
    //            var aData = new CompanyWiseSalesDao();

    //            DataTable dueTable = aDashboardDal.GetDueUpto(Convert.ToDateTime(DR["InvoiceDate"]), companyId);

    //            aData.DueValue = dueTable.Rows.Count > 0 ? dueTable.Rows[0].Field<Decimal>("DueAmount") : Convert.ToDecimal(0);

    //            aData.InvoiceDate = DR["InvoiceDate2"].ToString();                
    //            aData.SalesValue = Convert.ToDecimal(DR["SalesValue"].ToString());
    //            aData.CollectionValue = Convert.ToDecimal(DR["Collection"].ToString());

    //            aList.Add(aData);

    //        }

    //        return aList.ToArray();
    //    }


    //    return aList.ToArray();

    //}

    
}