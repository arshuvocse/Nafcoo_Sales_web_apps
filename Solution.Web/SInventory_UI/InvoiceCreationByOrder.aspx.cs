using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.InternalCls;

public partial class SInventory_UI_InvoiceCreationByOrder : System.Web.UI.Page
{
    OrderInfoBLL aOrderInfoBll=new OrderInfoBLL();
    InvoiceBLL aInvoiceBll = new InvoiceBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DropDownlist();
                

                try
                {

                    if (Session["SalesCenterOrd"] != null)
                    {
                        salesCenterDropDownList.SelectedValue = Session["SalesCenterOrd"].ToString();
                        salesCenterDropDownList_SelectedIndexChanged(null, null);
                        rootDropDownList.SelectedValue = Session["RootOrd"].ToString();


                        DataTable aTable = new DataTable();
                        aTable = aOrderInfoBll.LoadOrderForOrderCreation(salesCenterDropDownList.SelectedValue, rootDropDownList.SelectedValue,
                            marketDropDownList.SelectedValue);
                        orderGridView.DataSource = aTable;
                        orderGridView.DataBind();



                        //salesCenterDropDownList_SelectedIndexChanged(null, null);

                        //GridView();
                    }
                    else
                    {
                        salesCenterDropDownList_SelectedIndexChanged(null, null);
                    }
                    if (Session["RootOrd"] != null)
                    {
                        rootDropDownList.SelectedValue = Session["RootOrd"].ToString();
                    }

                    //GridView();
                }
                catch (Exception ex)
                {
                    salesCenterDropDownList_SelectedIndexChanged(null, null);
                    // Optional: show or log the error
                    // lblError.Text = "Error loading dropdown values from session: " + ex.Message;
                }
            }
        }
        catch(Exception ex)
        {
            //Session["SalesCenterOrd"] = salesCenterDropDownList.SelectedValue;
            //Session["RootOrd"] = rootDropDownList.SelectedValue;

        }
    }
    public string GenerateParameter()
    {
        string pram = @" WHERE IV.InvoiceNo IN (SELECT InvoiceNo FROM dbo.tblInvoiceBatch
        LEFT JOIN dbo.tblInvoice ON tblInvoice.InvoiceId = tblInvoiceBatch.InvoiceId
        WHERE BatchNo = '" + batchno.Text + "')";
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

        try { 
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

            batchno.Text = batchn.ToString();
        }

        
        ShowMessageBox("Invoice Generated Successfully.");
        }
        catch(Exception ex)
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

 

    public void DropDownlist()
    {
        try
        {
            aOrderInfoBll.LoadSC(salesCenterDropDownList, Session["UserId"].ToString());
            aOrderInfoBll.LoadManufac(manufacDropDownList);
            // aOrderInfoBll.LoadDisRoute(rootDropDownList);
            manufacDropDownList.SelectedIndex = 1;
            salesCenterDropDownList.SelectedIndex = 1;
            

            try
            {
                rootDropDownList.SelectedValue= Session["RootOrd"].ToString();

                //GridView();
            }
            catch (Exception ex)
            {

            }
        }
        catch(Exception ex)
        {

        }
    }
    protected void salesCenterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aOrderInfoBll.LoadDisRouteforInvoice(rootDropDownList, Convert.ToInt32(salesCenterDropDownList.SelectedValue));
        //rootDropDownList.SelectedValue = Session["RootOrd"].ToString();
        //// aOrderInfoBll.LoadMarketOrderWise(marketDropDownList,salesCenterDropDownList.SelectedValue);
        // using (DataTable dt = aOrderInfoBll.LoadDistributionRoute(salesCenterDropDownList.SelectedValue))
        // {

        //     try
        //     {
        //         rootDropDownList.SelectedValue = dt.Rows[0]["RouteInformationMasterId"].ToString();
        //     }
        //     catch(Exception ex)
        //     {

        //     }
        // }

        orderGridView.DataSource = null;
        orderGridView.DataBind();
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
        int rowindex = currentRow.RowIndex;

        var OrderId = orderGridView.DataKeys[rowindex]["OrderId"].ToString();
        var ComUnitId = orderGridView.DataKeys[rowindex]["ComUnitId"].ToString();

        // Store OrderId in session
        Session["OrderId"] = OrderId;

        // Use string.Format for the query string
        Response.Redirect(string.Format("InvoiceCreationForCustomerByOrder.aspx?OrderId={0}&ComUnitId={1}", OrderId, ComUnitId));
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
 

    public void GridView()
    {
        try
        {
            Session["SalesCenterOrd"] = salesCenterDropDownList.SelectedValue;
            Session["RootOrd"] = rootDropDownList.SelectedValue;

            DataTable aTable = new DataTable();
            aTable = aOrderInfoBll.LoadOrderForOrderCreation(salesCenterDropDownList.SelectedValue, rootDropDownList.SelectedValue,
                marketDropDownList.SelectedValue);
            orderGridView.DataSource = aTable;
            orderGridView.DataBind();



            //for (int i = 0; i < orderGridView.Rows.Count; i++)
            //{
            //   string isInvoiceAble = orderGridView.DataKeys[i][3].ToString();
            //   Button gotoinvoiceButton = (Button)orderGridView.Rows[i].Cells[0].FindControl("gotoinvoiceButton");

            //   if (isInvoiceAble.Trim() != "Yes")
            //   {
            //       gotoinvoiceButton.Enabled = false;
            //       gotoinvoiceButton.ToolTip = "Credit limit exceeded  !!";
            //       System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FF7376");
            //       orderGridView.Rows[i].BackColor = col;
            //    }

            //}

            if (aTable.Rows.Count > 0)
            {

                for (int i = 0; i < orderGridView.Rows.Count; i++)
                {
                    HiddenField hfCustomerMasterId = (HiddenField)orderGridView.Rows[i].Cells[0].FindControl("hfCustomerMasterId");
                    HiddenField hfCustomerCode = (HiddenField)orderGridView.Rows[i].Cells[0].FindControl("hfCustomerCode");

                    DataTable dtwar = aInvoiceBll.GetWarning(hfCustomerMasterId.Value, hfCustomerCode.Value);

                    if (dtwar.Rows.Count > 0)
                    {
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FF7376");
                        orderGridView.Rows[i].BackColor = col;
                    }
                    else
                    {
                        //warningLabel.Text = "";
                    }
                }
            }
           


            // decimal total = aTable.AsEnumerable().Sum(row => row.Field<int?>("NumberofProformaInvoice") == null ? 0 : row.Field<int>("NumberofProformaInvoice"));
            try
            {
                orderGridView.FooterRow.Cells[6].Text = "Total";
                orderGridView.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                // orderGridView.FooterRow.Cells[2].Text = total.ToString();

                decimal total2 = aTable.AsEnumerable().Sum(row => row.Field<decimal?>("GrossValue") == null ? 0 : row.Field<decimal>("GrossValue"));

                orderGridView.FooterRow.Cells[7].Text = total2.ToString("N2");
            }
            catch (Exception)
            {

                //  throw;
            }

        }
        catch (Exception)
        {
            
          //  throw;
        }
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView();
    }
}