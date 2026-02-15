using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.BLL.SInventory_BLL;
using Library.DAL.MasterSetup_DAL;

public partial class SInventory_RPTVIEW_AuditReportOneReportViewer : System.Web.UI.Page
{

    ExpenseDal aExpenseDal = new ExpenseDal();
    ReportDocument rptdoc = new ReportDocument();

    protected void Page_Init(object sender, EventArgs e)
    {
        string fType = (Request.QueryString["fType"]);
        string EmpId = (Request.QueryString["rptType"]);
        string Month = (Request.QueryString["Month"]);
        string Year = (Request.QueryString["Year"]);
   


        DataSet Ds = new DataSet();
  

        DataTable dtExpense = aExpenseDal.GetExpenseMasterById( Month,  Year,  EmpId).Copy();

        dtExpense.TableName = "dtExpense";
        Ds.Tables.Add(dtExpense);


        DataTable dtTotal = aExpenseDal.GetExpenseMasterTotalById(Month, Year, EmpId).Copy();

        dtTotal.TableName = "dtTotal";
        Ds.Tables.Add(dtTotal);


        DataTable dtAllowance = aExpenseDal.Get_EmpAllawance(EmpId).Copy();

        dtAllowance.TableName = "dtAllowance";
        Ds.Tables.Add(dtAllowance);

        DataTable dtStationType = aExpenseDal.GetGet_TourPlanBalanceMasterById(Month, Year, EmpId).Copy();

        dtStationType.TableName = "dtStationType";
        Ds.Tables.Add(dtStationType);


        rptdoc.Load(ReportPath("crpEmpExpense.rpt"));
        rptdoc.SetDataSource(Ds);
        if (fType == "Crys")
        {
            crvSalesRpt.ReportSource = rptdoc;
            crvSalesRpt.DataBind();
        }
        else
        {
           
           // rptdoc.PrintToPrinter(1, false, 0, 0);
            rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
           "Monthly Expense Claim");
        }

       

    }
    private string ReportPath(string rptName)
    {
        return Convert.ToString(Server.MapPath("~\\Reports\\CrystalReports\\" + rptName));
    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "Close", "window.close()", true);
    }
    
    protected void crvCustMasterRpt_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvSalesRpt.Dispose();
        }
    }
    protected void crvCustMasterRpt_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvSalesRpt.Dispose();
        }
    }
}