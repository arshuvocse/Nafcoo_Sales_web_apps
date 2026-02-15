using Library.DAL.MasterSetup_DAL;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_CustomerChangeProgramType : System.Web.UI.Page
{


    private static SeedDataDAL _seedRepo = new SeedDataDAL();

    private static CustomerInfoDAL _DAL = new CustomerInfoDAL();
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
            frmDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            toDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");

            try
            {
                using (DataTable dt = _seedRepo.GetStationTypeList())
                {
                    ddlStationType.DataSource = dt;
                    ddlStationType.DataValueField = "StationTypeId";
                    ddlStationType.DataTextField = "StationTypeName";
                    ddlStationType.DataBind();
                    ddlStationType.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlStationType.SelectedIndex = 0;
                }


            }
            catch (Exception ex) { }

            try
            {
                using (DataTable dt = _seedRepo.GetApprovalStatusList())
                {
                    ddlApprovalStatus.DataSource = dt;
                    ddlApprovalStatus.DataValueField = "SoftwareUseId";
                    ddlApprovalStatus.DataTextField = "WebShow";
                    ddlApprovalStatus.DataBind();
                    ddlApprovalStatus.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlApprovalStatus.SelectedIndex = 0;
                }


            }
            catch (Exception ex) { }

            //try
            //{
            //    using (DataTable dt = _seedRepo.GetProgramTypeList())
            //    {
            //        ddlProgramType.DataSource = dt;
            //        ddlProgramType.DataValueField = "ProgramTypeId";
            //        ddlProgramType.DataTextField = "ProgramTypeName";
            //        ddlProgramType.DataBind();
            //        ddlProgramType.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
            //        ddlProgramType.SelectedIndex = 0;
            //    }


            //}
            //catch (Exception ex) { }


            try
            {
                using (DataTable dt = _seedRepo.GetProgramTypeWithoutGeneralList())
                {
                    ddlProgramType.DataSource = dt;
                    ddlProgramType.DataValueField = "ProgramTypeId";
                    ddlProgramType.DataTextField = "ProgramTypeName";
                    ddlProgramType.DataBind();
                    ddlProgramType.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlProgramType.SelectedIndex = 0;
                }


            }
            catch (Exception ex) { }

            try
            {
                using (DataTable dt = _seedRepo.GetDistributionCenterDataTableList())
                {
                    ddlDistributionCenter.DataSource = dt;
                    ddlDistributionCenter.DataValueField = "ComUnitId";
                    ddlDistributionCenter.DataTextField = "ComUnitName";
                    ddlDistributionCenter.DataBind();
                    ddlDistributionCenter.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlDistributionCenter.SelectedIndex = 0;

                    if (Session["RoleTypeName"].ToString() == "DIC")
                    {
                        ddlDistributionCenter.SelectedValue = Session["DICCompanyUnitId"].ToString();
                        ddlDistributionCenter.Enabled = false;
                    }
                }


            }
            catch (Exception ex) { }


         //   btnSearch_Click(null, null);
        }
    }
    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData(Parm());
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
    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerEntry.aspx");
    }
    protected void custNameTextBox_TextChanged(object sender, EventArgs e)
    {


        string empName = custNameTextBox.Text.Trim();
        if (empName.Contains(':'))
        {
            string[] emp = empName.Split('|');

            hfCustomerId.Value = emp[1].Trim();
            custNameTextBox.Text = emp[0].Trim();



        }
        else
        {

            custNameTextBox.Text = "";
            hfCustomerId.Value = "";

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Input Correct Data !" + "','Faild');", true);
            
        }


    }
    private void LoadData(string parm)
    {
        DataTable aDataTable = _DAL.GetCustomerList(parm);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        if (aDataTable.Rows.Count > 0)
        {

            DataTable dt = _seedRepo.GetProgramTypeList();

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {

               DropDownList ddlProgramType_G = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("ddlProgramType_G");
                try
                {
                   
                        ddlProgramType_G.DataSource = dt;
                        ddlProgramType_G.DataValueField = "ProgramTypeId";
                        ddlProgramType_G.DataTextField = "ProgramTypeName";
                        ddlProgramType_G.DataBind();
                        ddlProgramType_G.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                        ddlProgramType_G.SelectedIndex = 0;

                        try
                        {
                            ddlProgramType_G.SelectedValue = aDataTable.Rows[i]["ProgramTypeId"].ToString();
                        }
                        catch (Exception ex) { }
                    


                }
                catch (Exception ex) { }
            }


        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();
            TextBox txtProgramTypeCode = ((TextBox)loadGridView.Rows[rowindex].Cells[1].FindControl("txtProgramTypeCode"));
            DropDownList ddlProgramType_G = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("ddlProgramType_G");

            LinkButton lbtUpDate = ((LinkButton)loadGridView.Rows[rowindex].Cells[1].FindControl("lbtUpDate"));

            txtProgramTypeCode.ReadOnly = false;
            lbtUpDate.Visible = true;
            ddlProgramType_G.Enabled = true;
        }


        if (e.CommandName == "UpdateData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();
            TextBox txtProgramTypeCode = ((TextBox)loadGridView.Rows[rowindex].Cells[1].FindControl("txtProgramTypeCode"));
            DropDownList ddlProgramType_G = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("ddlProgramType_G");

            if (ddlProgramType_G.SelectedValue != "")
            {
           string  ProgramTypeCode   =  string.IsNullOrEmpty(txtProgramTypeCode.Text) ? null : txtProgramTypeCode.Text;

             int?   ProgramTypeId = ddlProgramType_G.SelectedIndex > 0 ? int.Parse(ddlProgramType_G.SelectedValue) : (int?)null;


                int UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                DateTime UpdateDate = DateTime.Now;
                ResultInfo Res = _DAL.Update__ProgramTypeInfo(unitPriceId, ProgramTypeCode, ProgramTypeId, UpdateBy, UpdateDate);
                if (Res.isSuccess == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','CustomerChangeProgramType.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please Select Program Type!" + "','Faild');", true);

                ddlProgramType_G.Focus();
            }
          

            //Response.Redirect("GroupWisePromoQtyEntry.aspx?MID=" + unitPriceId);
        }

    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlProgramType.SelectedValue != "")
        {
            LoadData(Parm());
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please Select Program Type!" + "','Faild');", true);

            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    private string Parm()
    {
        string param = "";


        //if (ddlDistributionCenter.SelectedValue != "")
        //{
        //    param = param + " AND mas.ComUnitId='" + ddlDistributionCenter.SelectedValue + "' ";
        //}

        //if (GroupSelect.SelectedValue != "")
        //{
        //    param = param + " AND mas.GroupId='" + GroupSelect.SelectedValue + "' ";
        //}

        //if (ZoneSelect.SelectedValue != "")
        //{
        //    param = param + " AND mas.RegionId='" + ZoneSelect.SelectedValue + "' ";
        //}

        //if (AreaSelect.SelectedValue != "")
        //{
        //    param = param + " AND mas.AreaId='" + AreaSelect.SelectedValue + "' ";
        //}

        //if (TeritorySelect.SelectedValue != "")
        //{
        //    param = param + " AND mas.TerritoryId='" + TeritorySelect.SelectedValue + "' ";
        //}

        //if (SubTeritory.SelectedValue != "")
        //{
        //    param = param + " AND mas.SubTerritoryId='" + SubTeritory.SelectedValue + "' ";
        //}

        if (ddlProgramType.SelectedValue != "")
        {
            param = param + " and mas.ActionStatus='2' AND  mas.ProgramTypeId='" + ddlProgramType.SelectedValue + "' ";
        }


        if (hfCustomerId.Value != "")
        {
            param = param + " AND  mas.CustomerMasterId='" + hfCustomerId.Value + "' ";
        }
        //if (ddlStationType.SelectedValue != "")
        //{
        //    param = param + " AND mas.StationTypeId='" + ddlStationType.SelectedValue + "' ";
        //}

        //if (ddlApprovalStatus.SelectedValue != "")
        //{
        //    param = param + " AND mas.ActionStatus='" + ddlApprovalStatus.SelectedValue + "' ";
        //}

        //if ( frmDate.Text != "" && toDate.Text != "") {
        //    param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + toDate.Text + "' ";
        //}
        //if (frmDate.Text != "" && toDate.Text == "") {
        //    param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + DateTime.Now + "' ";
        //}

        //if (frmDate.Text != "" && toDate.Text == "") {
        //    param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + DateTime.Now + "' ";
        //}

        return param;
    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerView.aspx");
    }

    protected void ddlStationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}