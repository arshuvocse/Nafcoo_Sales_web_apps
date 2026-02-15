using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.CrystalReports.SInventory_DS;
using Library.DAL.SInventory_DAL;
using SalesSolution.Web.DataLayer;

public partial class SInventory_UI_AreaWiseMonthlyInventoryReport : System.Web.UI.Page
{

    AreaWiseMonthlyInventoryReportBll areaWise = new AreaWiseMonthlyInventoryReportBll();
    DCHtmlReportDal aReportDal = new DCHtmlReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropdownList();
        }
    }

    protected void fromDateTextBox_TextChanged(object sender, EventArgs e)
    {
        DateTime Fromd = Convert.ToDateTime("01-Apr-2022");
        DateTime inputDateTime = Convert.ToDateTime(fromDateTextBox.Text);
        if (inputDateTime < Fromd)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("01 April, 2022");
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockHTMLReportPanel.aspx");
    }

    private static SeedDataDAL _seedRepo = new SeedDataDAL();

    private void LoadDropdownList()
    {

        try
        {
            using (DataTable dt = _seedRepo.GetProductNameList())
            {
                ddlProductName.DataSource = dt;
                ddlProductName.DataValueField = "ProductCode";
                ddlProductName.DataTextField = "ProductName";
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlProductName.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        aOrderInfoBll.LoadSC(branchDropDownList, Session["UserId"].ToString());
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    private void ReportPopUp()
    {
        //if (branchDropDownList.SelectedValue != "")
        {
            if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
            {
                var fromDate = Convert.ToDateTime(fromDateTextBox.Text.Trim());
                var toDate = Convert.ToDateTime(todateTextBox.Text.Trim());
                var branchId = (branchDropDownList.SelectedValue);
               

                var url = "../HTMLReport/StockHTMLReport.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&branchId=" + branchId + "&national=" + "";
                var fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            }
            else
            {
                ShowMessageBox("Please Select adate range!!");
            }
        }
        //else
        //{
        //    ShowMessageBox("Please Select a branch!!");
        //}

        
    }

    protected void viewRptButton_Click(object sender, EventArgs e)
    {
        //ReportPopUp();

        LoadData();
    }
    
    private void LoadData()
    {
        DataTable comUnitDetailDataTable = new DataTable();

                if (fromDateTextBox.Text == "" || todateTextBox.Text == "")
                {
                    ShowMessageBox("Please select date range !!");
                }
                else
                {
                    //if (branchDropDownList.SelectedValue!="")
                    //{
                        int idd = 0;
                        string BranchName = "";
                        try
                        {
                            idd = Convert.ToInt32(branchDropDownList.SelectedValue);
                            BranchName = branchDropDownList.SelectedItem.Text;
                        }
                        catch { }

                        string ProCode = "";
                        try
                        {
                            ProCode = (ddlProductName.SelectedValue);
                        }
                        catch { }
                        comUnitDetailDataTable = aReportDal.LoadDepotWiseStock(Convert.ToDateTime(fromDateTextBox.Text), Convert.ToDateTime(todateTextBox.Text), idd, ProCode, BranchName);


                        if (comUnitDetailDataTable.Rows.Count > 0)
                        {
                            loadGridView.DataSource = comUnitDetailDataTable;
                            loadGridView.DataBind();
                        }
                        else
                        {
                            loadGridView.DataSource = null;
                            loadGridView.DataBind();
                        }
                    //}
                    //else
                    //{
                    //    ShowMessageBox("Please select Branch Name !!");
                            
                    //}
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

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData();
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Stock_Report_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
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
}