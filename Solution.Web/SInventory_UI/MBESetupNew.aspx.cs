using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.ReportAppServer.CommonObjectModel;
using Library.DAL;
using System.Data;
using Library.DAO.DoctorModule_DAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using ResultInfo = SalesSolution.Web.Models.ResultInfo;

public partial class SInventory_UI_MBESetupNew : System.Web.UI.Page
{

    MBESetupDal aDal = new MBESetupDal();
    CommonDataLoad aCommonDataLoad = new CommonDataLoad();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //LoadDropdownList();

            if (Session["MBEId"] != null)
            {
                LoadEditDropdownList();

                GetOneRecord(Convert.ToInt32(Session["MBEId"].ToString()));
                Session["MBEId"] = null;
            }
            else
            {
                LoadDropdownList();
            }

        }

    }


    #region Edit



    private void GetOneRecord(int mbeId)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetEMBEditDataById(mbeId);

        if (aTable.Rows.Count > 0)
        {
            submitButton.Text = "Update";

            //submitButton.BackColor = Color.DodgerBlue;

            Int32 rowIndex = 0;

            mioIdHiddenField.Value = aTable.Rows[0].Field<Int32>("MBEInfoId").ToString();

            ddlGroup.SelectedValue = aTable.Rows[0].Field<Int32>("GroupId").ToString();

            // Zone

            LoadZoneEdit(ddlZone, aTable.Rows[0].Field<Int32>("GroupId"));
            ddlZone.SelectedValue = aTable.Rows[0].Field<Int32>("RegionId").ToString();

            // Area

            LoadAreaEdit(ddlArea, aTable.Rows[0].Field<Int32>("RegionId"));
            ddlArea.SelectedValue = aTable.Rows[0].Field<Int32>("AreaId").ToString();

            // Territory

            LoadTerritoryEdit(ddlTerritory, aTable.Rows[0].Field<Int32>("AreaId"));
            ddlTerritory.SelectedValue = aTable.Rows[0].Field<Int32>("TerritoryId").ToString();

            // Territory

            LoadSubTerritoryEdit(ddlSubTerritory, aTable.Rows[0].Field<Int32>("TerritoryId"));
            ddlSubTerritory.SelectedValue = aTable.Rows[0].Field<Int32>("SubTerritoryId").ToString();

            // Employee
            LoadMbeEmployeeEdit(ddlMbe, aTable.Rows[0].Field<Int32>("MBEInfoId"));
            ddlMbe.SelectedValue = aTable.Rows[0].Field<Int32>("EmployeeId").ToString();

            if (aTable.Rows[0].Field<bool>("IsActive"))
            {
                cbxIsActive.Checked = true;
            }
            else
            {
                cbxIsActive.Checked = false;
            }

            tbxActiveDate.Text = aTable.Rows[0].Field<string>("ActiveDateStr");

        }
    }



    private void LoadEditDropdownList()
    {

        LoadGroupEdit(ddlGroup);
    }


    // Group
    private void LoadGroupEdit(DropDownList ddl)
    {
        DataTable aDataTable = aCommonDataLoad.GetGroupInfo_All();

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "GroupName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "GroupId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    // Zone

    private void LoadZoneEdit(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aCommonDataLoad.GetZone_byGroupId_All(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "RegionName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "RegionId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Area

    private void LoadAreaEdit(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aCommonDataLoad.GetArea_ByZoneId_All(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "AreaName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "AreaId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Territory

    private void LoadTerritoryEdit(DropDownList ddl, int areaId)
    {
        DataTable aDataTable = aCommonDataLoad.GetTerritory_ByAreaId_All(areaId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "TerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "TerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    // Subterritory

    private void LoadSubTerritoryEdit(DropDownList ddl, int territoryId)
    {
        DataTable aDataTable = aCommonDataLoad.GetSubTerritory_ByTerritoryId_Active(territoryId);

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "SubTerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "SubTerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    
    #endregion



   

    private void LoadDropdownList()
    {

        LoadGroup(ddlGroup);
        LoadMbeEmployee(ddlMbe);

    }

    private void LoadMbeEmployee(DropDownList ddl)
    {
        DataTable aDataTable = aDal.GetEmployee_AllFieldForceEmployeeList();

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "EmployeeName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "EmpInfoId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }
    }

    private void LoadGroup(DropDownList ddl)
    {
        DataTable aDataTable = aDal.GetGroupInfo_Active();

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "GroupName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "GroupId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    // Group wise Zone

    protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        ddlZone.Items.Clear();

        if (ddlGroup.SelectedValue != "0")
        {
            LoadZone(ddlZone, Convert.ToInt32(ddlGroup.SelectedValue));
        }
    }


    // Employee
    private void LoadMbeEmployeeEdit(DropDownList ddl, int mbeId)
    {
        DataTable aDataTable = aCommonDataLoad.GetMIOEmployee_All(mbeId);

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "EmployeeName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "EmpInfoId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }
    }



    private void LoadZone(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aDal.GetZone_byGroupId_Active(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "RegionName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "RegionId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Zone wise Area

    protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        ddlArea.Items.Clear();

        if (ddlZone.SelectedValue != "0")
        {
            LoadArea(ddlArea, Convert.ToInt32(ddlZone.SelectedValue));
        }
    }


    private void LoadArea(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aDal.GetArea_ByZoneId_Active(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "AreaName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "AreaId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Area wise Terittory

    protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTerritory.Items.Clear();

        if (ddlArea.SelectedValue != "0")
        {
            LoadTerritory(ddlTerritory, Convert.ToInt32(ddlArea.SelectedValue));
        }
    }


    private void LoadTerritory(DropDownList ddl, int areaId)
    {
        DataTable aDataTable = aDal.Get_VacentTerritory(areaId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "TerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "TerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Territory Wise Subterritory

    protected void ddlTerritory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubTerritory.Items.Clear();

        if (ddlTerritory.SelectedValue != "0")
        {
            LoadSubTerritory(ddlSubTerritory, Convert.ToInt32(ddlTerritory.SelectedValue));
        }
    }


    private void LoadSubTerritory(DropDownList ddl, int territoryId)
    {
        DataTable aDataTable = aDal.GetSubTerritory_ByTerritoryId_Active(territoryId);

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "SubTerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "SubTerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    protected void buttonListPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("MBESetupNewView.aspx");
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            var aInformationDao = new MBEInfoDao();


            aInformationDao.MBEInfoId = string.IsNullOrEmpty(mioIdHiddenField.Value) ? 0 : Convert.ToInt32(mioIdHiddenField.Value);
            aInformationDao.SubTerritoryId = Convert.ToInt32(ddlSubTerritory.SelectedValue);
            aInformationDao.EmployeeId = Convert.ToInt32(ddlMbe.SelectedValue);

            //aInformationDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());

            aInformationDao.IsActive = cbxIsActive.Checked ? true : false;
            aInformationDao.ActiveDate = Convert.ToDateTime(tbxActiveDate.Text.Trim());


            ResultInfo aInfo = aDal.Save_MBEInfo(aInformationDao,
                Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString()));

            if (aInfo.isSuccess)
            {
                ShowMessageBox("Operation Successful !!!");
            }
            else
            {
                ShowMessageBox("Operation Failed !!!");
            }

            Response.Redirect("MBESetupNew.aspx");
        }

       
    }


    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }




    private bool Validation()
    {

     

        if (ddlSubTerritory.SelectedValue == "0")
        {
            ShowMessageBox("Please Select a territory!!");
            return false;
        }


        if (ddlMbe.SelectedValue == "0")
        {
            ShowMessageBox("Please Select a MBE Name!!");
            return false;
        }


        if (tbxActiveDate.Text == "")
        {
            ShowMessageBox("Active Date is required!!");
            return false;
        }

        return true;
    }





    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MBESetupNew.aspx");
    }


   
}