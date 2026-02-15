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

public partial class SInventory_UI_TopSheetGenerateByRouteView : System.Web.UI.Page
{
    NewSalesReturnDal aDal = new NewSalesReturnDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           LoadDropdownlist();
           txtFromDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
           txtToDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
           LoadGridView();
            rbApprovalStatus.SelectedIndex = 0;
        }
    }

    private void LoadDropdownlist()
    {
        aDal.DCLoad(ddlDepot);
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
        System.Data.DataTable aTable = new System.Data.DataTable();

        aTable = aDal.GetReturnData(GenerateParameter());

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

    private string GenerateParameter()
    {
        string pram = " AND RI.ApprovalStatus = '0' ";

        if (ddlDepot.SelectedValue != "")
        {
            pram = pram + " AND RI.ComUnitId = " + ddlDepot.SelectedValue;
        }

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            pram = pram + " AND CONVERT(date,ReturnInvoiceDate) Between '" + txtFromDate.Text.Trim() + "' AND '" + txtToDate.Text + "' ";
        }

        if (txtFromDate.Text != "" && txtToDate.Text == "")
        {
            pram = pram + " AND CONVERT(date,ReturnInvoiceDate) Between '" + txtFromDate.Text.Trim() + "' AND '" + DateTime.Now + "' ";
        }

        if (txtFromDate.Text == "" && txtToDate.Text != "")
        {
            pram = pram + " AND CONVERT(date,ReturnInvoiceDate) Between '" + DateTime.Now + "' AND '" + txtToDate.Text.Trim() + "' ";
        }

        return pram;
    }

    public string GenerateReportParameter()
    {
        string pram = "";

        if (ddlDepot.SelectedValue != "")
        {
            pram = pram + " AND TSG.DAId = " + ddlDepot.SelectedValue;
        }

        return pram;
    }

    protected void topSheetButton_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        string invTextBox = orderGridView.DataKeys[rowindex][1].ToString();


        if (invTextBox != "")
        {
            string url = "../SInventory_RPTVIEW/ReturnInvoiceReportViewer.aspx?InvNo=" + Server.UrlEncode(invTextBox);
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL =
                "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" +
                url +
                "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                "faildalert('" + "Please Select at Least one row from Table!" + "','Faild');", true);

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

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)orderGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[6].FindControl("chkSelect");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    protected void submitLinkButton_Click(object sender, EventArgs e)
    {
        if (Valid())
        {
            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {
                CheckBox ChkBoxRows = (CheckBox) orderGridView.Rows[i].Cells[6].FindControl("chkSelect");

                if (ChkBoxRows.Checked)
                {
                    string returnInvoiceId = orderGridView.DataKeys[i][0].ToString();
                    
                    aDal.AppReturnInvoice(Session["EmpName"].ToString(),returnInvoiceId, rbApprovalStatus.SelectedValue);
                    
                    
                }
            }

            LoadGridView();
        }
    }

    private bool Valid()
    {
        int count = 0;

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {

            CheckBox ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[6].FindControl("chkSelect");

            if (ChkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
            
        }

        if (count == 0)
        {
            ShowMessageBox("Please select at least one row !!");
            return false;
        }

        return true;
    }
}