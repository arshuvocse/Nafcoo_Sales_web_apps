using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.Panal_BLL;
using Library.DAL.MasterSetup_DAL;
using Library.DAL.SInventory_DAL;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;

public partial class TourPlanApprovalListForApp : System.Web.UI.Page
{

    string RoleTypeName = "";
    string EmpInfoId = "";
    string ToRoleTypeId = "";
    string ApprovalStatus = "";
    private static SeedDataDAL _seedRepo = new SeedDataDAL();
    PanalBLL aPanalBll = new PanalBLL();
    private static CustomerInfoDAL _DAL = new CustomerInfoDAL();
    private CommonDataLoad _dataLoad = new CommonDataLoad();
    static Setup2DAL _setupDAL = new Setup2DAL();
    private CommonDataLoad _CmnLoad = new CommonDataLoad();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
      

            if (!IsPostBack)
            {
                LoadInitialInfo();

                try
                {
                    GetMonthList(ddlmonth);
                    GetYearList(ddlYear);
                }

                catch (Exception ex) { }
                DataTable aTableLogin = new DataTable();
                aTableLogin = NewMethod(aTableLogin);
            }
        }
        catch (Exception ex)
        {
             }
    }


    protected string GetStatusBadgeCss(object statusObj)
    {
        var s = (statusObj ?? "").ToString().ToLowerInvariant();
        if (s.Contains("approved")) return "badge bg-success";
        if (s.Contains("rejected")) return "badge bg-danger";
        if (s.Contains("pending") || s.Contains("waiting")) return "badge bg-warning text-dark";
        return "badge bg-secondary";
    }

    protected string ShowApproveRejectCss(object statusObj)
    {
        var s = (statusObj ?? "").ToString().ToLowerInvariant();
        bool hide = s.Contains("approved") || s.Contains("rejected");
        return hide ? "d-none" : "";
    }

    private DataTable NewMethod(DataTable aTableLogin)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["empId"]))
        {
            int empID;
            if (int.TryParse(Request.QueryString["empId"], out empID))
            {




                aTableLogin = aPanalBll.LoginByEmpId(empID);
                if (aTableLogin.Rows.Count > 0)
                {

                    Session["UserId"] = aTableLogin.Rows[0]["UserId"].ToString().Trim();
                    Session["UserRoleID"] = aTableLogin.Rows[0]["UserRoleID"].ToString().Trim();
                    Session["RoleTypeId"] = aTableLogin.Rows[0]["RoleTypeId"].ToString().Trim();
                    Session["RoleTypeName"] = aTableLogin.Rows[0]["RoleTypeName"].ToString().Trim();

                    Session["LoginName"] = aTableLogin.Rows[0]["LoginName"].ToString().Trim();
                    Session["UserType"] = aTableLogin.Rows[0]["UserType"].ToString().Trim();
                    Session["CentralWareHouse"] = aTableLogin.Rows[0]["CentralWareHouse"].ToString().Trim();
                    if (aTableLogin.Rows[0]["UserType"].ToString().Trim() != "Admin")
                    {
                        Session["ComUnitId"] = aTableLogin.Rows[0]["CompanyUnitId"].ToString().Trim();
                    }
                    //  aPanalBll.LoginLog(Session["UserId"].ToString(), Session["LoginName"].ToString(), DateTime.Now);
                    Session["UserTime"] = DateTime.Now.ToString("f");
                    Session["EmpInfoId"] = aTableLogin.Rows[0]["EmpInfoId"].ToString().Trim();
                    Session["EmpName"] = aTableLogin.Rows[0]["EmpName"].ToString().Trim();
                    Session["DesigName"] = aTableLogin.Rows[0]["DesigName"].ToString().Trim();
                    Session["DICCompanyUnitId"] = aTableLogin.Rows[0]["DICCompanyUnitId"].ToString().Trim();

                    bool IsMainDashboard = false;
                    try
                    {

                        //   IsMainDashboard = Convert.ToBoolean(aTableLogin.Rows[0]["IsMainDashboard"].ToString().Trim());

                    }
                    catch (Exception ex)
                    {

                    }

                    RoleTypeName = Session["RoleTypeName"].ToString();
                    EmpInfoId = Session["EmpInfoId"].ToString();
                    ToRoleTypeId = Session["RoleTypeId"].ToString();

                    LoadData();
                }




                //UserPersmissionValidation();
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

              
            }
        }

        return aTableLogin;
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
                  //  Response.Redirect("../Dashboard_UI/DashboardOne.aspx");
                }
            }
            catch (Exception ex)
            {
               // Response.Redirect("../Dashboard_UI/DashboardOne.aspx");
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
            // Row object বের করুন
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;  // ✅ এখানে index সেট হলো

            // DataKeyNames থেকে TableId (বা প্রথম DataKey) নিন
            string unitPriceId = loadGridView.DataKeys[rowindex].Value.ToString();
             
            // Dropdown থেকে month/year
            string selectedMonth = ddlmonth.SelectedValue;
            string selectedYear = ddlYear.SelectedValue;
            Session["TourPlanTableId"] = unitPriceId.ToString();
            // Redirect to detail page
            Response.Redirect(
                "TourPlanDetailViewForApp.aspx?id=" + unitPriceId +
                "&month=" + selectedMonth +
                "&year=" + selectedYear +
                "&empId=" + Request.QueryString["empId"]
            );
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
            aMaster.Type = "Tour Plan";
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation  Approved successful!" + "','Success');", true);
                LoadData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }



        if (e.CommandName == "RejectData")
        {
            ApprovalStatus = "Rejected";

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
             HiddenField hfCustomerApprovalId = (HiddenField)row.FindControl("hfCustomerApprovalId");
            HiddenField hfToEmpId = (HiddenField)row.FindControl("hfToEmpId");
            HiddenField hfStep = (HiddenField)row.FindControl("hfStep");
            HiddenField hfCustomerMasterId = (HiddenField)row.FindControl("hfCustomerMasterId");


             

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
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation disapproved successful!" + "','Success');", true);
                LoadData();

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

    private void LoadData()
    {
        string pram = "", Role = "";

        if (!string.IsNullOrEmpty(EmpInfoId))
        {
            DataTable dtMarket = _dataLoad.GetEmpMarketStructure_Active(EmpInfoId);

            try
            {
                hfEmpGroupId.Value = dtMarket.Rows[0]["EmpGroupId"].ToString();
                hfEmpRegionId.Value = dtMarket.Rows[0]["EmpRegionId"].ToString();
                hfEmpAreaId.Value = dtMarket.Rows[0]["EmpAreaId"].ToString();
                hfEmpTerrId.Value = dtMarket.Rows[0]["EmpTerrId"].ToString();
            }
            catch { }
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

            if (!string.IsNullOrEmpty(dataCol) && dtMarket != null && dtMarket.Rows.Count > 0)
            {
                // সব rows থেকে distinct IDs
                var idSet = new HashSet<string>();
                foreach (DataRow row in dtMarket.Rows)
                {
                    object obj = row[dataCol];
                    string v = (obj == null || obj == DBNull.Value) ? null : obj.ToString().Trim();
                    if (!string.IsNullOrEmpty(v))
                        idSet.Add(v);
                }

                if (idSet.Count > 0)
                {
                    // ===== যদি IDs numeric হয় =====
                    FFID = string.Join(",", new List<string>(idSet).ToArray());
                    pram = " AND View_Webapi_EmployeeFieldForceInfo." + viewCol + " IN (" + FFID + ")";

                    // ===== যদি IDs string হয় (quote দরকার) -> উপরের দুই লাইন কমেন্ট করে নিচের দুই লাইন আনকমেন্ট করুন =====
                    // var quoted = idSet.Select(x => "'" + x.Replace("'", "''") + "'").ToArray();
                    // FFID = string.Join(",", quoted);
                    // pram = " AND View_Webapi_EmployeeFieldForceInfo." + viewCol + " IN (" + FFID + ")";
                }
                else
                {
                    pram = "";
                }
            }

            if (!string.IsNullOrEmpty(ddlYear.SelectedValue))
                pram += " AND mas.YearValue='" + ddlYear.SelectedValue + "' ";

            if (!string.IsNullOrEmpty(ddlmonth.SelectedValue))
                pram += " AND mas.MonthValue='" + ddlmonth.SelectedValue + "' ";

            if (!string.IsNullOrEmpty(ApprovalStatusSelect.SelectedValue))
                pram += " AND mas.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "' ";

            if (!string.IsNullOrEmpty(UserRoleSelect.SelectedValue))
                pram += " AND us.UserRoleID='" + UserRoleSelect.SelectedValue + "' ";

            pram += " AND mas.EmpInfoId <>'" + EmpInfoId + "'";

            string AppStatus = null;
            int? EmpId = null;
            DateTime? FromDt = null;
            DateTime? ToDt = null;

            DataTable aDataTable = _DAL.GetTourPlan_ApplogDAL(pram, Role, AppStatus, FromDt, ToDt, EmpId);

            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();

            // === Mirror of Android onGetTPApprovalList() ===
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
                        canDisapprove = false;
                    }
                }

                // === Apply to buttons if you use them ===
                // if (lbApprove != null) lbApprove.Visible = canApprove;
                // if (lbReject  != null) lbReject.Visible  = canDisapprove;
            }

        }
    }



    private void EmpMarketAccess(string pram, string Role)
    {



    }
    //protected void resetBtn_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("TourPlanApprovalListForApp.aspx");
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable aTableLogin = new DataTable();
        aTableLogin = NewMethod(aTableLogin);
    }
}