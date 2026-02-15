using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using DataTable = System.Data.DataTable;
using Library.DAL.TargetDAL;

public partial class Target_UI_TargetSchemeView : System.Web.UI.Page
{

    //private QuotedPriceSetupDAL _Dal = new QuotedPriceSetupDAL();
    private CampaignSetupDal _Dal = new CampaignSetupDal();
    private TargetSchemaDAL _tDal = new TargetSchemaDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownlist();
        }
    }

    protected void LoadDropdownlist()
    {
        _tDal.GetSchemaDropDown(schemaDropdown);
    }

    public void schemaDropdown_IndexChange(object sender, EventArgs e)
    {
        GetTargetSchemaViewData();
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void GetTargetSchemaViewData()
    {
        DataTable aTable = new DataTable();

        //aTable = _Dal.GetTargetSchemaViewData(Pram());
        aTable = _tDal.GetTargetSchemaData(Pram());

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

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.GetTargetSchemaViewData();
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


    private String Pram()
    {
        string param = "";

        if (schemaDropdown.SelectedValue != "")
        {
            param = param + "TRM.SchemaMasterId =" + schemaDropdown.SelectedValue;
        }

        return param;

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Quoted_Price_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            loadGridView.AllowPaging = false;
            this.GetTargetSchemaViewData();

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

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        GetTargetSchemaViewData();
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SpecialDiscountView.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("TargetSchema.aspx");
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string SchemaMasterId = loadGridView.DataKeys[rowindex][0].ToString();
            // ✅ Redirect to the target page
            Response.Redirect("TargetSchema.aspx?SchemaMasterId=" + SchemaMasterId);
        }

    }
}