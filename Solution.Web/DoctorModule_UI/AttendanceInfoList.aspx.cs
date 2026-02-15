using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.SInventory_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;

public partial class DoctorModule_UI_AttendanceInfoList : System.Web.UI.Page
{
    static CommonDataLoad _dataLoad = new CommonDataLoad();

    private static AttendanceDAL _AttendanceDAL = new AttendanceDAL();
    CommonStructureDal aStructureDal = new CommonStructureDal();
    static SeedDataDAL _seedRepo = new SeedDataDAL();
    static Setup2DAL _setupDAL = new Setup2DAL();
    static SetupDAL _setupDAL2 = new SetupDAL();

    string RoleTypeName = "";
    string EmpInfoId = "";
    string ToRoleTypeId = "";
    string ApprovalStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            RoleTypeName = Session["RoleTypeName"].ToString();
            EmpInfoId = Session["EmpInfoId"].ToString();
            ToRoleTypeId = Session["RoleTypeId"].ToString();

            if (!IsPostBack)
            {
                fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
                todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
                LoadDropdownList();
                LoadInitialInfo();

                LoadData();
            }

        }
        catch (Exception ex)
        {
        }
        }
    [WebMethod]
    public static string Emp_AttendanceInfoList(string param)
    {
        DataTable dt = _AttendanceDAL.Get_Emp_AttendanceInfoList(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }

    private void LoadDropdownList()
    {
        try
        {
            using (DataTable aTable = aStructureDal.LoadClusterMeansRegion())
            {
                ddlCluster.DataSource = aTable;
                ddlCluster.DataValueField = "ValueField";
                ddlCluster.DataTextField = "TextField";
                ddlCluster.DataBind();
                ddlCluster.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlCluster.SelectedIndex = 0;
            }
        }
        catch { }

        try
        {
            using (DataTable aTable = aStructureDal.LoadGroup())
            {
                ddlSalesLine.DataSource = aTable;
                ddlSalesLine.DataValueField = "ValueField";
                ddlSalesLine.DataTextField = "TextField";
                ddlSalesLine.DataBind();
                ddlSalesLine.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlSalesLine.SelectedIndex = 0;
            }
        }
        catch { }

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

    protected void ddlCluster_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlCluster.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadRegionMeansAreaByClusterId(Convert.ToInt32(ddlCluster.SelectedValue)))
                {
                    ddlRegion.DataSource = aTable;
                    ddlRegion.DataValueField = "ValueField";
                    ddlRegion.DataTextField = "TextField";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlRegion.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select cluster !!!");
            }


        }
        catch { }
    }

    protected void ddlRegion_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlRegion.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadAreaMeansTerritoryByRegionId(Convert.ToInt32(ddlRegion.SelectedValue)))
                {
                    ddlArea.DataSource = aTable;
                    ddlArea.DataValueField = "ValueField";
                    ddlArea.DataTextField = "TextField";
                    ddlArea.DataBind();
                    ddlArea.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlArea.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select region !!!");
            }


        }
        catch { }
    }

    protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlArea.SelectedValue != "")
            {
                using (DataTable aTable = aStructureDal.LoadTerritoryMeansSubTerritoryByAreaId(Convert.ToInt32(ddlArea.SelectedValue)))
                {
                    ddlTerritory.DataSource = aTable;
                    ddlTerritory.DataValueField = "ValueField";
                    ddlTerritory.DataTextField = "TextField";
                    ddlTerritory.DataBind();
                    ddlTerritory.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlTerritory.SelectedIndex = 0;
                }
            }
            else
            {
                showMessageBox("Please select team !!!");
            }


        }
        catch { }
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData();
    }


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Attendance_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            GridView1.AllowPaging = false;
            this.LoadData();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in GridView1.Rows)
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

            Response.Redirect("../DoctorModule_UI/ExpenseClaim.aspx?id=" + unitPriceId);
        }

    }


    private void LoadInitialInfo()
    {


        

        try
        {
            using (DataTable dt = _setupDAL.Get_UserRoleInfo())
            {
                UserRoleSelect.DataSource = dt;
                UserRoleSelect.DataValueField = "UserRoleID";
                UserRoleSelect.DataTextField = "RoleName";
                UserRoleSelect.DataBind();
                UserRoleSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                UserRoleSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }


        try
        {
            using (DataTable dt = _seedRepo.GetApprovalStatusList())
            {
                ApprovalStatusSelect.DataSource = dt;
                ApprovalStatusSelect.DataValueField = "SoftwareUseId";
                ApprovalStatusSelect.DataTextField = "WebShow";
                ApprovalStatusSelect.DataBind();
                ApprovalStatusSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ApprovalStatusSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }


    }
    private void LoadData()
    {



        DataTable aDataTable = _AttendanceDAL.Get_Emp_AttendanceInfoList(param());
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        GridView1.DataSource = aDataTable;
        GridView1.DataBind();


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }


    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = " ";
            HeaderCell.BackColor = Color.FromName("#F5F5F5");
            HeaderCell.BorderColor = Color.FromName("#F5F5F5");

            HeaderCell.ColumnSpan = 0;
            HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = " ";
            //HeaderCell.BackColor = Color.FromName("#F5F5F5");
            //HeaderCell.BorderColor = Color.FromName("#F5F5F5");


            //HeaderCell.ColumnSpan = 1;

            //HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Invoice";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.BackColor = Color.DeepSkyBlue;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Return";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = Color.Red;
            HeaderGridRow.Cells.Add(HeaderCell);

             



            loadGridView.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    private string param()
    {
        var param = "  ";

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,att1.AttendanceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        if (fromDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,att1.AttendanceDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ddlCluster.SelectedValue != "")
        {
            param = param + " AND CLS.RegionId ='" + ddlCluster.SelectedValue + "' ";
        }

        if (ddlRegion.SelectedValue != "")
        {
            param = param + " AND RGN.AreaId ='" + ddlRegion.SelectedValue + "' ";
        }

        if (ddlArea.SelectedValue != "")
        {
            param = param + " AND ARA.TerritoryId ='" + ddlArea.SelectedValue + "' ";
        }

        if (ddlTerritory.SelectedValue != "")
        {
            param = param + " AND TTR.SubTerritoryId ='" + ddlTerritory.SelectedValue + "' ";
        }
        
        if (ddlSalesLine.SelectedValue != "")
        {
            param = param + " AND att1.SalesLine ='" + ddlSalesLine.SelectedItem.Text.Trim() + "' ";
        }


        if (ApprovalStatusSelect.SelectedValue != "")
        {

            param = param + " AND att1.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "'";


        }

        if (UserRoleSelect.SelectedValue != "")
        {

            param = param + " AND  att1.UserRoleID='" + UserRoleSelect.SelectedValue + "'";

        }

        if (EmployeeIdSelect.SelectedValue != "")
        {

            param = param + " AND att1.EmpInfoId ='" + EmployeeIdSelect.SelectedValue + "'";

        }


        string Role = "";
        DataTable dtMarket = _dataLoad.GetEmpMarketStructure_Active(EmpInfoId);
         
        string FFID = "";
        switch (RoleTypeName)
        {



            case "MIO":
                FFID = dtMarket.Rows[0]["MIOEmpId"].ToString();
                param = param + " AND View_Webapi_EmployeeFieldForceInfo.MIOEmpId=" + FFID;
                Role = "AM";

                break;

            case "AM":
                FFID = dtMarket.Rows[0]["ASMEMPId"].ToString();
                param = param + " AND View_Webapi_EmployeeFieldForceInfo.ASMEMPId=" + FFID;
                Role = "AM";

                break;
            case "DZSM":
                FFID = dtMarket.Rows[0]["RSMEMPId"].ToString();
                param = param + " AND  View_Webapi_EmployeeFieldForceInfo.RSMEMPId=" + FFID;
                Role = "DZSM";
                break;
            case "NSM":
                FFID = dtMarket.Rows[0]["NSMEMPId"].ToString();
                param = param + " AND  View_Webapi_EmployeeFieldForceInfo.NSMEMPId=" + FFID;
                Role = "NSM";
                break;

           
            default:
                
                Role = "";
                break;
        }

        return param;
    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("AttendanceInfoList.aspx");
    }


    protected void PageSize_Changed(object sender, EventArgs e)
    {
        LoadData();
    }
}