using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_PrescriptionReports : System.Web.UI.Page
{
    CommonStructureDal aStructureDal = new CommonStructureDal();
    public static SetupDAL _setupDAL = new SetupDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropdownList();
        }
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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
            using (DataTable aTable = aStructureDal.LoadBrand())
            {
                ddlBrand.DataSource = aTable;
                ddlBrand.DataValueField = "ValueField";
                ddlBrand.DataTextField = "TextField";
                ddlBrand.DataBind();
                ddlBrand.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlBrand.SelectedIndex = 0;
            }
        }
        catch { }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private string GenerateParameter()
    {

        var param = " and  PM.PrescriptionId IS NOT NULL";

        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,PM.EntryDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        if (fromDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,PM.EntryDate)  BETWEEN '" + fromDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
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

        if (ddlBrand.SelectedValue != "")
        {
            param = param + " AND SQ.ProductBrandId ='" + ddlBrand.SelectedValue + "' ";
        }

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


        //string Role = "";
        //DataTable dtMarket = _dataLoad.GetEmpMarketStructure_Active(EmpInfoId);

        //string FFID = "";
        //switch (RoleTypeName)
        //{



        //    case "MIO":
        //        FFID = dtMarket.Rows[0]["MIOEmpId"].ToString();
        //        param = param + " AND View_Webapi_EmployeeFieldForceInfo.MIOEmpId=" + FFID;
        //        Role = "AM";

        //        break;

        //    case "AM":
        //        FFID = dtMarket.Rows[0]["ASMEMPId"].ToString();
        //        param = param + " AND View_Webapi_EmployeeFieldForceInfo.ASMEMPId=" + FFID;
        //        Role = "AM";

        //        break;
        //    case "DZSM":
        //        FFID = dtMarket.Rows[0]["RSMEMPId"].ToString();
        //        param = param + " AND  View_Webapi_EmployeeFieldForceInfo.RSMEMPId=" + FFID;
        //        Role = "DZSM";
        //        break;
        //    case "NSM":
        //        FFID = dtMarket.Rows[0]["NSMEMPId"].ToString();
        //        param = param + " AND  View_Webapi_EmployeeFieldForceInfo.NSMEMPId=" + FFID;
        //        Role = "NSM";
        //        break;


        //    default:

        //        Role = "";
        //        break;
        //}


        return param;
    }

    private void LoadData()
    {
        if (fromDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            DataTable aDataTable = _setupDAL.Get_PrescriptionDetailList(GenerateParameter());
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();

            //for (int i = 0; i < loadGridView.Rows.Count; i++)
            //{

            //    Image imgShow = ((Image) loadGridView.Rows[i].Cells[1].FindControl("imgShow"));
            //    HyperLink hpImg = ((HyperLink) loadGridView.Rows[i].Cells[1].FindControl("hpImg"));

            //    try
            //    {
            //        string imagefullpath = aDataTable.Rows[i]["ImagePreName"].ToString();


            //        try
            //        {
            //            byte[] imageArray = System.IO.File.ReadAllBytes(@imagefullpath);
            //            var src = "data:image/jpeg;base64,";

            //            imgShow.ImageUrl = src + Convert.ToBase64String(imageArray);

            //            hpImg.NavigateUrl = src + Convert.ToBase64String(imageArray);
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
        }
        else
        {
            ShowMessageBox("Please select data range !!!");
        }
    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}