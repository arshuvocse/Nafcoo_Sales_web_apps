using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Library.BLL.SInventory_BLL;
using Library.CrystalReports.SInventory_RPT;

public partial class SInventory_RPTVIEW_PayImageViewer : System.Web.UI.Page
{

    ReportDocument rptdoc = new ReportDocument();

    private InvoiceBLL aInvoiceBll = new InvoiceBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["fromDate"];

        DataSet mainDS = new DataSet();

        if (rptType != "")
        {
            DataTable allDataTable = new DataTable();
            allDataTable = aInvoiceBll.GETpaySlip(rptType.ToString()).Copy();
            allDataTable.TableName = "PaySlipImage";
            mainDS.Tables.Add(allDataTable);
            if (mainDS.Tables[0].Rows.Count > 0)
            {
               // mainDS.WriteXmlSchema(MapPath("~\\Library.CrystalReports\\SInventory_DS\\dsPaySlipImage.xsd"));
                ShowReport(mainDS, "RptPaySlip.rpt");
            }

        }
    }
    private void ShowReport(DataSet dsDataSet, string reportName)
    {
        if (dsDataSet.Tables[0].Rows.Count > 0)
        {
            rptdoc.Load(ReportPath(reportName));
            rptdoc.SetDataSource(dsDataSet);
            crReportViewer.ReportSource = rptdoc;
            crReportViewer.DataBind();
        }
        else
        {
            lblMsg.Text = "No Data Found!!!!";
        }

    }
    private string ReportPath(string rptName)
    {
        return Convert.ToString(Server.MapPath("~\\Reports\\CrystalReports\\" + rptName));

    }
    protected void rptViewerBasic_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }

    protected void rptViewerBasic_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }


}