using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.SInventory_DAL;
using System.Data;
using DataTable = DocumentFormat.OpenXml.Drawing.Charts.DataTable;
using Library.BLL.SInventory_BLL;

public partial class SInventory_UI_TopSheetGenerateByRouteView : System.Web.UI.Page
{
    TopSheetGenerateByRouteDal aDal = new TopSheetGenerateByRouteDal();
    OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownlist();

       

           txtFromDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
           txtToDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
        }
    }

    private void LoadDropdownlist()
    {
       
        aOrderInfoBll.LoadSC(salesCenterDropDownList, Session["UserId"].ToString());
        try
        {
            salesCenterDropDownList.SelectedIndex = 1;
            salesCenterDropDownList_SelectedIndexChanged(null, null);
        }
        catch
        {

        }
    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string id = orderGridView.DataKeys[rowindex][0].ToString();
            Session["TopSheetId"] = orderGridView.DataKeys[rowindex][0].ToString();
            Response.Redirect("TopSheetGenerateByRoute.aspx");

        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string stockInId = loadGridView.DataKeys[rowindex][0].ToString();

        //    aWarehouseStockInBll.DeleteWhStockInInfoById(stockInId);
        //    ShowMessageBox("Welldone! Stockin Information Deleted Successfully!!!");
        //}

        LoadGridView();

    }

    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerateByRoute.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadGridView();
    }

    public void LoadGridView()
    {

        if (salesCenterDropDownList.SelectedValue == "")
        {
            // Show alert message
            string script = "alert('Please select a Sales Center.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", script, true);
            return;
        }
        System.Data.DataTable aTable = new System.Data.DataTable();

        aTable = aDal.GetTopSheetList(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {
            orderGridView.DataSource = aTable;
            orderGridView.DataBind();
        }
        else
        {
            orderGridView.DataSource = null;
            orderGridView.DataBind();
        }
    }

    public string GenerateParameter()
    {
        string pram ="";

        if (ddlDA.SelectedValue != "")
        {
            pram = pram + " AND TSG.DAId = " + ddlDA.SelectedValue;
           
        }

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            pram = pram + " AND CONVERT(date,TSG.EntryDate) Between '" + txtFromDate.Text.Trim() + "' AND '" + txtToDate.Text + "' ";
        }

        if (txtFromDate.Text != "" && txtToDate.Text == "")
        {
            pram = pram + " AND CONVERT(date,TSG.EntryDate) Between '" + txtFromDate.Text.Trim() + "' AND '" + DateTime.Now + "' ";
        }

        if (txtFromDate.Text == "" && txtToDate.Text != "")
        {
            pram = pram + " AND CONVERT(date,TSG.EntryDate) Between '" + DateTime.Now + "' AND '" + txtToDate.Text.Trim() + "' ";
        }
        pram = pram + " AND TSG.EntryBy = " + Session["UserId"].ToString();
        pram = pram + @"  and  TopSheetGenReportId in (select TopSheetGenReportId from tblTopSheetGenReportDetails dtl inner join tblInvoice inv on dtl.InvoiceId=inv.InvoiceId  where inv.ComUnitId=" + salesCenterDropDownList.SelectedValue + ") ";
        return pram;
    }

    public string GenerateReportParameter()
    {
        string pram = "";

        if (ddlDA.SelectedValue != "")
        {
            pram = pram + " AND TSG.DAId = " + ddlDA.SelectedValue;
        }

        return pram;
    }

    protected void topSheetButton_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        string topSheetId = orderGridView.DataKeys[rowindex][0].ToString();

        if (Convert.ToInt32(topSheetId) > 0)
        {
            if (topSheetId != "")
            {

                string url = "../SInventory_RPTVIEW/TopSheetReportViewer.aspx?TopSheetId=" + topSheetId + "&Code=" + orderGridView.Rows[rowindex].Cells[1].Text.Trim();
                // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
                string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please Select at Least one row from Table!" + "','Faild');", true);

            }
        }
    }

    public void PrintReport(string topSheetId,string rptType)
    {
        Session["RptType"] = "";
        Session["RptType"] = rptType;

        string url = "../SInventory_RPTVIEW/MarketwisePicking.aspx?TopSheetId=" + Server.UrlEncode(topSheetId);
        // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void pcSlipButton_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        string topSheetId = orderGridView.DataKeys[rowindex][0].ToString();

        if (Convert.ToInt32(topSheetId) > 0)
        {
            PrintReport(topSheetId, "PickingSlip");
        }
    }

    protected void salesCenterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aDal.LoadDA(ddlDA, salesCenterDropDownList.SelectedValue);
    }
}