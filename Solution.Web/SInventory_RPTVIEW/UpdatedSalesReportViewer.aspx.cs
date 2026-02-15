using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.BLL.SInventory_BLL;
using Library.CrystalReports.SInventory_RPT;
using Library.DAL.SInventory_DAL;

public partial class SInventory_RPTVIEW_SalesReturnViewer : System.Web.UI.Page
{
    StockTransportOrderReportBLL aOrderReportBll = new StockTransportOrderReportBLL();
    CompanySalesReportBLL aCompanySalesReportBll = new CompanySalesReportBLL();
    ReportDocument rptdoc = new ReportDocument();
    InvoiceBLL aInvoiceBll = new InvoiceBLL();
    protected void Page_Init(object sender, EventArgs e)
    {
        SalesReportDal aDal = new SalesReportDal();

        DataTable companyInfoDataTable = aOrderReportBll.CompanyInfoBLL().Copy();

        string parameter = Convert.ToString(Session["SalesReport"]);

        if (parameter != "")
        {
            DataTable mainDataTable = aDal.GetSalesReport(parameter).Copy();

            DataSet Ds = new DataSet();

            mainDataTable.TableName = "SalesData";
            companyInfoDataTable.TableName = "companyInfoDataTable";
            Ds.Tables.Add(companyInfoDataTable);
            Ds.Tables.Add(mainDataTable);

            rptdoc.Load(ReportPath("rptUpdatedSalesReport.rpt"));
            rptdoc.SetDataSource(Ds);

            crvSalesRpt.ReportSource = rptdoc;
            crvSalesRpt.DataBind();

            //rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true,
            //   "Sales_Report");
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
    //protected void crViewer_Unload(object sender, EventArgs e)
    //{
       
    //}
    //protected void crViewer_Disposed(object sender, EventArgs e)
    //{
       
    //}
    protected void crvSalesRpt_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvSalesRpt.Dispose();
        }
    }
    protected void crvSalesRpt_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvSalesRpt.Dispose();
        }
    }
}