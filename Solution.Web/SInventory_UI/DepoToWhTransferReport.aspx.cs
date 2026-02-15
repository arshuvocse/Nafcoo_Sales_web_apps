using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_DepoToWhTransferReport : System.Web.UI.Page
{

    DataTable aDataTable = new DataTable();

    SCtoWHTransferDal areaBll = new SCtoWHTransferDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadB2BInformation();
        }
    }


    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        LoadB2BInformation();
    }


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (detailGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DepotWH_Chalan_Report_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            detailGridView.AllowPaging = false;
            this.LoadB2BInformation();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in detailGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in detailGridView.Rows)
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

    private void LoadB2BInformation()
    {

        detailGridView.DataSource = null;
        detailGridView.DataBind();
 

        DataTable aTable = areaBll.GetB2bTransferInfo(GetPrameterList());

        if (aTable.Rows.Count > 0)
        {
            detailGridView.DataSource = aTable;
            detailGridView.DataBind();
        }
        else
        {
            detailGridView.DataSource = null;
            detailGridView.DataBind();
        }

    }

    private String GetPrameterList()
    {
        string parameter = "";

        // Get current date
        DateTime currentDate = DateTime.Now;

        // Get first date of the current month
        DateTime firstDate = new DateTime(currentDate.Year, currentDate.Month, 1);

        // Get last date of the current month
        DateTime lastDate = firstDate.AddMonths(1).AddDays(-1);


        if (fromDateTextBox.Text != "" && toDateTextBox.Text != "")
        {
            parameter = parameter + "AND ChalanDate BETWEEN '" + Convert.ToDateTime(fromDateTextBox.Text.Trim()) +
                        "' AND ' " + Convert.ToDateTime(toDateTextBox.Text.Trim()) + "'";
        }
        else
        {
            parameter = parameter + "AND ChalanDate BETWEEN '" + firstDate + "' AND ' " + lastDate + "'";
        }


        return parameter;

    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepoToWhTransferReport.aspx");
    }
}