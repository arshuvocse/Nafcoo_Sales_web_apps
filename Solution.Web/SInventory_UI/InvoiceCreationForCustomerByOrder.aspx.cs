using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.ReportAppServer.DataDefModel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Library.BLL.SInventory_BLL;
using Library.DAL.InvoiceOrderDAL;
using Library.DAL.SInventory_DAL;
using Library.DAO.InvoiceCamDAO;
using Library.DAO.SInventory_Entities;
using Color = System.Drawing.Color;

public partial class SInventory_UI_InvoiceCreationForCustomer : System.Web.UI.Page
{
    RequisitionBLL aRequisitionBll = new RequisitionBLL();
    InvoiceBLL aInvoiceBll = new InvoiceBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["OrderId"] != null)
            {
                orderHiddenField.Value = Session["OrderId"].ToString();
                LoadAllDataByOrder(orderHiddenField.Value.ToString());
                //LoadAllDataByOrder2(orderHiddenField.Value.ToString());
                CustomerCreditAmount();
                Session["OrderId"] = null;

               
                TotalValueCalculation();
            }
            aInvoiceBll.PaymentTypeLoadBLL(payTypeDDL);
            payTypeDDL.SelectedIndex = 1;
            Todate();
        }
    }

    public DataTable TempOrderData(List<OrderDetails> aDetailses)
    {
        //DataTable aDataTable=new DataTable();
        //aDataTable.Rows.Add("ProductCode");
        //aDataTable.Rows.Add("Quantiy");
        //aDataTable.Rows.Add("CampaignName");

        return null;
    }

    public DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }


    public void ChangedOrder(DataTable aDataTable)
    {
        //orderHiddenField.Value = Session["OrderId"].ToString();
        LoadAllDataByOrder2(orderHiddenField.Value.ToString(),aDataTable);
        CustomerCreditAmount();
        Session["OrderId"] = null;

        DataTable dtwar = aInvoiceBll.GetWarning(hdCustomerMasterId.Value, custCodeTextBox.Text);
        if (dtwar.Rows.Count > 0)
        {
            string a = dtwar.Rows[0]["Details"].ToString();
            warningLabel.Text = "This customer has Dues(time  more than 30 days)" + a;
        }
        else
        {
            warningLabel.Text = "";
        }
        AdjustmentAmount();

        //shuvo
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            TextBox npTextBox = (TextBox)GridView1.Rows[i].Cells[11].FindControl("npTextBox");
            TextBox tpVatTextBox = (TextBox)GridView1.Rows[i].Cells[8].FindControl("tpVatTextBox");
            decimal tpTextBox = 0;
            try
            {
                tpTextBox = Convert.ToDecimal(
                ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim());
            }
            catch
            {

            }
            decimal dpAmtTextBox = 0;
            try
            {

                dpAmtTextBox = Convert.ToDecimal(
                                   ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                                       .Text.Trim());

            }
            catch
            {

            }

            decimal amTextBox = 0;
            try
            {

                amTextBox = Convert.ToDecimal(
                                   ((TextBox)GridView1.Rows[i].Cells[10].FindControl("amTextBox"))
                                       .Text.Trim());

            }
            catch
            {

            }

            decimal tpVat = 0;
            try
            {

                tpVat = Convert.ToDecimal(tpVatTextBox.Text);

            }
            catch
            {

            }

            decimal rS = (tpTextBox - (dpAmtTextBox + amTextBox)) + tpVat;
            npTextBox.Text = rS.ToString();
        }
        TotalValueCalculation2();
    }


    public void AdjustmentAmount()
    {
        decimal divider = Convert.ToDecimal(gridLineItemGridView.Rows.Count);
        decimal amount =string.IsNullOrEmpty(crAmountTextBox.Text)?0:Convert.ToDecimal(crAmountTextBox.Text);
        decimal count = 0;
        for(int i =0;i<gridLineItemGridView.Rows.Count;i++)
        {
            if(gridLineItemGridView.Rows[i].BackColor == Color.Red)
            {
                divider = divider - 1;
               
            }
        }
        if (divider>0)
        {
            decimal peramount = amount / divider;
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {

                if (gridLineItemGridView.Rows[i].BackColor == Color.Red)
                {
                 
                    ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("amTextBox")).Text = "0";
                }

                else { 
                ((TextBox) gridLineItemGridView.Rows[i].Cells[1].FindControl("amTextBox")).Text = peramount.ToString("F");
                }
            }
        }
       
    }

    private void CustomerCreditAmount()
    {
        //OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        //DataTable custTable = new DataTable();
        //custTable = aOrderInfoBll.GetCustomerCredit(hdCustomerMasterId.Value);
        //crAmountTextBox.Text = custTable.Rows[0]["Amount"].ToString();
        rcvAmountTextBox.Text = ((string.IsNullOrEmpty(grandTotalTextBox.Text) ? 0 : Convert.ToDecimal((grandTotalTextBox.Text))) - (string.IsNullOrEmpty(crAmountTextBox.Text) ? 0 : Convert.ToDecimal((crAmountTextBox.Text)))).ToString();
        //(Convert.ToDecimal(grandTotalTextBox.Text) - Convert.ToDecimal(crAmountTextBox.Text)).ToString(); ddd
    }
    protected void backLinkButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceCreationByOrder.aspx");
        //Response.Redirect("http://103.244.247.93:91/SInventory_UI/InvoiceCreationByOrder.aspx", true);

    }

    public void Todate()
    {
        invDateTextBox.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy");
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
    private bool Validation()
    {
        //if (warningLabel.Text == "This customer has Dues(time  more than 30 days)")
        //{
        //    showMessageBox("This customer has Dues(time  more than 30 days)!!");
        //    return false;
        //}

        rcvAmountTextBox.ReadOnly = false;

        if (rcvAmountTextBox.Text.Trim() == "" || rcvAmountTextBox.Text.Trim() == "0.000")
        {

            if (Convert.ToDecimal(rcvAmountTextBox.Text) <= 0)
            {
                showMessageBox("Receiveable Amount should not be 0 or less than 0 !!");
                return false;
            }
            
        }

        rcvAmountTextBox.ReadOnly = true; 

        if (orderNoTextBox.Text == "")
        {
            showMessageBox("Please Input Order Number!!");
            return false;
        }

        if (orderDateTextBox.Text == "")
        {
            showMessageBox("Please Input Order Date!!");
            return false;
        }

        if (payTypeDDL.Text == "")
        {
            showMessageBox("Please Input Payment Type!!");
            return false;
        }
        if (Convert.ToDecimal(rcvAmountTextBox.Text) < 0)
        {
            showMessageBox("Invalid Receivable Amount!!");
            return false;
        }
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim() == "" || ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim() == "")
            {
                showMessageBox("Please Remove Blank Row!!");
                return false;
            }
        }
        return true;
    }

    private void GetCustInfo(string custCode, string   OrderNO)
    {
        if (!string.IsNullOrEmpty(custCode))
        {
            custCodeTextBox.Text = custCode;
            DataTable aDataTable = new DataTable();
            aDataTable = aInvoiceBll.CustomerMaster( OrderNO);
            
            if (aDataTable.Rows.Count > 0)
            {
                hdComUnitId.Value = aDataTable.Rows[0]["ComUnitId"].ToString();
                hdCustomerMasterId.Value = aDataTable.Rows[0]["CustomerMasterId"].ToString();
                custNameTextBox.Text = aDataTable.Rows[0]["CustomerName"].ToString();
                custAddressTextBox.Text = aDataTable.Rows[0]["Address"].ToString();
                districtNameTextBox.Text = aDataTable.Rows[0]["ASMEmpName"].ToString();
                areaNameTextBox.Text = aDataTable.Rows[0]["AreaName"].ToString();
                comUnitNameTextBox.Text = aDataTable.Rows[0]["ComUnitCode"].ToString() + ":" + aDataTable.Rows[0]["ComUnitName"].ToString();
                miaCodeTextBox.Text = aDataTable.Rows[0]["MIOEmpMastercode"].ToString();
                hdMiaId.Value = aDataTable.Rows[0]["MIOEmpInfoId"].ToString();
                marketNameTextBox.Text = aDataTable.Rows[0]["MarketName"].ToString();
                //miaNameTextBox.Text = aDataTable.Rows[0]["MIOEmpName"].ToString();
                custCategoryTextBox.Text = aDataTable.Rows[0]["Type"].ToString();//Green/Blue/Pnk
                //hdMiaId.Value = aDataTable.Rows[0]["MiaId"].ToString();
                cusTypeTextBox.Text = aDataTable.Rows[0]["CustomerType"].ToString();//FCB/Institue/Genral
                //payTypeDDL.SelectedValue = aDataTable.Rows[0]["TermOfPayment"].ToString();//Cash/Credit
                
                


                //DataTable aTable = aInvoiceBll.GetCustomerPaymebtType(hdCustomerMasterId.Value);
                //var t = aTable.Rows[0]["TermOfPayment"].ToString();

                //payTypeDDL.SelectedValue = t;

                
            }
            else
            {

            }
        }
    }

    public bool OrderExists(string orderId)
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderExistsBll(orderId);
        if (aTable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Moticare()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "FGDsa02" && Qty >= 4)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool Nervaid75mgCapsuleFOC()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "AELss02" && Qty >= 3)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool FlexidolFOC()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "AIssD01" && Qty >= 422222)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool Ezepain()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "AIDs04" && Qty >= 5)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool MaxiventFOC()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "sOAD04" && Qty >= 2)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool SeacoralDTablet()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty =
                    Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "MNSs03" && Qty >= 5)
                {
                    return true;
                }
            }
        }
        return false;
    }
    //private bool ChkProductOffer()
    //{
    //    for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
    //    {
    //        string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
    //        int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
    //        if (ProductCode == "AID01" && Qty >= 2)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    private bool CefimaxFOC()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "ANB0s85" && Qty >= 9898884)
                {
                    return true;
                }
            }
        }

        return false;
    }
    private bool CiprodylFOC()
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();

        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderHiddenField.Value);
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(aTable.Rows[0]["CustomerCode"].ToString());
        if (Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"]) == true)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
                if (ProductCode == "ANBs098" && Qty >= 989894)
                {
                    return true;
                }
            }
        }

        return false;
    }


