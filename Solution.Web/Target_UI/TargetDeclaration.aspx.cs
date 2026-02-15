using Library.DAL.TargetDAL;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Target_UI_TargetDeclaration : System.Web.UI.Page
{
    private TargetSchemaDAL _tDal = new TargetSchemaDAL();
    private CommonDataLoad _dataLoad = new CommonDataLoad();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadDropDown();

            if (hfGroupId.Value == "")
            {

                try
                {
                    using (DataTable dt = _dataLoad.GetGroupInfo_All())
                    {
                        GroupSelect.DataSource = dt;
                        GroupSelect.DataValueField = "GroupId";
                        GroupSelect.DataTextField = "GroupName";
                        GroupSelect.DataBind();
                        GroupSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        GroupSelect.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {

                }
                // showMessageBox(hfGroupId.Value);
                GroupSelect.SelectedValue = hfGroupId.Value;


                try
                {

                    using (DataTable dt = _dataLoad.GetZone_byGroupId_All(Convert.ToInt32(GroupSelect.SelectedValue)))
                    {
                        ZoneSelect.DataSource = dt;
                        ZoneSelect.DataValueField = "RegionId";
                        ZoneSelect.DataTextField = "RegionName";
                        ZoneSelect.DataBind();
                        ZoneSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        ZoneSelect.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {

                }


                ZoneSelect.SelectedValue = hfZone.Value;


                try
                {

                    using (DataTable dt = _dataLoad.GetArea_ByZoneId_All(Convert.ToInt32(ZoneSelect.SelectedValue)))
                    {
                        AreaSelect.DataSource = dt;
                        AreaSelect.DataValueField = "AreaId";
                        AreaSelect.DataTextField = "AreaName";
                        AreaSelect.DataBind();
                        AreaSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        AreaSelect.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {

                }

                AreaSelect.SelectedValue = hfArea.Value;


                try
                {

                    using (DataTable dt = _dataLoad.GetTerritory_ByAreaId_All(Convert.ToInt32(AreaSelect.SelectedValue)))
                    {
                        TeritorySelect.DataSource = dt;
                        TeritorySelect.DataValueField = "TerritoryId";
                        TeritorySelect.DataTextField = "TerritoryName";
                        TeritorySelect.DataBind();
                        TeritorySelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        TeritorySelect.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {

                }
                TeritorySelect.SelectedValue = hfTeritory.Value;


                try
                {

                    using (DataTable dt = _dataLoad.GetSubTerritory_ByTerritoryId_Alle(Convert.ToInt32(TeritorySelect.SelectedValue)))
                    {
                        SubTeritory.DataSource = dt;
                        SubTeritory.DataValueField = "SubTerritoryId";
                        SubTeritory.DataTextField = "SubTerritoryName";
                        SubTeritory.DataBind();
                        SubTeritory.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        SubTeritory.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {

                }
                SubTeritory.SelectedValue = hfSubTeritory.Value;

            }



            if (!string.IsNullOrEmpty(Request.QueryString["TargetDeclarationId"]))
            {
                btnUpdate.Visible = true;

                var TargetDeclarationId = Request.QueryString["TargetDeclarationId"];
                GetOneRecord(TargetDeclarationId);
            }
            else
            {
                //btnSave.Visible = true;
            }
        }
    }

    public void LoadDropDown()
    {
        LoadYears();
        LoadMonths();
        _tDal.GetSchemaDropDown(schemaDropdown);
    }

    private void LoadYears()
    {
        int startYear = 2020;
        int endYear = DateTime.Now.Year + 5;

        ddlYear.Items.Clear();

        for (int year = startYear; year <= endYear; year++)
        {
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
    }

    private void LoadMonths()
    {
        ddlMonth.Items.Clear();

        for (int month = 1; month <= 12; month++)
        {
            string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, monthName)); // Use month name as value
        }
    }



    public void GetOneRecord(string Id)
    {
        hfSchemaId.Value = Id;

        try
        {
            DataTable dt = _tDal.GetTargetDeclarationDataById(Id);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                string schemaId = row["SchemaMasterId"].ToString();
                string year = row["Year"].ToString();
                string monthName = row["MonthName"].ToString();

                // ✅ Set Schema Dropdown (schemaDropdown)
                schemaDropdown.ClearSelection();
                ListItem schemaItem = schemaDropdown.Items.FindByValue(schemaId);
                if (schemaItem != null)
                    schemaItem.Selected = true;

                // ✅ Set Year Dropdown (ddlYear)
                ddlYear.ClearSelection();
                ListItem yearItem = ddlYear.Items.FindByValue(year);
                if (yearItem != null)
                    yearItem.Selected = true;

                // ✅ Set Month ListBox (ddlMonth - multiselect)
                ddlMonth.ClearSelection();
                foreach (ListItem item in ddlMonth.Items)
                {
                    if (item.Text.Equals(monthName, StringComparison.OrdinalIgnoreCase))
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error loading record: " + ex.Message);
        }
    }


    protected void GroupSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            using (DataTable dt = _dataLoad.GetZone_byGroupId_Rpt(Convert.ToInt32(GroupSelect.SelectedValue)))
            {
                ZoneSelect.DataSource = dt;
                ZoneSelect.DataValueField = "RegionId";
                ZoneSelect.DataTextField = "RegionName";
                ZoneSelect.DataBind();
                ZoneSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ZoneSelect.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }


        AreaSelect.Items.Clear();
        TeritorySelect.Items.Clear();
        SubTeritory.Items.Clear();
    }



    protected void ZoneSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            using (DataTable dt = _dataLoad.GetArea_ByZoneId_Rpt(Convert.ToInt32(ZoneSelect.SelectedValue)))
            {
                AreaSelect.DataSource = dt;
                AreaSelect.DataValueField = "AreaId";
                AreaSelect.DataTextField = "AreaName";
                AreaSelect.DataBind();
                AreaSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                AreaSelect.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }


        TeritorySelect.Items.Clear();
        SubTeritory.Items.Clear();
    }




    protected void AreaSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            using (DataTable dt = _dataLoad.GetTerritory_ByAreaId_Rpt(Convert.ToInt32(AreaSelect.SelectedValue)))
            {
                TeritorySelect.DataSource = dt;
                TeritorySelect.DataValueField = "TerritoryId";
                TeritorySelect.DataTextField = "TerritoryName";
                TeritorySelect.DataBind();
                TeritorySelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                TeritorySelect.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }



        SubTeritory.Items.Clear();
    }

    protected void TeritorySelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            using (DataTable dt = _dataLoad.GetSubTerritory_ByTerritoryId_Rpt(Convert.ToInt32(TeritorySelect.SelectedValue)))
            {
                SubTeritory.DataSource = dt;
                SubTeritory.DataValueField = "SubTerritoryId";
                SubTeritory.DataTextField = "SubTerritoryName";
                SubTeritory.DataBind();
                SubTeritory.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                SubTeritory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public bool Validation()
    {


        if (ddlMonth.GetSelectedIndices().Length == 0)
        {
            ddlMonth.ToolTip = "Please Select At Least One Month!";
            ddlMonth.Focus();
            return false;
        }

        if (ddlYear.SelectedValue == "")
        {
            ddlYear.ToolTip = "Please Select Year!";
            ddlYear.Focus();
            return false;
        }


        if (schemaDropdown.SelectedValue == "")
        {
            schemaDropdown.ToolTip = "Please Select Schema!";
            schemaDropdown.Focus();
            return false;
        }

        if (SubTeritory.SelectedValue == "")
        {
            SubTeritory.ToolTip = "Please Select Territory!";
            SubTeritory.Focus();
            return false;
        }

        return true;
    }



    protected void SaveData(object sender, EventArgs e)
    {

        if (Validation())
        {
            string val = hfSchemaId.Value;
            string TargetDeclarationId = " ";
            if (!string.IsNullOrEmpty(hfSchemaId.Value))
            {
                TargetDeclarationId = hfSchemaId.Value;
            }
            else
            {
                TargetDeclarationId = "0";
            }
            string selectedSchemaId = schemaDropdown.SelectedValue;
            string selectedYear = ddlYear.SelectedValue;
            string subTerritoryId = SubTeritory.SelectedValue;

            foreach (ListItem monthItem in ddlMonth.Items)
            {
                if (monthItem.Selected)
                {
                    string selectedMonth = monthItem.Value;

                    bool result = false;

                    ResultInfo Res = _tDal.SaveTargetDeclarationData(Convert.ToInt32(TargetDeclarationId), Convert.ToInt32(selectedSchemaId), Convert.ToInt32(subTerritoryId), Convert.ToInt32(selectedYear), selectedMonth, Session["UserId"].ToString());
                    if (Res.isSuccess == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','TargetDeclarationView.aspx');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

                    }
                }
            }

        }

    }
}