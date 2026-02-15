using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
//using Library.DAO.InvoiceCamDAO;
using Library.DAO.SInventory_Entities;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

public partial class SInventory_UI_CustomerUpload : System.Web.UI.Page
{

    WarehouseStockInBll aWarehouseStockInBll = new WarehouseStockInBll();


    private CustomerExcelUploadDal aUploadDal = new CustomerExcelUploadDal();

   // private int count=0;

    private int ExcelCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialGrid();
          
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

   

   

    private void ExcelToGrid()
    {

        lbl_up_status.CssClass = "";
        string FileName = Path.GetFileName(id_fu.PostedFile.FileName);
        string Extension = Path.GetExtension(id_fu.PostedFile.FileName);
        string FilePath = "~/ExcelFiles/" + id_fu.FileName;
        id_fu.SaveAs(MapPath(FilePath));

        string path = System.IO.Path.GetFullPath(Server.MapPath(FilePath));
        OleDbConnection oledbConn = null;

        if (Path.GetExtension(path) == ".xls")
        {
            oledbConn =
                new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                    ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
        }
        else if (Path.GetExtension(path) == ".xlsx")
        {
            oledbConn =
                new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                    ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
        }

        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = oledbConn;

        oledbConn.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        oledbConn.Close();

        oledbConn.Open();
        cmdExcel.CommandText = "SELECT  * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        oledbConn.Close();

        DataTable destinationTable = new DataTable();
        destinationTable = dt.Clone();

        foreach (DataRow row in dt.Rows)
        {
            if (!string.IsNullOrEmpty(row[0].ToString()))
            {
                destinationTable.ImportRow(row);
            }
        }
        string fileName = Path.GetFileName(FilePath);
        //txtSheetName.Text = fileName;

        productGridView.DataSource = destinationTable;
        productGridView.DataBind();
        lbl_up_status.CssClass = "alert alert-info";

        lbl_up_status.Text = "File Name:" + fileName + " [ " + productGridView.Rows.Count.ToString() + " records Found!]";
        IsFileUploaded.Value = "true";

 

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            TextBox MarketCodeTextBoxTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("MarketCodeTextBox");

            CheckCustomerInGrid(i, MarketCodeTextBoxTextBox.Text.Trim());

        }
    }

    protected void btnUpload_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (id_fu.PostedFile.FileName != "")
            {
                ExcelToGrid();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Excel file is not a correct format !" + "','Faild');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Excel file is not a correct format !" + "','Faild');", true);
        }
    }

    private void InitialGrid()
    {
        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("MarketCode");
        aDataTable.Columns.Add("CustomerCode");
        aDataTable.Columns.Add("CustomerName");
        aDataTable.Columns.Add("Address");
        aDataTable.Columns.Add("OwnerName");
        aDataTable.Columns.Add("CellNo");
        aDataTable.Columns.Add("TermOfPayment");

        DataRow dataRow;

        dataRow = aDataTable.NewRow();

        dataRow["MarketCode"] = "";
        dataRow["CustomerCode"] = "";
        dataRow["CustomerName"] = "";
        dataRow["Address"] = "";
        dataRow["OwnerName"] = "";
        dataRow["CellNo"] = "";
        dataRow["TermOfPayment"] = "";

        aDataTable.Rows.Add(dataRow);

        productGridView.DataSource = null;
        productGridView.DataBind();
        productGridView.DataSource = aDataTable;
        productGridView.DataBind();

    }

    private void AddRowInGrid()
    {
        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("MarketCode");
        aDataTable.Columns.Add("CustomerCode");
        aDataTable.Columns.Add("CustomerName");
        aDataTable.Columns.Add("Address");
        aDataTable.Columns.Add("OwnerName");
        aDataTable.Columns.Add("CellNo");
        aDataTable.Columns.Add("TermOfPayment");

        DataRow dataRow;

        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {

                dataRow = aDataTable.NewRow();

                TextBox MarketCodeTextBox = (TextBox)productGridView.Rows[i].Cells[1].FindControl("MarketCodeTextBox");
                dataRow["ProductCode"] = MarketCodeTextBox.Text.Trim();

                TextBox CustomerCodeTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("CustomerCodeTextBox");
                dataRow["ProductName"] = CustomerCodeTextBox.Text.Trim();

                TextBox CustomerNameTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("CustomerNameTextBox");
                dataRow["PackSize"] = CustomerNameTextBox.Text;

                TextBox AddressTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("AddressTextBox");
                dataRow["UOM"] = AddressTextBox.Text;

                TextBox OwnerNameTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("OwnerNameTextBox");
                dataRow["Batch"] = OwnerNameTextBox.Text;

                TextBox CellNoTextBox = (TextBox)productGridView.Rows[i].Cells[6].FindControl("CellNoTextBox");
                dataRow["ExpDate"] = CellNoTextBox.Text.Trim();

                aDataTable.Rows.Add(dataRow);
            }
        }
        int sl = aDataTable.Rows.Count;

        dataRow = aDataTable.NewRow();

        dataRow["MarketCode"] = "";
        dataRow["CustomerCode"] = "";
        dataRow["CustomerName"] = "";
        dataRow["Address"] = "";
        dataRow["OwnerName"] = "";
        dataRow["CellNo"] = "";
        dataRow["TermOfPayment"] = "";


        aDataTable.Rows.Add(dataRow);


        productGridView.DataSource = null;
        productGridView.DataBind();
        productGridView.DataSource = aDataTable;
        productGridView.DataBind();


       // CalculateQuantityVatAndValue();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        AddRowInGrid();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        aDataTable.Columns.Add("UOM");
        aDataTable.Columns.Add("ProductId");
        aDataTable.Columns.Add("PackSize");

        aDataTable.Columns.Add("Batch");
        aDataTable.Columns.Add("ExpDate");
        aDataTable.Columns.Add("MfgDate");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("Price");
        aDataTable.Columns.Add("Vat");
        aDataTable.Columns.Add("TotalAmount");

        DataRow dataRow;

        if (productGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    TextBox productCodeTextBox2 = (TextBox)productGridView.Rows[i].Cells[1].FindControl("productCodeTextBox");
                    dataRow["ProductCode"] = productCodeTextBox2.Text.Trim();
                    TextBox productNameTextBox = (TextBox)productGridView.Rows[i].Cells[2].FindControl("productNameTextBox");
                    dataRow["ProductName"] = productNameTextBox.Text.Trim();
                    TextBox packSizeTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("packSizeTextBox");
                    TextBox uomTextBox = (TextBox)productGridView.Rows[i].Cells[3].FindControl("uomTextBox");
                    dataRow["PackSize"] = packSizeTextBox.Text;
                    dataRow["UOM"] = uomTextBox.Text;

                    HiddenField productidHiddenField = (HiddenField)productGridView.Rows[i].Cells[0].FindControl("productidHiddenField");

                    TextBox batchTextBox = (TextBox)productGridView.Rows[i].Cells[4].FindControl("batchTextBox");
                    dataRow["Batch"] = batchTextBox.Text;

                    TextBox expDateTextBox =
                        (TextBox)productGridView.Rows[i].Cells[6].FindControl("expDateDateTextBox");
                    dataRow["ExpDate"] = expDateTextBox.Text.Trim();

                    TextBox mfgDateTextBox = (TextBox)productGridView.Rows[i].Cells[5].FindControl("mfgDateTextBox");
                    dataRow["MfgDate"] = mfgDateTextBox.Text.Trim();

                    TextBox quantityTextBox = (TextBox)productGridView.Rows[i].Cells[7].FindControl("reqQtyTextBox");
                    dataRow["Quantity"] = quantityTextBox.Text.Trim();

                    TextBox costPriceTextBox = (TextBox)productGridView.Rows[i].Cells[8].FindControl("costPriceTextBox");
                    dataRow["Price"] = costPriceTextBox.Text.Trim();

                    TextBox vatTextBox = (TextBox)productGridView.Rows[i].Cells[9].FindControl("vatTextBox");
                    dataRow["Vat"] = vatTextBox.Text.Trim();

                    TextBox totalValueTextBox = (TextBox)productGridView.Rows[i].Cells[10].FindControl("totalValueTextBox");
                    dataRow["TotalAmount"] = totalValueTextBox.Text.Trim();

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

        CalculateQuantityVatAndValue();
    }



    private void CheckCustomerInGrid(int rowindex, string productCode)
    {
        DataTable aDataTable = new DataTable();

        if (!string.IsNullOrEmpty(productCode))
        {
            aDataTable = aUploadDal.CheckMarketCode(productCode);
            if (aDataTable.Rows.Count == 0)
            {
                ExcelCount++;
                productGridView.Rows[rowindex].BorderColor = Color.Brown;
                productGridView.Rows[rowindex].BackColor = Color.Crimson;
            }
        }
    }

    protected void productCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox productCodeTextBox = (TextBox)productGridView.Rows[rowindex].Cells[1].FindControl("productCodeTextBox");
        string[] splitstring = new string[] { };
        if (productCodeTextBox.Text.Contains(':'))
        {
            splitstring = productCodeTextBox.Text.Split(':');
            productCodeTextBox.Text = splitstring[0];
            string productCode = splitstring[0];
            CheckCustomerInGrid(rowindex, productCode);
        }

    }

    
    private void SaveAllData()
    {
        List<CustomerMaster> aDetailList = new List<CustomerMaster>();

        CustomerMaster aStockInMasterDao;

        for (int i = 0; i < productGridView.Rows.Count; i++)
        {
            aStockInMasterDao = new CustomerMaster();

            TextBox MarketCodeTextBox = (TextBox)productGridView.Rows[i].FindControl("MarketCodeTextBox");
            TextBox CustomerCodeTextBox = (TextBox)productGridView.Rows[i].FindControl("CustomerCodeTextBox");
            TextBox CustomerNameTextBox = (TextBox)productGridView.Rows[i].FindControl("CustomerNameTextBox");
            TextBox AddressTextBox = (TextBox)productGridView.Rows[i].FindControl("AddressTextBox");
            TextBox OwnerNameTextBox = (TextBox)productGridView.Rows[i].FindControl("OwnerNameTextBox");
            TextBox CellNoTextBox = (TextBox)productGridView.Rows[i].FindControl("CellNoTextBox");
            TextBox TermofPaymentTextBox = (TextBox)productGridView.Rows[i].FindControl("TermofPaymentTextBox");

            aStockInMasterDao.CustomerMasterId = 0;
            aStockInMasterDao.MarketCode = MarketCodeTextBox.Text.Trim();
            aStockInMasterDao.CustomerCode = CustomerCodeTextBox.Text;
            aStockInMasterDao.CustomerName = CustomerNameTextBox.Text;
            aStockInMasterDao.Address = AddressTextBox.Text;
            aStockInMasterDao.CellNo = CellNoTextBox.Text;
            aStockInMasterDao.TermOfPayment = TermofPaymentTextBox.Text;
            //Here Addrees2 Is OwnerName
            aStockInMasterDao.Addrees2 = OwnerNameTextBox.Text;
            aDetailList.Add(aStockInMasterDao);
        }


        ResultInfo aInfo = new ResultInfo();

        aInfo =  aUploadDal.Save_CustomerInfoByExcel(aDetailList);

        if (aInfo.isSuccess)
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successfully Done...');window.location ='CustomerUpload.aspx';",

       
                true);
        }


    }

    private bool Validation()
    {
        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (productGridView.Rows[i].BackColor == Color.Crimson)
                {
                    showMessageBox("Invalid Data detected");
                    return false;
                }

                TextBox MarketCodeTextBox = (TextBox) productGridView.Rows[i].FindControl("MarketCodeTextBox");

                if (MarketCodeTextBox.Text.Trim()== "")
                {
                    showMessageBox("Please Input Market Code");
                    productGridView.Rows[i].FindControl("MarketCodeTextBox").Focus();
                    return false;
                }

                TextBox CustomerCodeTextBox = (TextBox) productGridView.Rows[i].FindControl("CustomerCodeTextBox");
                if (CustomerCodeTextBox.Text.Trim() == "")
                {
                    showMessageBox("Please Input Customer Code");
                    productGridView.Rows[i].FindControl("CustomerCodeTextBox").Focus();
                    return false;
                }

                TextBox CustomerNameTextBox = (TextBox) productGridView.Rows[i].FindControl("CustomerNameTextBox");
                if (CustomerNameTextBox.Text.Trim() == "")
                {
                    showMessageBox("Please Input Customer Name");
                    productGridView.Rows[i].FindControl("CustomerNameTextBox").Focus();
                    return false;
                }

                TextBox AddressTextBox = (TextBox) productGridView.Rows[i].FindControl("AddressTextBox");
                if (AddressTextBox.Text.Trim() == "")
                {
                    showMessageBox("Please Input Customer Address");
                    productGridView.Rows[i].FindControl("AddressTextBox").Focus();
                    return false;
                }

                TextBox TermofPaymentTextBox = (TextBox) productGridView.Rows[i].FindControl("TermofPaymentTextBox");
                if (TermofPaymentTextBox.Text.Trim() == "")
                {
                    showMessageBox("Please Input Term of Payment");
                    productGridView.Rows[i].FindControl("TermofPaymentTextBox").Focus();
                    return false;
                }



            }
        }
        return true;
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            
            SaveAllData();
            
        }
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
        InitialGrid();


        submitButton.Text = "Save";

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
            CheckCustomerInGrid(rowindex, productCode);
        }
        else
        {
            showMessageBox("Input Correct Data!!");
        }
    }

    protected void reqQtyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        CalculateTotalValue(rowindex);
    }

    protected void costPriceTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        CalculateTotalValue(rowindex);
    }

    private void CalculateTotalValue(int rowindex)
    {
        TextBox quantityTextBox = (TextBox)productGridView.Rows[rowindex].Cells[7].FindControl("reqQtyTextBox");
        TextBox costPriceTextBox = (TextBox)productGridView.Rows[rowindex].Cells[8].FindControl("costPriceTextBox");
        TextBox vatTextBox = (TextBox)productGridView.Rows[rowindex].Cells[9].FindControl("vatTextBox");
        TextBox totalValueTextBox = (TextBox)productGridView.Rows[rowindex].Cells[10].FindControl("totalValueTextBox");

        decimal totalValue = 0;
        decimal totalVat = 0;
        string vat = vatTextBox.Text;
        string quantity = quantityTextBox.Text;
        string price = costPriceTextBox.Text;

        if (quantity != "" && price != "" && vat != "")
        {
            totalVat = totalVat + (Convert.ToDecimal(quantity) * Convert.ToDecimal(vat));
            totalValue = totalValue + (totalVat + (Convert.ToDecimal(quantity) * Convert.ToDecimal(price)));
            totalValueTextBox.Text = totalValue.ToString(CultureInfo.InvariantCulture);

            CalculateQuantityVatAndValue();
        }

        else
        {
            totalValueTextBox.Text = "";
            CalculateQuantityVatAndValue();
        }
    }

    private void CalculateQuantityVatAndValue()
    {
        int totalQuantity = 0;
        decimal value = 0;
        decimal totalVat = 0;

        if (productGridView.Rows.Count > 0)
        {
            for (int i = 0; i < productGridView.Rows.Count; i++)
            {
                if (((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Text != "")
                {
                    int qty = Convert.ToInt32(((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Text);
                    totalQuantity = totalQuantity + qty;
                }

                if (((TextBox)productGridView.Rows[i].FindControl("vatTextBox")).Text != "")
                {
                    if (((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Text != "")
                    {
                        int qty = Convert.ToInt32(((TextBox)productGridView.Rows[i].FindControl("reqQtyTextBox")).Text);
                        decimal vat = Convert.ToDecimal(((TextBox)productGridView.Rows[i].FindControl("vatTextBox")).Text);
                        totalVat = totalVat + (qty * vat);
                    }

                }

                if (((TextBox)productGridView.Rows[i].FindControl("totalValueTextBox")).Text != "")
                {
                    decimal costPrice = Convert.ToDecimal(((TextBox)productGridView.Rows[i].FindControl("totalValueTextBox")).Text);
                    value = value + costPrice;
                }
            }


        }
    }

    protected void totalValueTextBox_OnTextChanged(object sender, EventArgs e)
    {
        CalculateQuantityVatAndValue();
    }

    protected void viewLinkButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseStockInView.aspx");
    }

    protected void vatTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        CalculateTotalValue(rowindex);
    }



    protected void mfgDateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox mfgDateTextBox = (TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("mfgDateTextBox");

        DateTime value;

        if (!DateTime.TryParse(mfgDateTextBox.Text, out value))
        {
            mfgDateTextBox.Text = "";
            showMessageBox("Please insert valid date");
        }
        else
        {
            DateTime dd = Convert.ToDateTime(((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("mfgDateTextBox")).Text);
            ((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("expDateDateTextBox")).Text = (dd.AddYears(2).ToString("dd-MMM-yyyy"));
            //string dateStr = ((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("mfgDateTextBox")).ToString();
            //DateTime date;
            //if (DateTime.TryParseExact(dateStr, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            //{
            //    // successfully parsed the string into a DateTime instance =>
            //    // here we could add the desired number of months to it and construct
            //    // a new DateTime
            //    DateTime newDate = date.AddYears(2);
            //    ((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("expDateDateTextBox")).Text = newDate.ToString();
            //}

        }


    }

    protected void expDateDateTextBox_OnTextChanged(object sender, EventArgs e)
    {

        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        if (((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("mfgDateTextBox")).Text != "")
        {
            TextBox expDateDateTextBox = (TextBox)productGridView.Rows[rowindex].Cells[6].FindControl("expDateDateTextBox");
            DateTime value;

            if (!DateTime.TryParse(expDateDateTextBox.Text, out value))
            {
                expDateDateTextBox.Text = "";
                showMessageBox("Please insert valid date");
            }
            DateTime Mdate =
                Convert.ToDateTime(((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("mfgDateTextBox")).Text);
            DateTime ExDate =
              Convert.ToDateTime(((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("expDateDateTextBox")).Text);

            if (ExDate < Mdate)
            {
                ((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("expDateDateTextBox")).Text = string.Empty;
                showMessageBox("Date Error!!");
            }
        }
        else
        {
            showMessageBox("Please Insert Manufacturing Date!!");
            ((TextBox)productGridView.Rows[rowindex].Cells[5].FindControl("expDateDateTextBox")).Text = string.Empty;
        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Clear();
    }
}