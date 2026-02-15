using Library.DAL.DoctorModule_DAL;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_RouteInformationEntry : System.Web.UI.Page
{
    private RouteInformationDAL _Dal = new RouteInformationDAL();
    private CommonDataLoad _dataLoad = new CommonDataLoad();
    private static DepotWiseAreaSetupDal _aRepo = new DepotWiseAreaSetupDal();

    private int mid = 0;
    private string _userId;

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
            LoadDropdownList();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                btnUpdate.Visible = true;

                id_mastetID.Value = Request.QueryString["MID"];
                GetOneRecord(id_mastetID.Value);
            }
            else
            {
                btnSave.Visible = true;
            }
        }
    }

    public void Market_gv_Initial()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("GroupId");
        aDataTable.Columns.Add("RegionId");
        aDataTable.Columns.Add("AreaId");
        aDataTable.Columns.Add("TerritoryId");
        aDataTable.Columns.Add("SubTerritoryId");
        aDataTable.Columns.Add("MarketId");

        aDataTable.Columns.Add("GroupName");
        aDataTable.Columns.Add("RegionName");
        aDataTable.Columns.Add("AreaName");
        aDataTable.Columns.Add("TerritoryName");
        aDataTable.Columns.Add("SubTerritoryName");
        aDataTable.Columns.Add("MarketName");
        gv_Market.DataSource = aDataTable;
        gv_Market.DataBind();

    }
    private void GetOneRecord(string Id)
    {
        try
        {
            using (DataTable dt = _Dal.GetRouteInformationMasterById(Id))
            {
                ddlDepotName.Text = dt.Rows[0]["DCId"].ToString();

                try
                {
                    chkIsSubDepo.Checked = Convert.ToBoolean(dt.Rows[0]["IsSubDepo"].ToString());
                }
                catch(Exception ex)
                {
                    chkIsSubDepo.Checked = false;
                }

                txtRouteName.Text = dt.Rows[0]["RouteName"].ToString();
                txtTotalDistance.Text = dt.Rows[0]["TotalDistance"].ToString();
                txtTotalDay.Text = dt.Rows[0]["TotalDay"].ToString();

                txtTAAmount.Text = dt.Rows[0]["TAAmount"].ToString();
                txtDAAmount.Text = dt.Rows[0]["DAAmount"].ToString();

                ddlRouteType.SelectedValue= dt.Rows[0]["RouteTypeId"].ToString();


                string[] degree = dt.Rows[0]["BrandId"].ToString().Split(',');

                foreach (ListItem item in ddlRouteDay.Items)
                {
                    for (int i = 0; i < degree.Length; i++)
                    {
                        if (item.Value == degree[i].ToString())
                        {
                            item.Selected = true;

                        }
                    }
                }

            }


            using (DataTable dtDetail = _Dal.GeteRouteInformationDA_DetailById(Id))
            {
                gv_DA.DataSource = dtDetail;
                gv_DA.DataBind();

            }


            using (DataTable dtDetail = _Dal.GetRouteInformationDetailMarketById(Id))
            {
                gv_Market.DataSource = dtDetail;
                gv_Market.DataBind();

            }
        }
        catch (Exception ex) { }
    }
    //private void LoadInitialGrid()
    //{
    //    DataTable aDataTable = new DataTable();
    //    aDataTable.Columns.Add("DANameId");
    //    aDataTable.Columns.Add("DAName");
    //    DataRow row = null;

    //    row = aDataTable.NewRow();

    //    row["DANameId"] = "";
    //    row["DAName"] = "";

    //    aDataTable.Rows.Add(row);

    //    gv_DA.DataSource = aDataTable;
    //    gv_DA.DataBind();

    //  //  Remove(0);
    //}

    private void LoadDropdownList()
    {

        try
        {
            using (DataTable dt33 = _aRepo.GetDepotList(1))
            {
                ddlDepotName.DataSource = dt33;
                ddlDepotName.DataValueField = "ComUnitId";
                ddlDepotName.DataTextField = "UnitName";
                ddlDepotName.DataBind();
                ddlDepotName.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlDepotName.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }
        try
        {
            using (DataTable dt = _Dal.GetDANameList())
            {
                ddlDAName.DataSource = dt;
                ddlDAName.DataValueField = "Value";
                ddlDAName.DataTextField = "TextField";
                ddlDAName.DataBind();
                ddlDAName.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlDAName.SelectedIndex = 0;
            }
        }
        catch (Exception ex) { }

        try
        {
            using (DataTable dt = _Dal.GetWeekNameList())
            {
                ddlRouteDay.DataSource = dt;
                ddlRouteDay.DataValueField = "Value";
                ddlRouteDay.DataTextField = "TextField";
                ddlRouteDay.DataBind();
                ddlRouteDay.Items.Insert(-1, "");
                ddlRouteDay.SelectedIndex = 0;
            }
        }
        catch (Exception ex) { }

        try
        {
            using (DataTable dt = _Dal.GetRouteTypeInfoList())
            {
                ddlRouteType.DataSource = dt;
                ddlRouteType.DataValueField = "Value";
                ddlRouteType.DataTextField = "TextField";
                ddlRouteType.DataBind();
                ddlRouteType.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlRouteType.SelectedIndex = 0;
            }
        }
        catch (Exception ex) { }

        Market_gv_Initial();
        DA_gv_Initial();
    }

    private void DA_gv_Initial()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("DANameId");
        aDataTable.Columns.Add("DAName");
        gv_DA.DataSource = aDataTable;
        gv_DA.DataBind();
    }

    protected void addButtonDA_Click(object sender, EventArgs e)
    {

        ddlDAName.CssClass = "form-select form-select-sm mb-3 mySelect2";

        if (ddlDAName.SelectedValue != "")
        {
            Add();

        }
        else
        {
            ddlDAName.ToolTip = "please fill out this field";
            ddlDAName.CssClass = "form-select form-select-sm mb-3 mySelect2 is-invalid";
            ddlDAName.Focus();

        }

        
    }


    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void Add()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("DANameId");
        aDataTable.Columns.Add("DAName");


        DataRow dataRow = null;
        for (int i = 0; i < gv_DA.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["DAName"] = gv_DA.Rows[i].Cells[1].Text;
            dataRow["DANameId"] = gv_DA.DataKeys[i][0].ToString();


            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["DAName"] = ddlDAName.SelectedItem.Text;
        dataRow["DANameId"] = ddlDAName.SelectedValue;


        aDataTable.Rows.Add(dataRow);
        gv_DA.DataSource = aDataTable;
        gv_DA.DataBind();
        ddlDAName.SelectedValue = string.Empty;

    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("DANameId");
        aDataTable.Columns.Add("DAName");

        DataRow dataRow = null;
        for (int i = 0; i < gv_DA.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["DAName"] = gv_DA.Rows[i].Cells[1].Text;
                dataRow["DANameId"] = gv_DA.DataKeys[i][0].ToString();
                aDataTable.Rows.Add(dataRow);
            }
        }
        gv_DA.DataSource = aDataTable;
        gv_DA.DataBind();

    }




    public void AddMarket()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("GroupId");
        aDataTable.Columns.Add("RegionId");
        aDataTable.Columns.Add("AreaId");
        aDataTable.Columns.Add("TerritoryId");
        aDataTable.Columns.Add("SubTerritoryId");
        aDataTable.Columns.Add("MarketId");

        aDataTable.Columns.Add("GroupName");
        aDataTable.Columns.Add("RegionName");
        aDataTable.Columns.Add("AreaName");
        aDataTable.Columns.Add("TerritoryName");
        aDataTable.Columns.Add("SubTerritoryName");
        aDataTable.Columns.Add("MarketName");




        DataRow dataRow = null;
        for (int i = 0; i < gv_Market.Rows.Count; i++)
        {




            dataRow = aDataTable.NewRow();


            HiddenField hfGroupId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfGroupId"));
            HiddenField hfRegionId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfRegionId"));
            HiddenField hfAreaId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfAreaId"));
            HiddenField hfTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfTerritoryId"));

            HiddenField hfSubTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfSubTerritoryId"));

            HiddenField hfMarketId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfMarketId"));


            Label lbl_GroupName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_GroupName"));

            Label lbl_RegionName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_RegionName"));
            Label lbl_AreaName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_AreaName"));
            Label lbl_TerritoryName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_TerritoryName"));
            Label lbl_SubTerritoryName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_SubTerritoryName"));
            Label lbl_MarketName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_MarketName"));


            dataRow["GroupId"] = hfGroupId.Value;
            dataRow["RegionId"] = hfRegionId.Value;
            dataRow["AreaId"] = hfAreaId.Value;
            dataRow["TerritoryId"] = hfTerritoryId.Value;
            dataRow["SubTerritoryId"] = hfSubTerritoryId.Value;
            dataRow["MarketId"] = hfMarketId.Value;

            dataRow["GroupName"] = lbl_GroupName.Text;
            dataRow["RegionName"] = lbl_RegionName.Text;
            dataRow["AreaName"] = lbl_AreaName.Text;
            dataRow["TerritoryName"] = lbl_TerritoryName.Text;
            dataRow["SubTerritoryName"] = lbl_SubTerritoryName.Text;
            dataRow["MarketName"] = lbl_MarketName.Text;



            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["GroupId"] = GroupSelect.SelectedIndex > 0 ? int.Parse(GroupSelect.SelectedValue) : (int?)null;
        dataRow["RegionId"] = ZoneSelect.SelectedIndex > 0 ? int.Parse(ZoneSelect.SelectedValue) : (int?)null;
        dataRow["AreaId"] = AreaSelect.SelectedIndex > 0 ? int.Parse(AreaSelect.SelectedValue) : (int?)null;
        dataRow["TerritoryId"] = TeritorySelect.SelectedIndex > 0 ? int.Parse(TeritorySelect.SelectedValue) : (int?)null;
        dataRow["SubTerritoryId"] = SubTeritory.SelectedIndex > 0 ? int.Parse(SubTeritory.SelectedValue) : (int?)null;
        dataRow["MarketId"] = MarketSelect.SelectedIndex > 0 ? int.Parse(MarketSelect.SelectedValue) : (int?)null;


        dataRow["GroupName"] = GroupSelect.SelectedIndex > 0 ? GroupSelect.SelectedItem.Text : null;
        dataRow["RegionName"] = ZoneSelect.SelectedIndex > 0 ? ZoneSelect.SelectedItem.Text : null;


        dataRow["AreaName"] = AreaSelect.SelectedIndex > 0 ? AreaSelect.SelectedItem.Text : null;
        dataRow["TerritoryName"] = TeritorySelect.SelectedIndex > 0 ? TeritorySelect.SelectedItem.Text : null;
        dataRow["SubTerritoryName"] = SubTeritory.SelectedIndex > 0 ? SubTeritory.SelectedItem.Text : null;
        dataRow["MarketName"] = MarketSelect.SelectedIndex > 0 ? MarketSelect.SelectedItem.Text : null;




        aDataTable.Rows.Add(dataRow);
        gv_Market.DataSource = aDataTable;
        gv_Market.DataBind();
        //GroupSelect.SelectedValue = string.Empty;
        //ZoneSelect.Items.Clear();
        //AreaSelect.Items.Clear();
        //TeritorySelect.Items.Clear();
        //SubTeritory.Items.Clear();
        //MarketSelect.Items.Clear();


    }

    public void RemoveMarket(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("GroupId");
        aDataTable.Columns.Add("RegionId");
        aDataTable.Columns.Add("AreaId");
        aDataTable.Columns.Add("TerritoryId");
        aDataTable.Columns.Add("SubTerritoryId");
        aDataTable.Columns.Add("MarketId");

        aDataTable.Columns.Add("GroupName");
        aDataTable.Columns.Add("RegionName");
        aDataTable.Columns.Add("AreaName");
        aDataTable.Columns.Add("TerritoryName");
        aDataTable.Columns.Add("SubTerritoryName");
        aDataTable.Columns.Add("MarketName");

        DataRow dataRow = null;
        for (int i = 0; i < gv_Market.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                HiddenField hfGroupId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfGroupId"));
                HiddenField hfRegionId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfRegionId"));
                HiddenField hfAreaId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfAreaId"));
                HiddenField hfTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfTerritoryId"));

                HiddenField hfSubTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfSubTerritoryId"));

                HiddenField hfMarketId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfMarketId"));


                Label lbl_GroupName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_GroupName"));

                Label lbl_RegionName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_RegionName"));
                Label lbl_AreaName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_AreaName"));
                Label lbl_TerritoryName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_TerritoryName"));
                Label lbl_SubTerritoryName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_SubTerritoryName"));
                Label lbl_MarketName = ((Label)gv_Market.Rows[i].Cells[1].FindControl("lbl_MarketName"));


                dataRow["GroupId"] = hfGroupId.Value;
                dataRow["RegionId"] = hfRegionId.Value;
                dataRow["AreaId"] = hfAreaId.Value;
                dataRow["TerritoryId"] = hfTerritoryId.Value;
                dataRow["SubTerritoryId"] = hfSubTerritoryId.Value;
                dataRow["MarketId"] = hfMarketId.Value;

                dataRow["GroupName"] = lbl_GroupName.Text;
                dataRow["RegionName"] = lbl_RegionName.Text;
                dataRow["AreaName"] = lbl_AreaName.Text;
                dataRow["TerritoryName"] = lbl_TerritoryName.Text;
                dataRow["SubTerritoryName"] = lbl_SubTerritoryName.Text;
                dataRow["MarketName"] = lbl_MarketName.Text;

                aDataTable.Rows.Add(dataRow);
            }
        }
        gv_Market.DataSource = aDataTable;
        gv_Market.DataBind();

    }
    protected void btnAddtoListMarket_Click(object sender, EventArgs e)
    {
        MarketSelect.CssClass = "form-select form-select-sm mb-3 mySelect2";

        if (MarketSelect.SelectedValue != "")
        {
            if (MarketValidation(Convert.ToInt32(MarketSelect.SelectedValue)))
            {
                AddMarket();

            }


        }
        else
        {
            MarketSelect.ToolTip = "please fill out this field";
            MarketSelect.CssClass = "form-select form-select-sm mb-3 mySelect2 is-invalid";
            MarketSelect.Focus();

        }
    }

    protected void deleteImageButton_Click(object sender, EventArgs e)
    {
        LinkButton ImageButton = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Remove(rowindex);
    }

    protected void MarketdeleteImageButton_Click(object sender, EventArgs e)
    {
        LinkButton ImageButton = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        RemoveMarket(rowindex);
    }

    public bool Validation()
    {


        txtRouteName.CssClass = "form-control form-control-sm";
        ddlDepotName.CssClass = "form-control form-control-sm mySelect2";
        ddlRouteType.CssClass = "form-control form-control-sm mySelect2";
        ddlRouteDay.CssClass = "form-select form-select-sm mb-3 multiple-select";

        txtTotalDistance.CssClass = "form-control form-control-sm";
        txtTAAmount.CssClass = "form-control form-control-sm";
        txtDAAmount.CssClass = "form-control form-control-sm";
        addButtonDA.CssClass = "btn btn-sm btn-success";
        btnAddtoListMarket.CssClass = "btn btn-sm btn-success";

        txtTotalDay.CssClass = "form-control form-control-sm";

        if (ddlDepotName.SelectedValue == "")
        {
            ddlDepotName.ToolTip = "please fill out this field";
            ddlDepotName.CssClass = "form-select form-select-sm mb-3 mySelect2 is-invalid";
            ddlDepotName.Focus();
            return false;
        }

        if (txtRouteName.Text == "")
        {
            txtRouteName.ToolTip = "please fill out this field";
            txtRouteName.CssClass = "form-control form-control-sm is-invalid";
            txtRouteName.Focus();
            return false;
        }


        if (gv_DA.Rows.Count ==0)
        {
            showMessageBox("Please Add to List DA Name!");
            addButtonDA.ToolTip = "please fill out this field";
            addButtonDA.CssClass = "btn btn-sm btn-success is-invalid";
            addButtonDA.Focus();
            return false;
        }

        if (gv_Market.Rows.Count == 0)
        {
            showMessageBox("Please Add to List Market!");
            btnAddtoListMarket.ToolTip = "please fill out this field";
            btnAddtoListMarket.CssClass = "btn btn-sm btn-success is-invalid";
            btnAddtoListMarket.Focus();
            return false;
        }


        if (txtTotalDistance.Text == "")
        {
            txtTotalDistance.ToolTip = "please fill out this field";
            txtTotalDistance.CssClass = "form-control form-control-sm is-invalid";
            txtTotalDistance.Focus();
            return false;
        }


        if (txtTotalDay.Text == "")
        {
            txtTotalDay.ToolTip = "please fill out this field";
            txtTotalDay.CssClass = "form-control form-control-sm is-invalid";
            txtTotalDay.Focus();
            return false;
        }

        if (ddlRouteType.SelectedValue == "")
        {
            ddlRouteType.ToolTip = "please fill out this field";
            ddlRouteType.CssClass = "form-select form-select-sm mb-3 mySelect2 is-invalid";
            ddlRouteType.Focus();
            return false;
        }

        if (txtTAAmount.Text == "")
        {
            txtTAAmount.ToolTip = "please fill out this field";
            txtTAAmount.CssClass = "form-control form-control-sm is-invalid";
            txtTAAmount.Focus();
            return false;
        }

        if (txtDAAmount.Text == "")
        {
            txtDAAmount.ToolTip = "please fill out this field";
            txtDAAmount.CssClass = "form-control form-control-sm is-invalid";
            txtDAAmount.Focus();
            return false;
        }


        if (ddlRouteDay.SelectedValue == "")
        {
            ddlRouteDay.ToolTip = "please fill out this field";
            ddlRouteDay.CssClass = "form-select form-select-sm mb-3 multiple-select is-invalid";
            ddlRouteDay.Focus();
            return false;
        }


        return true;
    }


    public bool MarketValidation(int MarketId)
    {


        MarketSelect.CssClass = "form-select form-select-sm mb-3 mySelect2";


        for (int i = 0; i < gv_Market.Rows.Count; i++)
        {
            HiddenField hfMarketId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfMarketId"));

            int? markId = string.IsNullOrEmpty(hfMarketId.Value) ? (int?)null : int.Parse(hfMarketId.Value);

            if(markId== MarketId)
            {
                showMessageBox("This Market is already exist in list!");
                MarketSelect.ToolTip = "please fill out this field";
                MarketSelect.CssClass = "form-select form-select-sm mb-3 mySelect2 is-invalid";
                MarketSelect.Focus();
                return false;
            }

          
        }


        

        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            RouteInformationMasterDAO aMaster = new RouteInformationMasterDAO();

            List<BonusCampaignMarketDetailDAO> MarketList = new List<BonusCampaignMarketDetailDAO>();

            string Market = "";

            for (int i = 0; i < gv_Market.Rows.Count; i++)
            {
                HiddenField hfGroupId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfGroupId"));
                HiddenField hfRegionId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfRegionId"));
                HiddenField hfAreaId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfAreaId"));
                HiddenField hfTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfTerritoryId"));

                HiddenField hfSubTerritoryId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfSubTerritoryId"));

                HiddenField hfMarketId = ((HiddenField)gv_Market.Rows[i].Cells[1].FindControl("hfMarketId"));




                BonusCampaignMarketDetailDAO _DAO = new BonusCampaignMarketDetailDAO();

                _DAO.GroupId = string.IsNullOrEmpty(hfGroupId.Value) ? (int?)null : int.Parse(hfGroupId.Value);

                _DAO.RegionId = string.IsNullOrEmpty(hfRegionId.Value) ? (int?)null : int.Parse(hfRegionId.Value);
                _DAO.AreaId = string.IsNullOrEmpty(hfAreaId.Value) ? (int?)null : int.Parse(hfAreaId.Value);
                _DAO.TerritoryId = string.IsNullOrEmpty(hfTerritoryId.Value) ? (int?)null : int.Parse(hfTerritoryId.Value);
                _DAO.SubTerritoryId = string.IsNullOrEmpty(hfSubTerritoryId.Value) ? (int?)null : int.Parse(hfSubTerritoryId.Value);
                _DAO.MarketId = string.IsNullOrEmpty(hfMarketId.Value) ? (int?)null : int.Parse(hfMarketId.Value);


                Market = Market + _DAO.MarketId+",";





                MarketList.Add(_DAO);

            }
            aMaster.MarketIdStr = Market.Trim(',');
            List<RouteInformationDADetailDAO> DtlList = new List<RouteInformationDADetailDAO>();


            for (int i = 0; i < gv_DA.Rows.Count; i++)
            {
                HiddenField hfDANameId = (HiddenField)gv_DA.Rows[i].FindControl("hfDANameId");





                RouteInformationDADetailDAO _DAO = new RouteInformationDADetailDAO();

                _DAO.DAId = string.IsNullOrEmpty(hfDANameId.Value) ? (int?)null : int.Parse(hfDANameId.Value);

 



                DtlList.Add(_DAO);

            }



            aMaster.RouteInformationMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.DCId = ddlDepotName.SelectedIndex > 0 ? int.Parse(ddlDepotName.SelectedValue) : (int?)null;
            aMaster.IsSubDepo = chkIsSubDepo.Checked;

            aMaster.RouteName = string.IsNullOrEmpty(txtRouteName.Text) ? null : txtRouteName.Text;
            

            aMaster.TotalDistance = string.IsNullOrEmpty(txtTotalDistance.Text) ? (decimal?)null : decimal.Parse(txtTotalDistance.Text);
            aMaster.TotalDay = string.IsNullOrEmpty(txtTotalDay.Text) ? (decimal?)null : decimal.Parse(txtTotalDay.Text);


            aMaster.RouteTypeId = ddlRouteType.SelectedIndex > 0 ? int.Parse(ddlRouteType.SelectedValue) : (int?)null;

            aMaster.TAAmount = string.IsNullOrEmpty(txtTAAmount.Text) ? (decimal?)null : decimal.Parse(txtTAAmount.Text);
            aMaster.DAAmount = string.IsNullOrEmpty(txtDAAmount.Text) ? (decimal?)null : decimal.Parse(txtDAAmount.Text);


            string RouteDayArray = "";

            foreach (ListItem item in ddlRouteDay.Items)
            {
                if (item.Selected)
                {

                    RouteDayArray = RouteDayArray + item.Value + ",";
                }
            }

            RouteDayArray = RouteDayArray.TrimEnd(',');
            ResultInfo Res = _Dal.SaveRouteInformation(aMaster, DtlList, MarketList, RouteDayArray, Session["UserId"].ToString());
            if (Res.isSuccess == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','RouteInformationList.aspx');", true);

            }

            if (Res.isDuplicateCheck == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Market Already Exist in Another Route!" + "','Faild');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
}