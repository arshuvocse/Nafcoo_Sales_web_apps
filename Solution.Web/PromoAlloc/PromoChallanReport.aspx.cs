using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;

public partial class PromoAlloc_PromoChallanReport : System.Web.UI.Page
{
    PromoMaterialReportDal aDal = new PromoMaterialReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDown();
            fromDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            this.masterButton_Click(null, null);
        }
    }


    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    public void LoadDropDown()
    {

        aDal.LoadSC(ddlUnit);
        LoadChallanDropdown();
    }

    private void LoadChallanDropdown()
    {
        ddlPromoChallan.Items.Clear();

        using (DataTable dt = aDal.LoadChallanReport(GenerateParameterForDropdown()))
        {
            ddlPromoChallan.DataSource = dt;
            ddlPromoChallan.DataValueField = "PromoChallanId";
            ddlPromoChallan.DataTextField = "PromoChallanCode";
            ddlPromoChallan.DataBind();
            ddlPromoChallan.Items.Insert(0, new ListItem("Select from list", String.Empty));
            ddlPromoChallan.SelectedIndex = 0;
        }
    }

    protected void masterButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.LoadPromoChallanReport(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {

            detailGridView.DataSource = null;
            detailGridView.DataBind();

            masterGridView.DataSource = aTable;
            masterGridView.DataBind();


            for (int i = 0; i < masterGridView.Rows.Count; i++)
            {
                string isForwardAble = masterGridView.DataKeys[i][1].ToString();
                LinkButton btnForward = (LinkButton)masterGridView.Rows[i].Cells[0].FindControl("btnForward");

                if (isForwardAble.Trim() != "Yes")
                {
                    btnForward.Visible = false;
                    btnForward.ToolTip = "Already Forwarded !!";
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F2E7FE");
                    masterGridView.Rows[i].BackColor = col;
                }
                else
                {
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F1C40F");
                    masterGridView.Rows[i].Cells[4].BackColor = col;
                    masterGridView.Rows[i].Cells[4].ForeColor = Color.White;
                    masterGridView.Rows[i].Cells[4].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
        else
        {
            detailGridView.DataSource = null;
            detailGridView.DataBind();


            masterGridView.DataSource = null;
            masterGridView.DataBind();
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Param"] = "";
        Session["Param"] = GenerateParameter();

        PopUp("QA");

    }

    private void PopUp(string rpt)
    {

        string url = "../FinishedGoodInventory_RPTView/ProductionReportViewer.aspx?rptType=" + rpt + "&rpt=" + 0;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

    }

    private string GenerateParameterForDropdown()
    {
        string pram = "";

        if (fromDateTextBox.Text.Trim() != "" && todateTextBox.Text.Trim() != "")
        {
            pram = pram + " AND CONVERT(date,M.ChallanDate) BETWEEN '" + fromDateTextBox.Text.Trim() + "' AND '" + todateTextBox.Text.Trim() + "'";
        }

        if (fromDateTextBox.Text.Trim() != "" && todateTextBox.Text.Trim() == "")
        {
            pram = pram + "  AND CONVERT(date,M.ChallanDate) => '" + fromDateTextBox.Text.Trim() + "'";
        }

        if (fromDateTextBox.Text.Trim() == "" && todateTextBox.Text.Trim() != "")
        {
            pram = pram + " AND CONVERT(date,M.ChallanDate) <= '" + todateTextBox.Text.Trim() + "'";
        }

        if (ddlUnit.SelectedValue != "")
        {
            pram = pram + " AND M.ComUnitId = '" + ddlUnit.SelectedValue + "'";
        }

        return pram;
    }


    private string GenerateParameter()
    {
        string pram = "";

        if (fromDateTextBox.Text.Trim() != "" && todateTextBox.Text.Trim() != "")
        {
            pram = pram + " AND CONVERT(date,M.ChallanDate) BETWEEN '" + fromDateTextBox.Text.Trim() + "' AND '" + todateTextBox.Text.Trim() + "'";
        }

        if (fromDateTextBox.Text.Trim() != "" && todateTextBox.Text.Trim() == "")
        {
            pram = pram + "  AND CONVERT(date,M.ChallanDate) => '" + fromDateTextBox.Text.Trim() + "'";
        }

        if (fromDateTextBox.Text.Trim() == "" && todateTextBox.Text.Trim() != "")
        {
            pram = pram + " AND CONVERT(date,M.ChallanDate) <= '" + todateTextBox.Text.Trim() + "'";
        }

        if (ddlUnit.SelectedValue != "")
        {
            pram = pram + " AND M.ComUnitId = '" + ddlUnit.SelectedValue + "'";
        }

        if (ddlPromoChallan.SelectedValue != "")
        {
            pram = pram + " AND M.PromoChallanId = '" + ddlPromoChallan.SelectedValue + "'";
        }

        return pram;
    }


    private void PopupReport(int masterId)
    {
        string pram = "";
        if (masterId.ToString(CultureInfo.InvariantCulture) != "")
        {
            pram = pram + " AND M.IssueMasterId = " + masterId;
        }

        string url = "../FinishedGoodInventory_RPTView/FinishedGoodPackegingReportViewer.aspx?rpt=" + pram;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void detailButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetChallanDetailList(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {
            masterGridView.DataSource = null;
            masterGridView.DataBind();

            detailGridView.DataSource = aTable;
            detailGridView.DataBind();

        }
        else
        {
            detailGridView.DataSource = null;
            detailGridView.DataBind();

            masterGridView.DataSource = null;
            masterGridView.DataBind();
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (masterGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Packing_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
            Response.Charset = "";
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Default;
            //To Export all pages.
            masterGridView.AllowPaging = false;
            this.masterButton_Click(null, null);

            StringBuilder sb = new StringBuilder();
            foreach (TableCell cell in masterGridView.HeaderRow.Cells)
            {
                //Append data with separator.
                sb.Append(HttpUtility.HtmlDecode(cell.Text) + ',');
            }
            //Append new line character.
            sb.Append("\r\n");

            foreach (GridViewRow row in masterGridView.Rows)
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
            Response.AddHeader("content-disposition", "attachment;filename=Challan_report_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
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

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        //confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromoChallanReport.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        masterGridView.PageIndex = e.NewPageIndex;
        this.masterButton_Click(null, null);
    }

    protected void loadGridView_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {
        detailGridView.PageIndex = e.NewPageIndex;
        this.detailButton_Click(null, null);
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

    protected void btnReport_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;


        string masterId = masterGridView.DataKeys[rowIndex][0].ToString();

        if (masterId != null)
        {
            PopupReport(Convert.ToInt32(masterId));
        }
    }

    protected void editImageButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        string masterId = masterGridView.DataKeys[rowIndex][0].ToString();

        if (masterId != null)
        {
            if (aDal.ForwardChallanList(masterId))
            {
                ShowMessageBox("Forwarded successfully !!!");
                masterButton_Click(null, null);
            }
        }

    }

    protected void fromDateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        LoadChallanDropdown();
    }

    protected void todateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        LoadChallanDropdown();
    }

    protected void ddlUnit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadChallanDropdown();
    }
}