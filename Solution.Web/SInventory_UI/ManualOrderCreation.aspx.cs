using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;
using DataTable = System.Data.DataTable;


public partial class SInventory_UI_ManualOrderCreation : System.Web.UI.Page
{
    OrderListBLL aOrderListBLL = new OrderListBLL();
    OrderListDAL aOrderListDal = new OrderListDAL();
    OrderStatusBll aOrderStatusBll = new OrderStatusBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialGrid();
            Todate();
            OrdNo();
            DropDownLoad();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                GetOneRecord(Request.QueryString["MID"]);
            }
        }
    }

    public void GetOneRecord(string orderNo)
    {
        if (orderNo != "")
        {
            string pram1 = "";
            string pram2 = "";


            if (orderNo.Trim() != "")
            {
                pram1 = pram1 + " AND ODR.OrderCode= '" + orderNo.Trim() + "'";
                pram1 = pram1 + " AND ODRD.NetAmount > 0 ";
                
                pram2 = pram2 + " AND ODR.OrderCode= '" + orderNo.Trim() + "'";
            }

            DataTable aTable = aOrderListDal.LoadOrderDetailById(pram1);

            if (aTable.Rows.Count > 0)
            {
                custCodeTextBox.Text = aTable.Rows[0].Field<String>("Customer");
                tbxDeliveryDate.Text = aTable.Rows[0].Field<DateTime>("DeliveryDate").ToString("dd-MMM-yyyy");
                tbxRemrks.Text = aTable.Rows[0].Field<String>("Remarks");
                hfCustomerId.Value = aTable.Rows[0].Field<Int32>("CustomerMasterId").ToString();
                hfCustomerPriceGroupId.Value = aTable.Rows[0].Field<Int32>("PriceGroupId").ToString();
                hdfOrderMasterId.Value = aTable.Rows[0].Field<Int32>("OrderId").ToString();
                hdfCustomerTypeId.Value = aTable.Rows[0].Field<Int32>("CustomerTypeId").ToString();

                productGridView.DataSource = aTable;
                productGridView.DataBind();

                for (int i = 0; i < productGridView.Rows.Count; i++)
                {
                    DropDownList isCampaignProductDropDownList = (DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsCampaignProductDropDownList");

                    if (Convert.ToBoolean(aTable.Rows[i]["IsCampaignProduct"]))
                    {
                        isCampaignProductDropDownList.SelectedIndex = 0;
                    }
                    else
                    {
                        isCampaignProductDropDownList.SelectedIndex = 1;
                    }
                }

                

                CalculateTotal();

                btnSetCalculation_Click(null,null);

                //DataTable aTable2 = aOrderListDal.LoadOrderDetailById(pram2);
                //orderGridView.DataSource = aTable2;
                //orderGridView.DataBind();

                // Calculate total

                decimal vatCount = 0;
                decimal totalCount = 0;
                decimal totalGross = 0;
                decimal totalDiscount = 0;

                for (int i = 0; i < orderGridView.Rows.Count; i++)
                {
                    totalCount = totalCount + (orderGridView.Rows[i].Cells[6].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[6].Text.Trim()));
                    vatCount = vatCount + (orderGridView.Rows[i].Cells[10].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[10].Text.Trim()));
                    totalGross = totalGross + (orderGridView.Rows[i].Cells[11].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[11].Text.Trim()));
                    totalDiscount = totalDiscount + (orderGridView.Rows[i].Cells[8].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[8].Text.Trim()));
                }

                orderGridView.FooterRow.Cells[5].Text = "Total:";
                //productGridView.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                orderGridView.FooterRow.Cells[6].Text = totalCount.ToString("N2");
                orderGridView.FooterRow.Cells[10].Text = vatCount.ToString("N2");
                orderGridView.FooterRow.Cells[11].Text = totalGross.ToString("N2");
                orderGridView.FooterRow.Cells[8].Text = totalDiscount.ToString("N2");
            }
            else
            {
                Clear();
                showMessageBox("No Data found!!!");
            }
        }

        else
        {
            Clear();
            showMessageBox("Please Search by Order Number !!!");
        }
    }
  
    private void DropDownLoad()
    {
        aOrderListBLL.LoadmanufacturerName(manufacturerDropDownList);
        
        aOrderListBLL.DCLoad(dcDropDownList);
    }
    private void OrdNo()
    {

        orderNoTextBox.Text = aOrderListBLL.OrdNo();
    }
    private void Todate()
    {
        orderDateTextBox.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd MMMM, yyyy");
    }

    public void CustomerInfo(string custCode)
    {
        DataTable dtcust = aOrderListBLL.CustomerInfo(custCode);
        if (dtcust.Rows.Count>0)
        {
            //if (dcDropDownList.SelectedValue ==dtcust.Rows[0]["ComUnitId"].ToString())
            //{
                custNameLabel.Text = dtcust.Rows[0]["CustomerName"].ToString();
                mioCodeLabel.Text = dtcust.Rows[0]["MiaCode"].ToString();
                mioNameLabel.Text = dtcust.Rows[0]["MiaName"].ToString();
                marketNameLabel.Text = dtcust.Rows[0]["MarketName"].ToString();
                teritory.Text = dtcust.Rows[0]["AreaCode"].ToString();
                FCBLabel3.Text = dtcust.Rows[0]["FixedCustomer"].ToString();
            //}
            //else
            //{
            //    //showMessageBox("Customer is not Valid");
            //}
        }
    }
    private void InitialGrid()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        aDataTable.Columns.Add("PackSize");
        aDataTable.Columns.Add("Quantity");
        aDataTable.Columns.Add("ProductId");
        aDataTable.Columns.Add("TradePrice");
        aDataTable.Columns.Add("UnitVatAmount");
        aDataTable.Columns.Add("TotalTradePrice");
        aDataTable.Columns.Add("TotalVatAmount");
        aDataTable.Columns.Add("NetAmount");


        DataRow dataRow;

        dataRow = aDataTable.NewRow();

        dataRow["SL"] = "1";
        dataRow["ProductCode"] = "";
        dataRow["ProductName"] = "";
        dataRow["PackSize"] = "";
        dataRow["Quantity"] = "";
        dataRow["TradePrice"] = "";
        dataRow["ProductId"] = "";
        dataRow["UnitVatAmount"] = "";
        dataRow["TotalTradePrice"] = "";
        dataRow["TotalVatAmount"] = "";
        dataRow["NetAmount"] = "";


        aDataTable.Rows.Add(dataRow);

        productGridView.DataSource = null;
        productGridView.DataBind();
        productGridView.DataSource = aDataTable;
        productGridView.DataBind();

        foreach (GridViewRow row in productGridView.Rows)
        {
            TextBox productTextBox = (TextBox)productGridView.Rows[row.RowIndex].Cells[3].FindControl("productNameTextBox");
           
        }
       
    }

    private void AddRowInGrid()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        //aDataTable.Columns.Add("PackSize");
        aDataTable.Columns.Add("Quantity");
        aDataTable.Columns.Add("TradePrice");
        aDataTable.Columns.Add("UnitVatAmount");
        aDataTable.Columns.Add("ProductId");

        aDataTable.Columns.Add("IsCampaignProductDropDownList");
        aDataTable.Columns.Add("IsGiftProductDropDownList");


        aDataTable.Columns.Add("TotalTradePrice");
        aDataTable.Columns.Add("TotalVatAmount");
        aDataTable.Columns.Add("NetAmount");

        DataRow dataRow;

        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {

                dataRow = aDataTable.NewRow();
                TextBox vatTextBox =
                    (TextBox)productGridView.Rows[i].Cells[5].FindControl("vatTextBox");
                TextBox tpTextBox =
                    (TextBox)productGridView.Rows[i].Cells[6].FindControl("tpTextBox");

                dataRow["SL"] = Convert.ToString(i + 1);
                TextBox productCodeTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("productCodeTextBox");
                dataRow["ProductCode"] = productCodeTextBox.Text.Trim();
                TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                dataRow["ProductName"] = productNameTextBox.Text.Trim();
                //TextBox packSizeTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("packSizeTextBox");
                //dataRow["PackSize"] = packSizeTextBox.Text;
                TextBox quantityTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("reqQtyTextBox");
                HiddenField unitpriceHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("unitpriceHiddenField");
                HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");
                dataRow["Quantity"] = quantityTextBox.Text.Trim();
                dataRow["ProductId"] = productidHiddenField.Value.Trim();
                dataRow["TradePrice"] = unitpriceHiddenField.Value;
                dataRow["UnitVatAmount"] = vatTextBox.Text;

                TextBox TotaltpTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("TotaltpTextBox");
                TextBox TotaltpVatTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("TotaltpVatTextBox");
                TextBox TotalGrossValueTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("tblGrossValue");

                dataRow["TotalTradePrice"] = TotaltpTextBox.Text;
                dataRow["TotalVatAmount"] = TotaltpVatTextBox.Text;
                dataRow["NetAmount"] = TotalGrossValueTextBox.Text;



                DropDownList C =
                (DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsCampaignProductDropDownList");
                DropDownList G =
                (DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsGiftProductDropDownList");



                dataRow["IsCampaignProductDropDownList"] = C.SelectedItem.Text;
                dataRow["IsGiftProductDropDownList"] = G.SelectedItem.Text;

                aDataTable.Rows.Add(dataRow);
            }
        }
        int sl = aDataTable.Rows.Count;

        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "";
        dataRow["ProductName"] = "";
        //dataRow["PackSize"] = "";
        dataRow["Quantity"] = "";
        dataRow["ProductId"] = "";
        dataRow["TradePrice"] = "";
        dataRow["UnitVatAmount"] = "";
        dataRow["IsCampaignProductDropDownList"] = "";
        dataRow["IsGiftProductDropDownList"] = "";

        dataRow["TotalTradePrice"] = "";
        dataRow["TotalVatAmount"] = "";
        dataRow["NetAmount"] = "";


        aDataTable.Rows.Add(dataRow);


        productGridView.DataSource = null;
        productGridView.DataBind();
        productGridView.DataSource = aDataTable;
        productGridView.DataBind();
        foreach (GridViewRow row in productGridView.Rows)
        {
            TextBox productTextBox = (TextBox)productGridView.Rows[row.RowIndex].Cells[3].FindControl("productNameTextBox");
            
        }
        for (int j = 0; j < aDataTable.Rows.Count; j++)
        {
            DropDownList ddlIsCampaignProductDropDownList =
                ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsCampaignProductDropDownList"));
            ddlIsCampaignProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsCampaignProductDropDownList"].ToString();


            DropDownList ddIsGiftProductDropDownList =
               ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsGiftProductDropDownList"));
            ddIsGiftProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsGiftProductDropDownList"].ToString();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        AddRowInGrid();
        CalculateTotal();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        aDataTable.Columns.Add("ProductId");
        //aDataTable.Columns.Add("PackSize");
        aDataTable.Columns.Add("Quantity");
        aDataTable.Columns.Add("TradePrice");
        aDataTable.Columns.Add("UnitVatAmount");
        aDataTable.Columns.Add("IsCampaignProductDropDownList");
        aDataTable.Columns.Add("IsGiftProductDropDownList");

        aDataTable.Columns.Add("TotalTradePrice");
        aDataTable.Columns.Add("TotalVatAmount");
        aDataTable.Columns.Add("NetAmount");

        DataRow dataRow;

        if (productGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    TextBox vatTextBox =
                    (TextBox)productGridView.Rows[i].Cells[5].FindControl("vatTextBox");
                    dataRow["SL"] = Convert.ToString(sl1);
                    TextBox productCodeTextBox2 = (TextBox)productGridView.Rows[i].Cells[1].FindControl("productCodeTextBox");
                    dataRow["ProductCode"] = productCodeTextBox2.Text.Trim();
                    TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                    dataRow["ProductName"] = productNameTextBox.Text.Trim();
                    //TextBox packSizeTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("packSizeTextBox");
                    //dataRow["PackSize"] = packSizeTextBox.Text;
                    TextBox quantityTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("reqQtyTextBox");
                    HiddenField unitpriceHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("unitpriceHiddenField");
                    HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");
                    dataRow["Quantity"] = quantityTextBox.Text.Trim();
                    dataRow["TradePrice"] = unitpriceHiddenField.Value.Trim();
                    dataRow["UnitVatAmount"] = vatTextBox.Text;

                    TextBox TotaltpTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("TotaltpTextBox");
                    TextBox TotaltpVatTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("TotaltpVatTextBox");
                    TextBox TotalGrossValueTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("tblGrossValue");

                    dataRow["TotalTradePrice"] = TotaltpTextBox.Text;
                    dataRow["TotalVatAmount"] = TotaltpVatTextBox.Text;
                    dataRow["NetAmount"] = TotalGrossValueTextBox.Text;

                    DropDownList C =
                    (DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsCampaignProductDropDownList");
                    DropDownList G =
                    (DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsGiftProductDropDownList");



                    dataRow["IsCampaignProductDropDownList"] = C.SelectedItem.Text;
                    dataRow["IsGiftProductDropDownList"] = G.SelectedItem.Text;


                    dataRow["ProductId"] = productidHiddenField.Value.Trim();
                    aDataTable.Rows.Add(dataRow);
                    sl1 += 1;
                }
            }
        }
        productGridView.DataSource = null;
        productGridView.DataBind();
        productGridView.DataSource = aDataTable;
        productGridView.DataBind();
        if (productGridView.Rows.Count < 1)
        {
            InitialGrid();
        }
        foreach (GridViewRow row in productGridView.Rows)
        {
            TextBox productTextBox = (TextBox)productGridView.Rows[row.RowIndex].Cells[3].FindControl("productNameTextBox");
            
        }
        for (int j = 0; j < aDataTable.Rows.Count; j++)
        {
            DropDownList ddlIsCampaignProductDropDownList =
                ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsCampaignProductDropDownList"));
            ddlIsCampaignProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsCampaignProductDropDownList"].ToString();


            DropDownList ddIsGiftProductDropDownList =
               ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsGiftProductDropDownList"));
            ddIsGiftProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsGiftProductDropDownList"].ToString();
        }

        CalculateTotal();
    }
    private void GetProductInGrid(int rowindex, string productCode)
    {
        DCStoreBLL _aDcStockReceiveBll = new DCStoreBLL();
        DataTable aDataTable = new DataTable();
        if (!string.IsNullOrEmpty(productCode))
        {
            if (hfCustomerId.Value != "")
            {
                //aDataTable = _aDcStockReceiveBll.ProductInfoNew(productCode);
                aDataTable = _aDcStockReceiveBll.ProductInfoNewByPriceGroup(productCode, Convert.ToInt32(hfCustomerId.Value));
                if (aDataTable.Rows.Count > 0)
                {
                    HiddenField productidHiddenField = (HiddenField)productGridView.Rows[rowindex].Cells[0].FindControl("productidHiddenField");
                    TextBox productNameTextBox =
                        (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("productNameTextBox");
                    productNameTextBox.Text = aDataTable.Rows[0]["ProductName"].ToString();
                    //TextBox packSizeTextBox = (TextBox)productGridView.Rows[rowindex].Cells[3].FindControl("packSizeTextBox");
                    //packSizeTextBox.Text = aDataTable.Rows[0]["PackSize"].ToString();
                    HiddenField unitpriceHiddenField = (HiddenField)productGridView.Rows[rowindex].Cells[0].FindControl("unitpriceHiddenField");
                    unitpriceHiddenField.Value = aDataTable.Rows[0]["UnitPrice"].ToString();
                    productidHiddenField.Value = aDataTable.Rows[0]["ProductId"].ToString();
                    TextBox vatTextBox =
                        (TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("vatTextBox");
                    TextBox tpTextBox =
                        (TextBox)productGridView.Rows[rowindex].Cells[6].FindControl("tpTextBox");
                    tpTextBox.Text = aDataTable.Rows[0]["UnitPrice"].ToString();
                    vatTextBox.Text = aDataTable.Rows[0]["VATAmountPerUnit"].ToString();

                }
            }
            
        }
    }
    protected void productCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        DCStoreBLL _aDcStockReceiveBll = new DCStoreBLL();

        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox productCodeTextBox = (TextBox)productGridView.Rows[rowindex].Cells[1].FindControl("productCodeTextBox");

        string productCode = productCodeTextBox.Text.Trim();
        GetProductInGrid(rowindex, productCode);
    }

    public void CalculateTotal()
    {
        decimal vatCount = 0;
        decimal totalCount = 0;
        decimal totalGross = 0;

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            TextBox tbxTotalVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpVatTextBox");
            TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");
            TextBox tblGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");

            vatCount = vatCount +  (tbxTotalVat.Text.Trim() == "" ? 0 : Convert.ToDecimal(tbxTotalVat.Text.Trim()));
            totalCount = totalCount + (tbxTotalTradePrice.Text.Trim() == "" ? 0 : Convert.ToDecimal(tbxTotalTradePrice.Text.Trim()));
            totalGross = totalGross + (tblGrossValue.Text.Trim() == "" ? 0 : Convert.ToDecimal(tblGrossValue.Text.Trim()));
        }

        productGridView.FooterRow.Cells[5].Text = "Total:";
        //productGridView.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        productGridView.FooterRow.Cells[6].Text = totalCount.ToString("N2");
        productGridView.FooterRow.Cells[7].Text = vatCount.ToString("N2");
        productGridView.FooterRow.Cells[8].Text = totalGross.ToString("N2");
    }


    private void SaveData()
    {
        if (Validation())
        {
            var aMasterDao = new OrderInfoMaster();

            aMasterDao.CustomerMasterId = Convert.ToInt32(hfCustomerId.Value);
            aMasterDao.DateOfDelivery = Convert.ToDateTime(tbxDeliveryDate.Text);
            aMasterDao.Remarks = tbxRemrks.Text;
            

            if (hdfOrderMasterId.Value == "")
            {
                aMasterDao.IsManual = true;
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
                aMasterDao.SubmissionDate = DateTime.Today;
            }
            else
            {
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.OrderId = Convert.ToInt32(hdfOrderMasterId.Value);
                aMasterDao.UpdateDate = DateTime.Now;
            }
            

            OrderInfoDetail aDetaildao;
            List<OrderInfoDetail> aList = new List<OrderInfoDetail>();

            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {
                aDetaildao = new OrderInfoDetail();

                //Label productCodeTextBox = orderGridView.Rows[i].Cells[1].Text;
                //TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                //HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");
                //TextBox tbxQuantity = (TextBox)productGridView.Rows[i].Cells[2].FindControl("reqQtyTextBox");
                //TextBox tbxTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tpTextBox");
                //TextBox tbxVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("vatTextBox");
                //TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");
                //TextBox tbxTotalVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpVatTextBox");
                //TextBox tblGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");

                aDetaildao.ProductId = Convert.ToInt32(orderGridView.DataKeys[i][0]);
                aDetaildao.ProductCode = orderGridView.Rows[i].Cells[1].Text.Trim();
                aDetaildao.ProductName = orderGridView.Rows[i].Cells[2].Text;
                aDetaildao.TradePrice = Convert.ToDecimal(orderGridView.Rows[i].Cells[3].Text);
                aDetaildao.Vat = Convert.ToDecimal(orderGridView.Rows[i].Cells[4].Text);
                aDetaildao.TotalTradePrice = Convert.ToDecimal(orderGridView.Rows[i].Cells[6].Text);
                aDetaildao.Quantity = Convert.ToDecimal(orderGridView.Rows[i].Cells[5].Text);
                aDetaildao.TotalVat = Convert.ToDecimal(orderGridView.Rows[i].Cells[10].Text);
                aDetaildao.GrossValue = Convert.ToDecimal(orderGridView.Rows[i].Cells[11].Text);
                aDetaildao.CampaignType = Convert.ToInt32(orderGridView.DataKeys[i][1]).ToString();

                if (orderGridView.DataKeys[i][1] != null )
                {
                    if (Convert.ToInt32(orderGridView.DataKeys[i][1].ToString()) == 0)
                    {
                        aDetaildao.IsCampaignProduct = false;
                    }
                    else
                    {
                        aDetaildao.IsCampaignProduct = true;
                    }        
                }

                aDetaildao.CampaignName = orderGridView.Rows[i].Cells[11].Text;
                aDetaildao.DiscountPercent = Convert.ToDecimal(orderGridView.Rows[i].Cells[7].Text);
                aDetaildao.DiscountAmount = Convert.ToDecimal(orderGridView.Rows[i].Cells[8].Text);
                aDetaildao.DiscountValue = 0;

                aList.Add(aDetaildao);
            }

            ResultInfo Res = aOrderListDal.SaveOrder(aMasterDao, aList);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','../MasterSetup_UI/OrderTrackingList.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }
        } 

    }

    private void SaveAllData()
    {
        
        int maxReqId;
        OrderInfoMaster aListMasterDao = new OrderInfoMaster()
        {
            OrderCode = orderNoTextBox.Text,
            ComUnitId= Convert.ToInt32(dcDropDownList.SelectedValue),
            ComUnitName = dcDropDownList.SelectedItem.Text.Split(':')[1].Trim(),
            ComUnitCode = dcDropDownList.SelectedItem.Text.Split(':')[0].Trim(),
            MIOCode = mioCodeLabel.Text,
            MIOName = mioNameLabel.Text,
            teritory = teritory.Text,
            ManufacId = Convert.ToInt32(manufacturerDropDownList.SelectedValue),
            CustomerCode = custCodeTextBox.Text,
            SubmissionDate = Convert.ToDateTime(orderDateTextBox.Text),
            CustomerName = custNameLabel.Text,
            IsManual = true,
            FCB=Convert.ToBoolean(FCBLabel3.Text)

        };
        decimal totalprice = 0;
        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            TextBox quantityTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("reqQtyTextBox");
            HiddenField unitpriceHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("unitpriceHiddenField");

            totalprice += Convert.ToDecimal(unitpriceHiddenField.Value)*Convert.ToDecimal(quantityTextBox.Text);
        }
        aListMasterDao.GrossValue = totalprice;

        bool requsitionSave = aOrderListBLL.SaveOrderMaster(aListMasterDao, out maxReqId);

        List<OrderInfoDetail> aOrderInfoDetailList = new List<OrderInfoDetail>();
        
        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            
            TextBox productCodeTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("productCodeTextBox");
            TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
            TextBox quantityTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("reqQtyTextBox");
            HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");
            HiddenField unitpriceHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("unitpriceHiddenField");
            OrderInfoDetail aOrderInfoDetail = new OrderInfoDetail();
            aOrderInfoDetail.ProductCode = productCodeTextBox.Text.Trim();
            aOrderInfoDetail.ProductName = productNameTextBox.Text.Trim();
            aOrderInfoDetail.ProductId = Convert.ToInt32(productidHiddenField.Value);
            aOrderInfoDetail.Quantity = Convert.ToDecimal(quantityTextBox.Text.Trim());
            aOrderInfoDetail.OrderId = maxReqId;
            aOrderInfoDetail.TradePrice = Convert.ToDecimal(unitpriceHiddenField.Value);
            aOrderInfoDetail.TotalTradePrice = aOrderInfoDetail.Quantity*aOrderInfoDetail.TradePrice;


            DropDownList giftTextBox = (DropDownList)productGridView.Rows[i].Cells[2].FindControl("IsGiftProductDropDownList");
            DropDownList campTextBox = (DropDownList)productGridView.Rows[i].Cells[4].FindControl("IsCampaignProductDropDownList");

            //aOrderInfoDetail.IsgiftProduct = giftTextBox.Text;
            //aOrderInfoDetail.IsCampaignProduct = campTextBox.Text;


            aOrderInfoDetailList.Add(aOrderInfoDetail);
        }

        string msg = aOrderListBLL.SaveOrderDetail(aOrderInfoDetailList);
        Clear();
        showMessageBox(msg);
    }

    public bool Validation()
    {
        if (hfCustomerId.Value == "")
        {
            showMessageBox("Please Select customer !!!");
            custCodeTextBox.Focus();
            custCodeTextBox.BackColor = Color.GhostWhite;
            return false;
        }
        
        if (tbxDeliveryDate.Text == "")
        {
            showMessageBox("Please Select delivery Date  !!!");
            tbxDeliveryDate.Focus();
            tbxDeliveryDate.BackColor = Color.GhostWhite;
            return false;
        }

        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Text == "")
                {
                    ((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Focus();
                    showMessageBox("Please fill out Req.Qty!!");
                    return false;
                }
            }
        }
        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (((TextBox)productGridView.Rows[i].FindControl("productCodeTextBox")).Text == "")
                {
                    ((TextBox)productGridView.Rows[i].FindControl("productCodeTextBox")).Focus();
                    showMessageBox("Please fill out productCode!!");
                    return false;
                }
            }
        }
        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (((TextBox)productGridView.Rows[i].FindControl("productNameTextBox")).Text == "")
                {
                    ((TextBox)productGridView.Rows[i].FindControl("productNameTextBox")).Focus();
                    showMessageBox("Please fill out productName!!");
                    return false;
                }
            }
        }
        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        //if (Validation())
        //{
            SaveData();
        //}
        //else
        //{
        //    showMessageBox("Select Manufacturer Name!!");
        //}
       
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private void Clear()
    {
       // manufacturerDropDownList.SelectedValue = "";
        InitialGrid();
        Todate();
        OrdNo();
        DropDownLoad();
        teritory.Text = string.Empty;
        custNameLabel.Text = string.Empty;
        mioCodeLabel.Text = string.Empty;
        mioNameLabel.Text = string.Empty;
        marketNameLabel.Text = string.Empty;
        custCodeTextBox.Text = string.Empty;
    }
    protected void productNameTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox productNameTextBox = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("productNameTextBox");

        string productName = productNameTextBox.Text.Trim();
        if (productName.Contains(':'))
        {
            string[] productInfo = productName.Split(':');

            TextBox productCodeTextBox = (TextBox)productGridView.Rows[rowindex].Cells[1].FindControl("productCodeTextBox");

            productCodeTextBox.Text = productInfo[0];
            //productNameTextBox.Text = productInfo[1];
            string productCode = productCodeTextBox.Text.Trim();
            GetProductInGrid(rowindex, productCode);
        }
        else
        {
            showMessageBox("Input Correct Data!!");
        }
    }
    protected void miaImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("OrderRequisitionView.aspx");
    }
    protected void manufacturerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitialGrid();
    }
    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {

        string empName = custCodeTextBox.Text.Trim();
        if (empName.Contains(':'))
        {
            string[] emp = empName.Split('|');

            hfCustomerId.Value = emp[1].Trim();
            hfCustomerPriceGroupId.Value = emp[2].Trim();
            hdfCustomerTypeId.Value = emp[3].Trim();
            custCodeTextBox.Text = emp[0].Trim();
        }
        else
        {

            custCodeTextBox.Text = "";
            hfCustomerId.Value = "";
            showMessageBox("Input Correct Data !!");
        }

    }

    protected void resetbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManualOrderCreation.aspx");
    }

    protected void reqQtyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox tbxQuantity = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("reqQtyTextBox");
        TextBox tbxTradePrice = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("tpTextBox");
        TextBox tbxVat = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("vatTextBox");
        TextBox tbxTotalTradePrice= (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("TotaltpTextBox");
        TextBox tbxTotalVat = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("TotaltpVatTextBox");
        TextBox tblGrossValue = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("tblGrossValue");

        decimal totalVat = 0;
        decimal totalPrice = 0;
        decimal grossValue = 0;

        if (tbxQuantity.Text.Trim() != "")
        {
            if (Convert.ToDecimal(tbxQuantity.Text.Trim()) != 0)
            {
                totalVat = Convert.ToDecimal(tbxQuantity.Text.Trim()) * Convert.ToDecimal(tbxVat.Text.Trim());
                totalPrice = Convert.ToDecimal(tbxQuantity.Text.Trim()) * Convert.ToDecimal(tbxTradePrice.Text.Trim());
                grossValue = totalPrice + totalVat;
            }
            else
            {
                showMessageBox("Quantity can not be 0 !!");
            }
        }
        else
        {
            showMessageBox("Quantity can not be empty !!");
        }

        tbxTotalTradePrice.Text = totalPrice.ToString(CultureInfo.InvariantCulture);
        tbxTotalVat.Text = totalVat.ToString(CultureInfo.InvariantCulture);
        tblGrossValue.Text = grossValue.ToString(CultureInfo.InvariantCulture);

        CheckCampaign(rowindex);
        CalculateTotal();

    }

    private void CheckCampaign(int rowindex)
    {
        var tbxProductCode = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("productCodeTextBox");
        var tbxQuantity = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("reqQtyTextBox");
        var isCampaignProductDropDownList = (DropDownList)productGridView.Rows[rowindex].Cells[2].FindControl("IsCampaignProductDropDownList");
        var productidHiddenField = (HiddenField)productGridView.Rows[rowindex].Cells[2].FindControl("productidHiddenField");

        DataTable aTable = aOrderListDal.GetCampaignInfo(Convert.ToInt32(productidHiddenField.Value), Convert.ToInt32(hfCustomerId.Value), Convert.ToInt32(hdfCustomerTypeId.Value));

        decimal qty = 0;

        if (aTable.Rows.Count > 0)
        {
            isCampaignProductDropDownList.SelectedIndex = 0;
        }
        else
        {
            isCampaignProductDropDownList.SelectedIndex = 1;
        }


    }

    protected void btnSetCalculation_Click(object sender, EventArgs e)
    {
        if (productGridView.Rows.Count > 0)
        {
            DataTable aDataTable = new DataTable();

            aDataTable.Columns.Add("ProductId");
            aDataTable.Columns.Add("CampaignTypeId");
            aDataTable.Columns.Add("ProductCode");
            aDataTable.Columns.Add("ProductName");
            aDataTable.Columns.Add("TP");
            aDataTable.Columns.Add("Vat");
            aDataTable.Columns.Add("Quantity");
            aDataTable.Columns.Add("TotalTP");
            aDataTable.Columns.Add("DiscountPercentage");
            aDataTable.Columns.Add("DiscountAmount");
            aDataTable.Columns.Add("CampaignName");
            aDataTable.Columns.Add("TotalVat");
            aDataTable.Columns.Add("GrossValue");

            // Trade policy 

            decimal restAmount = 0;

            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                var isCampaignProductDropDownList = (DropDownList)productGridView.Rows[i].Cells[2].FindControl("IsCampaignProductDropDownList");
                var tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");

                //if (isCampaignProductDropDownList.SelectedIndex == 1)
                //{
                    restAmount = restAmount + Convert.ToDecimal(tbxTotalTradePrice.Text.Trim());
                //}
            }

            // Check Trade policy 

            decimal discountPercentage = 0;

            DataTable aTradePolicy = aOrderListDal.GetTradePolicyInfo(Convert.ToInt32(hfCustomerId.Value), Convert.ToInt32(hdfCustomerTypeId.Value), restAmount);

            if (aTradePolicy.Rows.Count > 0)
            {
                try
                {
                    discountPercentage = Convert.ToDecimal(aTradePolicy.Rows[0]["DiscountPercentage"]);
                }
                catch (Exception)
                {
                    discountPercentage = 0;
                    throw;
                }
                
            }

            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                TextBox productCodeTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("productCodeTextBox");
                TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");
                TextBox tbxQuantity = (TextBox)productGridView.Rows[i].Cells[2].FindControl("reqQtyTextBox");
                TextBox tbxTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tpTextBox");
                TextBox tbxVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("vatTextBox");
                TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");
                TextBox tbxTotalVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpVatTextBox");
                TextBox tblGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");
                var isCampaignProductDropDownList = (DropDownList)productGridView.Rows[i].Cells[2].FindControl("IsCampaignProductDropDownList");
               
                // Check Campaign

                if (productCodeTextBox.Text.Trim() != "" && tbxQuantity.Text != "")
                {

                    // Bind data

                    DataRow dataRow;
                    dataRow = aDataTable.NewRow();

                    dataRow["ProductId"] = productidHiddenField.Value;
                    dataRow["CampaignTypeId"] = 0;
                    dataRow["ProductCode"] = productCodeTextBox.Text.Trim();
                    dataRow["ProductName"] = productNameTextBox.Text;
                    dataRow["TP"] = tbxTradePrice.Text;
                    dataRow["Vat"] = tbxVat.Text;
                    dataRow["Quantity"] = tbxQuantity.Text;
                    dataRow["TotalTP"] = tbxTotalTradePrice.Text;

                    decimal discountAmt = 0;
                    decimal grossValue = 0;

                    if (isCampaignProductDropDownList.SelectedIndex == 1 && discountPercentage > 0)
                    {
                        discountAmt = Convert.ToDecimal(tbxTotalTradePrice.Text.Trim())*(discountPercentage/100);
                        grossValue = (Convert.ToDecimal(tbxTotalVat.Text.Trim()) +
                                      (Convert.ToDecimal(tbxTotalTradePrice.Text.Trim()) - discountAmt));

                        dataRow["DiscountPercentage"] = discountPercentage.ToString("#.000");
                        dataRow["DiscountAmount"] = discountAmt.ToString("#.000");
                        dataRow["GrossValue"] = grossValue.ToString("#.000");
                    }
                    else
                    {
                        dataRow["DiscountPercentage"] = "0";
                        dataRow["DiscountAmount"] ="0";
                        dataRow["GrossValue"] = tblGrossValue.Text.Trim();
                    }
                    
                    dataRow["CampaignName"] = "";
                    dataRow["TotalVat"] = tbxTotalVat.Text.Trim();
                    
                    aDataTable.Rows.Add(dataRow);

                    // Campaign Check

                    DataTable aTable = aOrderListDal.GetCampaignInfo(Convert.ToInt32(productidHiddenField.Value), Convert.ToInt32(hfCustomerId.Value), Convert.ToInt32(hdfCustomerTypeId.Value));

                    decimal qty = 0;

                    if (aTable.Rows.Count > 0)
                    {

                        try
                        {
                            qty = (Convert.ToDecimal(tbxQuantity.Text)/Convert.ToDecimal(aTable.Rows[0]["Quantity"].ToString()));
                        }
                        catch (Exception)
                        {
                            qty = 0;
                            throw;
                        }

                        if (qty > 0)
                        {
                            dataRow = aDataTable.NewRow();

                            dataRow["ProductId"] = productidHiddenField.Value;
                            dataRow["CampaignTypeId"] = aTable.Rows[0]["CampaignDetailId"].ToString();
                            dataRow["ProductCode"] = productCodeTextBox.Text.Trim();
                            dataRow["ProductName"] = productNameTextBox.Text;
                            dataRow["TP"] = "0";
                            dataRow["Vat"] = "0";
                            dataRow["Quantity"] = Math.Floor(qty);
                            dataRow["TotalTP"] = "0";
                            dataRow["DiscountPercentage"] = "0";
                            dataRow["DiscountAmount"] = "0";
                            dataRow["CampaignName"] = aTable.Rows[0]["CampaignName"].ToString();
                            dataRow["TotalVat"] = "0";
                            dataRow["GrossValue"] = "0";

                            aDataTable.Rows.Add(dataRow);
                        }

                        
                    }


                }


            }

            orderGridView.DataSource = null;
            orderGridView.DataBind();

            orderGridView.DataSource = aDataTable;
            orderGridView.DataBind();

            // Calculate total

            decimal vatCount = 0;
            decimal totalCount = 0;
            decimal totalGross = 0;
            decimal totalDiscount = 0;

            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {


                totalCount = totalCount + (orderGridView.Rows[i].Cells[6].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[6].Text.Trim()));
                vatCount = vatCount + (orderGridView.Rows[i].Cells[10].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[10].Text.Trim()));
                totalGross = totalGross + (orderGridView.Rows[i].Cells[11].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[11].Text.Trim()));
                totalDiscount = totalDiscount + (orderGridView.Rows[i].Cells[8].Text.Trim() == "" ? 0 : Convert.ToDecimal(orderGridView.Rows[i].Cells[8].Text.Trim()));
            }

            orderGridView.FooterRow.Cells[5].Text = "Total:";
            //productGridView.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            orderGridView.FooterRow.Cells[6].Text = totalCount.ToString("N2");
            orderGridView.FooterRow.Cells[10].Text = vatCount.ToString("N2");
            orderGridView.FooterRow.Cells[11].Text = totalGross.ToString("N2");
            orderGridView.FooterRow.Cells[8].Text = totalDiscount.ToString("N2");
            
        }
        else
        {
            orderGridView.DataSource = null;
            orderGridView.DataBind();
        }
    }
}