using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

public partial class SInventory_UI_TopSheetGenerateByRoute : System.Web.UI.Page
{
    OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
    InvoiceBLL aInvoiceBll = new InvoiceBLL();
    TopSheetGenerateByRouteDal aDal = new TopSheetGenerateByRouteDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DropDownlist();

                if (Session["TopSheetId"] != null)
                {
                    masterHiddenFieldId.Value = Session["TopSheetId"].ToString();
                    GetOneRecord(Convert.ToInt32(masterHiddenFieldId.Value));
                    Session["TopSheetId"] = null;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void GetOneRecord(Int32 masterId)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetTopSheetById(masterId);

        if (aTable.Rows.Count > 0)
        {
            ddlDa.SelectedValue = DBNull.Value.Equals(aTable.Rows[0]["DAId"]) ? "0" : aTable.Rows[0].Field<Int32>("DAId").ToString();

            chalanGridView.DataSource = aTable;
            chalanGridView.DataBind();
        }
        else
        {
            chalanGridView.DataSource = null;
            chalanGridView.DataBind();
        }
    }

    public string GenerateParameter()
    {
        string pram = @" WHERE IV.InvoiceNo IN (SELECT InvoiceNo FROM dbo.tblInvoiceBatch
        LEFT JOIN dbo.tblInvoice ON tblInvoice.InvoiceId = tblInvoiceBatch.InvoiceId
        WHERE BatchNo = '')";
        return pram;
    }
    protected void viewRptButton_Click(object sender, EventArgs e)
    {

        string pram = "";
        pram = GenerateParameter();

        Session["paydetailId"] = "";
        Session["paydetailId"] = pram;


        string url = "../SInventory_RPTVIEW/ProformaReportPrintViewer.aspx";
        // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void invoiceButton_Click(object sender, EventArgs e)
    {

        try
        {
            string batchn = "";
            //ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();

            batchn = (aOrderInfoBll.LoadInvoiceBatchId().Rows[0][0].ToString());
            batchn = rootDropDownList.SelectedValue + "-" + batchn;
            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)orderGridView.Rows[i].FindControl("chkSelect");
                if (cb.Checked)
                {
                    Int32 orderId = Convert.ToInt32(orderGridView.DataKeys[i]["OrderId"].ToString());
                    aOrderInfoBll.GenerateInvoiceByOrderId(orderId, Convert.ToInt32(Session["UserId"].ToString()), batchn);
                }

                //batchno.Text = batchn.ToString();
            }

            if (Session["MarketId"] != null)
            {
                marketDropDownList.SelectedValue = Session["MarketId"].ToString();
                GridView();

            }
            ShowMessageBox("Invoice Generated Successfully.");
        }
        catch (Exception ex)
        {

        }

    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)orderGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[6].FindControl("chkSelect");
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

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceCreationByOrder.aspx");
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

    protected void rootDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["RouteId"] = rootDropDownList.SelectedValue;
        }
        catch (Exception ex)
        {

        }
    }

    public void DropDownlist()
    {
        try
        {
            aOrderInfoBll.LoadSC(salesCenterDropDownList, Session["UserId"].ToString());
            aOrderInfoBll.LoadManufac(manufacDropDownList);
          
            // aOrderInfoBll.LoadDisRoute(rootDropDownList);
            manufacDropDownList.SelectedIndex = 1;
            salesCenterDropDownList.SelectedIndex = 1;
            salesCenterDropDownList_SelectedIndexChanged(null, null);

            try
            {
                rootDropDownList.SelectedValue = Session["RouteId"].ToString();

                GridView();
            }
            catch (Exception ex)
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void salesCenterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aDal.LoadDisRouteforInvoice(rootDropDownList, Convert.ToInt32(salesCenterDropDownList.SelectedValue));
        orderGridView.DataSource = null;
        orderGridView.DataBind();

        aDal.LoadDA(ddlDa, salesCenterDropDownList.SelectedValue);
    }
    protected void manufacDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void marketDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable aTable = new DataTable();
        //aTable = aOrderInfoBll.LoadOrderForOrderCreation(salesCenterDropDownList.SelectedValue, manufacDropDownList.SelectedValue,
        //    marketDropDownList.SelectedValue);
        //orderGridView.DataSource = aTable;
        //orderGridView.DataBind();
    }
    protected void gotoinvoiceButton_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        Session["OrderId"] = orderGridView.DataKeys[rowindex]["OrderId"].ToString();
        Response.Redirect("InvoiceCreationForCustomerByOrder.aspx");

    }
    protected void GeneratetoinvoiceButton_Click(object sender, EventArgs e)
    {
        //Button button = (Button)sender;
        //GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        //int rowindex = 0;
        //rowindex = currentRow.RowIndex;

        //Session["OrderId"] = orderGridView.DataKeys[rowindex]["OrderId"].ToString();
        //Response.Redirect("InvoiceCreationForCustomerByOrder.aspx");

    }

    public void SessionChoose()
    {
        try
        {
            DataTable aTable = new DataTable();
            if (Session["MarketId"] != null)
            {
                aTable = aOrderInfoBll.LoadOrderForOrderCreation(salesCenterDropDownList.SelectedValue, manufacDropDownList.SelectedValue,
                Session["MarketId"].ToString());
                if (aTable.Rows.Count > 0)
                {
                    marketDropDownList.SelectedValue = Session["MarketId"].ToString();
                }
                else
                {
                    marketDropDownList.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {

        }

    }

    public void GridView()
    {
        try
        {
            DataTable aTable = new DataTable();
            aTable = aDal.LoadOrderForOrderCreation(GeneratePrameter());
            
            orderGridView.DataSource = aTable;
            orderGridView.DataBind();

            //if (aTable.Rows.Count > 0)
            //{

            //    for (int i = 0; i < orderGridView.Rows.Count; i++)
            //    {
            //        HiddenField hfCustomerMasterId = (HiddenField)orderGridView.Rows[i].Cells[0].FindControl("hfCustomerMasterId");
            //        HiddenField hfCustomerCode = (HiddenField)orderGridView.Rows[i].Cells[0].FindControl("hfCustomerCode");

            //        DataTable dtwar = aInvoiceBll.GetWarning(hfCustomerMasterId.Value, hfCustomerCode.Value);
            //        if (dtwar.Rows.Count > 0)
            //        {
            //            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FF7376");
            //            orderGridView.Rows[i].BackColor = col;
            //        }
            //        else
            //        {
            //            //warningLabel.Text = "";
            //        }
            //    }
            //}
            //Session["MarketId"] = rootDropDownList.SelectedValue;


            //// decimal total = aTable.AsEnumerable().Sum(row => row.Field<int?>("NumberofProformaInvoice") == null ? 0 : row.Field<int>("NumberofProformaInvoice"));
            //try
            //{
            //    orderGridView.FooterRow.Cells[4].Text = "Total";
            //    orderGridView.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //    // orderGridView.FooterRow.Cells[2].Text = total.ToString();

            //    decimal total2 = aTable.AsEnumerable().Sum(row => row.Field<decimal?>("GrossValue") == null ? 0 : row.Field<decimal>("GrossValue"));

            //    orderGridView.FooterRow.Cells[5].Text = total2.ToString("N2");
            //}
            //catch (Exception)
            //{

            //    //  throw;
            //}

        }
        catch (Exception)
        {

            //  throw;
        }

    }


    public string GeneratePrameter()
    {
        //ODR.ComUnitId='1' AND ODR.DistributionRouteId='54'

        string pram = "";

        if (salesCenterDropDownList.SelectedValue != "")
        {
            pram = pram + " AND ODR.ComUnitId = '" + salesCenterDropDownList.SelectedValue + "'";
        }

        if (rootDropDownList.SelectedValue != "")
        {
            pram = pram + " AND ODR.DistributionRouteId = '" + rootDropDownList.SelectedValue + "'";
        }

        return pram;
          
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView();
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("InvoiceId");
        aDataTable.Columns.Add("OrderNo");
        aDataTable.Columns.Add("SubmissionDate");
        aDataTable.Columns.Add("InvoiceNo");
        aDataTable.Columns.Add("InvoiceDate");
        aDataTable.Columns.Add("CustomerCode");
        aDataTable.Columns.Add("CustomerName");
        aDataTable.Columns.Add("MarketName");
        aDataTable.Columns.Add("TpGrandTotal");
        aDataTable.Columns.Add("CustomerType");
        aDataTable.Columns.Add("DeliveryDate");

        DataRow dataRow = null;
        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            var ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (ChkBoxRows.Checked)
            {
                if (HasInvoiceId(Convert.ToInt32(orderGridView.DataKeys[i][0].ToString())))
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["InvoiceId"] = orderGridView.DataKeys[i][0].ToString();
                    dataRow["OrderNo"] = orderGridView.Rows[i].Cells[2].Text;
                    dataRow["SubmissionDate"] = orderGridView.Rows[i].Cells[3].Text;
                    dataRow["InvoiceNo"] = orderGridView.Rows[i].Cells[4].Text;
                    dataRow["InvoiceDate"] = orderGridView.Rows[i].Cells[5].Text;
                    dataRow["CustomerCode"] = orderGridView.Rows[i].Cells[6].Text;
                    dataRow["CustomerName"] = orderGridView.Rows[i].Cells[7].Text;
                    dataRow["MarketName"] = orderGridView.Rows[i].Cells[8].Text;
                    dataRow["TpGrandTotal"] = orderGridView.Rows[i].Cells[9].Text;
                    dataRow["CustomerType"] = orderGridView.Rows[i].Cells[10].Text.Trim();
                    dataRow["DeliveryDate"] = orderGridView.Rows[i].Cells[11].Text;

                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        for (int i = 0; i < chalanGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();

            dataRow["InvoiceId"] = chalanGridView.DataKeys[i][0].ToString();
            dataRow["OrderNo"] = chalanGridView.Rows[i].Cells[1].Text;
            dataRow["SubmissionDate"] = chalanGridView.Rows[i].Cells[2].Text;
            dataRow["InvoiceNo"] =  
            dataRow["InvoiceDate"] = chalanGridView.Rows[i].Cells[4].Text;
            dataRow["CustomerCode"] = chalanGridView.Rows[i].Cells[5].Text;
            dataRow["CustomerName"] = chalanGridView.Rows[i].Cells[6].Text;
            dataRow["MarketName"] = chalanGridView.Rows[i].Cells[7].Text;
            dataRow["TpGrandTotal"] = chalanGridView.Rows[i].Cells[8].Text;
            dataRow["CustomerType"] = chalanGridView.Rows[i].Cells[9].Text.Trim();
            dataRow["DeliveryDate"] = chalanGridView.Rows[i].Cells[10].Text;

            aDataTable.Rows.Add(dataRow);
        }

        chalanGridView.DataSource = aDataTable;
        chalanGridView.DataBind();
    }

    public bool HasInvoiceId(int dcstoreId)
    {
        for (int i = 0; i < chalanGridView.Rows.Count; i++)
        {
            if (Convert.ToInt32(chalanGridView.DataKeys[i][0].ToString()) == dcstoreId)
            {
                return false;
                break;

            }
        }
        return true;
    }
    protected void DeleteImageButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        
        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("InvoiceId");
        aDataTable.Columns.Add("OrderNo");
        aDataTable.Columns.Add("SubmissionDate");
        aDataTable.Columns.Add("InvoiceNo");
        aDataTable.Columns.Add("InvoiceDate");
        aDataTable.Columns.Add("CustomerCode");
        aDataTable.Columns.Add("CustomerName");
        aDataTable.Columns.Add("MarketName");
        aDataTable.Columns.Add("TpGrandTotal");
        aDataTable.Columns.Add("CustomerType");
        aDataTable.Columns.Add("DeliveryDate");

        DataRow dataRow = null;
        for (int i = 0; i < chalanGridView.Rows.Count; i++)
        {
            if (i != rowindex)
            {
                dataRow = aDataTable.NewRow();

                dataRow["InvoiceId"] = chalanGridView.DataKeys[i][0].ToString();
                dataRow["OrderNo"] = chalanGridView.Rows[i].Cells[1].Text;
                dataRow["SubmissionDate"] = chalanGridView.Rows[i].Cells[2].Text;
                dataRow["InvoiceNo"] = chalanGridView.Rows[i].Cells[3].Text;
                dataRow["InvoiceDate"] = chalanGridView.Rows[i].Cells[4].Text;
                dataRow["CustomerCode"] = chalanGridView.Rows[i].Cells[5].Text;
                dataRow["CustomerName"] = chalanGridView.Rows[i].Cells[6].Text;
                dataRow["MarketName"] = chalanGridView.Rows[i].Cells[7].Text;
                dataRow["TpGrandTotal"] = chalanGridView.Rows[i].Cells[8].Text;
                dataRow["CustomerType"] = chalanGridView.Rows[i].Cells[9].Text.Trim();
                dataRow["DeliveryDate"] = chalanGridView.Rows[i].Cells[10].Text;


                aDataTable.Rows.Add(dataRow);
            }

        }

        chalanGridView.DataSource = aDataTable;
        chalanGridView.DataBind();

    }

    protected void resetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerateByRoute.aspx");
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (SaveValidation())
        {
            TopSheetMasterDao aMasterDao = new TopSheetMasterDao();

            aMasterDao.DAId = Convert.ToInt32(ddlDa.SelectedValue.Trim());

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            else
            {
                aMasterDao.TopSheetGenReportId = Convert.ToInt32(masterHiddenFieldId.Value);
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.UpdateDate = DateTime.Now;
            }
            

            TopSheetDetaildao aDetaildao;
            List<TopSheetDetaildao> aList = new List<TopSheetDetaildao>();

            for (int i = 0; i < chalanGridView.Rows.Count; i++)
            {
                aDetaildao = new TopSheetDetaildao();

                aDetaildao.InvoiceId = Convert.ToInt32(chalanGridView.DataKeys[i][0].ToString());
                aList.Add(aDetaildao);
            }

            ResultInfo Res = aDal.SaveTopSheet(aMasterDao, aList);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','TopSheetGenerateByRouteView.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }
    }

    private bool SaveValidation()
    {
        if (ddlDa.SelectedValue == "")
        {
            ShowMessageBox("Please select delivery man !!!");
            return false;
        }

        if (chalanGridView.Rows.Count < 1)
        {
            ShowMessageBox("Please select at lest one invoice !!!");
            return false;
        }

        return true;
    }


    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerateByRouteView.aspx");
    }

    protected void printButton_Click(object sender, EventArgs e)
    {
        LinkButton productCodeTextBox = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var invNo = orderGridView.Rows[rowindex].Cells[4].Text.Trim();

        
            string url = "../SInventory_RPTVIEW/InvoiceReportViewer.aspx?InvNo=" + Server.UrlEncode(invNo);
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
            
        
    }
}