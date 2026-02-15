using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Library.DAL;
using SalesSolution.Web.DataLayer;

public partial class SInventory_UI_MBESetupNewView : System.Web.UI.Page
{

    MBESetupDal aDal = new MBESetupDal();
    CommonDataLoad aCommonDataLoad = new CommonDataLoad();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownList();
            this.LoadInitialGrid(itemsGridView);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (itemsGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=MBE_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            itemsGridView.AllowPaging = false;
            LoadInitialGrid(itemsGridView);

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in itemsGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in itemsGridView.Rows)
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

        //if (loadGridView.Rows.Count > 0)
        //{
        //    DataTable dt = new DataTable("GridView_Data");
        //    foreach (TableCell cell in loadGridView.HeaderRow.Cells)
        //    {
        //        dt.Columns.Add(cell.Text);
        //    }
        //    loadGridView.AllowPaging = false;
        //    this.LoadData();
        //    foreach (GridViewRow row in loadGridView.Rows)
        //    {
        //        dt.Rows.Add();
        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {
        //            if (row.Cells[i].Controls.Count > 0)
        //            {
        //                dt.Rows[dt.Rows.Count - 1][i] = (row.Cells[i].Controls[1] as Label).Text;
        //            }
        //            else
        //            {
        //                dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
        //            }
        //        }
        //    }
        //    loadGridView.AllowPaging = false;
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report_List (Date Range= " + InvoiceDateTextBox.Text + "-" + todateTextBox.Text + ").xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}

        //if (loadGridView.Rows.Count > 0)
        //{


        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice_Report_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls"));
        //    Response.ContentType = "application/ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    loadGridView.AllowPaging = false;

        //    this.LoadData();
        //    //Change the Header Row back to white color
        //    loadGridView.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //    //Applying stlye to gridview header cells
        //    //for (int i = 0; i < loadGridView.HeaderRow.Cells.Count; i++)
        //    //{
        //    //    loadGridView.HeaderRow.Cells[i].Style.Add("background-color", "#8BA8E0");
        //    //}
        //    //int j = 1;
        //    ////This loop is used to apply stlye to cells based on particular row
        //    //foreach (GridViewRow gvrow in loadGridView.Rows)
        //    //{
        //    //    gvrow.BackColor = Color.White;
        //    //    if (j <= loadGridView.Rows.Count)
        //    //    {
        //    //        if (j % 2 != 0)
        //    //        {
        //    //            for (int k = 0; k < gvrow.Cells.Count; k++)
        //    //            {
        //    //                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
        //    //            }
        //    //        }
        //    //    }
        //    //    j++;
        //    //}

        //    string headerTable = @"<span  style='text-align:center'><h3>  Invoice Report   (Date Range : " + InvoiceDateTextBox.Text + "- " + todateTextBox.Text + ") </h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("MMMM dd, yyyy") + "</h4></span>";

        //    HttpContext.Current.Response.Write(headerTable);

        //    loadGridView.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
        //else
        //{
        //    showMessageBox("No Data Found!!");
        //}
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


    private void LoadDropdownList()
    {

        LoadGroup(ddlGroup);
        //LoadMbeEmployee(ddlMbe);

    }

