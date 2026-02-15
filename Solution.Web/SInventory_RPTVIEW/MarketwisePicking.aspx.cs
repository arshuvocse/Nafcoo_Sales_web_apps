using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.BLL.SInventory_BLL;
using Library.CrystalReports.SInventory_RPT;
using Library.DAL.SInventory_DAL;

public partial class SInventory_RPTVIEW_MarketwisePicking : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();
    StockTransportOrderReportBLL aOrderReportBll = new StockTransportOrderReportBLL();
    InvoiceBLL aInvoiceBll = new InvoiceBLL();
    InvoiceDAL aDal = new InvoiceDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
       // int MarketID = 0;
       // try
       // {
       //      MarketID = Convert.ToInt32(Session["Market"]);
       // }
       // catch (Exception)
       // {
       //      MarketID =0;
       // }

       // string Territory = "";
       // try
       // {
       //     Territory = (Session["Territory"]).ToString();
       // }
       // catch (Exception)
       // {
       //     Territory = "";
       // }

       // string TerritoryID = (Session["Territory"]).ToString();
       //string Route =  (Session["Route"].ToString()) ;
       //string InvDate =  (Session["invoicedate"].ToString()) ;
       //string SC = Server.UrlDecode(Request.QueryString["SC"]);

        string topSheetId = Server.UrlDecode(Request.QueryString["TopSheetId"]);
        string rptType = string.IsNullOrEmpty(Session["RptType"].ToString()) ? "" : Session["RptType"].ToString();
     

        DataTable mainDataTable;
        DataTable invDataTable;


        mainDataTable = aDal.GetTopSheetInfo(topSheetId, "", "").Copy();
        invDataTable = aDal.GetTopSheetWiseInvoice(topSheetId, "", "").Copy();
        //DataTable detailDataTable = aInvoiceBll.InvoiceDetailDataForReportBLL(invColl).Copy();
        DataTable companyInfoDataTable = aOrderReportBll.CompanyInfoBLL().Copy();

       // rptMarketwisePicking aRptInvoiceForCustomer = new rptMarketwisePicking();

        DataSet Ds = new DataSet();

        mainDataTable.TableName = "marketwisepickingDataTable";
        invDataTable.TableName = "invoicegDataTable";
       // detailDataTable.TableName = "detailDataTable";
        companyInfoDataTable.TableName = "companyInfoDataTable";
        Ds.Tables.Add(mainDataTable);
        Ds.Tables.Add(invDataTable);
       // Ds.Tables.Add(detailDataTable);
        Ds.Tables.Add(companyInfoDataTable);
       // aRptInvoiceForCustomer.SetDataSource(Ds);
       //crvInvoiceReport.ReportSource = aRptInvoiceForCustomer;
        //aRptInvoiceForCustomer.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
        //       "Picking");

        if (rptType == "TopSheet")
        {
            //rptdoc.Load(ReportPath("rptTopSheet.rpt"));
            //rptdoc.SetDataSource(Ds);
            //rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
            //    "Top Sheet -" + (DateTime.Now).ToString(CultureInfo.InvariantCulture));
        }
        else
        {
            rptdoc.Load(ReportPath("rptMarketwisePicking.rpt"));
            rptdoc.SetDataSource(Ds);
            rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true,
                "Picking Slip -" + (DateTime.Now).ToString(CultureInfo.InvariantCulture));
        }
        

        //crvInvoiceReport.ReportSource = rptdoc;
        //crvInvoiceReport.DataBind();
    }

    private string ReportPath(string rptName)
    {
        return Convert.ToString(Server.MapPath("~\\Reports\\CrystalReports\\" + rptName));
    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "Close", "window.close()", true);
    }
    protected void crvInvoiceReport_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvInvoiceReport.Dispose();
        }
    }
    protected void crvInvoiceReport_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crvInvoiceReport.Dispose();
        }
    }
}