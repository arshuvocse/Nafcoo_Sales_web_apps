using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.DataManager;
using Library.DAL.SInventory_DAL;
using System.Data;
using DataTable = System.Data.DataTable;

public partial class SInventory_UI_CustomerCreditLimitView : System.Web.UI.Page
{
    CustomerCreditLimitDal aDal = new CustomerCreditLimitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridView();
        }
    }

    private void LoadGridView()
    {
        DataTable aTable = aDal.GetData("");

        if (aTable.Rows.Count > 0)
        {
            dataGridView.DataSource = aTable;
            dataGridView.DataBind();
        }
        else
        {
            dataGridView.DataSource = null;
            dataGridView.DataBind();
        }
        
        
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (dataGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Customer_Credit_Limit_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            dataGridView.AllowPaging = false;
            this.LoadGridView();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in dataGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in dataGridView.Rows)
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

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
       Response.Redirect("CustomerCreditLimitSetup.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string id = dataGridView.DataKeys[rowindex][0].ToString();
            Session["MasterId"] = dataGridView.DataKeys[rowindex][0].ToString();
            Response.Redirect("CustomerCreditLimitSetup.aspx");

        }
    }
}