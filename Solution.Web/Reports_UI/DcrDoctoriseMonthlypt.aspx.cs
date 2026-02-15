using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.MasterSetup_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;

public partial class Reports_UI_DcrDoctoriseMonthlypt : System.Web.UI.Page
{
    static SeedDataDAL _seedRepo = new SeedDataDAL();
    static Setup2DAL _setupDAL = new Setup2DAL();
    static SetupDAL _setupDAL2 = new SetupDAL();
    private static CmnCrystaltoView _DAL = new CmnCrystaltoView();

    static CommonDataLoad _dataLoad = new CommonDataLoad();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialInfo();

          //  LoadData();
        }
    }
    public void GetYearList(DropDownList ddl)
    {


        int i;

        for (i = 2015; i <= 2050; i++)
        {
            ddl.Items.Add(i.ToString());
            ddl.Items.FindByValue(System.DateTime.Now.Year.ToString());
        }
        string strYear = System.DateTime.Now.Year.ToString();

        ddl.SelectedValue = strYear;


    }
    public void GetMonthList(DropDownList ddl)
    {
        DateTime month = Convert.ToDateTime(DateTime.Now);
        for (int i = 0; i < 12; i++)
        {
            DateTime NextMont = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = NextMont.ToString("MMMM");
            list.Value = NextMont.Month.ToString();
            ddl.Items.Add(list);
        }
        //ddl.Items.Insert(0, "Select Month");
        ddl.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
    }
    private void LoadInitialInfo()
    {
        try
        {
            GetMonthList(ddlmonth);
            GetYearList(ddlYear);
        }

        catch (Exception ex) { }

        try
        {
            using (DataTable dt = _dataLoad.GetEmployeeList_Active())
            {
                EmployeeIdSelect.DataSource = dt;
                EmployeeIdSelect.DataValueField = "EmpInfoId";
                EmployeeIdSelect.DataTextField = "EmpName";
                EmployeeIdSelect.DataBind();
                EmployeeIdSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                EmployeeIdSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }

       


    }
        private void LoadData()
    {
        string Type = "";
        if (rbType.Items[0].Selected)
        {
            Type = "DCR";
        }
        else
        {
            Type = "RX";
        }

            data.InnerHtml = "";


        if (rbReportTypeName.Items[0].Selected)
        {
            DataTable aDataTable = _DAL.GetDoctorWiseDayList(Type, ddlmonth.SelectedValue, ddlYear.SelectedItem.Text);
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
        }


        if (rbReportTypeName.Items[1].Selected)
        {

            DataTable dtDate = _DAL.GetDatefromMonthYear(Type, ddlmonth.SelectedValue, ddlYear.SelectedValue);


            string mainDate = "";
            foreach (DataRow row in dtDate.Rows)
            {
                mainDate = mainDate + row["mainDate"].ToString() + ',';
            }
            string mainDate_ = mainDate.TrimEnd(',');
            DataTable aDataTable = _DAL.GetDoctorBrandWiseList(mainDate_, ddlmonth.SelectedValue, ddlYear.SelectedValue);
            string html = "<table id='tblTable' style='height:200px;' class='table table-bordered text-center thead-dark table-hover table-striped tableFixHead' >";
            html = html + "<thead> " + "<tr>";

            //+"<tr>" +
            //             "<th colspan='5' colspan='8'  style='background-color: #F5F5F5!important;border:None!important'>  </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #4DDEC1!important'> Input </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #64ADFF!important'> Output </th>" +
            //             "</tr><tr>";
            for (int i = 0; i < aDataTable.Columns.Count; i++)
            {

                html = html + "<th>" + aDataTable.Columns[i].ColumnName + "</th>";
            }

            html = html + "<th>" + "Total" + "</th>";

            html = html + "</tr></thead>";

            html = html + "<tbody>";

            for (int i = 0; i < aDataTable.Rows.Count; i++)
            {
                html = html + "<tr>";
                int Total = 0;
                for (int j = 0; j < aDataTable.Columns.Count; j++)
                {

                    try
                    {
                        Total = Total + Convert.ToInt32(aDataTable.Rows[i][j].ToString());
                    }
                    catch { }

                    html = html + "<td>" + aDataTable.Rows[i][j].ToString() + "</td>";
                }
                html = html + "<td>" + Total + "</td>";
                html = html + "</tr>";
            }


            html = html + "</tbody>";



            html = html + "</table>";
            data.InnerHtml = html;
        }

        if (rbReportTypeName.Items[2].Selected)
        {

            DataTable dtDate = _DAL.GetDatefromMonthYear(Type, ddlmonth.SelectedValue, ddlYear.SelectedValue);


            string mainDate = "";
            foreach (DataRow row in dtDate.Rows)
            {
                mainDate = mainDate+ row["mainDate"].ToString()+',';
            }
          string  mainDate_ = mainDate.TrimEnd(',');
            DataTable aDataTable = _DAL.GetDoctorProductWiseList(mainDate_, ddlmonth.SelectedValue, ddlYear.SelectedValue);
            string html = "<table id='tblTable' style='height:200px;' class='table table-bordered text-center thead-dark table-hover table-striped tableFixHead' >";
            html = html + "<thead> " + "<tr>";

            //+"<tr>" +
            //             "<th colspan='5' colspan='8'  style='background-color: #F5F5F5!important;border:None!important'>  </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #4DDEC1!important'> Input </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #64ADFF!important'> Output </th>" +
            //             "</tr><tr>";
            for (int i = 0; i < aDataTable.Columns.Count; i++)
            {
               
                html = html + "<th>" + aDataTable.Columns[i].ColumnName + "</th>";
            }

            html = html + "<th>" + "Total" + "</th>";

            html = html + "</tr></thead>";

            html = html + "<tbody>";

            for (int i = 0; i < aDataTable.Rows.Count; i++)
            {
                html = html + "<tr>";
                int Total = 0;
                for (int j = 0; j < aDataTable.Columns.Count; j++)
                {

                    try
                    {
                        Total = Total + Convert.ToInt32(aDataTable.Rows[i][j].ToString());
                    }
                    catch { }

                    html = html + "<td>" + aDataTable.Rows[i][j].ToString() + "</td>";
                }
                html = html + "<td>" + Total + "</td>";
                html = html + "</tr>";
            }


            html = html + "</tbody>";
 
 

            html = html + "</table>";
            data.InnerHtml = html;
        }



        if (rbReportTypeName.Items[3].Selected)
        {

            DataTable dtDate = _DAL.GetDatefromMonthYear(Type, ddlmonth.SelectedValue, ddlYear.SelectedValue);


            string mainDate = "";
            foreach (DataRow row in dtDate.Rows)
            {
                mainDate = mainDate + row["mainDate"].ToString() + ',';
            }
            string mainDate_ = mainDate.TrimEnd(',');
            DataTable aDataTable = _DAL.GetDCRUserWiseList(mainDate_, ddlmonth.SelectedValue, ddlYear.SelectedValue);
            string html = "<table id='tblTable' style='height:200px;' class='table table-bordered text-center thead-dark table-hover table-striped tableFixHead' >";
            html = html + "<thead> " + "<tr>";

            //+"<tr>" +
            //             "<th colspan='5' colspan='8'  style='background-color: #F5F5F5!important;border:None!important'>  </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #4DDEC1!important'> Input </th>" +
            //             "<th colspan='3'  style='color: white;background-color: #64ADFF!important'> Output </th>" +
            //             "</tr><tr>";
            for (int i = 0; i < aDataTable.Columns.Count; i++)
            {

                html = html + "<th>" + aDataTable.Columns[i].ColumnName + "</th>";
            }

            html = html + "<th>" + "Total" + "</th>";

            html = html + "</tr></thead>";

            html = html + "<tbody>";

            for (int i = 0; i < aDataTable.Rows.Count; i++)
            {
                html = html + "<tr>";
                int Total = 0;
                for (int j = 0; j < aDataTable.Columns.Count; j++)
                {

                    try
                    {
                        Total = Total + Convert.ToInt32(aDataTable.Rows[i][j].ToString());
                    }
                    catch { }

                    html = html + "<td>" + aDataTable.Rows[i][j].ToString() + "</td>";
                }
                html = html + "<td>" + Total + "</td>";
                html = html + "</tr>";
            }


            html = html + "</tbody>";



            html = html + "</table>";
            data.InnerHtml = html;
        }





    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        //if (loadGridView.Rows.Count > 0)
        //{
        //    string Type = "";
        //    if (rbType.Items[0].Selected)
        //    {
        //        Type = "DCR";
        //    }
        //    else
        //    {
        //        Type = "RX";
        //    }

        //    string attachment = "attachment; filename="+ Type + "_Doctor_Wise_" + DateTime.Now.ToLongDateString()+".xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    loadGridView.AllowPaging = false;



        //    //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
        //    //            false;
        //    //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
        //    //   false;
        //    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
        //    //   false;

         
        //    // Create a form to contain the grid  
        //    HtmlForm frm = new HtmlForm();
        //    loadGridView.Parent.Controls.Add(frm);
        //    //frm.Attributes["runat"] = "server";
        //    //frm.Controls.Add(loadGridView);
        //    //frm.RenderControl(htw);

        //    loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

        //    // Set background color of each cell of GridView1 header row
        //    foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
        //    {
        //        tableCell.Style["background-color"] = "#E5EEF1";
        //    }

        //    // Set background color of each cell of each data row of GridView1
        //    foreach (GridViewRow gridViewRow in loadGridView.Rows)
        //    {
        //        gridViewRow.BackColor = System.Drawing.Color.White;

        //        foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
        //        {
        //            gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

        //        }
        //    }

        //    loadGridView.RenderControl(htw);
        //    string headerTable = @"<span  style='text-align:left'><h3> "+ Type + " Doctor Wise List of Month: " + ddlmonth.SelectedItem.Text +
        //                         ", Year: "+ddlYear.SelectedValue+"</h3>  ";



        //    HttpContext.Current.Response.Write(headerTable);

        //    string style = @"<style> .text { mso-number-format:\@; } </style> ";
        //    Response.Write(style);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //else
        //{
        //    showMessageBox("No Data Found!!");
        //}
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private string param()
    {
         

        var param = "  ";

        //if (FromDate.Text != "" &&  ToDate.Text != "") {
        //    param = param + " AND CONVERT(date,mas.EntryDate)  BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ";
        //}
        //if ( FromDate.Text != "" && ToDate.Text == "") {
        //    param = param + " AND CONVERT(date,mas.EntryDate)  BETWEEN '" + FromDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        //}

      

        if (EmployeeIdSelect.SelectedValue != "" ) {

            param = param + " AND mas.EmpInfoId='" + EmployeeIdSelect.SelectedValue + "'";

        }


        return param;
    }

    [WebMethod]
    public static string GetMileageClaimList(string param)
    {
        DataTable dt = _setupDAL.GetMileageClaimList(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;

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
        //if (e.CommandName == "EditData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();

        //    Response.Redirect("../DoctorModule_UI/MileageClaim.aspx?id=" + unitPriceId);
        //}

    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
         Response.Redirect("EmpMonthlyExpenseRpt.aspx");
    }

    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadGridView.DataSource = null;
        //loadGridView.DataBind();
    }

    protected void rbReportTypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGridView.DataSource = null;
        loadGridView.DataBind();
        data.InnerHtml = "";
    }
}