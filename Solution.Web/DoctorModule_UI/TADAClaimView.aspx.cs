using Library.DAL.DoctorModule_DAL;
using SalesSolution.Web.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoctorModule_UI_TADAClaimView : System.Web.UI.Page
{
    public static SetupDAL _setupDAL = new SetupDAL();
    static SeedDataDAL _seedRepo = new SeedDataDAL();
    static CommonDataLoad _dataLoad = new CommonDataLoad();
    private static Setup2DAL _setupDALss = new Setup2DAL();

    static SetupDAL _setupDAL2 = new SetupDAL();
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


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();

      

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('TADAClaimEdit.aspx?MID=" + unitPriceId + "' ,'_blank');", true);

        }

    }


    private string param()
    {


        var param = "  ";

        if (FromDate.Text != "" && ToDate.Text != "")
        {
            param = param + " AND CONVERT(date,mas.TadaDate)  BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ";
        }
        if (FromDate.Text != "" && ToDate.Text == "")
        {
            param = param + " AND CONVERT(date,mas.TadaDate)  BETWEEN '" + FromDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }


        if (ApprovalStatusSelect.SelectedValue != "")
        {

            param = param + " AND mas.ApprovalStatus='" + ApprovalStatusSelect.SelectedValue + "'";


        }

        if (UserRoleSelect.SelectedValue != "")
        {

            param = param + " AND us.UserRoleID='" + UserRoleSelect.SelectedValue + "'";

        }

        if (EmployeeIdSelect.SelectedValue != "")
        {

            param = param + " AND mas.EmpInfoId='" + EmployeeIdSelect.SelectedValue + "'";

        }


        return param;
    }
    private void LoadData()
    {
        DataTable aDataTable = _setupDALss.Get_TADAList(param());
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

          }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
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


    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("TADAClaimView.aspx");
    }
}