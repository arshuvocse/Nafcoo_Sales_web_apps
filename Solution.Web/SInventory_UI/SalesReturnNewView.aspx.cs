using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.SInventory_DAL;
using DataTable = System.Data.DataTable;

public partial class SInventory_UI_SalesReturnNewView : System.Web.UI.Page
{
    NewSalesReturnDal aReturnDal = new NewSalesReturnDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        aReturnDal.DCLoad(ddlDepot);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        //DataTable aTable = aReturnDal.GetReturnData(GetPram());

        DataTable aTable = new DataTable();

        aTable = aReturnDal.GetReturnData(GetPram());

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

    private String GetPram()
    {
        string param = "";

        if (ddlDepot.SelectedValue != "")
        {
            param = param + " AND RI.ComUnitId='" + ddlDepot.SelectedValue + "' ";
        }

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,ReturnInvoiceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        
        if (fromDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,ReturnInvoiceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + DateTime.Now + "' ";
        }

       
        return param;
    }




    protected void printButton_Click(object sender, EventArgs e)
    {
        //string url = "../SInventory_RPTVIEW/ReturnInvoiceReportViewer.aspx?InvNo=" + Server.UrlEncode(invTextBox.Text.Trim());
        //// string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" +
        //                 url +
        //                 "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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

    protected void viewRptButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void invoiceButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string invTextBox = orderGridView.DataKeys[rowindex][1].ToString();


            string url = "../SInventory_RPTVIEW/ReturnInvoiceReportViewer.aspx?InvNo=" + Server.UrlEncode(invTextBox);
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" +
                             url +
                             "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        }
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
            ScriptManager.RegisterStartupScript(this, typeof (string), "OPEN_WINDOW", fullURL, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                "faildalert('" + "Please Select at Least one row from Table!" + "','Faild');", true);

        }


    }

    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesReturnNew.aspx");
    }
}