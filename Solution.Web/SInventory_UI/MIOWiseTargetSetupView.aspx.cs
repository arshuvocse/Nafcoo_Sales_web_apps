using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.SInventory_DAL;
using System.Data;
using System.Data;
using System.Data;
using DataTable = System.Data.DataTable;

public partial class SInventory_UI_MIOWiseTargetSetupView : System.Web.UI.Page
{
    MIOWiseTargetSetupDal aSetupDal = new MIOWiseTargetSetupDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            LoadData();
        }
    }

    private void LoadDropDownList()
    {
        try
        {
            aSetupDal.LoadTargetCategory(ddlCategory);
        }
        catch { }

        GetYearList(ddlYear);
        GetMonthList(periodDropDownList);
    }

    public void GetMonthList(DropDownList ddl)
    {
        DateTime month = Convert.ToDateTime(DateTime.Now);
        for (int i = 0; i < 12; i++)
        {
            DateTime NextMont = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = NextMont.ToString("MMMM");
            list.Value = NextMont.ToString("MMMM");
            ddl.Items.Add(list);
        }

        var a = DateTime.Now.Month.ToString();
        //ddl.Items.Insert(0, "Select Month");
        ddl.Items.FindByValue(DateTime.Now.ToString("MMMM")).Selected = true;
    }

    public void GetYearList(DropDownList ddl)
    {
        int i;

        for (i = 2021; i <= 2050; i++)
        {
            ddl.Items.Add(i.ToString());
            ddl.Items.FindByValue(System.DateTime.Now.Year.ToString());
        }
        string strYear = System.DateTime.Now.Year.ToString();

        ddl.SelectedValue = strYear;


    }

    private void LoadData()
    {
        var aTable = new DataTable();

        aTable = aSetupDal.GetMIOTarget(GenerateParam());

        if (aTable.Rows.Count > 0)
        {
            loadGridView.DataSource = aTable;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    private String GenerateParam()
    {
        string pram = "";

        if (ddlYear.SelectedValue != "")
        {
            pram = pram + " AND M.Year = '" + ddlYear.SelectedItem.Text.Trim() + "'";
        }

        if (periodDropDownList.SelectedValue != "")
        {
            pram = pram + " AND M.Month = '" + periodDropDownList.SelectedItem.Text.Trim() + "'";
        }

        if (ddlCategory.SelectedValue != "")
        {
            pram = pram + " AND D.TargetCategoryId = '" + ddlCategory.SelectedValue + "'";
        }

        return pram;
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        LoadData();
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
        this.LoadData();
    }

    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MIOWiseTargetSetup.aspx");
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
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
            this.LoadData();

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

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MIOWiseTargetSetupView.aspx");
    }

}