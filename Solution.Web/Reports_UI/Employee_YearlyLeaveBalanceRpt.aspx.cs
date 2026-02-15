using Library.DAL.MasterSetup_DAL;
using Library.DAL.PromoAllocDAL;
using SalesSolution.Web.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_UI_Employee_YearlyLeaveBalanceRpt : System.Web.UI.Page
{


    public static GroupWisePromoQtyDAL aTargetDal = new GroupWisePromoQtyDAL();
    public static PromoMITagDAL aTargetDal2 = new PromoMITagDAL();
    private CommonDataLoad _dataLoad = new CommonDataLoad();
    private static SeedDataDAL _seedRepo = new SeedDataDAL();
    private static CmnCrystaltoView _RptDAL = new CmnCrystaltoView();

    private static CustomerInfoDAL _DAL = new CustomerInfoDAL(); 
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            LoadInitialInfo();

          //  btnSearch_Click(null, null);
        }
    }

    private void LoadInitialInfo()
    {

        try
        {
            using (DataTable dt = _dataLoad.GetEmployeeList_All())
            {
                ddlMIO.DataSource = dt;
                ddlMIO.DataValueField = "EmpInfoId";
                ddlMIO.DataTextField = "EmpName";
                ddlMIO.DataBind();
                ddlMIO.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlMIO.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }

       
        try
        {
            GetMonthList(ddlmonth);
            GetYearList(ddlYear);
        }

        catch (Exception ex) { }


        //for (int i = 2013; i <= 2020; i++)
        //{
        //    ddlYear.Items.Add(i.ToString());
        //}
        //ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected

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

    private void LoadData(string parm)
    {
        if ( ddlYear.SelectedValue != "")
        {
            DataTable aDataTable = _RptDAL.GetEmployee_YearlyLeaveBalanceRptList(parm);
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();

        }
        else
        {
            showMessageBox("Please Select Year !!");
            loadGridView.DataSource = null;
            loadGridView.DataBind();

        }

    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string unitPriceId = loadGridView.DataKeys[rowindex][0].ToString();

            //Response.Redirect("GroupWisePromoQtyEntry.aspx?MID=" + unitPriceId);
        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData(Parm());
    }

    private string Parm()
    {
        string param = "";

        if (ddlYear.SelectedValue != "")
        {
            param = param + " AND  FiscalYear='" + ddlYear.SelectedValue + "' ";
        }

      

       

        if (ddlMIO.SelectedValue != "")
        {
            param = param + " AND  EmployeeInfoId='" + ddlMIO.SelectedValue + "' ";
        }


        return param;
    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Employee_YearlyLeaveBalanceRpt.aspx");
    }
}