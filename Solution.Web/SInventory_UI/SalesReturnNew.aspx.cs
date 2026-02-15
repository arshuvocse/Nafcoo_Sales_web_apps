using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

public partial class SInventory_UI_SalesReturnNew : System.Web.UI.Page
{

    NewSalesReturnDal aReturnDal = new NewSalesReturnDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownList();
            InitialGrid();
            orderDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }

    private void LoadDropdownList()
    {
       aReturnDal.DCLoad(ddlSalesCenter);
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

    private void GetProductInGrid(int rowindex, string productCode)
    {
        DCStoreBLL _aDcStockReceiveBll = new DCStoreBLL();
        DataTable aDataTable = new DataTable();
        if (!string.IsNullOrEmpty(productCode))
        {
            //if (hdCustomerMasterId.Value != "")
            //{
                aDataTable = _aDcStockReceiveBll.ProductInfoNew(productCode);
                //aDataTable = _aDcStockReceiveBll.ProductInfoNewByPriceGroup(productCode, Convert.ToInt32(hdCustomerMasterId.Value));
                if (aDataTable.Rows.Count > 0)
                {
                    HiddenField productidHiddenField =
                        (HiddenField) productGridView.Rows[rowindex].Cells[0].FindControl("productidHiddenField");
                    TextBox productNameTextBox =
                        (TextBox) productGridView.Rows[rowindex].Cells[2].FindControl("productNameTextBox");
                    productNameTextBox.Text = aDataTable.Rows[0]["ProductName"].ToString();
                    //TextBox packSizeTextBox = (TextBox)productGridView.Rows[rowindex].Cells[3].FindControl("packSizeTextBox");
                    //packSizeTextBox.Text = aDataTable.Rows[0]["PackSize"].ToString();
                    HiddenField unitpriceHiddenField =
                        (HiddenField) productGridView.Rows[rowindex].Cells[0].FindControl("unitpriceHiddenField");
                    unitpriceHiddenField.Value = aDataTable.Rows[0]["UnitPrice"].ToString();
                    productidHiddenField.Value = aDataTable.Rows[0]["ProductId"].ToString();
                    TextBox vatTextBox =
                        (TextBox) productGridView.Rows[rowindex].Cells[5].FindControl("vatTextBox");
                    TextBox tpTextBox =
                        (TextBox) productGridView.Rows[rowindex].Cells[6].FindControl("tpTextBox");
                    tpTextBox.Text = aDataTable.Rows[0]["UnitPrice"].ToString();
                    vatTextBox.Text = aDataTable.Rows[0]["VATAmountPerUnit"].ToString();

                }
            //}
            //else
            //{
            //    sEL
            //}

        }
    }

    protected void referenceInvoiceTextBox_TextChanged(object sender, EventArgs e)
    {
        string productName = ddlInvoice.Text.Trim();
        if (productName.Contains(':'))
        {
            string[] productInfo = productName.Split(':');

            hdfInvoiceId.Value = productInfo[1];
            ddlInvoice.Text = productInfo[0];
            ddlSalesCenter.SelectedValue = productInfo[2];
            custCodeTextBox.Text = productInfo[4];
            custCodeTextBox_TextChanged(null,null);

            if (hdfInvoiceId.Value != "")
            {
                LoadInvoiceDetail();

                for (int i = 0; i < productGridView.Rows.Count; i++)
                {
                    CheckBox isGiftProductTextBox = (CheckBox)productGridView.Rows[i].Cells[3].FindControl("chkIsGiftProduct");
                    TextBox tblGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");

                    isGiftProductTextBox.Checked = Convert.ToDecimal(tblGrossValue.Text) == 0;
                }
            }

        }
        else
        {
            ShowMessageBox("Please Input Correct Data!!");
        }
    }

    private void LoadInvoiceDetail()
    {
        DataTable aDataTable = aReturnDal.LoadInvoiceDetail(Convert.ToInt32(hdfInvoiceId.Value));

        if (aDataTable.Rows.Count > 0)
        {
            productGridView.DataSource = aDataTable;
            productGridView.DataBind();

            CalculateTotal();
        }
        else
        {
            productGridView.DataSource = null;
            productGridView.DataBind();
        }
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
        TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[rowindex].Cells[2].FindControl("TotaltpTextBox");
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
                ShowMessageBox("Quantity can not be 0 !!");
            }
        }
        else
        {
            ShowMessageBox("Quantity can not be empty !!");
        }

        tbxTotalTradePrice.Text = totalPrice.ToString(CultureInfo.InvariantCulture);
        tbxTotalVat.Text = totalVat.ToString(CultureInfo.InvariantCulture);
        tblGrossValue.Text = grossValue.ToString(CultureInfo.InvariantCulture);

        //CheckCampaign(rowindex);

        CalculateTotal();

    }

    private void CalculateTotal()
    {
        decimal totalTp = 0;
        decimal totalVat = 0;
        decimal totalGrossValue = 0;

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");
            TextBox tbxTotalVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpVatTextBox");
            TextBox tblGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");

            if (tbxTotalTradePrice.Text != "")
            {
                totalTp = totalTp + (tbxTotalTradePrice.Text != "" ? Convert.ToDecimal(tbxTotalTradePrice.Text) : 0);
                totalVat = totalVat + (tbxTotalVat.Text != "" ? Convert.ToDecimal(tbxTotalVat.Text) : 0);
                totalGrossValue = totalGrossValue + (tblGrossValue.Text != "" ? Convert.ToDecimal(tblGrossValue.Text) : 0);
            }
            

        }

        tpTptalTextBox.Text = totalTp.ToString();
        vatTotalTextBox.Text = totalVat.ToString();
        grandTotalTextBox.Text = totalGrossValue.ToString();

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
            ShowMessageBox("Input Correct Data!!");
        }
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        string custCode = custCodeTextBox.Text.Trim();
        GetCustInfo(custCode);
    }
    protected void custNameTextBox_TextChanged(object sender, EventArgs e)
    {

        string custName = custNameTextBox.Text.Trim();
        if (!string.IsNullOrEmpty(custName))
        {
            if (custName.Contains(':'))
            {

                string[] custInfo = custName.Split(':');
                custCodeTextBox.Text = custInfo[0];
                string custCode = custCodeTextBox.Text.Trim();
                GetCustInfo(custCode);
            }
        }
    }

    private void GetCustInfo(string custCode)
    {
        if (!string.IsNullOrEmpty(custCode))
        {
            custCodeTextBox.Text = custCode;
            DataTable aDataTable = new DataTable();
            aDataTable = aReturnDal.LoadCustomerMaster(custCode);
            if (aDataTable.Rows.Count > 0)
            {
                hdComUnitId.Value = aDataTable.Rows[0]["ComUnitId"].ToString();
                hdCustomerMasterId.Value = aDataTable.Rows[0]["CustomerMasterId"].ToString();
                custNameTextBox.Text = aDataTable.Rows[0]["CustomerName"].ToString();
                custAddressTextBox.Text = aDataTable.Rows[0]["Address"].ToString();
                
                //areaNameTextBox.Text = aDataTable.Rows[0]["AreaName"].ToString();
                //comUnitNameTextBox.Text = aDataTable.Rows[0]["ComUnitCode"].ToString() + ":" + aDataTable.Rows[0]["ComUnitName"].ToString();
               
                //marketNameTextBox.Text = aDataTable.Rows[0]["MarketName"].ToString();
                
            }
            else
            {
            }
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
        aDataTable.Columns.Add("ExpireDate");
        aDataTable.Columns.Add("BatchNo");


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
        dataRow["ExpireDate"] = "";
        dataRow["BatchNo"] = "";


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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        AddRowInGrid();
        CalculateTotal();
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

        aDataTable.Columns.Add("ExpireDate");
        aDataTable.Columns.Add("BatchNo");

        aDataTable.Columns.Add("IsFoc");


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

                TextBox expireDateTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("expDateTextBox");
                TextBox batchNoTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("batchNoTextBox");

                CheckBox chkIsGiftProduct = (CheckBox)productGridView.Rows[i].Cells[3].FindControl("chkIsGiftProduct");
                dataRow["IsFoc"] = chkIsGiftProduct.Checked ? true : false;

                dataRow["ExpireDate"] = expireDateTextBox.Text;
                dataRow["BatchNo"] = batchNoTextBox.Text.Trim();

                dataRow["TotalTradePrice"] = TotaltpVatTextBox.Text;
                dataRow["TotalVatAmount"] = TotaltpVatTextBox.Text;
                dataRow["NetAmount"] = TotalGrossValueTextBox.Text;

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
        dataRow["IsFoc"] = false;
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
            //TextBox productTextBox = (TextBox)productGridView.Rows[row.RowIndex].Cells[3].FindControl("productNameTextBox");
           

        }
        for (int j = 0; j < aDataTable.Rows.Count; j++)
        {

            var a = aDataTable.Rows[j]["IsFoc"];


            CheckBox isGiftProductTextBox = (CheckBox)productGridView.Rows[j].Cells[3].FindControl("chkIsGiftProduct");
            isGiftProductTextBox.Checked = Convert.ToBoolean(aDataTable.Rows[j]["IsFoc"]);
            //DropDownList ddlIsCampaignProductDropDownList =
            //    ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsCampaignProductDropDownList"));
            //ddlIsCampaignProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsCampaignProductDropDownList"].ToString();


            //DropDownList ddIsGiftProductDropDownList =
            //   ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsGiftProductDropDownList"));
            //ddIsGiftProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsGiftProductDropDownList"].ToString();
        }
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

        aDataTable.Columns.Add("ExpireDate");
        aDataTable.Columns.Add("BatchNo");
        aDataTable.Columns.Add("IsFoc");

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
                    TextBox expireDateTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("expDateTextBox");
                    TextBox batchNoTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("batchNoTextBox");

                    dataRow["ExpireDate"] = expireDateTextBox.Text;
                    dataRow["BatchNo"] = batchNoTextBox.Text.Trim();

                    dataRow["TotalTradePrice"] = TotaltpTextBox.Text;
                    dataRow["TotalVatAmount"] = TotaltpVatTextBox.Text;
                    dataRow["NetAmount"] = TotalGrossValueTextBox.Text;

                    CheckBox isGiftProductTextBox = (CheckBox)productGridView.Rows[i].Cells[3].FindControl("chkIsGiftProduct"); 

                    //DropDownList C =
                    //(DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsCampaignProductDropDownList");
                    //DropDownList G =
                    //(DropDownList)productGridView.Rows[i].Cells[1].FindControl("IsGiftProductDropDownList");

                    //dataRow["IsCampaignProductDropDownList"] = C.SelectedItem.Text;
                    dataRow["IsFoc"] = isGiftProductTextBox.Checked ? true:false;


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
            CheckBox isGiftProductTextBox = (CheckBox)productGridView.Rows[j].Cells[3].FindControl("chkIsGiftProduct");

            isGiftProductTextBox.Checked = Convert.ToBoolean(aDataTable.Rows[j]["IsFoc"]);


            //DropDownList ddlIsCampaignProductDropDownList =
            //    ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsCampaignProductDropDownList"));
            //ddlIsCampaignProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsCampaignProductDropDownList"].ToString();


            //DropDownList ddIsGiftProductDropDownList =
            //   ((DropDownList)productGridView.Rows[j].Cells[5].FindControl("IsGiftProductDropDownList"));
            //ddIsGiftProductDropDownList.SelectedValue = aDataTable.Rows[j]["IsGiftProductDropDownList"].ToString();
        }

        CalculateTotal();
    }



    protected void printButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }


    private bool SaveValidation()
    {
        if (ddlSalesCenter.SelectedValue == "")
        {
            ShowMessageBox("Please select Sales Center !!!");
            return false;
        }
        
        if (custCodeTextBox.Text.Trim() == "")
        {
            ShowMessageBox("Please select customer code !!!");
            return false;
        }
        
        if (custNameTextBox.Text.Trim() == "")
        {
            ShowMessageBox("Please select customer name !!!");
            return false;
        }

        if (ddlReturnReason.SelectedValue == "")
        {
            ShowMessageBox("Please select return reason !!!");
            return false;
        }

        if (orderDateTextBox.Text == "")
        {
            ShowMessageBox("Please select return date !!!");
            return false;
        }

        if (productGridView.Rows.Count < 1)
        {
            ShowMessageBox("Please select at lest one item !!!");
            return false;
        }

        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (SaveValidation())
        {
            Invoice aMasterDao = new Invoice();

            aMasterDao.ComUnitId = Convert.ToInt32(ddlSalesCenter.SelectedValue.Trim());
            aMasterDao.CustomerMasterId = Convert.ToInt32(hdCustomerMasterId.Value);
            aMasterDao.TpTotal = Convert.ToDecimal(tpTptalTextBox.Text);
            aMasterDao.TpDiscount = Convert.ToDecimal(0);
            aMasterDao.TpVat = Convert.ToDecimal(vatTotalTextBox.Text);
            aMasterDao.TpGrandTotal = Convert.ToDecimal(grandTotalTextBox.Text);
            aMasterDao.InvoiceId = hdfInvoiceId.Value != "" ? Convert.ToInt32(hdfInvoiceId.Value) : 0;
            aMasterDao.InvoiceDate = Convert.ToDateTime(orderDateTextBox.Text);

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                //aMasterDao.EntryDate = DateTime.Now;
            }
            //else
            //{
            //    aMasterDao.TopSheetGenReportId = Convert.ToInt32(masterHiddenFieldId.Value);
            //    aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
            //    aMasterDao.UpdateDate = DateTime.Now;
            //}


            InvoiceDetail aDetaildao;
            List<InvoiceDetail> aList = new List<InvoiceDetail>();

            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                aDetaildao = new InvoiceDetail();

                TextBox productCodeTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productCodeTextBox");
                TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                TextBox tbxQuantity = (TextBox)productGridView.Rows[i].Cells[2].FindControl("reqQtyTextBox");
                TextBox tbxTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tpTextBox");
                TextBox tbxVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("vatTextBox");
                TextBox tbxTotalTradePrice = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpTextBox");
                TextBox tbxTotalVat = (TextBox)productGridView.Rows[i].Cells[2].FindControl("TotaltpVatTextBox");
                TextBox tbxGrossValue = (TextBox)productGridView.Rows[i].Cells[2].FindControl("tblGrossValue");
                TextBox tbxBatchTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("batchNoTextBox");
                TextBox expireDateTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("expDateTextBox");
                CheckBox isFocCheckBox = (CheckBox)productGridView.Rows[i].Cells[2].FindControl("chkIsGiftProduct");

                aDetaildao.ProductCode = productCodeTextBox.Text.Trim();
                aDetaildao.ProductName = productNameTextBox.Text.Trim();
                aDetaildao.UnitPrice = Convert.ToDecimal(tbxTradePrice.Text.Trim());
                aDetaildao.UnitVatAmount = Convert.ToDecimal(tbxVat.Text.Trim());
                aDetaildao.TotalPrice = Convert.ToDecimal(tbxTotalTradePrice.Text.Trim());
                aDetaildao.TotalVat = Convert.ToDecimal(tbxTotalVat.Text.Trim());
                aDetaildao.Quantity = Convert.ToDecimal(tbxQuantity.Text.Trim());
                aDetaildao.NetAmount = Convert.ToDecimal(tbxGrossValue.Text.Trim());
                aDetaildao.BatchNo = tbxBatchTextBox.Text.Trim();
                aDetaildao.ExpDate = Convert.ToDateTime(expireDateTextBox.Text.Trim());
                aDetaildao.ISGiftProductforInv = isFocCheckBox.Checked ? Convert.ToBoolean(1) : Convert.ToBoolean(0);
                aList.Add(aDetaildao);
            }

            ResultInfo Res = aReturnDal.SaveSalesReturn(aMasterDao, aList);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','SalesReturnNew.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesReturnNewView.aspx");
    }
}