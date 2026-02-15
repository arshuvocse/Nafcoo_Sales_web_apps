using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.MasterSetup_DAL;
using OfficeOpenXml;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class SInventory_UI_ProformaReport : System.Web.UI.Page
{
    private static CmnCrystaltoView _DAL = new CmnCrystaltoView();
    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;
    protected void Page_Load(object sender, EventArgs e)
    {
        GroupSelect = (DropDownList)IVMarketStructure.FindControl("GroupSelect") as DropDownList;
        ZoneSelect = (DropDownList)IVMarketStructure.FindControl("ZoneSelect") as DropDownList;
        AreaSelect = (DropDownList)IVMarketStructure.FindControl("AreaSelect") as DropDownList;
        TeritorySelect = (DropDownList)IVMarketStructure.FindControl("TeritorySelect") as DropDownList;
        SubTeritory = (DropDownList)IVMarketStructure.FindControl("SubTeritory") as DropDownList;
        MarketSelect = (DropDownList)IVMarketStructure.FindControl("MarketSelect") as DropDownList;
        if (!IsPostBack)
        {
            InvoiceDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropDown();
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    public void LoadDropDown()
    {
        try
        {
            OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
            aOtherStockActionBLL.DCLoad(dcDropDownList1);
        }
        catch { }
    }
    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData();
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaReport.aspx");
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        LoadData();

        //Session["ProformaReport"] = "";
        //Session["ProformaReport"] = 0;

        //if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "" && dcDropDownList1.SelectedValue != "")
        //{
        //    if (todateTextBox.Text == "")
        //    {
        //        InvoiceDateTextBox.Text = todateTextBox.Text;
        //    }

        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string districtId = dcDropDownList1.SelectedValue;

        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&districtId=" + districtId;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
        //if (CheckBox1.Checked && todateTextBox.Text != "" && InvoiceDateTextBox.Text != "")
        //{
        //    int i = 1;
        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&NationalReport=" + 1;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
    }

    private void LoadData()
    {
        Stopwatch stopwatch = new Stopwatch(); // Create a Stopwatch instance
        stopwatch.Start(); // Start measuring time
        DataTable comUnitDetailDataTable = new DataTable();


        comUnitDetailDataTable = _DAL.GetProformaInvoListDAL(Parm());


        if (comUnitDetailDataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = comUnitDetailDataTable;
            loadGridView.DataBind(); // Ensure GridView is bound before using HeaderRow

            // 🔹 Store Only the Number of Rows in HiddenField (NOT the full JSON)
            HiddenFieldData.Value = comUnitDetailDataTable.Rows.Count.ToString();

            // 🔹 Store the FULL dataset in Session (Avoids Memory Issue)
            Session["ProformaInvoiceData"] = comUnitDetailDataTable;
        }
        else
        {
            HiddenFieldData.Value = "0";
            Session["ProformaInvoiceData"] = null;
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }

        stopwatch.Stop(); // Stop measuring time

        // Log or display the execution time
        TimeSpan elapsedTime = stopwatch.Elapsed;
        string executionTimeMessage = "Execution Time: {elapsedTime.TotalMilliseconds} ms";
        // You can log this message or display it as needed
        System.Diagnostics.Debug.WriteLine(executionTimeMessage); // Example: Write to Debug output
    }
    // ✅ Converts DataTable to JSON (For Storing in HiddenField)
 

private string DataTableToJson(DataTable table, GridView gridView)
{
    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

    // 🔹 Include GridView Headers First
    if (gridView.HeaderRow != null)
    {
        Dictionary<string, object> headerRow = new Dictionary<string, object>();
        foreach (DataControlField column in gridView.Columns)
        {
            headerRow[column.HeaderText] = column.HeaderText;
        }
        rows.Add(headerRow);
    }

    // 🔹 Include Data Rows
    foreach (DataRow dr in table.Rows)
    {
        Dictionary<string, object> row = new Dictionary<string, object>();

        foreach (DataColumn col in table.Columns)
        {
            string columnHeader = GetGridViewHeader(gridView, col.ColumnName);
            row[columnHeader] = dr[col];
        }
        rows.Add(row);
    }

    // ✅ Use `Newtonsoft.Json` for Efficient Serialization
    return JsonConvert.SerializeObject(rows, Formatting.None);
}


// ✅ Function to get GridView HeaderText using DataField
private string GetGridViewHeader(GridView gridView, string dataField)
    {
        foreach (DataControlField column in gridView.Columns)
        {
            if (column is BoundField && ((BoundField)column).DataField == dataField)
            {
                return column.HeaderText; // Return GridView Header instead of SQL Column Name
            }
        }
        return dataField; // Default to SQL column name if no match
    }


    private string Parm()
    {
        
        string param = "";
        
        if (dcDropDownList1.SelectedValue != "")
        {
            param = param + " AND SLS.ComUnitId='" + dcDropDownList1.SelectedValue + "' ";
        }

        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,SLS.InvoiceDate)  BETWEEN  CONVERT(date,'" + InvoiceDateTextBox.Text + "') AND CONVERT(date,'" + todateTextBox.Text + "')";
        }
        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,SLS.InvoiceDate)  BETWEEN CONVERT(date,'" + InvoiceDateTextBox.Text + "') AND CONVERT(date,'" + DateTime.Today + "')";
        }

        if (GroupSelect.SelectedValue != "")
        {
            param = param + " AND SLS.GroupId='" + GroupSelect.SelectedValue + "' ";
        }

        if (ZoneSelect.SelectedValue != "")
        {
            param = param + " AND SLS.RegionId='" + ZoneSelect.SelectedValue + "' ";
        }

        if (AreaSelect.SelectedValue != "")
        {
            param = param + " AND SLS.AreaId='" + AreaSelect.SelectedValue + "' ";
        }

        if (TeritorySelect.SelectedValue != "")
        {
            param = param + " AND SLS.TerritoryId='" + TeritorySelect.SelectedValue + "' ";
        }

        if (SubTeritory.SelectedValue != "")
        {
            param = param + " AND SLS.SubTerritoryId='" + SubTeritory.SelectedValue + "' ";
        }

        if (MarketSelect.SelectedValue != "")
        {
            param = param + " AND SLS.MarketId='" + MarketSelect.SelectedValue + "' ";
        }


        return param;
    }
    
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
        aOtherStockActionBLL.DCLoad(dcDropDownList1);
         
    }
    private List<Dictionary<string, object>> JsonToDataTablesss(string jsonData)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Deserialize<List<Dictionary<string, object>>>(jsonData);
    }
    // ✅ Converts JSON back to DataTable
    private DataTable JsonToDataTable(string jsonData)
    {
        DataTable dt = new DataTable();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = serializer.Deserialize<List<Dictionary<string, object>>>(jsonData);

        if (rows.Count > 0)
        {
            foreach (var column in rows[0].Keys)
            {
                dt.Columns.Add(column);
            }

            foreach (var row in rows)
            {
                DataRow dr = dt.NewRow();
                foreach (var key in row.Keys)
                {
                    dr[key] = row[key];
                }
                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Stopwatch stopwatch = new Stopwatch(); // Create a Stopwatch instance
        stopwatch.Start(); // Start measuring time

        DataTable dt = Session["ProformaInvoiceData"] as DataTable;

        if (dt != null && dt.Rows.Count > 0)
        {
            // ✅ Set EPPlus License Context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Invoice Report");

                // Write Headers
                int rowIndex = 1, colIndex = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    worksheet.Cells[rowIndex, colIndex].Value = column.ColumnName;
                    colIndex++;
                }

                // Write Data
                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    colIndex = 1;
                    foreach (var value in row.ItemArray)
                    {
                        worksheet.Cells[rowIndex, colIndex].Value = value;
                        colIndex++;
                    }
                }

                // Send the Excel file as a response
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Invoice_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    excelPackage.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                }
            }

            Response.Flush();
            Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "alert('No Data Found!');", true);
        }



        stopwatch.Stop(); // Stop measuring time

        // Log or display the execution time
        TimeSpan elapsedTime = stopwatch.Elapsed;
        string executionTimeMessage = "Execution Time: {elapsedTime.TotalMilliseconds} ms";
        // You can log this message or display it as needed
        System.Diagnostics.Debug.WriteLine(executionTimeMessage); // Example: Write to Debug output

        //if (loadGridView.Rows.Count > 0)
        //{
        //    DataTable dt = new DataTable("GridView_Data");
        //    foreach (TableCell cell in loadGridView.HeaderRow.Cells)
        //    {
        //        dt.Columns.Add(cell.Text);
        //    }
        //    loadGridView.AllowPaging = false;
        //    this.LoadData();
        //    foreach (GridViewRow row in loadGridView.Rows)
        //    {
        //        dt.Rows.Add();
        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {
        //            if (row.Cells[i].Controls.Count > 0)
        //            {
        //                dt.Rows[dt.Rows.Count - 1][i] = (row.Cells[i].Controls[1] as Label).Text;
        //            }
        //            else
        //            {
        //                dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
        //            }
        //        }
        //    }
        //    loadGridView.AllowPaging = false;
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report_List (Date Range= " + InvoiceDateTextBox.Text + "-" + todateTextBox.Text + ").xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}

        //if (loadGridView.Rows.Count > 0)
        //{


        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice_Report_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls"));
        //    Response.ContentType = "application/ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    loadGridView.AllowPaging = false;

        //    this.LoadData();
        //    //Change the Header Row back to white color
        //    loadGridView.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //    //Applying stlye to gridview header cells
        //    //for (int i = 0; i < loadGridView.HeaderRow.Cells.Count; i++)
        //    //{
        //    //    loadGridView.HeaderRow.Cells[i].Style.Add("background-color", "#8BA8E0");
        //    //}
        //    //int j = 1;
        //    ////This loop is used to apply stlye to cells based on particular row
        //    //foreach (GridViewRow gvrow in loadGridView.Rows)
        //    //{
        //    //    gvrow.BackColor = Color.White;
        //    //    if (j <= loadGridView.Rows.Count)
        //    //    {
        //    //        if (j % 2 != 0)
        //    //        {
        //    //            for (int k = 0; k < gvrow.Cells.Count; k++)
        //    //            {
        //    //                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
        //    //            }
        //    //        }
        //    //    }
        //    //    j++;
        //    //}

        //    string headerTable = @"<span  style='text-align:center'><h3>  Invoice Report   (Date Range : " + InvoiceDateTextBox.Text + "- " + todateTextBox.Text + ") </h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("MMMM dd, yyyy") + "</h4></span>";

        //    HttpContext.Current.Response.Write(headerTable);

        //    loadGridView.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //else
        //{
        //    showMessageBox("No Data Found!!");
        //}
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

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }
    protected void viewRptButton_Click(object sender, EventArgs e)
    {
        Session["ProformaReport"] = "";
        Session["ProformaReport"] = 1;

        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "" && dcDropDownList1.SelectedValue != "")
        {
            if (todateTextBox.Text == "")
            {
                InvoiceDateTextBox.Text = todateTextBox.Text;
            }

            string fromDate = InvoiceDateTextBox.Text;
            string toDate = todateTextBox.Text;
            string districtId = dcDropDownList1.SelectedValue;

            string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&districtId=" + districtId;
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        //if (CheckBox1.Checked && todateTextBox.Text != "" && InvoiceDateTextBox.Text != "")
        //{
        //    int i = 1;
        //    string fromDate = InvoiceDateTextBox.Text;
        //    string toDate = todateTextBox.Text;
        //    string url = "../SInventory_RPTVIEW/ProformaReportViewer.aspx?fromDate=" + fromDate + "&toDate=" + toDate + "&NationalReport=" + 1;
        //    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //    string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        //}
    }

    protected void fromDateTextBox_TextChanged(object sender, EventArgs e)
    {
        DateTime Fromd = Convert.ToDateTime("01-Apr-2022");
        DateTime inputDateTime = Convert.ToDateTime(InvoiceDateTextBox.Text);
        if (inputDateTime < Fromd)
        {
            InvoiceDateTextBox.Text = DateTime.Now.ToString("01 April, 2022");
        }
    }
}