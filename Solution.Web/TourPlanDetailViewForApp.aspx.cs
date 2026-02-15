using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;

public partial class TourPlanDetailViewForApp : System.Web.UI.Page
{
    string RoleTypeName = "";
    string EmpInfoId = "";
    string ToRoleTypeId = "";
    int TourPlanTableId = 0;
    string ApprovalStatus = "";

    private static CustomerInfoDAL _DAL = new CustomerInfoDAL();
    private CommonDataLoad _dataLoad = new CommonDataLoad();
    private static SeedDataDAL _seedRepo = new SeedDataDAL();
    static Setup2DAL _setupDAL = new Setup2DAL();
    private static TourTypeDal tourTypeDal = new TourTypeDal();
    private CommonDataLoad _CmnLoad = new CommonDataLoad();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RoleTypeName = Session["RoleTypeName"].ToString();
            EmpInfoId = Session["EmpInfoId"].ToString();
            ToRoleTypeId = Session["RoleTypeId"].ToString();
            TourPlanTableId = Convert.ToInt32(Session["TourPlanTableId"].ToString());


            if (!IsPostBack)
            {
               // UserPersmissionValidation();
                //try
                //{
                //    using (DataTable dt = _seedRepo.GetDistributionCenterDataTableList())
                //    {
                //        ddlDistributionCenter.DataSource = dt;
                //        ddlDistributionCenter.DataValueField = "ComUnitId";
                //        ddlDistributionCenter.DataTextField = "ComUnitName";
                //        ddlDistributionCenter.DataBind();
                //        ddlDistributionCenter.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                //        ddlDistributionCenter.SelectedIndex = 0;

                //        if (Session["RoleTypeName"].ToString() == "DIC")
                //        {
                //            ddlDistributionCenter.SelectedValue = Session["DICCompanyUnitId"].ToString();
                //            ddlDistributionCenter.Enabled = false;
                //        }
                //    }


                //}
                //catch (Exception ex) { }

                LoadInitialInfo();

                try
                {
                    GetMonthList(ddlmonth);
                    GetYearList(ddlYear);

                    // Read values from the query string
                    string queryMonth = Request.QueryString["month"]; // Expected as a number: "6"
                    string queryYear = Request.QueryString["year"];

                    if (!string.IsNullOrEmpty(queryMonth) && ddlmonth.Items.FindByValue(queryMonth) != null)
                    {
                        ddlmonth.SelectedValue = queryMonth;
                    }

                    if (!string.IsNullOrEmpty(queryYear) && ddlYear.Items.FindByValue(queryYear) != null)
                    {
                        ddlYear.SelectedValue = queryYear;
                    }



                }

                catch (Exception ex) { }
                LoadData();
            }
        }
        catch (Exception ex)
        {
           
            string redirectUrl = "TourPlanApprovalListForApp.aspx?empId=" + Request.QueryString["empId"];

            Response.Redirect(redirectUrl);
        }
    }


    public void UserPersmissionValidation()
    {
        if (Session["UserRoleID"].ToString() != "2")
        {
            try
            {
                string filepath = Path.GetDirectoryName(Request.Path);
                filepath = filepath.TrimStart('\\');
                string text = Path.GetExtension(Request.Path);
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                DataTable dtuserpermission = _CmnLoad.GetPermissionForUserRole(filepath);
                if (dtuserpermission.Rows.Count > 0)
                {
                    if (Session["UserRoleID"].ToString() != "2")
                    {



                    }
                }
                else
                {
                    Response.Redirect("../Dashboard_UI/DashboardOne.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../Dashboard_UI/DashboardOne.aspx");
            }
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

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "EditData")
        {


            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('TourPlanDetailViewForApp.aspx?id=" + unitPriceId + "' ,'_blank');", true);

            //Response.Redirect("TourPlanDetailViewForApp.aspx?id=" + unitPriceId);
        }

        if (e.CommandName == "ApproveData")
        {
            ApprovalStatus = "Verified";
            int rowindex = Convert.ToInt32(e.CommandArgument);

            HiddenField hfCustomerApprovalId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerApprovalId"));
            HiddenField hfToEmpId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfToEmpId"));
            HiddenField hfStep = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfStep"));
            HiddenField hfCustomerMasterId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerMasterId"));


            OrderSaveApprovalLogDAO aMaster = new OrderSaveApprovalLogDAO();
            aMaster.OrderApprovalId = hfCustomerApprovalId.Value == "" ? 0 : Convert.ToInt32(hfCustomerApprovalId.Value);

            aMaster.TableId = hfCustomerMasterId.Value == "" ? 0 : Convert.ToInt32(hfCustomerMasterId.Value);

            aMaster.GroupId = hfEmpGroupId.Value == "" ? 0 : Convert.ToInt32(hfEmpGroupId.Value);
            aMaster.RegionId = hfEmpRegionId.Value == "" ? 0 : Convert.ToInt32(hfEmpRegionId.Value);
            aMaster.AreaId = hfEmpAreaId.Value == "" ? 0 : Convert.ToInt32(hfEmpAreaId.Value);
            aMaster.TerritoryId = hfEmpTerrId.Value == "" ? 0 : Convert.ToInt32(hfEmpTerrId.Value);

            aMaster.FromEmpId = EmpInfoId == "" ? 0 : Convert.ToInt32(EmpInfoId);
            aMaster.ToEmpId = hfToEmpId.Value == "" ? 0 : Convert.ToInt32(hfToEmpId.Value);
            int InStep = hfStep.Value == "" ? 0 : Convert.ToInt32(hfStep.Value);
            aMaster.Step = InStep + 1;
            aMaster.Type = "TourPlan";
            aMaster.Status = ApprovalStatus;
            aMaster.Date = DateTime.Now;
            aMaster.EntryDateS = DateTime.Now;
            aMaster.ApproveDateS = DateTime.Now;
            aMaster.EntryByS = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.EntryByApp = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.MenuId = 371;
            ResultInfo Res = _DAL.SaveTourPlan_ApplogDAL(aMaster);
            if (Res.isSuccess == true)
            {

                string empId = Session["EmpInfoId"].ToString();
                string redirectUrl = "TourPlanApprovalListForApp.aspx?empId=" + empId;

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                    "successalert('Operation Approved successful!', 'Success', '" + redirectUrl + "');", true);

                
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

            HiddenField hfCustomerApprovalId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerApprovalId"));
            HiddenField hfToEmpId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfToEmpId"));
            HiddenField hfStep = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfStep"));
            HiddenField hfCustomerMasterId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfCustomerMasterId"));


            OrderSaveApprovalLogDAO aMaster = new OrderSaveApprovalLogDAO();
            aMaster.OrderApprovalId = hfCustomerApprovalId.Value == "" ? 0 : Convert.ToInt32(hfCustomerApprovalId.Value);
            aMaster.TableId = hfCustomerMasterId.Value == "" ? 0 : Convert.ToInt32(hfCustomerMasterId.Value);
            aMaster.GroupId = hfEmpGroupId.Value == "" ? 0 : Convert.ToInt32(hfEmpGroupId.Value);
            aMaster.RegionId = hfEmpRegionId.Value == "" ? 0 : Convert.ToInt32(hfEmpRegionId.Value);
            aMaster.AreaId = hfEmpAreaId.Value == "" ? 0 : Convert.ToInt32(hfEmpAreaId.Value);
            aMaster.TerritoryId = hfEmpTerrId.Value == "" ? 0 : Convert.ToInt32(hfEmpTerrId.Value);

            aMaster.FromEmpId = EmpInfoId == "" ? 0 : Convert.ToInt32(EmpInfoId);
            aMaster.ToEmpId = hfToEmpId.Value == "" ? 0 : Convert.ToInt32(hfToEmpId.Value);
            int InStep = hfStep.Value == "" ? 0 : Convert.ToInt32(hfStep.Value);
            aMaster.Step = InStep + 1;
            aMaster.Type = "Leave";
            aMaster.Status = ApprovalStatus;
            aMaster.Date = DateTime.Now;
            aMaster.EntryDateS = DateTime.Now;
            aMaster.ApproveDateS = DateTime.Now;
            aMaster.EntryByS = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.EntryByApp = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.MenuId = 371;
            ResultInfo Res = _DAL.SaveTourPlan_ApplogDAL(aMaster);
            if (Res.isSuccess == true)
            {
                

                string empId = Session["EmpInfoId"].ToString();
                string redirectUrl = "TourPlanApprovalListForApp.aspx?empId=" + empId;

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup",
                    "successalert('Operation Disapproved successful!', 'Success', '" + redirectUrl + "');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }

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
        Response.Redirect("DoctorEntry.aspx");
    }

    // helper (class-level বা method-topে রাখতে পারেন)
    private static int ToInt(object o)
    {
        int x;
        if (o == null) return 0;
        string s = Convert.ToString(o);
        return int.TryParse(s, out x) ? x : 0;
    }

    private static string Norm(object o)
    {
        string s = (o == null) ? "" : Convert.ToString(o);
        if (string.IsNullOrEmpty(s)) return "";
        s = s.Trim();
        return s.ToUpperInvariant();
    }



    private void LoadData()
    {
        string pram = string.Empty;
        string Role = string.Empty;

        // ===== Market structure for current employee =====
        if (!string.IsNullOrEmpty(EmpInfoId))
        {
            DataTable dtMarket = _dataLoad.GetEmpMarketStructure_Active(EmpInfoId);
            if (dtMarket != null && dtMarket.Rows.Count > 0)
            {
                try
                {
                    hfEmpGroupId.Value = Convert.ToString(dtMarket.Rows[0]["EmpGroupId"]);
                    hfEmpRegionId.Value = Convert.ToString(dtMarket.Rows[0]["EmpRegionId"]);
                    hfEmpAreaId.Value = Convert.ToString(dtMarket.Rows[0]["EmpAreaId"]);
                    hfEmpTerrId.Value = Convert.ToString(dtMarket.Rows[0]["EmpTerrId"]);
                }
                catch { /* ignore */ }

                string dataCol = null;   // dtMarket-এর কলাম নাম
                string viewCol = null;   // View_Webapi_EmployeeFieldForceInfo-এর কলাম নাম

                string FFID = "";
                switch (RoleTypeName)
                {
                    case "MIO":
                        dataCol = "EmpTerrId";
                        viewCol = "EmpTerrId";
                        Role = "MIO";
                        break;

                    case "AM":
                        dataCol = "EmpAreaId";
                        viewCol = "EmpAreaId";
                        Role = "AM";
                        break;

                    case "DZSM":
                        dataCol = "EmpRegionId";
                        viewCol = "EmpRegionId";
                        Role = "DZSM";
                        break;

                    case "NSM":
                        dataCol = "EmpGroupId";
                        viewCol = "EmpGroupId";
                        Role = "NSM";
                        break;

                    default:
                        pram = "";
                        Role = "";
                        break;
                }
            }
        }

        // ===== Filters =====
        if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
            pram += " AND mas.YearValue='" + ddlYear.SelectedValue + "' ";
        if (!string.IsNullOrEmpty(ddlmonth.SelectedValue))
            pram += " AND mas.MonthValue='" + ddlmonth.SelectedValue + "' ";
        if (!string.IsNullOrEmpty(ApprovalStatusSelect.SelectedValue))
            pram += " AND mas.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "' ";
        if (!string.IsNullOrEmpty(UserRoleSelect.SelectedValue))
            pram += " AND us.UserRoleID='" + UserRoleSelect.SelectedValue + "' ";
        if (!string.IsNullOrEmpty(EmployeeIdSelect.SelectedValue))
            pram += " AND mas.EmpInfoId='" + EmployeeIdSelect.SelectedValue + "' ";

        // নিজের রেকর্ড বাদ দিতে চাইলে
        pram += " AND mas.EmpInfoId <>'" + EmpInfoId + "'";

        // ===== Load & bind =====
        DataTable aDataTable = _DAL.GetTourPlan_ApplogDAL(pram, Role, null, null, null, TourPlanTableId);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        // ===== PAGE-LEVEL BUTTON LOGIC =====
        btnApprove.Visible = false;
        btnReject.Visible = false;
        warnToast.Visible = false;
        int createdRole = 0;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            HiddenField hfToEmpId = (HiddenField)loadGridView.Rows[i].FindControl("hfToEmpId");
            HiddenField hfToRoleTypeId = (HiddenField)loadGridView.Rows[i].FindControl("hfToRoleTypeId");
            HiddenField hfRoleTypeId = (HiddenField)loadGridView.Rows[i].FindControl("hfRoleTypeId");
            HiddenField hfApprovalStatusWeb = (HiddenField)loadGridView.Rows[i].FindControl("hfApprovalStatusWeb");
            HiddenField hfMaxStep = (HiddenField)loadGridView.Rows[i].FindControl("hfMaxStep");
            HiddenField hfStep = (HiddenField)loadGridView.Rows[i].FindControl("hfStep");

            //LinkButton lbApprove = (LinkButton)loadGridView.Rows[i].FindControl("lbApprove");
            //LinkButton lbReject  = (LinkButton)loadGridView.Rows[i].FindControl("lbReject");
            Label lbMsg = (Label)loadGridView.Rows[i].FindControl("lbMsg");

            // defaults
            bool canApprove = false, canDisapprove = false;
            lbMsg.Visible = true;

            int myRole = Convert.ToInt32(ToRoleTypeId);
            int recordRole = Convert.ToInt32(hfToRoleTypeId.Value);
            int currentStep = Convert.ToInt32(hfStep.Value);
            int maxStep = Convert.ToInt32(hfMaxStep.Value);

            string status = (hfApprovalStatusWeb.Value ?? "").Trim();
            bool isApproved = status.Equals("Approved", StringComparison.OrdinalIgnoreCase);
            bool isDisapproved = status.Equals("Disapproved", StringComparison.OrdinalIgnoreCase); // টাইপো ফিক্স

            // === Case A: Disapproved ===
            if (isDisapproved)
            {
                lbMsg.Text = "Disapproved";
                lbMsg.CssClass = "badge bg-danger";
                canApprove = false;
                canDisapprove = false;
            }
            // === Case B: Approved (সবার Disapprove করার ক্ষমতা থাকবে) ===
            else if (isApproved)
            {
                lbMsg.Text = "Approved (Anyone can Disapprove)";
                lbMsg.CssClass = "badge bg-success";
                canApprove = false;
                canDisapprove = true;   // role নির্বিশেষে
            }
            else
            {
                // === Pending states ===
                if (recordRole == myRole)
                {
                    // আপনি current approver
                    if (currentStep == maxStep)
                    {
                        // Final step: Approve + Disapprove দুটোই পারবে
                        lbMsg.Text = "Final Step - You can Approve/Disapprove";
                        lbMsg.CssClass = "badge bg-primary";
                        canApprove = true;
                        canDisapprove = true;
                    }
                    else
                    {
                        // মাঝের ধাপ: সাধারণত দুটোই রাখা হলো
                        lbMsg.Text = "Pending for Your Approval";
                        lbMsg.CssClass = "badge bg-info";
                        canApprove = true;
                        canDisapprove = true;
                    }
                }
                else
                {
                    // আপনি current approver নন
                    if (Convert.ToInt32(hfRoleTypeId.Value) >= myRole)
                    {
                        lbMsg.Text = "Waiting For Final Approval";
                        lbMsg.CssClass = "badge bg-success";
                    }
                    else
                    {
                        lbMsg.Text = "Need Approval from Previous Approver";
                        lbMsg.CssClass = "badge bg-warning";
                    }

                    // যেহেতু এখনো Approved না, তাই এখানে approve/disapprove দেয় না
                    canApprove = false;
                    canDisapprove = true;
                }
            }

            // === Apply to buttons if you use them ===
            if (btnApprove != null) btnApprove.Visible = canApprove;
            if (btnReject != null) btnReject.Visible = canDisapprove;
        }


        foreach (GridViewRow row in loadGridView.Rows)
        {
            var hfApprovalStatusWeb = (HiddenField)row.FindControl("hfApprovalStatusWeb");
            var hfRoleTypeId = (HiddenField)row.FindControl("hfRoleTypeId");
            var lbMsg = (Label)row.FindControl("lbMsg");

            string st = Norm(hfApprovalStatusWeb != null ? hfApprovalStatusWeb.Value : null);

            bool isApproved = (st == "ACCEPTED" || st == "APPROVED" );
            bool isRejected = (st == "REJECTED" || st == "DISAPPROVED" || st == "DISAPPROVEEED");

            if (isApproved)
            {
                lbMsg.Text = "Approved";
                lbMsg.CssClass = "badge bg-success";
                continue;
            }

            if (isRejected)
            {
                lbMsg.Text = "Rejected";
                lbMsg.CssClass = "badge bg-danger";
                continue;
            }

            // Pending / In-progress
            int rowRoleTypeId = ToInt(createdRole);

            if (rowRoleTypeId >= Convert.ToInt32(ToRoleTypeId))
            {
                lbMsg.Text = "Waiting For Final Approval";
                lbMsg.CssClass = "badge bg-success";
            }
            else
            {
                lbMsg.Text = "Need to be Approved by Previous Approver";
                lbMsg.CssClass = "badge bg-warning";
            }
        }
    }
    private void EmpMarketAccess(string pram, string Role)
    {



    }
    protected void resetBtn_Click(object sender, EventArgs e)
    {
        if (Session["EmpInfoId"] != null)
        {
            string empId = Session["EmpInfoId"].ToString();
            Response.Redirect("TourPlanApprovalListForApp.aspx?empId=" + empId);
        }
        else
        {
            // Handle missing session (e.g., redirect to login or show an error)
            Response.Redirect("Login.aspx"); // or show a message
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }








    protected void btnApprove_Click(object sender, EventArgs e)
    {
        TriggerGridCommand("ApproveData");
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        TriggerGridCommand("RejectData");
    }

    private void TriggerGridCommand(string commandName)
    {
        string masterId = Request.QueryString["id"];
        if (string.IsNullOrEmpty(masterId)) return;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            string rowTableId = loadGridView.DataKeys[i]["TableId"].ToString();
            if (rowTableId == masterId)
            {
                GridViewRow row = loadGridView.Rows[i];

                // Fake a click event with a Command
                GridViewCommandEventArgs args = new GridViewCommandEventArgs(
                    row, new CommandEventArgs(commandName, i.ToString()));

                loadGridView_RowCommand(loadGridView, args);
                break;
            }
        }
    }


    [WebMethod]
    public static string GetTourPlanDetailsViewDatabyID(int id)
    {

        DataTable dt = tourTypeDal.GetTourPlanDetailsViewDatabyID(id);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }

    [WebMethod]
    public static string Get_TourPlanBalance(int empId, int Month, int year)
    {


        DataTable dt = tourTypeDal.Get_TourPlanBalanceDAL(empId, Month, year);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }
}