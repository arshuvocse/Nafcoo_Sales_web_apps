using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.MasterSetup_DAL;
using DataTable = System.Data.DataTable;

public partial class SInventory_UI_QuotedPriceReport : System.Web.UI.Page
{

    //private QuotedPriceSetupDAL _Dal = new QuotedPriceSetupDAL();
    private CampaignSetupDal _Dal = new CampaignSetupDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadDropdownlist();
        }
    }

    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {

        string empName = custCodeTextBox.Text.Trim();
        if (empName.Contains(':'))
        {
            string[] emp = empName.Split('|');

            hfCustomerId.Value = emp[1].Trim();
            custCodeTextBox.Text = emp[0].Trim();
        }
        else
        {

            custCodeTextBox.Text = "";
            hfCustomerId.Value = "";
            showMessageBox("Input Correct Data !!");
        }

    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void GetSpecialDiscountData()
    {
        DataTable aTable = new DataTable();

        aTable = _Dal.GetSpecialDiscountData(Pram());

        if (aTable.Rows.Count > 0)
        {
            loadGridView.DataSource = aTable;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.GetSpecialDiscountData();
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


    private String Pram()
    {
        string param = "";

        if (hfCustomerId.Value != "")
        {
            param = param + " AND QM.CustomerInformationId =" + hfCustomerId.Value;
        }

        return param;

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Quoted_Price_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.GetSpecialDiscountData();

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

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        GetSpecialDiscountData();
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SpecialDiscountView.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CampaignSetup.aspx");
    }
}