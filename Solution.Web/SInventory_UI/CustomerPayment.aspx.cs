using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_CustomerPayment : System.Web.UI.Page
{
    private CustPaymentBLL aCustPaymentBll = new CustPaymentBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
            paymentDtTextBox.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
        }
    }

    public void Clear()
    {
        //orderGridView.DataSource = null;
        //orderGridView.DataBind();

        //salesCenterDropDownList.SelectedIndex = 0;
        marketDropDownList.SelectedValue = "";
        //customerDropDownList.SelectedIndex = 0;
      //  paymentDtTextBox.Text = string.Empty;
        paymentAmountTextBox.Text = string.Empty;
        refDtTextBox.Text = string.Empty;
        refNameTextBox.Text = string.Empty;
        payTypeDDL.SelectedIndex = 0;
        //customerTextBox.Text = string.Empty;


        customerTextBox_TextChanged(null,null);

    }

    public void DropDownList()
    {
        aCustPaymentBll.LoadSC(salesCenterDropDownList,Session["UserId"].ToString());

        if (salesCenterDropDownList.Items.Count == 2)
        {
            salesCenterDropDownList.SelectedIndex = 1;
        }
        aCustPaymentBll.PaymentTypeLoadBLL(payTypeDDL);
      
    }

    protected void salesCenterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aCustPaymentBll.LoadMarket(marketDropDownList, salesCenterDropDownList.SelectedValue);
        orderGridView.DataSource = null;
        orderGridView.DataBind();
    }
    protected void customerTextBox_TextChanged(object sender, EventArgs e)
    {
        CustomerMaster aCustomerMaster;
        var CustomerID = CustomerId(out aCustomerMaster);
        DataTable dt = aCustPaymentBll.LoadInvoice(salesCenterDropDownList.SelectedValue,
         CustomerID.ToString()
          , marketDropDownList.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            orderGridView.DataSource = dt;
            orderGridView.DataBind();

        }

        else
        {
            orderGridView.DataSource = null;
            orderGridView.DataBind();
            showMessageBox("No Invoice Found!!");
        }
    }

    protected void discountTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox discountTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)discountTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        ValidatePaymentInfo(rowindex, discountTextBox);
    }



    private void ValidatePaymentInfo(int i, TextBox textBox)
    {
        decimal totalamount = 0;

        TextBox payAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");
        //TextBox aitAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("aitTextBox");
        TextBox discountAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("discountTextBox");

        if (payAmountTextBox.Text.Trim() != "")
        {
            totalamount = totalamount + Convert.ToDecimal((payAmountTextBox.Text));
        }

        //if (aitAmountTextBox.Text.Trim() != "")
        //{
        //    totalamount = totalamount + Convert.ToDecimal((aitAmountTextBox.Text));
        //}

        if (discountAmountTextBox.Text.Trim() != "")
        {
            totalamount = totalamount + Convert.ToDecimal((discountAmountTextBox.Text));
        }

        string amount = orderGridView.Rows[i].Cells[7].Text.Trim();

        if (totalamount > Convert.ToDecimal(amount))
        {
            showMessageBox("Total Amount Must Be less than or Equel to due Amount !!");
            textBox.Text = "";
        }


    }

    private int CustomerId(out CustomerMaster aCustomerMaster)
    {
        int CustomerID = 0;
        aCustomerMaster = new CustomerMaster();
        aCustomerMaster = aCustPaymentBll.CustomerLoad(customerTextBox.Text.Trim());
        CustomerID = aCustomerMaster.CustomerMasterId;
        return CustomerID;
    }

    protected void customerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void marketDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aCustPaymentBll.LoadCustomerMaster(customerDropDownList, marketDropDownList.SelectedValue);
        orderGridView.DataSource = null;
        orderGridView.DataBind();
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox) orderGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox ChkBoxRows = (CheckBox) orderGridView.Rows[i].Cells[0].FindControl("chkSelect");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }

        CalculateTotal();
    }

    public bool Validation()
    {
        int CK = 0;
        for (int j = 0; j < orderGridView.Rows.Count; j++)
        {
            CheckBox cbReject = (CheckBox)orderGridView.Rows[j].FindControl("chkSelect");
            if (cbReject.Checked)
            {
                CK = CK + 1;
            }
        }
        if (CK == 0)
        {
            showMessageBox("Please Select Invoice from List!!");
            return false;
        }
        int count = 0;
        if (orderGridView.Rows.Count > 0)
        {
            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {

                if (((CheckBox)orderGridView.Rows[i].Cells[1].FindControl("chkSelect")).Checked)
                {
                    if (((TextBox)orderGridView.Rows[i].FindControl("payAmountTextBox")).Text == "")
                    {
                        showMessageBox("Please fill out Pay Amount !!");
                        return false;
                    }
                    count++;
                }
            }
        }

        if (paymentAmountTextBox.Text.Trim() == "")
        {
            showMessageBox("Please Select Payment Amount!!");
            return false;
        }

        decimal totalamount = 0;
        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
             CheckBox cbReject = (CheckBox)orderGridView.Rows[i].FindControl("chkSelect");
            if (cbReject.Checked)
            {
                TextBox payAmountTextBox = (TextBox) orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");
                totalamount += Convert.ToDecimal((payAmountTextBox.Text));
            }

        }
        if (totalamount != Convert.ToDecimal((paymentAmountTextBox.Text)))
        {
            showMessageBox("Total Invoice Payment Amount Must Be Equel To Payment Amount");
            return false;
        }
        if (payTypeDDL.SelectedValue == "")
        {
            showMessageBox("Please Select Payment Type!!");
            return false;
        }
        if (paymentDtTextBox.Text == "")
        {
            showMessageBox("Please Select Payment Date!!");
            return false;
        }
        if (customerTextBox.Text.Trim() == "")
        {
            showMessageBox("Please Select Customer!!");
            return false;
        }
       
        return true;
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            CustomerMaster aCustomerMaster;

            CustPayment aCustPayment = new CustPayment();

            aCustPayment.CustomerMasterId = CustomerId(out aCustomerMaster);
            aCustPayment.MarketId = marketDropDownList.SelectedValue == "" ? 0 : Convert.ToInt32(marketDropDownList.SelectedValue);
            aCustPayment.ComUnitId = Convert.ToInt32(salesCenterDropDownList.SelectedValue);
            aCustPayment.PaymentDate = Convert.ToDateTime(paymentDtTextBox.Text);
            aCustPayment.PaymentAmount = Convert.ToDecimal(paymentAmountTextBox.Text);
            aCustPayment.PayType = payTypeDDL.SelectedItem.Text;
            aCustPayment.RefNo = refNameTextBox.Text;

            if (refDtTextBox.Text!="")
            {
               aCustPayment.RefDate = Convert.ToDateTime(refDtTextBox.Text);
            }

            aCustPayment.CreateBy = Session["LoginName"].ToString();
            aCustPayment.CreateDate = DateTime.Now;


            List<CustPaymentDetail> aCustPaymentDetails = new List<CustPaymentDetail>();

            for (int i = 0; i < orderGridView.Rows.Count; i++)
            {
                CheckBox ChkBoxRows = (CheckBox) orderGridView.Rows[i].Cells[0].FindControl("chkSelect");
                CheckBox chkAdjust = (CheckBox)orderGridView.Rows[i].Cells[0].FindControl("chkAdjust");
                TextBox payAmountTextBox = (TextBox) orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");

                // New 

                TextBox discountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("discountTextBox");

                decimal prevamount = 0;
                if (orderGridView.Rows[i].Cells[6].Text != "&nbsp;")
                {
                     prevamount = Convert.ToDecimal(orderGridView.Rows[i].Cells[6].Text);
                }

                if (ChkBoxRows.Checked)
                {

                    if ((Convert.ToDecimal(payAmountTextBox.Text) + prevamount) ==
                        Convert.ToDecimal(orderGridView.Rows[i].Cells[5].Text))
                    {
                        decimal totalamount = 0;
                        totalamount = (Convert.ToDecimal(payAmountTextBox.Text) + prevamount);
                        aCustPaymentBll.UpdateInvoicePaymentAmount(totalamount.ToString(), "Full",
                            orderGridView.DataKeys[i][0].ToString());
                    }
                    else
                    {
                        decimal totalamount = 0;
                        totalamount = (Convert.ToDecimal(payAmountTextBox.Text) + prevamount);
                        aCustPaymentBll.UpdateInvoicePaymentAmount(totalamount.ToString(), "Partial",
                            orderGridView.DataKeys[i][0].ToString());
                    }

                    CustPaymentDetail aCustPaymentDetail = new CustPaymentDetail()
                    {
                        InvoiceId = Convert.ToInt32(orderGridView.DataKeys[i]["InvoiceId"].ToString()),
                        PaymentAmount = Convert.ToDecimal(payAmountTextBox.Text),
                       DiscountAmount = string.IsNullOrEmpty(discountTextBox.Text) ? 0 : Convert.ToDecimal(discountTextBox.Text),

                        IsAdjust = chkAdjust.Checked ? Convert.ToBoolean(1):Convert.ToBoolean(0)
                    };

                    aCustPaymentDetails.Add(aCustPaymentDetail);
                }
            }

            if (aCustPaymentBll.SaveCustPayment(aCustPayment, aCustPaymentDetails))
            {

                foreach (var aDetail in aCustPaymentDetails)
                {
                    if (aDetail.IsAdjust)
                    {
                        aCustPaymentBll.UpdateAdjustment(aDetail.InvoiceId);
                    }
                }
                showMessageBox("Data Saved Successfully !!!!!");
                Clear();
            }
        }
    }
    protected void payAmountTextBox_TextChanged(object sender, EventArgs e)
    {



        TextBox qtyTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)qtyTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        ValidatePaymentInfo(rowindex, qtyTextBox);

        CalculateTotal();

        //decimal prevamount = 0;
        //TextBox payAmountTextBox = (TextBox)orderGridView.Rows[rowindex].Cells[7].FindControl("payAmountTextBox");
        //decimal mainamount = string.IsNullOrEmpty(payAmountTextBox.Text) ? 0 : Convert.ToDecimal(payAmountTextBox.Text);
        //decimal delamount = Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[6].Text);

        //if (orderGridView.Rows[rowindex].Cells[6].Text != "&nbsp;")
        //{
        //    prevamount = Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[7].Text);
        //}


        //if ((mainamount + prevamount) > delamount)
        //{
        //    payAmountTextBox.Text = "0";
        //    ShowMessageBox("Cannot Be Greater then Invoice Quantity ");

        //}

        //ClaculatePaymentAmount();




        //TextBox qtyTextBox = (TextBox)sender;
        //GridViewRow currentRow = (GridViewRow)qtyTextBox.Parent.Parent;
        //int rowindex = 0;
        //rowindex = currentRow.RowIndex;
        //decimal prevamount = 0;
        //TextBox payAmountTextBox = (TextBox) orderGridView.Rows[rowindex].Cells[7].FindControl("payAmountTextBox");
        //decimal mainamount = string.IsNullOrEmpty(payAmountTextBox.Text) ? 0 : Convert.ToDecimal(payAmountTextBox.Text);
        //decimal delamount = Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[5].Text);
        
        //if (orderGridView.Rows[rowindex].Cells[6].Text != "&nbsp;")
        //{
        //      prevamount =  Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[6].Text);
        //}
       
           
        //if ((mainamount+prevamount)>delamount)
        //{
        //    payAmountTextBox.Text = "0";
        //    showMessageBox("Cannot Be Greater then Invoice Quantity ");

        //}

        CalculateTotal();

    }

    public void CalculateTotal()
    {
        decimal prevamount = 0;

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox chkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[0].FindControl("chkSelect");
            TextBox payAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");

            if (chkBoxRows.Checked)
            {
                if (payAmountTextBox.Text.Trim() != "")
                {
                    if (payAmountTextBox.Text != "0")
                    {
                        prevamount = prevamount + Convert.ToDecimal(payAmountTextBox.Text.Trim());
                    }
                }
            }
        }

        paymentAmountTextBox.Text = prevamount.ToString(CultureInfo.InvariantCulture);
    }

    protected void chkAdjust_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox isAdjust = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)isAdjust.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        TextBox payAmountTextBox = (TextBox)orderGridView.Rows[rowindex].Cells[7].FindControl("payAmountTextBox");

        if (isAdjust.Checked)
        {
            payAmountTextBox.Text = orderGridView.Rows[rowindex].Cells[10].Text.Trim();
        }
        else
        {
            payAmountTextBox.Text = "";
        }
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerPayment.aspx");
    }
}