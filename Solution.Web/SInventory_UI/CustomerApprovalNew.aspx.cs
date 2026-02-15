using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.Models;

public partial class SInventory_UI_CustomerApprovalNew : System.Web.UI.Page
{
    CustomerInfoDAL aDal = new CustomerInfoDAL();


    string RoleTypeName = "";
    string EmpInfoId = "";
    string ToRoleTypeId = "";
    string ApprovalStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialGrid(itemsGridView);
        }
    }



    private void LoadInitialGrid(GridView gridView)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetCustomerListForApproval(GenerateParam());

        if (aTable.Rows.Count > 0)
        {
            gridView.DataSource = aTable;
            gridView.DataBind();


            //for (int i = 0; i < gridView.Rows.Count; i++)
            //{
            //    string activeStatus = gridView.DataKeys[i][1].ToString();

            //    if (activeStatus == "Inactive")
            //    {
            //        ImageButton editImageButton = ((ImageButton)gridView.Rows[i].Cells[0].FindControl("editImageButton"));
            //        editImageButton.Visible = false;
            //    }
            //}


        }
        else
        {
            gridView.DataSource = null;
            gridView.DataBind();
        }

    }

    private string GenerateParam()
    {
        StringBuilder param = new StringBuilder();

        //// Check if Zone is selected and not equal to "0"
        //if (!string.IsNullOrEmpty(ddlZone.SelectedValue) && ddlZone.SelectedValue != "0")
        //{
        //    param.Append(" AND RGN.RegionId = ").Append(ddlZone.SelectedValue);
        //}

        //// Check if Area is selected and not equal to "0"
        //if (!string.IsNullOrEmpty(ddlArea.SelectedValue) && ddlArea.SelectedValue != "0")
        //{
        //    param.Append(" AND ARA.AreaId = ").Append(ddlArea.SelectedValue);
        //}

        //// Check if Territory is selected and not equal to "0"
        //if (!string.IsNullOrEmpty(ddlTerritory.SelectedValue) && ddlTerritory.SelectedValue != "0")
        //{
        //    param.Append(" AND TTR.TerritoryId = ").Append(ddlTerritory.SelectedValue);
        //}

        //// Check if Active Status is selected and not empty
        //if (!string.IsNullOrEmpty(ddlActiveStatus.SelectedValue))
        //{
        //    param.Append(" AND MBE.IsActive = ").Append(ddlActiveStatus.SelectedValue);
        //}

        //// Check if search text is provided
        //if (!string.IsNullOrWhiteSpace(tbxSearch.Text))
        //{
        //    string searchTerm = tbxSearch.Text.Trim();
        //    param.Append(" AND (")
        //         .Append("RGN.RegionName LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR RGN.RegionCode LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR ARA.AreaCode LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR ARA.AreaName LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR TTR.TerritoryCode LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR TTR.TerritoryName LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR EGI.EmpMasterCode LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR EGI.EmpName LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR Sub.SubTerritoryCode LIKE '%").Append(searchTerm).Append("%' ")
        //         .Append("OR Sub.SubTerritoryName LIKE '%").Append(searchTerm).Append("%')");
        //}

        return param.ToString();
    }


    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        itemsGridView.PageIndex = e.NewPageIndex;
        this.LoadInitialGrid(itemsGridView);
    }


    protected void itemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "EditData")
        {


            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = itemsGridView.DataKeys[rowindex][0].ToString();
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('../MasterSetup_UI/CustomerEntry.aspx?MID=" + unitPriceId + "' ,'_blank');", true);

            //   Response.Redirect("CustomerEntry.aspx?MID=" + unitPriceId);
        }

        if (e.CommandName == "ApproveData")
        {
            ApprovalStatus = "Accepted";
            int rowindex = Convert.ToInt32(e.CommandArgument);

            HiddenField hfCustomerApprovalId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerApprovalId"));
            HiddenField hfToEmpId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfToEmpId"));
            HiddenField hfStep = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfStep"));
            HiddenField hfCustomerMasterId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerMasterId"));


            CustomerSaveAppLog_DAO aMaster = new CustomerSaveAppLog_DAO();
            aMaster.CustomerApprovalId = hfCustomerApprovalId.Value == "" ? 0 : Convert.ToInt32(hfCustomerApprovalId.Value);

            aMaster.TableId = hfCustomerMasterId.Value == "" ? 0 : Convert.ToInt32(hfCustomerMasterId.Value);

            aMaster.GroupId = hfEmpGroupId.Value == "" ? 0 : Convert.ToInt32(hfEmpGroupId.Value);
            aMaster.RegionId = hfEmpRegionId.Value == "" ? 0 : Convert.ToInt32(hfEmpRegionId.Value);
            aMaster.AreaId = hfEmpAreaId.Value == "" ? 0 : Convert.ToInt32(hfEmpAreaId.Value);
            aMaster.TerritoryId = hfEmpTerrId.Value == "" ? 0 : Convert.ToInt32(hfEmpTerrId.Value);

            aMaster.FromEmpId = EmpInfoId == "" ? 0 : Convert.ToInt32(EmpInfoId);
            aMaster.ToEmpId = hfToEmpId.Value == "" ? 0 : Convert.ToInt32(hfToEmpId.Value);
            int InStep = hfStep.Value == "" ? 0 : Convert.ToInt32(hfStep.Value);
            aMaster.Step = InStep + 1;
            aMaster.Type = "Customer";
            aMaster.Status = ApprovalStatus;
            aMaster.Date = DateTime.Now;
            aMaster.EntryDateS = DateTime.Now;
            aMaster.ApproveDateS = DateTime.Now;
            aMaster.EntryByS = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.EntryByApp = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.MenuId = 302;

            ResultInfo Res = aDal.SaveCustomer_ApplogDAL(aMaster);

            if (Res.isSuccess == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation successful!" + "','Success');", true);
                LoadInitialGrid(itemsGridView);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }



        if (e.CommandName == "RejectData")
        {
            ApprovalStatus = "Rejected";

            int rowindex = Convert.ToInt32(e.CommandArgument);

            HiddenField hfCustomerApprovalId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerApprovalId"));
            HiddenField hfToEmpId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfToEmpId"));
            HiddenField hfStep = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfStep"));
            HiddenField hfCustomerMasterId = ((HiddenField)itemsGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerMasterId"));


            CustomerSaveAppLog_DAO aMaster = new CustomerSaveAppLog_DAO();

            aMaster.CustomerApprovalId = hfCustomerApprovalId.Value == "" ? 0 : Convert.ToInt32(hfCustomerApprovalId.Value);
            aMaster.TableId = hfCustomerMasterId.Value == "" ? 0 : Convert.ToInt32(hfCustomerMasterId.Value);
            aMaster.GroupId = hfEmpGroupId.Value == "" ? 0 : Convert.ToInt32(hfEmpGroupId.Value);
            aMaster.RegionId = hfEmpRegionId.Value == "" ? 0 : Convert.ToInt32(hfEmpRegionId.Value);
            aMaster.AreaId = hfEmpAreaId.Value == "" ? 0 : Convert.ToInt32(hfEmpAreaId.Value);
            aMaster.TerritoryId = hfEmpTerrId.Value == "" ? 0 : Convert.ToInt32(hfEmpTerrId.Value);

            aMaster.FromEmpId = EmpInfoId == "" ? 0 : Convert.ToInt32(EmpInfoId);
            aMaster.ToEmpId = hfToEmpId.Value == "" ? 0 : Convert.ToInt32(hfToEmpId.Value);
            int InStep = hfStep.Value == "" ? 0 : Convert.ToInt32(hfStep.Value);
            aMaster.Step = InStep + 1;
            aMaster.Type = "Customer";
            aMaster.Status = ApprovalStatus;
            aMaster.Date = DateTime.Now;
            aMaster.EntryDateS = DateTime.Now;
            aMaster.ApproveDateS = DateTime.Now;
            aMaster.EntryByS = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.EntryByApp = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.MenuId = 302;

            ResultInfo Res = aDal.SaveCustomer_ApplogDAL(aMaster);

            if (Res.isSuccess == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation successful!" + "','Success');", true);
                LoadInitialGrid(itemsGridView);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }

    }
    //protected void itemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "EditData")
    //    {
    //        Session["MBEId"] = e.CommandArgument.ToString();
    //        Response.Redirect("MBESetupNew.aspx");
    //    }

    //}
}