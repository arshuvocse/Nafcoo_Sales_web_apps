using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.DoctorVisit_DAL;
using SalesSolution.Web.DataLayer;

public partial class DoctorVisit_UI_NewDCRReport : System.Web.UI.Page
{
    static CommonDataLoad _dataLoad = new CommonDataLoad();
    public static SetupDAL _setupDAL = new SetupDAL();
    public static DoctorVisitDAL _DoctorVisit_DAL = new DoctorVisitDAL();

    string RoleTypeName = "";
    string EmpInfoId = "";
    string ToRoleTypeId = "";
    string ApprovalStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FromDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            ToDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropdownList();
            LoadDcrReport();
        }
    }

    private void LoadDcrReport()
    {
        DataTable aDataTable = _DoctorVisit_DAL.GetDCRReport(param());
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    private string param()
    {
        var param = "  ";

        if (FromDate.Text != "" && ToDate.Text != "")
        {
            param = param + " AND CONVERT(date,A.DcrDate)  BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ";
        }
        if (FromDate.Text != "" && ToDate.Text == "")
        {
            param = param + " AND CONVERT(date,A.DcrDate)  BETWEEN '" + FromDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }


        if (UserRoleSelect.SelectedValue != "")
        {

            param = param + " AND USR.UserRoleID ='" + UserRoleSelect.SelectedValue + "'";

        }

        if (EmployeeIdSelect.SelectedValue != "")
        {

            param = param + " AND tblEmpGeneralInfo.EmpInfoId ='" + EmployeeIdSelect.SelectedValue + "'";

        }

        return param;
    }

    private void LoadDropdownList()
    {
        try
        {
            using (DataTable dt = _dataLoad.GetEmployeeList_Active())
            {
                EmployeeIdSelect.DataSource = dt;
                EmployeeIdSelect.DataValueField = "EmpInfoId";
                EmployeeIdSelect.DataTextField = "EmpName";
                EmployeeIdSelect.DataBind();
                EmployeeIdSelect.Items.Insert(0, new ListItem("Select From List", String.Empty));
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
                UserRoleSelect.Items.Insert(0, new ListItem("Select From List", String.Empty));
                UserRoleSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }
    }


    protected void SearchButton_Click(object sender, EventArgs e)
    {
       LoadDcrReport();
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewDCRReport.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Mio_Wise_Target_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.LoadDcrReport();

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in loadGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in loadGridView.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    //Append data with separator.
                    if (cell.Text.Contains(","))
                    {
                        sb.Append(String.Format("\"{0}\",", cell.Text));
                    }
                    else
                    { sb.Append(HttpUtility.HtmlDecode(cell.Text) + ','); }
                }
                //Append new line character.
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "No Data Found!" + "','Faild');", true);

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

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadDcrReport();
    }
}