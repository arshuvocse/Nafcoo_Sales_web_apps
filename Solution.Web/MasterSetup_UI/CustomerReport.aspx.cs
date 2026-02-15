using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using SalesSolution.Web.DataLayer;

public partial class MasterSetup_UI_CustomerReport : System.Web.UI.Page
{
    private static SeedDataDAL _seedRepo = new SeedDataDAL();

    static CommonDataLoad _dataLoad = new CommonDataLoad();

    private static CustomerInfoDAL _DAL = new CustomerInfoDAL();
    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InitailsLoad();

            btnSearch_Click(null, null);
        }
    }


    protected void InitailsLoad()
    {
        try
        {
            using (DataTable dt = _dataLoad.GetDivision_Active())
            {
                DivisionSelect.DataSource = dt;
                DivisionSelect.DataValueField = "DivisionId";
                DivisionSelect.DataTextField = "DivisionName";
                DivisionSelect.DataBind();
                DivisionSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                DivisionSelect.SelectedIndex = 0;
            }

            DivisionSelect.SelectedValue = 1.ToString();
            DivisionSelect_SelectedIndexChanged(null, null);
        }
        catch (Exception ex) { }
    }


    protected void DistrictSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ThanaSelect.Items.Clear();
        try
        {
            using (DataTable dt = _dataLoad.GetThana_ByDistrict_Active(Convert.ToInt32(DistrictSelect.SelectedValue)))
            {
                ThanaSelect.DataSource = dt;
                ThanaSelect.DataValueField = "ThanaId";
                ThanaSelect.DataTextField = "ThanaName";
                ThanaSelect.DataBind();
                ThanaSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ThanaSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }
    }


    protected void DivisionSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        DistrictSelect.Items.Clear();
        ThanaSelect.Items.Clear();
        try
        {
            using (DataTable dt = _dataLoad.GetDistrict_ByDivision_Active(Convert.ToInt32(DivisionSelect.SelectedValue)))
            {
                DistrictSelect.DataSource = dt;
                DistrictSelect.DataValueField = "DistrictId";
                DistrictSelect.DataTextField = "DistrictName";
                DistrictSelect.DataBind();
                DistrictSelect.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                DistrictSelect.SelectedIndex = 0;
            }


        }
        catch (Exception ex) { }

    }


    protected void custNameTextBox_TextChanged(object sender, EventArgs e)
    {



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
        DataTable aDataTable = _DAL.GetCustomerReport(parm);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
       // lblCount.Text = "Total : " + aDataTable.Rows.Count.ToString();
        //if (Session["RoleTypeId"].ToString()== "5" || Session["LoginName"].ToString()== "50639")
        //{
        //    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible = true;
        //}
        //else
        //{
        //    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible = false;
        //}
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {

            var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {

                string MId = e.CommandArgument.ToString();
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('CustomerEntry.aspx?mid=" + MId + "' ,'_blank');", true);

            }


        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData(Parm());
    }

    private string Parm()
    {
        string param = "";
  
        if (DivisionSelect.SelectedValue != "")
        {
            param = param + " AND DV.DivisionId='" + DivisionSelect.SelectedValue + "' ";
        }

        if (DistrictSelect.SelectedValue != "")
        {
            param = param + " AND DS.DistrictId='" + DistrictSelect.SelectedValue + "' ";
        }

        if (ThanaSelect.SelectedValue != "")
        {
            param = param + " AND TH.ThanaId='" + ThanaSelect.SelectedValue + "' ";
        }

        return param;
    }
    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadData(Parm());
    }
    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerReport.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Customer_Report_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.LoadData(Parm());

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
                    if (cell.Text.Contains(","))
                    {
                        sb.Append(String.Format("\"{0}\",", cell.Text));
                    }
                    else
                    { sb.Append(HttpUtility.HtmlDecode(cell.Text) + ','); }
                    //Append data with separator.

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

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}