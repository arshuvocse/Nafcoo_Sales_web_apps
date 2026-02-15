using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;

public partial class DoctorModule_UI_MileageClaimView : System.Web.UI.Page
{
    static SeedDataDAL _seedRepo = new SeedDataDAL();
    static Setup2DAL _setupDAL = new Setup2DAL();
    static SetupDAL _setupDAL2 = new SetupDAL();

    static CommonDataLoad _dataLoad = new CommonDataLoad();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FromDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            ToDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadInitialInfo();

            LoadData();
        }
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
        private void LoadData()
    {
        DataTable aDataTable = _setupDAL.GetMileageClaimList(param());
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{

        //    Image imgShow = ((Image)loadGridView.Rows[i].Cells[1].FindControl("imgShow"));
        //    HyperLink hpImg = ((HyperLink)loadGridView.Rows[i].Cells[1].FindControl("hpImg"));

        //    try
        //    {
        //        string imagefullpath = aDataTable.Rows[i]["ImagePreName"].ToString() ;


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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private string param()
    {
         

        var param = " and  mas.MileageClaimId IS NOT NULL";

        if (FromDate.Text != "" &&  ToDate.Text != "") {
            param = param + " AND CONVERT(date,mas.EntryDate)  BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ";
        }
        if ( FromDate.Text != "" && ToDate.Text == "") {
            param = param + " AND CONVERT(date,mas.EntryDate)  BETWEEN '" + FromDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

         
        if (ApprovalStatusSelect.SelectedValue != "" ) {

            param = param + " AND mas.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "'";


        }

        if (UserRoleSelect.SelectedValue != "" ) {

            param = param + " AND us.UserRoleID='" + UserRoleSelect.SelectedValue + "'";

        }

        if (EmployeeIdSelect.SelectedValue != "" ) {

            param = param + " AND mas.EmpInfoId='" + EmployeeIdSelect.SelectedValue + "'";

        }


        return param;
    }

    [WebMethod]
    public static string GetMileageClaimList(string param)
    {
        DataTable dt = _setupDAL.GetMileageClaimList(param);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;

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

            Response.Redirect("../DoctorModule_UI/MileageClaim.aspx?id=" + unitPriceId);
        }

    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
         Response.Redirect("MileageClaimView.aspx");
    }
}