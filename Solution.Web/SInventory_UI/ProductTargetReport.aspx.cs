using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SalesSolution.Web.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;


public partial class SInventory_UI_ProductTargetReport : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    ProductTargetDAL aProductTargetDAL = new ProductTargetDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadProductTargetReport();
            TargetCategoryDropdownList();
        }
    }
    protected void TargetCategoryDropDownList_OnTextChanged(object sender, EventArgs e)
    {
        LoadProductTargetReport();
    }
    
    private void TargetCategoryDropdownList()
    {
        aProductTargetDAL.LoadTargetCategory(TargetCategoryDropDownList);
    }
    private void LoadProductTargetReport()
    {
        //aDataTable = aProductTargetDAL.LoadProductDetailsReport();
        //loadGridView.DataSource = aDataTable;
        //loadGridView.DataBind();

        aDataTable = aProductTargetDAL.LoadProductDetailsReportById(GenerateParameter());

        if (aDataTable.Rows.Count > 0)
        {
            
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    public string GenerateParameter()
    {
        string param = "";

        if (TargetCategoryDropDownList.SelectedValue != "")
        {
            param = param + " AND m.TargetId = " + TargetCategoryDropDownList.SelectedValue;
        }


        return param;
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
        // if (e.CommandName == "EditData")
        // {
        //     Session["DAEdit"] = e.CommandArgument.ToString();
        //     Response.Redirect("DASetup.aspx");
        // }

    }
}