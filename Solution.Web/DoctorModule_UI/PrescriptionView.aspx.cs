using Library.DAL.DoctorModule_DAL;
using Library.DAL.SInventory_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoctorModule_UI_PrescriptionView : System.Web.UI.Page
{
    public static SetupDAL _setupDAL = new SetupDAL();
    static SeedDataDAL _seedRepo = new SeedDataDAL();
    static CommonDataLoad _dataLoad = new CommonDataLoad();

    static SetupDAL _setupDAL2 = new SetupDAL();
    CommonStructureDal aStructureDal = new CommonStructureDal();

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
                FromDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
                ToDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
                LoadInitialInfo();
                LoadDropdownList();

            LoadData();
        }
        }
        catch (Exception ex)
        {
        }
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
            using (DataTable aTable = aStructureDal.LoadSalesLine())
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

        //try
        //{
        //    using (DataTable aTable = aStructureDal.LoadBrand())
        //    {
        //        ddlBr.DataSource = aTable;
        //        ddlCluster.DataValueField = "ValueField";
        //        ddlCluster.DataTextField = "TextField";
        //        ddlCluster.DataBind();
        //        ddlCluster.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
        //        ddlCluster.SelectedIndex = 0;
        //    }
        //}
        //catch { }
    }


    private void LoadData()
    {
        DataTable aDataTable = _setupDAL.Get_PrescriptionList2(param(), param2());
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {

            Image imgShow = ((Image)loadGridView.Rows[i].Cells[1].FindControl("imgShow"));
            HyperLink hpImg = ((HyperLink)loadGridView.Rows[i].Cells[1].FindControl("hpImg"));

            try
            {
                string imagefullpath = aDataTable.Rows[i]["ImagePreName"].ToString();


                try
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@imagefullpath);
                    var src = "data:image/jpeg;base64,";

                    imgShow.ImageUrl = src + Convert.ToBase64String(imageArray);

                    hpImg.NavigateUrl = src + Convert.ToBase64String(imageArray);
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private string param2()
    {
        var param = " ";

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,PrescriptionDate)  BETWEEN '" + FromDate.Text + "' AND '" +
                    ToDate.Text + "' ";
        }
        if (fromDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,PrescriptionDate)  BETWEEN '" + FromDate.Text + "' AND '" +
                    DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        return param;
    }

    private string param()
    {


        var param = " and  PM.PrescriptionId IS NOT NULL";

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,PM.PrescriptionDate)  BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ";
        }
        if (fromDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,PM.PrescriptionDate)  BETWEEN '" + FromDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ddlCluster.SelectedValue != "")
        {
            param = param + " AND PM.RegionId ='" + ddlCluster.SelectedValue + "' ";
        }

        if (ddlRegion.SelectedValue != "")
        {
            param = param + " AND PM.AreaId ='" + ddlRegion.SelectedValue + "' ";
        }

        if (ddlArea.SelectedValue != "")
        {
            param = param + " AND PM.TerritoryId ='" + ddlArea.SelectedValue + "' ";
        }

        if (ddlTerritory.SelectedValue != "")
        {
            param = param + " AND PM.SubTerritoryId ='" + ddlTerritory.SelectedValue + "' ";
        }

        if (ddlSalesLine.SelectedValue != "")
        {
            param = param + " AND PM.SalesLine ='" + ddlSalesLine.SelectedItem.Text.Trim() + "' ";
        }

        //if (ddl.SelectedValue != "")
        //{
        //    param = param + " AND PM.SalesLine ='" + ddlSalesLine.SelectedItem.Text.Trim() + "' ";
        //}



        if (ApprovalStatusSelect.SelectedValue != "")
        {

            param = param + " AND PM.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "'";


        }

        if (UserRoleSelect.SelectedValue != "")
        {

            param = param + " AND us.UserRoleID='" + UserRoleSelect.SelectedValue + "'";

        }

        if (EmployeeIdSelect.SelectedValue != "")
        {

            param = param + " AND em.EmpInfoId='" + EmployeeIdSelect.SelectedValue + "'";

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

    private void LoadInitialInfo()
    {


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

    [WebMethod]
    public static string Get_PrescriptionList(string param)
    {
 
        DataTable dt = _setupDAL.Get_PrescriptionList(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }

    


    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Prescription.aspx");

    }
    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrescriptionView.aspx");
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

            Response.Redirect("Prescription.aspx?id=" + unitPriceId);
        }

    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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
                ShowMessageBox("Please select cluster !!!");
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
                ShowMessageBox("Please select region !!!");
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
                ShowMessageBox("Please select team !!!");
            }


        }
        catch { }
    }
}