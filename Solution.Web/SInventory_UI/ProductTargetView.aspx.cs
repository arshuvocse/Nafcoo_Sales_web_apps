using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_ProductTargetView : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    ProductTargetViewDAL aProductTargetViewDAL = new ProductTargetViewDAL();
    ProductTargetDAL aProductTargetDAL = new ProductTargetDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownList();
            LoadProductTargetMaster();
        }
    }

    private void LoadDropdownList()
    {
        aProductTargetDAL.LoadExistingCategory(ddlSearchCategory);
    }

    private void LoadProductTargetMaster()
    {

        aDataTable = aProductTargetViewDAL.LoadProductTargetView(GenerateParameter());

        loadGridView.DataSource = null;
        loadGridView.DataBind();

        if (aDataTable.Rows.Count > 0)
        {

            detailGridView.DataSource = null;
            detailGridView.DataBind();

            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
        }
        
    }

    private String GenerateParameter()
    {
        string pram = "";

        if (ddlSearchCategory.SelectedValue != "")
        {
            pram = pram + " AND M.TargetId = '" + ddlSearchCategory.SelectedValue + "'";
        }

        return pram;
    }

    private void ProductTargetEdit_Click(string Id)
    {
        Response.Redirect("ProductTarget.aspx?ID=" + Id);
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
            string TargetId = loadGridView.DataKeys[rowindex][0].ToString();
            ProductTargetEdit_Click(TargetId);
            //Session["DAEdit"] = e.CommandArgument.ToString();
            //Response.Redirect("ProductTarget.aspx");
        }

    }

    protected void masterButton_Click(object sender, EventArgs e)
    {
        LoadProductTargetMaster();
    }

    protected void detailButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();

        aTable = aProductTargetViewDAL.LoadProductTargetDetailView(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();

            detailGridView.DataSource = aTable;
            detailGridView.DataBind();

        }
        else
        {
            detailGridView.DataSource = null;
            detailGridView.DataBind();

            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductTargetView.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Product_Target_Category_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.masterButton_Click(null, null);

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

        else if (detailGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Product_Target_Category_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            detailGridView.AllowPaging = false;
            this.detailButton_Click(null, null);

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in detailGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in detailGridView.Rows)
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

    protected void gv_DocumentUpload_PreRender2(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void loadGridView_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {
        detailGridView.PageIndex = e.NewPageIndex;
        this.detailButton_Click(null, null);
    }
}