protected void CheckAndUpdateGrid()
{
    // Loop through each row in the GridView

    if (gridLineItemGridView.Rows.Count > 0)
    {
        int count = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            if (Qty == 0)
            {
                count++;
            }

        }

        if (count > 0)
        {
            lowstockMsg.Visible = true;
        }
        else
        {
            lowstockMsg.Visible = false;
        }
    }

}

    public void LoadAllDataByOrder(string orderId)
    {
        if (OrderExists(orderHiddenField.Value) == false)
        {
            OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
            DataTable aTable = new DataTable();
            aTable = aOrderInfoBll.LoadOrderWithDetail(orderId);
            orderNoTextBox.Text = aTable.Rows[0]["OrderCode"].ToString();
            orderIdHiddenField.Value = aTable.Rows[0]["OrderId"].ToString();
            orderDateTextBox.Text = Convert.ToDateTime(aTable.Rows[0]["SubmissionDate"].ToString()).ToString("dd-MMM-yyyy");

            txtOrderTime.Text = Convert.ToDateTime(aTable.Rows[0]["EntryDate"].ToString()).ToString("HH:mm:ss:tt");

            GetCustInfo(aTable.Rows[0]["CustomerCode"].ToString(), orderNoTextBox.Text.Trim());
            int numberOfRecords = aTable.Rows.Count;


            // Start loop

            for (int i = 0; i < aTable.Rows.Count; i++)
            {
                AddFunc();
                ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value =
                    aTable.Rows[i]["OrderDetailId"].ToString();
                GetProduct(i, aTable.Rows[i]["ProductCode"].ToString());
                /////SMC Low Stock Method /////
                decimal cstock = 0;
                cstock = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                if (cstock < Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()))
                {
                    GetQty(cstock, i);
                   // GetQty(Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()), i);
                }
                ///////////////////
                else
                {
                    GetQty(Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()), i);
                }

                //if (((TextBox) gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text == "0")
                //{
                    ((TextBox) gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                       // "true";
                    aTable.Rows[i]["IsCampaignProduct"].ToString();

                //}
                //else
                //{
                //      ((TextBox) gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                //   "false";


                //}
              


                ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("bQtyTextBox")).Text =
                   aTable.Rows[i]["ISGiftProduct"].ToString();
                
                ((HiddenField)gridLineItemGridView.Rows[i].Cells[10].FindControl("CampaignTypeHiddenField")).Value =
                  aTable.Rows[i]["CampaignName"].ToString();

                //// hdfIsCampaignProduct

                //((HiddenField)gridLineItemGridView.Rows[i].Cells[10].FindControl("hdfIsCampaignProduct")).Value = aTable.Rows[i]["ISGiftProduct"].ToString();

                //// hdfIsGiftProduct

                //((HiddenField)gridLineItemGridView.Rows[i].Cells[10].FindControl("hdfIsGiftProduct")).Value = aTable.Rows[i]["ISGiftProduct"].ToString();
                //((HiddenField)gridLineItemGridView.Rows[i].Cells[10].FindControl("hdfOrderQuantity")).Value = Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()).ToString();

            }

            // End loop


            // ----------------------------------------------------------------------------------------------------------

            //   else
            {
                decimal totalprice = Convert.ToDecimal(tpTptalTextBox.Text);

                {
                    for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
                    {
                        //string ProductCode1 =
                        //    ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;

                        if ((((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "1" || ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "true"))
                        {
                            totalprice -=
                                Convert.ToDecimal(
                                    ((TextBox)gridLineItemGridView.Rows[i].FindControl("tpTextBox")).Text);
                        }
                    }
                }


               
                decimal percentage = 0;

                // JEWEL
                // Frinds Hospital Start
                if (custCategoryTextBox.Text == "Dosti")
                {
                    percentage = 0;
                }

                else
                {
                    DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(orderHiddenField.Value);
                    if (custCategoryTextBox.Text == "Account Hunting" || custCategoryTextBox.Text == "General")
                    {
                        //2
                        DataTable dttradepolicy = aOrderInfoBll.GetTradeTerm(totalprice.ToString());
                        if (dttradepolicy.Rows.Count > 0)
                        {
                            percentage = 0;
                        }
                    }
                    else
                    {
                        //1
                        DataTable dttradepolicy = aOrderInfoBll.GetTradeTerm(totalprice.ToString());
                        if (dttradepolicy.Rows.Count > 0)
                        {
                            percentage = Convert.ToDecimal(dttradepolicy.Rows[0]["DiscountPerc"].ToString());
                        }
                    }
                }
                // Frinds Hospital end

             

                // Campaing 

                decimal totaldiscount = 0;
                //  totaldiscount = (percentage * Convert.ToDecimal(tpTptalTextBox.Text)) / 100;
                disTotalTextBox.Text = totaldiscount.ToString();
                for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
                {
                    // ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text = ((TextBox)gridLineItemGridView.Rows[i].Cells[14].FindControl("IsCampaignProduct")).Text;
                    decimal cstock = 0;
                    decimal tqty = 0;
                    tqty = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);
                    cstock =
                        Convert.ToDecimal(
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                    DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value,
                        ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                    tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());
                    if (cstock < tqty)
                    {
                        gridLineItemGridView.Rows[i].BackColor = Color.Red;
                        /////////SMC Low Order Method//////////
                        //if (cstock != 0)
                        {
                            // showMessageBox("Stock Not Avaialable");
                            // saveButton.Visible = false;
                            GetQty(cstock, i,true);
                        }
                        ///////////////////////
                    }
                    //////// SMC Low Stock Method////////
                    //else
                    /////////////////
                    {
                        DataTable dtproductvat =
                            aOrderInfoBll.ProductVat(
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                        DataTable dtdiscount =
                           aOrderInfoBll.ProductDiscount(
                               ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text,
                               hdCustomerMasterId.Value, invDateTextBox.Text);
                        decimal percamount = 0;
                        if (dtdiscount.Rows.Count > 0)
                        {
                            percamount = Convert.ToDecimal(dtdiscount.Rows[0]["DiscountPercentage"].ToString());
                        }
                        decimal totalamount = 0;
                        totalamount = Convert.ToDecimal(tpTptalTextBox.Text);
                        decimal productamount = 0;
                        productamount =
                            Convert.ToDecimal(
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim());
                        decimal productperc = 0;
                        //productperc = (productamount*100)/totalamount;
                        decimal mainper = 0;
                        //  mainper = (percentage * productperc) / 100;

                        bool spDis = Convert.ToBoolean(aTable.Rows[i]["IsSpDis"]);

                        // Frinds Hospital Start
                       // if (aTable.Rows[0]["CustomerCode"].ToString() == "158964" || aTable.Rows[0]["CustomerCode"].ToString() == "158399")
                        if (Convert.ToBoolean(aTable.Rows[i]["IsSpDis"]))
                        {
                            DataTable aTable2 = new DataTable();
                            aTable2 = aOrderInfoBll.LoadOrderWithDetailFrindsHospital(orderId, ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value);
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountAmount"].ToString()).ToString();
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountPercent"].ToString()).ToString(); 
                        }
                        //else if (aTable.Rows[0]["CustomerType"].ToString() == "NCOD")
                        //{
                        //    //if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "True")
                        //    //{

                        //    //    ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                        //    //  0.ToString();
                        //    //    ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                        //    //       0.ToString("F");
                        //    //}
                        //    //else
                        //    //{
                        //    //    ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                        //    //       percentage.ToString();
                        //    //    ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                        //    //        (Convert.ToDecimal(
                        //    //            ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) *
                        //    //         (percentage / 100)).ToString("F");
                        //    //}
                        //    for (int j = 0; j < gridLineItemGridView.Rows.Count; j++)
                        //    {
                        //        DataTable aTable2 = new DataTable();
                        //        aTable2 = aOrderInfoBll.LoadOrderWithDetailFrindsHospital(orderId, ((HiddenField)gridLineItemGridView.Rows[j].Cells[1].FindControl("orderdetailIdHiddenField")).Value);
                        //        ((TextBox)gridLineItemGridView.Rows[j].Cells[10].FindControl("dpAmtTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountAmount"].ToString()).ToString();
                        //        ((TextBox)gridLineItemGridView.Rows[j].Cells[10].FindControl("dpTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountPercent"].ToString()).ToString();

                        //    }

                        //}

                        else
                        {
                            if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "1" || ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "true")
                            {

                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                              0.ToString();
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                   0.ToString("F");
                            }
                              if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text == "True")
                            {

                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                              0.ToString();
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                   0.ToString("F");

                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("tpTextBox")).Text =
                          0.ToString();

                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("tpVatTextBox")).Text =
                          0.ToString();

                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("npTextBox")).Text =
                          0.ToString();
                            }
                              if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text != "1"
                                  && ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text != "True")
                            {
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                                   percentage.ToString();
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                    (Convert.ToDecimal(
                                        ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) *
                                     (percentage / 100)).ToString("F");
                            }
                        }
                        // Frinds Hospital End

                        // Resectin FOC Offer Discount End//

                        //////////Modified Version/////////////
                        //decimal vat = 0;
                        //vat = Convert.ToDecimal(dtproductvat.Rows[0]["VATPercentage"].ToString());
                        //((TextBox) gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                        //    (Convert.ToDecimal(((TextBox) gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text)*
                        //     (percamount/100)).ToString();

                        decimal withdiscount = 0;
                        withdiscount =
                            (Convert.ToDecimal(
                                ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                             Convert.ToDecimal(
                                 (((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text)));
                        //-
                        // Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text));
                        decimal vatamount = 0;
                        //vatamount = (Convert.ToDecimal(
                        //    ((TextBox) gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim())*vat)/100;
                        TextBox tpVatTextBox = (TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox");
                        //tpVatTextBox.Text = vatamount.ToString("F");
                        //((TextBox) gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                        //    (Convert.ToDecimal(((TextBox) gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text)*
                        //     (percamount/100)).ToString();

                        //amTextBox
                        //ddddddddddddd
                        TextBox npTextBox = (TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox");


                        //npTextBox.Text = ((Convert.ToDecimal(
                        //   ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                        //                 (Convert.ToDecimal(
                        //                      ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                        //                          .Text.Trim()) + Convert.ToDecimal(
                        //                      ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("amTextBox"))
                        //                          .Text.Trim()))) +
                        //                 Convert.ToDecimal(tpVatTextBox.Text)).ToString();
                        //old
                        //     npTextBox.Text = ((Convert.ToDecimal(
                        //((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                        //               Convert.ToDecimal(
                        //                   ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                        //                       .Text.Trim())) +
                        //              Convert.ToDecimal(tpVatTextBox.Text)).ToString();


                        //AdjustmentAmount();

                        decimal tpTextBox = 0;
                        try
                        {
                            tpTextBox = Convert.ToDecimal(
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim());
                        }
                        catch
                        {

                        }
                        decimal dpAmtTextBox = 0;
                        try
                        {

                            dpAmtTextBox = Convert.ToDecimal(
                                               ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                                                   .Text.Trim());

                        }
                        catch
                        {

                        }

                        decimal amTextBox = 0;
                        try
                        {

                            amTextBox = Convert.ToDecimal(
                                               ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("amTextBox"))
                                                   .Text.Trim());

                        }
                        catch
                        {

                        }

                        decimal tpVat = 0;
                        try
                        {

                            tpVat = Convert.ToDecimal(tpVatTextBox.Text);

                        }
                        catch
                        {

                        }

                       decimal rS = (tpTextBox - (dpAmtTextBox + amTextBox)) + tpVat;
                        npTextBox.Text = rS.ToString();

                        var sstt = ((TextBox) gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text;

                        if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "True"

                            && ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text == "True")
                        {

                            ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("npTextBox")).Text = 0.ToString();
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("tpVatTextBox")).Text = 0.ToString();
                            ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("tpTextBox")).Text = 0.ToString();
                            //(TextBox) gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox").te = 0;
                        }

                        TotalValueCalculation();
                    }
                }
                GetDiscounttotalValue();
            }
            if (gridLineItemGridView.Rows.Count == aTable.Rows.Count)
            {
                
            }
            else
            {
                Response.Redirect("InvoiceCreationByOrder.aspx");

            }
        }
        else
        {
            showMessageBox("Order Information already Exists !!");
        }


        // Method for Stock not Avilable for Campaign product

        CheckAndUpdateGrid();
    }

    public void LoadAllDataByOrder2(string orderId,DataTable aTable)
    {
        if (OrderExists(orderHiddenField.Value) == false)
        {
            OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
            //DataTable aTable = new DataTable();
            //aTable = aOrderInfoBll.LoadOrderWithDetail(orderId);
            //orderNoTextBox.Text = aTable.Rows[0]["OrderCode"].ToString();
            //orderIdHiddenField.Value = aTable.Rows[0]["OrderId"].ToString();
            //orderDateTextBox.Text = Convert.ToDateTime(aTable.Rows[0]["SubmissionDate"].ToString()).ToString("dd-MMM-yyyy");
            //GetCustInfo(aTable.Rows[0]["CustomerCode"].ToString(), orderNoTextBox.Text.Trim());
            int numberOfRecords = aTable.Rows.Count;


            for (int i = 0; i < aTable.Rows.Count; i++)
            {
                AddFunc2();
                //((HiddenField)GridView1.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value =
                //    aTable.Rows[i]["OrderDetailId"].ToString();
                GetProduct3(i, aTable.Rows[i]["ProductId"].ToString());
                /////SMC Low Stock Method /////
                decimal cstock = 0;
                cstock = Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                if (cstock < Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()))
                {
                    GetQty2(cstock, i);
                    // GetQty(Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()), i);
                }
                ///////////////////
                else
                {
                    GetQty2(Convert.ToDecimal(aTable.Rows[i]["Quantity"].ToString()), i);
                }

                //if (((TextBox) GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text == "0")
                //{
                    ((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                    // "true";
                    aTable.Rows[i]["IsCampaignProduct"].ToString();

                //}
                //else
                //{
                //      ((TextBox) GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                //   "false";


                //}



                ((TextBox)GridView1.Rows[i].Cells[10].FindControl("bQtyTextBox")).Text =
                   aTable.Rows[i]["ISGiftProduct"].ToString();
                ((HiddenField)GridView1.Rows[i].Cells[10].FindControl("CampaignTypeHiddenField")).Value =
                  aTable.Rows[i]["CampaingName"].ToString();

            }

            //   else
            {
                decimal totalprice = Convert.ToDecimal(tpTptalTextBox.Text);

                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        //string ProductCode1 =
                        //    ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text;

                        if ((((TextBox)GridView1.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "1"))
                        {
                            totalprice -=
                                Convert.ToDecimal(
                                    ((TextBox)GridView1.Rows[i].FindControl("tpTextBox")).Text);
                        }
                    }
                }



                decimal percentage = 0;


                // Frinds Hospital Start
                if (custCategoryTextBox.Text == "DOSTI")
                {
                    percentage = 0;
                }


                else
                {
                    DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(orderHiddenField.Value);
                    if (custCategoryTextBox.Text == "FCB")
                    {
                        //2
                        DataTable dttradepolicy = aOrderInfoBll.GetTradeTerm(totalprice.ToString());
                        if (dttradepolicy.Rows.Count > 0)
                        {
                            percentage = 0;
                        }
                    }
                    else
                    {
                        //1
                        DataTable dttradepolicy = aOrderInfoBll.GetTradeTerm(totalprice.ToString());
                        if (dttradepolicy.Rows.Count > 0)
                        {
                            percentage = Convert.ToDecimal(dttradepolicy.Rows[0]["DiscountPerc"].ToString());
                        }
                    }
                }
                // Frinds Hospital end



                // Campaing 

                decimal totaldiscount = 0;
                //  totaldiscount = (percentage * Convert.ToDecimal(tpTptalTextBox.Text)) / 100;
                disTotalTextBox.Text = totaldiscount.ToString();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    // ((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text = ((TextBox)GridView1.Rows[i].Cells[14].FindControl("IsCampaignProduct")).Text;
                    decimal cstock = 0;
                    decimal tqty = 0;
                    tqty = Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);
                    cstock =
                        Convert.ToDecimal(
                            ((TextBox)GridView1.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                    DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value,
                        ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                    tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());
                    if (cstock < tqty)
                    {
                        GridView1.Rows[i].BackColor = Color.Red;
                        /////////SMC Low Order Method//////////
                        //if (cstock != 0)
                        {
                            // showMessageBox("Stock Not Avaialable");
                            // saveButton.Visible = false;
                            GetQty2(cstock, i);
                        }
                        ///////////////////////
                    }
                    //////// SMC Low Stock Method////////
                    //else
                    /////////////////
                    {
                        DataTable dtproductvat =
                            aOrderInfoBll.ProductVat(
                                ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                        DataTable dtdiscount =
                           aOrderInfoBll.ProductDiscount(
                               ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text,
                               hdCustomerMasterId.Value, invDateTextBox.Text);
                        decimal percamount = 0;
                        if (dtdiscount.Rows.Count > 0)
                        {
                            percamount = Convert.ToDecimal(dtdiscount.Rows[0]["DiscountPercentage"].ToString());
                        }
                        decimal totalamount = 0;
                        totalamount = Convert.ToDecimal(tpTptalTextBox.Text);
                        decimal productamount = 0;
                        productamount =
                            Convert.ToDecimal(
                                ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim());
                        decimal productperc = 0;
                        //productperc = (productamount*100)/totalamount;
                        decimal mainper = 0;
                        //  mainper = (percentage * productperc) / 100;


                        // Frinds Hospital Start
                        // if (aTable.Rows[0]["CustomerCode"].ToString() == "158964" || aTable.Rows[0]["CustomerCode"].ToString() == "158399")

                        //if (Convert.ToBoolean(aTable.Rows[0]["IsSpDis"]) == true)
                        //{
                        //    DataTable aTable2 = new DataTable();
                        //    aTable2 = aOrderInfoBll.LoadOrderWithDetailFrindsHospital(orderId, ((HiddenField)GridView1.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value);
                        //    ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountAmount"].ToString()).ToString();
                        //}
                        
                        
                        //else if (aTable.Rows[0]["CustomerType"].ToString() == "NCOD")
                        //{
                        //    //if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "True")
                        //    //{

                        //    //    ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                        //    //  0.ToString();
                        //    //    ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                        //    //       0.ToString("F");
                        //    //}
                        //    //else
                        //    //{
                        //    //    ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                        //    //       percentage.ToString();
                        //    //    ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                        //    //        (Convert.ToDecimal(
                        //    //            ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) *
                        //    //         (percentage / 100)).ToString("F");
                        //    //}
                        //    for (int j = 0; j < GridView1.Rows.Count; j++)
                        //    {
                        //        DataTable aTable2 = new DataTable();
                        //        aTable2 = aOrderInfoBll.LoadOrderWithDetailFrindsHospital(orderId, ((HiddenField)GridView1.Rows[j].Cells[1].FindControl("orderdetailIdHiddenField")).Value);
                        //        ((TextBox)GridView1.Rows[j].Cells[10].FindControl("dpAmtTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountAmount"].ToString()).ToString();
                        //        ((TextBox)GridView1.Rows[j].Cells[10].FindControl("dpTextBox")).Text = Convert.ToDecimal(aTable2.Rows[0]["DiscountPercent"].ToString()).ToString();

                        //    }

                        //}

                        //else
                        {
                            if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "1")
                            {

                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                              0.ToString();
                                ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                   0.ToString("F");
                            }
                            if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text == "True")
                            {

                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                              0.ToString();
                                ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                   0.ToString("F");

                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("tpTextBox")).Text =
                          0.ToString();

                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("tpVatTextBox")).Text =
                          0.ToString();

                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("npTextBox")).Text =
                          0.ToString();
                            }
                            if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("sdTextBox")).Text != "1"
                                && ((TextBox)GridView1.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text != "True")
                            {
                                ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text =
                                   percentage.ToString();
                                ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text =
                                    (Convert.ToDecimal(
                                        ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) *
                                     (percentage / 100)).ToString("F");
                            }
                        }
                        // Frinds Hospital End

                        // Resectin FOC Offer Discount End//

                        //////////Modified Version/////////////
                        //decimal vat = 0;
                        //vat = Convert.ToDecimal(dtproductvat.Rows[0]["VATPercentage"].ToString());
                        //((TextBox) GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                        //    (Convert.ToDecimal(((TextBox) GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text)*
                        //     (percamount/100)).ToString();

                        decimal withdiscount = 0;
                        withdiscount =
                            (Convert.ToDecimal(
                                ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                             Convert.ToDecimal(
                                 (((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text)));
                        //-
                        // Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text));
                        decimal vatamount = 0;
                        //vatamount = (Convert.ToDecimal(
                        //    ((TextBox) GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim())*vat)/100;
                        TextBox tpVatTextBox = (TextBox)GridView1.Rows[i].Cells[8].FindControl("tpVatTextBox");
                        //tpVatTextBox.Text = vatamount.ToString("F");
                        //((TextBox) GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text =
                        //    (Convert.ToDecimal(((TextBox) GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text)*
                        //     (percamount/100)).ToString();

                        //amTextBox
                        //ddddddddddddd
                        TextBox npTextBox = (TextBox)GridView1.Rows[i].Cells[11].FindControl("npTextBox");


                        //npTextBox.Text = ((Convert.ToDecimal(
                        //   ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                        //                 (Convert.ToDecimal(
                        //                      ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                        //                          .Text.Trim()) + Convert.ToDecimal(
                        //                      ((TextBox)GridView1.Rows[i].Cells[10].FindControl("amTextBox"))
                        //                          .Text.Trim()))) +
                        //                 Convert.ToDecimal(tpVatTextBox.Text)).ToString();
                        //old
                        //     npTextBox.Text = ((Convert.ToDecimal(
                        //((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim()) -
                        //               Convert.ToDecimal(
                        //                   ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                        //                       .Text.Trim())) +
                        //              Convert.ToDecimal(tpVatTextBox.Text)).ToString();


                        //AdjustmentAmount();

                        decimal tpTextBox = 0;
                        try
                        {
                            tpTextBox = Convert.ToDecimal(
                            ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim());
                        }
                        catch
                        {

                        }
                        decimal dpAmtTextBox = 0;
                        try
                        {

                            dpAmtTextBox = Convert.ToDecimal(
                                               ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox"))
                                                   .Text.Trim());

                        }
                        catch
                        {

                        }

                        decimal amTextBox = 0;
                        try
                        {

                            amTextBox = Convert.ToDecimal(
                                               ((TextBox)GridView1.Rows[i].Cells[10].FindControl("amTextBox"))
                                                   .Text.Trim());

                        }
                        catch
                        {

                        }

                        decimal tpVat = 0;
                        try
                        {

                            tpVat = Convert.ToDecimal(tpVatTextBox.Text);

                        }
                        catch
                        {

                        }

                        decimal rS = (tpTextBox - (dpAmtTextBox + amTextBox)) + tpVat;
                        npTextBox.Text = rS.ToString();

                        if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("sdTextBox")).Text == "True"

                            && ((TextBox)GridView1.Rows[i].Cells[1].FindControl("bQtyTextBox")).Text == "True")
                        {

                            ((TextBox)GridView1.Rows[i].Cells[1].FindControl("npTextBox")).Text = 0.ToString();
                            ((TextBox)GridView1.Rows[i].Cells[1].FindControl("tpVatTextBox")).Text = 0.ToString();
                            ((TextBox)GridView1.Rows[i].Cells[1].FindControl("tpTextBox")).Text = 0.ToString();
                            //(TextBox) GridView1.Rows[i].Cells[11].FindControl("npTextBox").te = 0;
                        }

                        TotalValueCalculation();
                    }
                }
                GetDiscounttotalValue();
            }
            if (GridView1.Rows.Count == aTable.Rows.Count)
            {

            }
            else
            {
                Response.Redirect("InvoiceCreationByOrder.aspx");

            }
        }
        else
        {
            showMessageBox("Order Information already Exists !!");
        }
    }


    private void GetOrderDetailValue(string orderId)
    {
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        DataTable aTable = new DataTable();
        aTable = aOrderInfoBll.LoadOrderWithDetail(orderId);

        for (int i = 0; i < aTable.Rows.Count; i++)
        {
            ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value =
                aTable.Rows[i]["OrderDetailId"].ToString();
        }
    }

    private void GetDiscounttotalValue()
    {
        decimal disTotal = 0;
        decimal tqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            tqty = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);

            //if (((TextBox) gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text == "ANB08" && tqty >= 3)
            //{
            //    disTotal = 0;
            //    //(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text) : 0;
            //}
            //else
            {
                disTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "")
                    ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text)
                    : 0;
            }
        }
        disTotalTextBox.Text = disTotal.ToString();
    }

    //FOC Bonus Qty Start//
    private bool B()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "AID01" && Qty >= 4)
            {

                return true;
            }
        }
        return false;
    }
    private bool B2()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "AEL02" && Qty >= 3)
            {

                return true;
            }
        }
        return false;
    }
    private bool B4()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "AID04" && Qty >= 5)
            {

                return true;
            }
        }
        return false;
    }
    private bool B3()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "OAD04" && Qty >= 2)
            {

                return true;
            }
        }
        return false;
    }
    private bool B5()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "MNS03" && Qty >= 5)
            {

                return true;
            }
        }
        return false;
    }
    private bool B6()
    {
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);
            if (ProductCode == "FGD02" && Qty >= 4)
            {

                return true;
            }
        }
        return false;
    }
    //FOC Bonus Qty End//

    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        string custCode = custCodeTextBox.Text.Trim();
        GetCustInfo(custCode,orderNoTextBox.Text.Trim());
    }

    private void InitialGrid()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("SpecialAmount");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");
        aDataTable.Columns.Add("Quantity");
        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");
        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");
        aDataTable.Columns.Add("NetPrice");
        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("ISGiftProduct");
        aDataTable.Columns.Add("IsCampaignProduct");
        aDataTable.Columns.Add("CampaignType");
        DataRow dataRow;

        dataRow = aDataTable.NewRow();

        dataRow["SL"] = "1";
        dataRow["ProductCode"] = "";
        dataRow["ProductName"] = "";
        dataRow["SpecialAmount"] = "";
        dataRow["OrderDetailsId"] = "";
        dataRow["StockQty"] = "";
        dataRow["UnitPrice"] = "";
        dataRow["UnitVAT"] = "";
        dataRow["Quantity"] = "";
        dataRow["TotalPrice"] = "";
        dataRow["VAT"] = "";
        dataRow["DiscountPercentage"] = "";
        dataRow["DiscountAmount"] = "";
        dataRow["NetPrice"] = "";
        dataRow["BonusQty"] = "";
        dataRow["TotalQty"] = "";
        dataRow["ISGiftProduct"] = "";
        dataRow["IsCampaignProduct"] = "";
        dataRow["CampaignType"] = "";
        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();

        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;

    }

    public void AddFunc()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");
        aDataTable.Columns.Add("SpecialAmount");
       
        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("ISGiftProduct");
        aDataTable.Columns.Add("IsCampaignProduct");
        aDataTable.Columns.Add("CampaignType");
        
        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value.Trim();
                dataRow["IsCampaignProduct"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
               // dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["ISGiftProduct"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();

                dataRow["CampaignType"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("CampaignTypeHiddenField")).Value.Trim();
                    //((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                aDataTable.Rows.Add(dataRow);
            }
        }

        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "";
        dataRow["OrderDetailsId"] = "";
        dataRow["ProductName"] = "";
        dataRow["StockQty"] = "";
        dataRow["UnitPrice"] = "";
        dataRow["UnitVAT"] = "";
        dataRow["Quantity"] = "";
        dataRow["TotalPrice"] = "";
        dataRow["VAT"] = "";
        dataRow["DiscountPercentage"] = "";
        dataRow["DiscountAmount"] = "";
        dataRow["SpecialAmount"] = "";
        dataRow["NetPrice"] = "";
        dataRow["BonusQty"] = "";
        dataRow["TotalQty"] = "";
        dataRow["ISGiftProduct"] = "";
        dataRow["IsCampaignProduct"] = "";
        dataRow["CampaignType"] = "";
        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }

    public void AddFunc2()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");
        aDataTable.Columns.Add("SpecialAmount");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("ISGiftProduct");
        aDataTable.Columns.Add("IsCampaignProduct");
        aDataTable.Columns.Add("CampaignType");

        DataRow dataRow;

        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)GridView1.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value.Trim();
                dataRow["IsCampaignProduct"] = ((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)GridView1.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)GridView1.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)GridView1.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)GridView1.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)GridView1.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)GridView1.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)GridView1.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)GridView1.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                //dataRow["BonusQty"] = ((TextBox)GridView1.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)GridView1.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["ISGiftProduct"] = ((TextBox)GridView1.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["CampaignType"] = ((HiddenField)GridView1.Rows[i].Cells[1].FindControl("CampaignTypeHiddenField")).Value.Trim();
                //((TextBox)GridView1.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                aDataTable.Rows.Add(dataRow);
            }
        }

        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "";
        dataRow["OrderDetailsId"] = "";
        dataRow["ProductName"] = "";
        dataRow["StockQty"] = "";
        dataRow["UnitPrice"] = "";
        dataRow["UnitVAT"] = "";
        dataRow["Quantity"] = "";
        dataRow["TotalPrice"] = "";
        dataRow["VAT"] = "";
        dataRow["DiscountPercentage"] = "";
        dataRow["DiscountAmount"] = "";
        dataRow["SpecialAmount"] = "";
        dataRow["NetPrice"] = "";
        dataRow["BonusQty"] = "";
        dataRow["TotalQty"] = "";
        dataRow["ISGiftProduct"] = "";
        dataRow["IsCampaignProduct"] = "";
        dataRow["CampaignType"] = "";
        aDataTable.Rows.Add(dataRow);

        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = aDataTable;
        GridView1.DataBind();
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
    }

    protected void addImageButton_Click6(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "FGD02");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "FGD02";
        dataRow["ProductName"] = "Moticare 10mg:FGD02 ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty6();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty6();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }
    protected void addImageButton_Click5(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "MNS03");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "MNS03";
        dataRow["ProductName"] = "Seacoral D Tablet:6 X 10s ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty5();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty5();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }
    protected void addImageButton_Click(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "AID01");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "AID01";
        dataRow["ProductName"] = "Flexidol Tablet~100mg :10 X 10s ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }
    protected void addImageButton_Click2(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "AEL02");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "AEL02";
        dataRow["ProductName"] = "Nervaid 75mg Capsule:6 X 10s ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty2();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty2();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }
    protected void addImageButton_Click3(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "OAD04");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "OAD04";
        dataRow["ProductName"] = "Maxivent 400 mg Tablet:3 X 10s ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty3();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty3();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }
    protected void addImageButton_Click4(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        aDataTable.Columns.Add("OrderDetailsId");
        aDataTable.Columns.Add("SpecialAmount");

        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                dataRow["StockQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                dataRow["OrderDetailsId"] = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aDataTable.Rows.Add(dataRow);
            }
        }

        decimal cs = 0;
        DataTable aDataTable2 = new DataTable();
        aDataTable2 = aInvoiceBll.ProductInfo(hdComUnitId.Value, "AID04");

        if (aDataTable2.Rows.Count > 0)
        {
            cs = Convert.ToDecimal(aDataTable2.Rows[0]["StockQty"].ToString());
        }


        int sl = aDataTable.Rows.Count;
        dataRow = aDataTable.NewRow();

        dataRow["SL"] = Convert.ToString(sl + 1);
        dataRow["ProductCode"] = "AID04";
        dataRow["ProductName"] = "Ezepain 90 mg Tablet :3 X 10s ";
        dataRow["StockQty"] = cs;
        dataRow["UnitPrice"] = "0";
        dataRow["UnitVAT"] = "0";
        dataRow["Quantity"] = Qty4();
        dataRow["TotalPrice"] = "0";
        dataRow["VAT"] = "0";
        dataRow["DiscountPercentage"] = "0";
        dataRow["DiscountAmount"] = "0";
        dataRow["NetPrice"] = "0";
        dataRow["BonusQty"] = "0";
        dataRow["TotalQty"] = Qty4();
        dataRow["OrderDetailsId"] = "0";
        dataRow["SpecialAmount"] = "0";

        aDataTable.Rows.Add(dataRow);

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }

    private int Qty()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "AID01")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    private int Qty5()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "MNS03")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    private int Qty6()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "FGD02")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    private int Qty4()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "AID04")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    private int Qty2()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "AEL02")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    private int Qty3()
    {
        int Bqty = 0;

        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string ProductCode = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
            int Qty = Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("qtyTextBox")).Text);

            DataTable aDataTableBQty = new DataTable();
            if (ProductCode == "OAD04")
            {
                aDataTableBQty = aInvoiceBll.ProductFocBonusQtyBLL(Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd-MMM-yyyy"), ProductCode, Qty);
                if (aDataTableBQty.Rows.Count > 0)
                {
                    return Bqty = Convert.ToInt16(aDataTableBQty.Rows[0]["BonusQty"]);
                }
            }
        }
        return 0;
    }
    protected void removeImageButton_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");

        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("UnitPrice");
        aDataTable.Columns.Add("UnitVAT");

        aDataTable.Columns.Add("Quantity");

        aDataTable.Columns.Add("TotalPrice");
        aDataTable.Columns.Add("VAT");

        aDataTable.Columns.Add("DiscountPercentage");
        aDataTable.Columns.Add("DiscountAmount");

        aDataTable.Columns.Add("NetPrice");

        aDataTable.Columns.Add("BonusQty");
        aDataTable.Columns.Add("TotalQty");
        DataRow dataRow;

        if (gridLineItemGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["SL"] = Convert.ToString(sl1);
                    dataRow["ProductCode"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim();
                    dataRow["ProductName"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text.Trim();
                    dataRow["StockQty"] =
                        ((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text.Trim();
                    dataRow["UnitPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text.Trim();
                    dataRow["UnitVAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text.Trim(); ;
                    dataRow["Quantity"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text.Trim();
                    dataRow["TotalPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text.Trim();
                    dataRow["VAT"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text.Trim();
                    dataRow["DiscountPercentage"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text.Trim();
                    dataRow["DiscountAmount"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text.Trim();
                    dataRow["NetPrice"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text.Trim();
                    dataRow["BonusQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim();
                    dataRow["TotalQty"] = ((TextBox)gridLineItemGridView.Rows[i].Cells[13].FindControl("tQtyTextBox")).Text.Trim();
                    aDataTable.Rows.Add(dataRow);
                    sl1 += 1;
                }
            }
        }

        gridLineItemGridView.DataSource = null;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.DataSource = aDataTable;
        gridLineItemGridView.DataBind();
        gridLineItemGridView.Columns[4].Visible = false;
        gridLineItemGridView.Columns[5].Visible = false;
    }

    protected void codeTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox productCodeTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox");

        string productCode = productCodeTextBox.Text.Trim();
        GetProduct(rowindex, productCode);
    }

    private void GetProduct(int rowindex, string productCode)
    {
        DataTable aDataTable = new DataTable();
        if (!string.IsNullOrEmpty(productCode))
        {
            if (ProductCodeValidation(productCode, rowindex) == true)
            {
                aDataTable = aInvoiceBll.ProductInfo(hdComUnitId.Value, productCode);
                ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = productCode;
                if (aDataTable.Rows.Count > 0)
                {
                    TextBox nameTextBox =
                        (TextBox)gridLineItemGridView.Rows[rowindex].Cells[2].FindControl("nameTextBox");
                    nameTextBox.Text = aDataTable.Rows[0]["ProductName"].ToString();
                    TextBox currentStockTextBox =
                        (TextBox)gridLineItemGridView.Rows[rowindex].Cells[3].FindControl("currentStockTextBox");
                    currentStockTextBox.Text = aDataTable.Rows[0]["StockQty"].ToString();
                    TextBox unitPriceTextBox =
                        (TextBox)gridLineItemGridView.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
                    unitPriceTextBox.Text = aDataTable.Rows[0]["UnitPrice"].ToString();
                    TextBox upVatTextBox =
                        (TextBox)gridLineItemGridView.Rows[rowindex].Cells[5].FindControl("upVatTextBox");
                    upVatTextBox.Text = aDataTable.Rows[0]["VAT"].ToString();
                }
                else
                {
                    ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                    ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                    showMessageBox("No Any Stock or Product of " + productCode);
                }
            }
            else
            {
                ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                showMessageBox(productCode + " No: Product Already Inserted!!!");
            }
        }
    }
    private void GetProduct2(int rowindex, string productCode)
    {
        DataTable aDataTable = new DataTable();
        if (!string.IsNullOrEmpty(productCode))
        {
            if (ProductCodeValidation(productCode, rowindex) == true)
            {
                aDataTable = aInvoiceBll.ProductInfo(hdComUnitId.Value, productCode);
                ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = productCode;
                if (aDataTable.Rows.Count > 0)
                {
                    TextBox nameTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox");
                    nameTextBox.Text = aDataTable.Rows[0]["ProductName"].ToString();
                    TextBox currentStockTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[3].FindControl("currentStockTextBox");
                    currentStockTextBox.Text = aDataTable.Rows[0]["StockQty"].ToString();
                    TextBox unitPriceTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
                    unitPriceTextBox.Text = aDataTable.Rows[0]["UnitPrice"].ToString();
                    TextBox upVatTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[5].FindControl("upVatTextBox");
                    upVatTextBox.Text = aDataTable.Rows[0]["VAT"].ToString();
                }
                else
                {
                    ((TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                    ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                    showMessageBox("No Any Stock or Product of " + productCode);
                }
            }
            else
            {
                ((TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                showMessageBox(productCode + " No: Product Already Inserted!!!");
            }
        }
    }

    private void GetProduct3(int rowindex, string productCode)
    {
        DataTable aDataTable = new DataTable();
        if (!string.IsNullOrEmpty(productCode))
        {
            if (ProductCodeValidation(productCode, rowindex) == true)
            {
                aDataTable = aInvoiceBll.ProductInfoid(hdComUnitId.Value, productCode);
                ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = productCode;
                if (aDataTable.Rows.Count > 0)
                {
                    ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = aDataTable.Rows[0]["ProductCode"].ToString();
                    TextBox nameTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox");
                    nameTextBox.Text = aDataTable.Rows[0]["ProductName"].ToString();
                    TextBox currentStockTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[3].FindControl("currentStockTextBox");
                    currentStockTextBox.Text = aDataTable.Rows[0]["StockQty"].ToString();
                    TextBox unitPriceTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
                    unitPriceTextBox.Text = aDataTable.Rows[0]["UnitPrice"].ToString();
                    TextBox upVatTextBox =
                        (TextBox)GridView1.Rows[rowindex].Cells[5].FindControl("upVatTextBox");
                    upVatTextBox.Text = aDataTable.Rows[0]["VAT"].ToString();
                }
                else
                {
                    ((TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                    ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                    showMessageBox("No Any Stock or Product of " + productCode);
                }
            }
            else
            {
                ((TextBox)GridView1.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text = "";
                ((TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text = "";
                showMessageBox(productCode + " No: Product Already Inserted!!!");
            }
        }
    }

    private bool ProductCodeValidation(string productCode, int rowindex)
    {

        //for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        //{
        //    if (rowindex!=i)
        //    {
        //        if (((TextBox) gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text.Trim() ==
        //            productCode.Trim())
        //        {
        //            return false;
        //        }
        //    }
        //}

        return true;
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void GetQty(decimal qty, int rowindex, bool islowStock = false)
    {
        decimal totalQuantity = 0;
        decimal orderQuantity = 0;
        bool IsCampaignProduct = false;
        bool ISGiftProduct = false;


        if (islowStock)
        {
            // Total Quantity

            DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value, ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox")).Text);

            if (dtdata.Rows.Count > 0 && dtdata.Rows[0][0] != DBNull.Value)
            {
                totalQuantity = Convert.ToDecimal(dtdata.Rows[0][0]);
            }

            // Order Quantity

            HiddenField orderdetailIdHiddenField = (HiddenField)gridLineItemGridView.Rows[rowindex].Cells[6].FindControl("orderdetailIdHiddenField");
            DataTable orderDetails = aInvoiceBll.LoadProductOrderDetails(orderdetailIdHiddenField.Value);

            decimal tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());

            if (orderDetails.Rows.Count > 0 && orderDetails.Rows[0][0] != DBNull.Value)
            {
                orderQuantity = Convert.ToDecimal(orderDetails.Rows[0]["Quantity"]);
                IsCampaignProduct = Convert.ToBoolean(orderDetails.Rows[0]["IsCampaignProduct"]);
                ISGiftProduct = Convert.ToBoolean(orderDetails.Rows[0]["ISGiftProduct"]);
            }


            // Procedure for Low stock

            if (qty < totalQuantity)
            {
                //For Gift Product

                if (ISGiftProduct)
                {
                    qty = 0;
                }
                else
                {

                    if (qty > 0) // Stock > 0
                    {

                        if (qty >= orderQuantity)
                        {
                            qty = orderQuantity;
                        }
                    }
                    else
                    {
                        qty = 0;
                    }
                    
                }
            }
        }

        TextBox qtyTextBox1 = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[6].FindControl("qtyTextBox");
        qtyTextBox1.Text = qty.ToString();

        if (islowStock)
        {
            if (qty == 0)
            {
                qtyTextBox1.ReadOnly = true;
            }
        }
        
        TextBox unitPriceTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
        TextBox upVatTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[5].FindControl("upVatTextBox");

        TextBox tpTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[7].FindControl("tpTextBox");
        tpTextBox.Text = Convert.ToString(Convert.ToDecimal(unitPriceTextBox.Text.Trim()) * qty);

        TextBox tpVatTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[8].FindControl("tpVatTextBox");
        tpVatTextBox.Text = Convert.ToString(Convert.ToDecimal(upVatTextBox.Text.Trim()) * qty);

        TextBox codeTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox");

        decimal discountPer = 0;
        //discountPer = aInvoiceBll.ProductDiscount(codeTextBox.Text.Trim(), qtyTextBox1.Text.Trim());

        TextBox dpTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[9].FindControl("dpTextBox");
        dpTextBox.Text = Convert.ToString(discountPer);
        TextBox dpAmtTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[10].FindControl("dpAmtTextBox");
        if (discountPer == 0)
        {
            dpAmtTextBox.Text = "0";
        }
        else
        {
            dpAmtTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) / 100) * discountPer);
        }

        TextBox npTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[11].FindControl("npTextBox");
        npTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) - Convert.ToDecimal(dpAmtTextBox.Text.Trim())) + Convert.ToDecimal(tpVatTextBox.Text.Trim()));

        ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[13].FindControl("tQtyTextBox")).Text = qtyTextBox1.Text.Trim();
        ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[12].FindControl("bQtyTextBox")).Text = "0";

        TotalValueCalculation();
    }
    public void GetQty2(decimal qty, int rowindex)
    {

        TextBox qtyTextBox1 = (TextBox)GridView1.Rows[rowindex].Cells[6].FindControl("qtyTextBox");
        qtyTextBox1.Text = qty.ToString();

        TextBox unitPriceTextBox = (TextBox)GridView1.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
        TextBox upVatTextBox = (TextBox)GridView1.Rows[rowindex].Cells[5].FindControl("upVatTextBox");

        TextBox tpTextBox = (TextBox)GridView1.Rows[rowindex].Cells[7].FindControl("tpTextBox");
        tpTextBox.Text = Convert.ToString(Convert.ToDecimal(unitPriceTextBox.Text.Trim()) * qty);

        TextBox tpVatTextBox = (TextBox)GridView1.Rows[rowindex].Cells[8].FindControl("tpVatTextBox");
        tpVatTextBox.Text = Convert.ToString(Convert.ToDecimal(upVatTextBox.Text.Trim()) * qty);

        TextBox codeTextBox = (TextBox)GridView1.Rows[rowindex].Cells[1].FindControl("codeTextBox");

        decimal discountPer = 0;
        //discountPer = aInvoiceBll.ProductDiscount(codeTextBox.Text.Trim(), qtyTextBox1.Text.Trim());

        TextBox dpTextBox = (TextBox)GridView1.Rows[rowindex].Cells[9].FindControl("dpTextBox");
        dpTextBox.Text = Convert.ToString(discountPer);
        TextBox dpAmtTextBox = (TextBox)GridView1.Rows[rowindex].Cells[10].FindControl("dpAmtTextBox");
        if (discountPer == 0)
        {
            dpAmtTextBox.Text = "0";
        }
        else
        {
            dpAmtTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) / 100) * discountPer);
        }

        TextBox npTextBox = (TextBox)GridView1.Rows[rowindex].Cells[11].FindControl("npTextBox");
        npTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) - Convert.ToDecimal(dpAmtTextBox.Text.Trim())) + Convert.ToDecimal(tpVatTextBox.Text.Trim()));

        ((TextBox)GridView1.Rows[rowindex].Cells[13].FindControl("tQtyTextBox")).Text = qtyTextBox1.Text.Trim();
        ((TextBox)GridView1.Rows[rowindex].Cells[12].FindControl("bQtyTextBox")).Text = "0";

        TotalValueCalculation();
    }
    protected void qtyTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox qtyTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)qtyTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        decimal qty = 0;
        TextBox qtyTextBox1 = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[6].FindControl("qtyTextBox");
        qty = Convert.ToDecimal(qtyTextBox1.Text.Trim());

        TextBox unitPriceTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[4].FindControl("unitPriceTextBox");
        TextBox upVatTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[5].FindControl("upVatTextBox");

        TextBox tpTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[7].FindControl("tpTextBox");
        tpTextBox.Text = Convert.ToString(Convert.ToDecimal(unitPriceTextBox.Text.Trim()) * qty);

        TextBox tpVatTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[8].FindControl("tpVatTextBox");
        tpVatTextBox.Text = Convert.ToString(Convert.ToDecimal(upVatTextBox.Text.Trim()) * qty);

        TextBox codeTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox");

        decimal discountPer = 0;
        discountPer = aInvoiceBll.ProductDiscount(codeTextBox.Text.Trim(), qtyTextBox1.Text.Trim());

        TextBox dpTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[9].FindControl("dpTextBox");
        dpTextBox.Text = Convert.ToString(discountPer);
        TextBox dpAmtTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[10].FindControl("dpAmtTextBox");

        if (discountPer == 0)
        {
            dpAmtTextBox.Text = "0";
        }
        else
        {
            dpAmtTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) / 100) * discountPer);
        }

        TextBox npTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[11].FindControl("npTextBox");
        npTextBox.Text = Convert.ToString((Convert.ToDecimal(tpTextBox.Text.Trim()) - Convert.ToDecimal(dpAmtTextBox.Text.Trim())) + Convert.ToDecimal(tpVatTextBox.Text.Trim()));

        ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[13].FindControl("tQtyTextBox")).Text = qtyTextBox1.Text.Trim();
        ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[12].FindControl("bQtyTextBox")).Text = "0";
        TotalValueCalculation();
    }
    protected void bQtyTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox bQtyTextBox1 = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)bQtyTextBox1.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[13].FindControl("tQtyTextBox")).Text =
            Convert.ToString(
                Convert.ToDecimal(
                    ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[12].FindControl("bQtyTextBox")).Text.Trim()) +
                Convert.ToDecimal(
                    ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[13].FindControl("tQtyTextBox")).Text.Trim()));

    }
    public void SaveReturnAmount(int invid)
    {
        ReturnAmountDAO amountDao = new ReturnAmountDAO()
        {
            CustomerId = Convert.ToInt32(hdCustomerMasterId.Value),
            Amount = Convert.ToDecimal(crAmountTextBox.Text)*(-1),
          //  ReturnInvoiceId = invid,
            InvoiceId = Convert.ToInt32(invid)
        };
        bool status = aInvoiceBll.SaveDataForReturnAmount(amountDao);

    }
    private bool ValiMethodAfterSave()
    {

        bool chk = true;
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        string OrderId = orderHiddenField.Value.ToString();
        int cont = 0;
        DataTable dtOrder = aOrderInfoBll.LoadDetalIdByMasCheck(OrderId);

        for (int i = 0; i < dtOrder.Rows.Count; i++)
        {
            int OrderDtlsId = 0;
            try
            {
                OrderDtlsId = Convert.ToInt32(dtOrder.Rows[i]["OrderDetailsId"].ToString());
            }
            catch
            {
                chk = false;
                break;
            }
            DataTable aTable = aOrderInfoBll.LoadInvoiceWithDetailIDCheck(OrderId, OrderDtlsId);

            if (aTable.Rows.Count == 0)
            {

                chk = false;
                break;

            }
        }

        //if (cont == 0)
        //{
        //    chk = false;
        //}

        return chk;
    }
    private bool ValiMethod()
    {

        bool chk = true;
        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        string OrderId = orderHiddenField.Value.ToString();
        int cont = 0;
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            string OrderDtlsId =
                ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
            DataTable aTable = aOrderInfoBll.LoadOrderWithDetailIDCheck(OrderId, OrderDtlsId);

            if (aTable.Rows.Count == 0)
            {

                chk = false;
                break;

            }
        }

        //if (cont == 0)
        //{
        //    chk = false;
        //}

        return chk;
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (ValiMethod() == true)
        {
        if (Validation() == true)
        {
            if (OrderExists(orderHiddenField.Value) == false)
            {
                {
                    try
                    {
                        int invId = SaveInvoice();
                        if (invId > 0)
                        {
                            ProformaOrInvoiceReturnBLL aInvoiceReturnBll2 = new ProformaOrInvoiceReturnBLL();
                            DataTable invoiceid = aInvoiceReturnBll2.SelectInvoiceID(Convert.ToInt32(orderHiddenField.Value));
                            int invoicePK = Convert.ToInt32(invoiceid.Rows[0]["InvoiceId"]);


                            bool dtl = SaveInvoiceDetail(invoicePK);
                            if (dtl == true)
                            {
                                //SaveReturnAmount(invId);
                                OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
                                aOrderInfoBll.UpdateInvoiceStatus(orderIdHiddenField.Value);
                                InvoiceDAL aDal = new InvoiceDAL();

                                // New add

                                aDal.UpdateInvoiceMasterDetail();

                                if (ValiMethodAfterSave())
                                {
                                    Clear();
                                        showMessageBox("Invoice Created Successfully");
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Invoice Created Successsfully!" + "','Success');", true);
                                }
                                else
                                {
                                    ProformaOrInvoiceReturnBLL aInvoiceReturnBll = new ProformaOrInvoiceReturnBLL();

                                    aInvoiceReturnBll.DeleteProforma(invTextBox.Text.Trim());
                                    //aInvoiceBll.DeleteInvoice(orderIdHiddenField.Value);
                                    //Update return amt
                                    // update order status
                                    Clear();


                                        showMessageBox("Please generate Invoice again");
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please generate Invoice again!" + "','Faild');", true);
                                }
                            }
                            else
                            {
                                aInvoiceBll.DeleteInvoice(orderIdHiddenField.Value);
                                Clear();
                                showMessageBox("Please generate Invoice again");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                         aInvoiceBll.DeleteInvoice(orderIdHiddenField.Value);
                        Clear();
                        showMessageBox("Please generate Invoice again");
                    }
                }
            }
            else
            {
                showMessageBox("Invoice already Generated !!");
            }
        }
        }
        else
        {
            Response.Redirect("InvoiceCreationByOrder.aspx");
        }
    }

    private bool SaveInvoiceDetail(int invoiceId)
    {

        List<InvoiceDetail> aInvoiceDetailsList = new List<InvoiceDetail>();

        if (gridLineItemGridView.Rows.Count > 0)
        {
            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
                DataTable dtdiscount =
                aOrderInfoBll.ProductDiscount(
                    ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text,
                    hdCustomerMasterId.Value, invDateTextBox.Text);

                InvoiceDetail aInvoiceDetail = new InvoiceDetail();
                aInvoiceDetail.ProductCode =
                    ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text;
                string product = ((TextBox)gridLineItemGridView.Rows[i].Cells[2].FindControl("nameTextBox")).Text;
                string[] proNameAndPackSize = product.Split(':');
                aInvoiceDetail.ProductName = proNameAndPackSize[0];
                aInvoiceDetail.PackSize = proNameAndPackSize[1];
                aInvoiceDetail.UnitPrice = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[4].FindControl("unitPriceTextBox")).Text);
                aInvoiceDetail.UnitVatAmount = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[5].FindControl("upVatTextBox")).Text);
                aInvoiceDetail.Quantity = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text);
                aInvoiceDetail.DiscountPercentage = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[9].FindControl("dpTextBox")).Text);
                aInvoiceDetail.DiscountAmount = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text);
                aInvoiceDetail.BonusQuantity = Convert.ToDecimal((0));
                aInvoiceDetail.SpecialAmount = 0;
                aInvoiceDetail.IsgiftProduct = (gridLineItemGridView.DataKeys[i][0].ToString());
                //Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text.Trim());
                string id =
                    ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("orderdetailIdHiddenField")).Value;
                aInvoiceDetail.OrderDetailsId = Convert.ToInt32(id);
                aInvoiceDetail.InvoiceId = invoiceId;
                aInvoiceDetail.TotalPriceVatAmount =
                    Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text);
                TextBox npTextBox = (TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox");
                aInvoiceDetail.NetAmount = Convert.ToDecimal(npTextBox.Text);
                if (dtdiscount.Rows.Count > 0)
                {
                    aInvoiceDetail.SpecialAmountPer = Convert.ToDecimal(dtdiscount.Rows[0]["DiscountPercentage"].ToString());
                }
                else
                {
                    aInvoiceDetail.SpecialAmountPer = 0;
                }
                try
                {

                    aInvoiceDetail.AdjustmentAmount = string.IsNullOrEmpty(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("amTextBox")).Text) 
                                                      ? 0 
                                                      : Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("amTextBox")).Text);

                }
                catch(Exception ex)
                {
                    aInvoiceDetail.AdjustmentAmount = 0;
                }
                decimal cstock = 0;
                decimal tqty = 0;
                tqty = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);
                cstock = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value,
                ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());


                /////SMC Low Order Method /////
                //if (tqty>cstock)
                //{
                //    aInvoiceBll.UpdateOrder("Undelivered", aInvoiceDetail.OrderDetailsId.ToString());
                //}
                //else
                //{
                //    aInvoiceDetailsList.Add(aInvoiceDetail);    
                //}
                ///////////////////


                aInvoiceDetail.CampaignType = ((HiddenField)gridLineItemGridView.Rows[i].Cells[1].FindControl("CampaignTypeHiddenField")).Value.Trim();

                try
                {
                  aInvoiceDetail.IsCampaignProductforInv =
                  Convert.ToBoolean(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text.Trim());
                    
                }
                catch (Exception)
                {
                    aInvoiceDetail.IsCampaignProductforInv = false;
                }

                try
                {
                    aInvoiceDetail.ISGiftProductforInv =
                   Convert.ToBoolean(((TextBox)gridLineItemGridView.Rows[i].Cells[12].FindControl("bQtyTextBox")).Text.Trim());
                }
                catch (Exception)
                {

                    aInvoiceDetail.ISGiftProductforInv = false;
                }

                if (cstock == 0)
                {
                    aInvoiceBll.UpdateOrder("Undelivered", aInvoiceDetail.OrderDetailsId.ToString());
                }
                else
                {
                    aInvoiceDetailsList.Add(aInvoiceDetail);
                }
            }


            // get  comUnitId

            string comUnitId = Request.QueryString["ComUnitId"];
            comUnitId = string.IsNullOrEmpty(comUnitId) ? "0" : comUnitId;

            aInvoiceBll.SaveInvoiceDetails(aInvoiceDetailsList, comUnitId);
        }

        return true;
    }

    private int SaveInvoice()
    {

        int invoiceId = 0;
        DataTable aDataTable = new DataTable();
        aDataTable = aInvoiceBll.CustomerMaster(orderNoTextBox.Text.Trim());

        OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
        DataTable dtFixedCustomer = aOrderInfoBll.GetFixedCustomer(orderHiddenField.Value);

        string invoiceNo = string.Empty;
        string[] forComUCode = comUnitNameTextBox.Text.Split(':');
        string ComUnitCode = forComUCode[0];
        Invoice aInvoice = new Invoice();
        // {
        aInvoice.InvoiceDate = Convert.ToDateTime(invDateTextBox.Text.Trim());
        aInvoice.OrderNo = orderNoTextBox.Text.Trim();
        aInvoice.OrderDate = Convert.ToDateTime(orderDateTextBox.Text.Trim());
        aInvoice.CustomerMasterId = Convert.ToInt32(hdCustomerMasterId.Value);
        aInvoice.ComUnitId = Convert.ToInt32(hdComUnitId.Value);
        try
        {
            aInvoice.MiaId = Convert.ToInt32(hdMiaId.Value);
        }
        catch (Exception ex) {
            aInvoice.MiaId = 0;
        }
        aInvoice.PaymentTypeId = Convert.ToInt32(payTypeDDL.SelectedValue);
        aInvoice.TpTotal = Convert.ToDecimal(tpTptalTextBox.Text.Trim());
        aInvoice.TpDiscount = Convert.ToDecimal(disTotalTextBox.Text.Trim());
        aInvoice.TpVat = Convert.ToDecimal(vatTotalTextBox.Text.Trim());
        aInvoice.TpGrandTotal = Convert.ToDecimal(grandTotalTextBox.Text.Trim());
        aInvoice.UserId = Convert.ToInt32(Session["UserId"].ToString());
        aInvoice.ComUnitCode = ComUnitCode;
        aInvoice.OrderId = Convert.ToInt32(orderIdHiddenField.Value);
        aInvoice.TotalSpecialAmount = Convert.ToDecimal(pdTextBox.Text);

        aInvoice.cusType = cusTypeTextBox.Text;

        aInvoice.OldTradePolicy = false;
        ////// SMC Low Stock Method////////
        aInvoice.Remarks = remarksTextBox.Text;
        aInvoice.MIACode = aDataTable.Rows[0]["MIOEmpMastercode"].ToString();
        aInvoice.MIAName = aDataTable.Rows[0]["MIOEmpName"].ToString();
        aInvoice.MarketCode = aDataTable.Rows[0]["MarketCode"].ToString();
        aInvoice.MarketName = aDataTable.Rows[0]["MarketName"].ToString();
        aInvoice.AreaCode = aDataTable.Rows[0]["AreaCode"].ToString();
        aInvoice.DisCode = aDataTable.Rows[0]["ASMEmpMasterCode"].ToString();
        aInvoice.FEName = aDataTable.Rows[0]["ASMEmpName"].ToString();
        aInvoice.RegionCode = aDataTable.Rows[0]["RegionCode"].ToString();
        aInvoice.DZSMName = aDataTable.Rows[0]["RSMEmpName"].ToString();
        aInvoice.FixedCustomer = Convert.ToBoolean(dtFixedCustomer.Rows[0]["FixedCustomer"].ToString());
        aInvoice.Type = Convert.ToString(aDataTable.Rows[0]["Type"].ToString());
        aInvoice.DpNAme = deliverypersonNameTextBox.Text.Trim();
        aInvoice.DpMob = deliverypersonMobileTextBox.Text.Trim();
        aInvoice.ProductOffer = "False";

        aInvoice.Createdate = DateTime.Now;
        aInvoice.AdjustAmount = Convert.ToDecimal(0);
        aInvoice.ReceivableAmount = Convert.ToDecimal(rcvAmountTextBox.Text);
        //if (Convert.ToDecimal(crAmountTextBox.Text) > 0)
        //{
        //    aInvoice.IsAdjustInvoice = true;
        //    aInvoice.AdjustInvoiceNo_ReturnInvoiceNo = adjustInvoiceNoTextBox.Text.Trim();
        //}
        //else
        {
            aInvoice.IsAdjustInvoice = false;
            
        }
        ////////////////Product Multiple Offer End ///////////

        aInvoiceBll.SaveInvoice(aInvoice, out invoiceId, out invoiceNo);

        invTextBox.Text = invoiceNo;

        return invoiceId;
    }
    private void Clear()
    {
        tpTptalTextBox.Text = "";
        vatTotalTextBox.Text = "";
        disTotalTextBox.Text = "";
        grandTotalTextBox.Text = "";
        hdComUnitId.Value = "";
        hdCustomerMasterId.Value = "";
        custNameTextBox.Text = "";
        custAddressTextBox.Text = "";
        districtNameTextBox.Text = "";
        areaNameTextBox.Text = "";
        comUnitNameTextBox.Text = "";
        miaCodeTextBox.Text = "";
        marketNameTextBox.Text = "";
        miaNameTextBox.Text = "";
        custCategoryTextBox.Text = "";
        hdMiaId.Value = "";
        custCodeTextBox.Text = "";
        orderNoTextBox.Text = "";
        pdTextBox.Text = "";
        orderHiddenField.Value = "";
        orderIdHiddenField.Value = "";
        InitialGrid();
    }

    private void TotalValueCalculation()
    {
        decimal tpTotal = 0;
        decimal vatTotal = 0;
        decimal disTotal = 0;
        decimal gTotal = 0;
        decimal sptotatl = 0;


        if (gridLineItemGridView.Rows.Count > 0)
        {

            for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
            {
                decimal cstock = 0;
                decimal tqty = 0;
                tqty = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);
                cstock = Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value,
                ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                /////SMC Low Stock Method/////
                //tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());
                /////////////
                if (cstock >= tqty)
                {
                    tpTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[7].FindControl("tpTextBox")).Text) : 0;
                    vatTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text) : 0;
                    //Offer Applied
                    //if (((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text == "ANB08" && tqty >= 3)
                    //{
                    //    disTotal = 0;
                    //    //(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text) : 0;

                    //}
                    //else
                    {
                        disTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text) : 0;

                    }

                    gTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[11].FindControl("npTextBox")).Text) : 0;
                  //  sptotatl += (((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("sdTextBox")).Text) : 0;
                }
            }
        }

        tpTptalTextBox.Text = tpTotal.ToString();
        vatTotalTextBox.Text = vatTotal.ToString();
        disTotalTextBox.Text = disTotal.ToString();
        grandTotalTextBox.Text = gTotal.ToString();
        pdTextBox.Text = sptotatl.ToString();

    }


    private void TotalValueCalculation2()
    {
        decimal tpTotal = 0;
        decimal vatTotal = 0;
        decimal disTotal = 0;
        decimal gTotal = 0;
        decimal sptotatl = 0;
        if (GridView1.Rows.Count > 0)
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                decimal cstock = 0;
                decimal tqty = 0;
                tqty = Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[14].FindControl("tQtyTextBox")).Text);
                cstock = Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[3].FindControl("currentStockTextBox")).Text);
                DataTable dtdata = aInvoiceBll.LoadProductQty(orderIdHiddenField.Value,
                ((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
                /////SMC Low Stock Method/////
                //tqty = Convert.ToDecimal(dtdata.Rows[0][0].ToString());
                /////////////
                if (cstock >= tqty)
                {
                    tpTotal += (((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[7].FindControl("tpTextBox")).Text) : 0;
                    vatTotal += (((TextBox)GridView1.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[8].FindControl("tpVatTextBox")).Text) : 0;
                    //Offer Applied
                    //if (((TextBox)GridView1.Rows[i].Cells[1].FindControl("codeTextBox")).Text == "ANB08" && tqty >= 3)
                    //{
                    //    disTotal = 0;
                    //    //(((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text) : 0;

                    //}
                    //else
                    {
                        disTotal += (((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)gridLineItemGridView.Rows[i].Cells[10].FindControl("dpAmtTextBox")).Text) : 0;

                    }

                    gTotal += (((TextBox)GridView1.Rows[i].Cells[11].FindControl("npTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[11].FindControl("npTextBox")).Text) : 0;
                    //  sptotatl += (((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text != "") ? Convert.ToDecimal(((TextBox)GridView1.Rows[i].Cells[10].FindControl("sdTextBox")).Text) : 0;
                }
            }
        }

        tpTptalTextBox.Text = tpTotal.ToString();
        vatTotalTextBox.Text = vatTotal.ToString();
        disTotalTextBox.Text = disTotal.ToString();
        grandTotalTextBox.Text = gTotal.ToString();
        pdTextBox.Text = sptotatl.ToString();

    }



    protected void printButton_Click(object sender, EventArgs e)
    {
        if (invTextBox.Text.Trim() != "")
        {
            // Check Invoice Master Detail

            DataTable aTable = aInvoiceBll.CheckInvoiceDetailExistOrNot(invTextBox.Text.Trim());

            if (aTable.Rows.Count > 0)
            {
                aInvoiceBll.UpdateInvoiceDetailNotExist();
                invTextBox.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('Something went wrong! Please generate invoice again..', 'Error');", true);
            }
            else
            {

                DataTable aTable2 = aInvoiceBll.CheckInvoiceExistOrNot(invTextBox.Text.Trim());

                if (aTable2.Rows.Count > 0)
                {
                    string url = "../SInventory_RPTVIEW/InvoiceReportViewer.aspx?InvNo=" +
                                 Server.UrlEncode(invTextBox.Text.Trim());
                    // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
                    string fullURL =
                        "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url +
                        "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
                    ScriptManager.RegisterStartupScript(this, typeof (string), "OPEN_WINDOW", fullURL, true);
                }
                else
                {
                    invTextBox.Text = "";
                }

                
            }

        }
        else
        {
            invTextBox.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('Something went wrong!. Please generate invoice again.', 'Error');", true);
        }

    
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
                GetCustInfo(custCode,orderNoTextBox.Text.Trim());
            }
        }
    }
    protected void nameTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox TextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)TextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        string product = ((TextBox)gridLineItemGridView.Rows[rowindex].Cells[2].FindControl("nameTextBox")).Text;

        if (!string.IsNullOrEmpty(product))
        {
            if (product.Contains(':'))
            {
                string[] proNameAndPackSize = product.Split(':');


                TextBox productCodeTextBox = (TextBox)gridLineItemGridView.Rows[rowindex].Cells[1].FindControl("codeTextBox");
                productCodeTextBox.Text = proNameAndPackSize[1];
                string productCode = productCodeTextBox.Text.Trim();
                GetProduct(rowindex, productCode);
            }

        }


    }

    protected void genordButton_OnClick(object sender, EventArgs e)
    {
        InvoiceCampGenDAL aCampGenDal=new InvoiceCampGenDAL();
        OrderMaster aMaster=new OrderMaster();
        List<OrderDetails> aDetailses= new List<OrderDetails>();
        for (int i = 0; i < gridLineItemGridView.Rows.Count; i++)
        {
            OrderDetails aOrderDetails=new OrderDetails();
            DataTable aDataTable = aInvoiceBll.ProductInfo(hdComUnitId.Value, ((TextBox)gridLineItemGridView.Rows[i].Cells[1].FindControl("codeTextBox")).Text);
            aOrderDetails.ProductId = Convert.ToInt32(aDataTable.Rows[0]["ProductId"].ToString());
            //aInvoiceDetail.Quantity = 
            aOrderDetails.Quantity= Convert.ToInt32(((TextBox)gridLineItemGridView.Rows[i].Cells[6].FindControl("qtyTextBox")).Text);

            aDetailses.Add(aOrderDetails);

        }
        List<CampaignMaster> aCampaignMasters=new List<CampaignMaster>();
        CampaignMaster acMaster=new CampaignMaster();
        acMaster.CampgainMasterId = 0;
        aCampaignMasters.Add(acMaster);

        aMaster.OrderDetails = aDetailses;
        aMaster.CampaignMasters = aCampaignMasters;
        List<OrderDetails> aDetail1=aCampGenDal.GetOrderProductWiseCampaign(aMaster);
        DataTable adata = ToDataTable(aDetail1);

        ChangedOrder(adata);

    }
}