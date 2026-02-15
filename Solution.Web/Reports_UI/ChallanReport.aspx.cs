using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.SInventory_DAL;
using DataTable = System.Data.DataTable;

public partial class Reports_UI_ChallanReport : System.Web.UI.Page
{


    private readonly ChallanReportDal aDal = new ChallanReportDal();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();

            Response.Redirect("../DoctorModule_UI/MileageClaim.aspx?id=" + unitPriceId);
        }

    }


    private bool Validation()
    {

        if(txtInvoiceFromDate.Text == "")
        {

            showMessageBox("Please Select From Date");
            txtInvoiceFromDate.Focus();
            return false;
        }

        if (txtInvoiceTodate.Text == "")
        {
            showMessageBox("Please Select To Date");
            txtInvoiceTodate.Focus();
            return false;
        }
        return true;
    }


    private string GenerateParameter()
    {

        string param = "";

        if (txtInvoiceFromDate.Text != "" && txtInvoiceTodate.Text !="")
        {
            param = param + " AND ChalanDate between '" + txtInvoiceFromDate.Text + "' and '" + txtInvoiceTodate.Text + "' ";
        }

        if (txtInvoiceFromDate.Text != "" && txtInvoiceTodate.Text == "")
        {
            param = param + " AND ChalanDate between '" + txtInvoiceFromDate.Text + "' and '" + DateTime.Now + "' ";
        }

        if (txtInvoiceFromDate.Text == "" && txtInvoiceTodate.Text != "")
        {
            param = param + " AND ChalanDate between '" + DateTime.Now + "' and '" + txtInvoiceTodate.Text + "' ";
        }

        return param;
    }




    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        LoadData();
    }



    private void LoadData()
    {
        if (Validation())
        {
            DataTable aTable = aDal.GetChallanReport(GenerateParameter());

            loadGridView.DataSource = aTable;
            loadGridView.DataBind();
        }
    }


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Challan_Report" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.LoadData();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in loadGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in loadGridView.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    //Append data with separator.
                    if (cell.Text.Contains(","))
                    {
                        sb.Append(String.Format("\"{0}\",", cell.Text));
                    }
                    else
                    { sb.Append(HttpUtility.HtmlDecode(cell.Text) + ','); }
                }
                //Append new line character.
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "No Data Found!" + "','Faild');", true);

        }
    }


    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
}