    private void LoadMbeEmployee(DropDownList ddl)
    {
        DataTable aDataTable = aDal.GetEmployee_AllFieldForceEmployeeList();

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "EmployeeName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "EmpInfoId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }
    }

    private void LoadGroup(DropDownList ddl)
    {
        DataTable aDataTable = aCommonDataLoad.GetGroupInfo_All();

        ddl.Items.Clear();

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "GroupName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "GroupId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    // Group wise Zone

    protected void ddlGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        ddlZone.Items.Clear();

        if (ddlGroup.SelectedValue != "0")
        {
            LoadZone(ddlZone, Convert.ToInt32(ddlGroup.SelectedValue));
        }
    }


    private void LoadZone(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aCommonDataLoad.GetZone_byGroupId_All(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "RegionName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "RegionId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Zone wise Area

    protected void ddlZone_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        ddlArea.Items.Clear();

        if (ddlZone.SelectedValue != "0")
        {
            LoadArea(ddlArea, Convert.ToInt32(ddlZone.SelectedValue));
        }
    }


    private void LoadArea(DropDownList ddl, int groupId)
    {
        DataTable aDataTable = aCommonDataLoad.GetArea_ByZoneId_All(groupId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "AreaName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "AreaId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    // Area wise Terittory

    protected void ddlArea_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTerritory.Items.Clear();

        if (ddlArea.SelectedValue != "0")
        {
            LoadTerritory(ddlTerritory, Convert.ToInt32(ddlArea.SelectedValue));
        }
    }


    private void LoadTerritory(DropDownList ddl, int areaId)
    {
        DataTable aDataTable = aDal.Get_VacentTerritory(areaId);


        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "TerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "TerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }

    // Territory All


    protected void ddlTerritory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubTerritory.Items.Clear();

        if (ddlTerritory.SelectedValue != "0")
        {
            LoadSubTerritory(ddlSubTerritory, Convert.ToInt32(ddlTerritory.SelectedValue));
        }
    }

    private void LoadSubTerritory(DropDownList ddl, int territoryId)
    {
        DataTable aDataTable = aCommonDataLoad.GetSubTerritory_ByTerritoryId_Active(territoryId);

        if (aDataTable != null && aDataTable.Rows.Count > 0)
        {
            ddl.DataSource = aDataTable;
            ddl.DataTextField = "SubTerritoryName";  // The column you want to display in the dropdown.
            ddl.DataValueField = "SubTerritoryId";   // The column that represents the value.
            ddl.DataBind();

            // Optionally add a default item.

            ddl.Items.Insert(0, new ListItem("Select from list", "0"));

        }

    }


    private void LoadInitialGrid(GridView gridView)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.Get_MBEInfo(GenerateParam());

        if (aTable.Rows.Count > 0)
        {
            gridView.DataSource = aTable;
            gridView.DataBind();


            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                string activeStatus = gridView.DataKeys[i][1].ToString();

                if (activeStatus == "Inactive")
                {
                    ImageButton editImageButton = ((ImageButton)gridView.Rows[i].Cells[0].FindControl("editImageButton"));

                    editImageButton.Visible = false;
                }
            }


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

        // Check if Zone is selected and not equal to "0"
        if (!string.IsNullOrEmpty(ddlZone.SelectedValue) && ddlZone.SelectedValue != "0")
        {
            param.Append(" AND RGN.RegionId = ").Append(ddlZone.SelectedValue);
        }

        // Check if Area is selected and not equal to "0"
        if (!string.IsNullOrEmpty(ddlArea.SelectedValue) && ddlArea.SelectedValue != "0")
        {
            param.Append(" AND ARA.AreaId = ").Append(ddlArea.SelectedValue);
        }

        // Check if Territory is selected and not equal to "0"
        if (!string.IsNullOrEmpty(ddlTerritory.SelectedValue) && ddlTerritory.SelectedValue != "0")
        {
            param.Append(" AND TTR.TerritoryId = ").Append(ddlTerritory.SelectedValue);
        }

        // Check if Active Status is selected and not empty
        if (!string.IsNullOrEmpty(ddlActiveStatus.SelectedValue) && ddlActiveStatus.SelectedValue != "0")
        {
            param.Append(" AND MBE.IsActive = ").Append(ddlActiveStatus.SelectedValue);
        }

        // Check if search text is provided
        if (!string.IsNullOrWhiteSpace(tbxSearch.Text))
        {
            string searchTerm = tbxSearch.Text.Trim();
            param.Append(" AND (")
                 .Append("RGN.RegionName LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR RGN.RegionCode LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR ARA.AreaCode LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR ARA.AreaName LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR TTR.TerritoryCode LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR TTR.TerritoryName LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR EGI.EmpMasterCode LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR EGI.EmpName LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR Sub.SubTerritoryCode LIKE '%").Append(searchTerm).Append("%' ")
                 .Append("OR Sub.SubTerritoryName LIKE '%").Append(searchTerm).Append("%')");
        }

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
            Session["MBEId"] = e.CommandArgument.ToString();
            Response.Redirect("MBESetupNew.aspx");
        }

    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void HyperLink1_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        LoadInitialGrid(itemsGridView);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
       Response.Redirect("MBESetupNewView.aspx");
    }
}