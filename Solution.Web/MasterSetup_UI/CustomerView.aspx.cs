using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library.DAL.DoctorModule_DAL;
using Library.DAL.MasterSetup_DAL;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;
public partial class MasterSetup_UI_CustomerView : System.Web.UI.Page
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
            //frmDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            //toDate.Text = DateTime.Now.ToString("dd MMMM, yyyy");

            try
            {
                using (DataTable dt = _seedRepo.GetChemistTypeList())
                {
                    ddlChemisType.DataSource = dt;
                    ddlChemisType.DataValueField = "CustomerTypeId";
                    ddlChemisType.DataTextField = "CustomerType";
                    ddlChemisType.DataBind();
                    ddlChemisType.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                    ddlChemisType.SelectedIndex = 0;
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

            try
            {
                using (DataTable dt = _seedRepo.GetProgramTypeList())
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


            btnSearch_Click(null, null);
        }
    }
    protected void custNameTextBox_TextChanged(object sender, EventArgs e)
    {


        string empName = custNameTextBox.Text.Trim();

        if (empName != "")
        {
            try
            {
                if (empName.Contains(':'))
                {
                    string[] emp = empName.Split('|');

                    hfCustomerId.Value = emp[1].Trim();
                    custNameTextBox.Text = emp[0].Trim();



                }
                else
                {

                    //custNameTextBox.Text = "";
                    hfCustomerId.Value = "";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Input Correct Data !" + "','Faild');", true);


                }

            }
            catch
            {
                hfCustomerId.Value = "";
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
        Response.Redirect("CustomerEntry.aspx");
    }

    private void LoadData(string parm)
    {
        DataTable aDataTable = _DAL.GetCustomerList(parm);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
        lblCount.Text = "Total : " + aDataTable.Rows.Count.ToString();
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


        if (ddlDistributionCenter.SelectedValue != "")
        {
            param = param + " AND    dcMas.DCId='" + ddlDistributionCenter.SelectedValue + "' ";
        }

        if (GroupSelect.SelectedValue != "")
        {
            param = param + " AND gr.GroupId='" + GroupSelect.SelectedValue + "' ";
        }

        if (ZoneSelect.SelectedValue != "")
        {
            param = param + " AND rg.RegionId='" + ZoneSelect.SelectedValue + "' ";
        }

        if (AreaSelect.SelectedValue != "")
        {
            param = param + " AND Ar.AreaId='" + AreaSelect.SelectedValue + "' ";
        }

        if (TeritorySelect.SelectedValue != "")
        {
            param = param + " AND Tr.TerritoryId='" + TeritorySelect.SelectedValue + "' ";
        }

        if (SubTeritory.SelectedValue != "")
        {
            param = param + " AND subTr.SubTerritoryId='" + SubTeritory.SelectedValue + "' ";
        }

        if (ddlProgramType.SelectedValue != "")
        {
            param = param + " AND mas.ProgramTypeId='" + ddlProgramType.SelectedValue + "' ";
        }
        if (ddlChemisType.SelectedValue != "")
        {
            param = param + " AND mas.CustomerTypeId='" + ddlChemisType.SelectedValue + "' ";
        }

        if (hfCustomerId.Value != "")
        {
            param = param + " AND mas.CustomerMasterId='" + hfCustomerId.Value + "' ";
        }
        else
        {

            if(custNameTextBox.Text != "") {
                param = param + " AND   ( ISNULL(mas.CustomerCode+' : ','') +ISNULL( + case when mas.IsActive=1 then  mas.CustomerName else mas.CustomerName +' (Inactive) ' end ,'') +ISNULL(' : '+mas.CellNo,'') like '%" + custNameTextBox.Text.Trim() + "%' )";
                    }
        }

        if (ddlApprovalStatus.SelectedValue != "")
        {
            param = param + " AND mas.ActionStatus='" + ddlApprovalStatus.SelectedValue + "' ";
        }
        if (ddlStatus.SelectedValue != "")
        {
            param = param + " AND mas.IsActive=" + ddlStatus.SelectedValue + " ";
        }
        if ( frmDate.Text != "" && toDate.Text != "") {
            param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + toDate.Text + "' ";
        }
        if (frmDate.Text != "" && toDate.Text == "") {
            param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + DateTime.Now + "' ";
        }

        if (frmDate.Text != "" && toDate.Text == "") {
            param = param + " AND CONVERT(date,mas.CreateDate)  BETWEEN '" + frmDate.Text+ "' AND '" + DateTime.Now + "' ";
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
        Response.Redirect("CustomerView.aspx");
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
            Response.AddHeader("content-disposition", "attachment;filename=Customer_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
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