using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared.Json;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_PromoMaterialsReport : System.Web.UI.Page
{
    PromoMaterialReportDal aDal = new PromoMaterialReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDown();
            masterButton_Click(null,null);
        }
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

        for (i = 2015; i <= 2050; i++)
        {
            ddl.Items.Add(i.ToString());
            ddl.Items.FindByValue(System.DateTime.Now.Year.ToString());
        }
        string strYear = System.DateTime.Now.Year.ToString();

        ddl.SelectedValue = strYear;


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
        GetYearList(ddlYear);
        GetMonthList(ddlmonth);

        //using (DataTable dt = aDal.LoadFabricInfo())
        //{
        //    ddlFabricInfo.DataSource = dt;
        //    ddlFabricInfo.DataValueField = "FabricName";
        //    ddlFabricInfo.DataTextField = "FabricName";
        //    ddlFabricInfo.DataBind();
        //    ddlFabricInfo.Items.Insert(0, new ListItem("Select from list", String.Empty));
        //    ddlFabricInfo.SelectedIndex = 0;
        //}

        //aDal.LoadShift(ddlShift);

        //using (DataTable dt = aDal.LoadShadeGradePlan())
        //{
        //    ddlShadeGradePlan.DataSource = dt;
        //    ddlShadeGradePlan.DataValueField = "ShadeGradePlanId";
        //    ddlShadeGradePlan.DataTextField = "PlanCode";
        //    ddlShadeGradePlan.DataBind();
        //    ddlShadeGradePlan.Items.Insert(0, new ListItem("Select from list", String.Empty));
        //    ddlShadeGradePlan.SelectedIndex = 0;
        //}

    }
    protected void masterButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetPromoMaterialSummery(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {

            detailGridView.DataSource = null;
            detailGridView.DataBind();

            divReport.Visible = false;

            masterGridView.DataSource = aTable;
            masterGridView.DataBind();



        }
        else
        {
            detailGridView.DataSource = null;
            detailGridView.DataBind();

            divReport.Visible = false;

            masterGridView.DataSource = null;
            masterGridView.DataBind();
        }

    }


    private string GenerateParameter()
    {
        string pram = "";



        if (ddlUnit.SelectedValue != "")
        {
            pram = pram + " AND UNT.ComUnitId = '" + ddlUnit.SelectedValue + "'";
        }

        if (ddlYear.SelectedValue != "")
        {
            pram = pram + " AND Year = '" + ddlYear.SelectedValue + "'";
        }

        if (ddlmonth.SelectedValue != "")
        {
            pram = pram + " AND Month = '" + ddlmonth.SelectedValue + "'";
        }

        

        return pram;
    }


    private void PopupReport(int masterId)
    {
        string pram = "";
        if (masterId.ToString() != "")
        {
            pram = pram + " AND QARM.StockReceiveMasterId = " + masterId;
        }
        string url = "../FinishedGoodInventory_RPTView/FinishedGoodReportViewer.aspx?rptType=QASR&rpt=" + pram;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void detailButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetDetail(GenerateParameter());

        if (aTable.Rows.Count > 0)
        {
            masterGridView.DataSource = null;
            masterGridView.DataBind();

            divReport.Visible = true;

            detailGridView.DataSource = aTable;
            detailGridView.DataBind();

            for (int i = 0; i < detailGridView.Rows.Count; i++)
            {
                string isForwardAble = detailGridView.DataKeys[i][1].ToString();
                CheckBox btnForward = (CheckBox)detailGridView.Rows[i].Cells[0].FindControl("chkSelect");

                if (isForwardAble.Trim() != "Yes")
                {
                    btnForward.Visible = false;
                    btnForward.ToolTip = "Already Forwarded !!";
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#F2E7FE");
                    detailGridView.Rows[i].BackColor = col;
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


    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (masterGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Promo_Material_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
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
            Response.AddHeader("content-disposition", "attachment;filename=Packing_Detail_List_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".csv");
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
        Response.Redirect("PromoMaterialsReport.aspx");
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
        //int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        //string masterId = masterGridView.DataKeys[rowIndex][0].ToString();

        //if (masterId != null)
        //{
        //    if (aDal.ForwardPackingList(masterId))
        //    {    
        //        ShowMessageBox("Forwarded successfully !!!");
        //        masterButton_Click(null, null);
        //    }
        //}

    }

    protected void btnProductionReport_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (SaveChanges() > 0)
            {

                ShowMessageBox("Challan Generated Successfully !!");
                cancelButton_Click(null, null);
            }

        }
    }

    private Int32 SaveChanges()
    {
        Int32 retVal;
        try
        {
            retVal = aDal.SaveProductionReport(PrepareMasterDataForSave(), PrepareDetailsDataForSave());
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }
        return retVal;
    }

    private List<PromoChallanDetailsDao> PrepareDetailsDataForSave()
    {

        List<PromoChallanDetailsDao> alist = new List<PromoChallanDetailsDao>();
        PromoChallanDetailsDao ashade;

        for (int i = 0; i < detailGridView.Rows.Count; i++)
        {
            var check = (CheckBox)detailGridView.Rows[i].FindControl("chkSelect");

            if (check.Checked)
            {
                ashade = new PromoChallanDetailsDao();
                ashade.GWPromoQtyId = int.Parse(detailGridView.DataKeys[i][0].ToString());
                alist.Add(ashade);
            }
        }

        return alist;

    }

    private PromoChallanMasterDao PrepareMasterDataForSave()
    {
        PromoChallanMasterDao aDao = new PromoChallanMasterDao();

        aDao.ChallanBy = Convert.ToInt32(Session["UserId"].ToString());
        aDao.ComUnitId = Convert.ToInt32(ddlUnit.SelectedValue);
        aDao.ChallanDate = DateTime.Today;

        return aDao;
    }

    private bool Validation()
    {

        if (ddlUnit.SelectedValue == "")
        {
            ddlUnit.Focus();
            ShowMessageBox("Please select Depot !!!");
            return false;
        }

        int count = 0;

        for (int i = 0; i < detailGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)detailGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }

        }

        if (count == 0)
        {
            ShowMessageBox("Please select at least one item !!!");
            return false;
        }

        

        return true;
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)detailGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < detailGridView.Rows.Count; i++)
        {
            CheckBox ChkBoxRows = (CheckBox)detailGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    protected void ddlUnit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int dCount = 0;
        dCount = detailGridView.Rows.Count;

        int mCount = 0;
        mCount = masterGridView.Rows.Count;

        if (dCount > 0)
        {
            detailButton_Click(null, null);
        }
        else
        {
            masterButton_Click(null, null);
        }
    }
}