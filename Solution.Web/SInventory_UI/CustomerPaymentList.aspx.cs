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

public partial class SInventory_UI_CustomerPaymentList : System.Web.UI.Page
{
    private CustPaymentBLL aCustPaymentBll = new CustPaymentBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
           // paymentDtTextBox.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
        }
    }

    public void Clear()
    {
        marketDropDownList.SelectedValue = "";
       
       // paymentAmountTextBox.Text = string.Empty;
      //  refDtTextBox.Text = string.Empty;
      //  refNameTextBox.Text = string.Empty;
        customerTextBox_TextChanged(null, null);
    }

    public void DropDownList()
    {
        aCustPaymentBll.LoadSC(salesCenterDropDownList, Session["UserId"].ToString());

        if (salesCenterDropDownList.Items.Count == 2)
        {
            salesCenterDropDownList.SelectedIndex = 1;
        }
       // aCustPaymentBll.PaymentTypeLoadBLL(payTypeDDL);
    }

    protected void salesCenterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aCustPaymentBll.LoadMarket(marketDropDownList, salesCenterDropDownList.SelectedValue);
        orderGridView.DataSource = null;
        orderGridView.DataBind();
    }
    protected void customerTextBox_TextChanged(object sender, EventArgs e)
    {
       
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
        CheckBox ChkBoxHeader = (CheckBox)orderGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[0].FindControl("chkSelect");
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

        //if (paymentAmountTextBox.Text.Trim() == "")
        //{
        //    showMessageBox("Please Select Payment Amount!!");
        //    return false;
        //}

        decimal totalamount = 0;
        for (int i = 0; i < orderGridView.Rows.Count; i++)
        {
            CheckBox cbReject = (CheckBox)orderGridView.Rows[i].FindControl("chkSelect");
            if (cbReject.Checked)
            {
                TextBox payAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");
                totalamount += Convert.ToDecimal((payAmountTextBox.Text));
            }

        }
        //if (totalamount != Convert.ToDecimal((paymentAmountTextBox.Text)))
        //{
        //    showMessageBox("Total Invoice Payment Amount Must Be Equel To Payment Amount");
        //    return false;
        //}
      
        //if (paymentDtTextBox.Text == "")
        //{
        //    showMessageBox("Please Select Payment Date!!");
        //    return false;
        //}
        if (customerTextBox.Text.Trim() == "")
        {
            showMessageBox("Please Select Customer!!");
            return false;
        }

        return true;
    }


    private string GenerateParam()
    {
        string param = "";


        if (salesCenterDropDownList.SelectedValue != "")
        {
            param = param + "AND INV.ComUnitId=" + salesCenterDropDownList.SelectedValue;
        }

        if (customerTextBox.Text.Trim() != "")
        {
            param = param + "AND CM.CustomerMasterId=" + customerTextBox.Text.Trim();
        }

        if (txtFromDate.Text !="" && txtToDate.Text !="")
        {
            param = param + "AND CONVERT(date,CP.PaymentDate) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "' ";
        }

        if (txtFromDate.Text != "" && txtToDate.Text == "")
        {
            param = param + "AND CONVERT(date,CP.PaymentDate) BETWEEN '" + txtFromDate.Text + "' AND '" + DateTime.Now + "' ";
        }

        if (txtFromDate.Text == "" && txtToDate.Text != "")
        {
            param = param + "AND CONVERT(date,CP.PaymentDate) BETWEEN '" + DateTime.Now + "' AND '" +txtToDate.Text+ "' ";
        }


        return param;
    }



    protected void saveButton_Click(object sender, EventArgs e)
    {

        DataTable dt = aCustPaymentBll.Load_CustomerPayment(GenerateParam());

        if (dt.Rows.Count > 0)
        {
            orderGridView.DataSource = dt;
            orderGridView.DataBind();

        }
        else
        {
            orderGridView.DataSource = null;
            orderGridView.DataBind();
            showMessageBox("No Not Found!!");
        }


        //if (Validation())
        //{
        //    CustomerMaster aCustomerMaster;

        //    CustPayment aCustPayment = new CustPayment();

        //    aCustPayment.CustomerMasterId = CustomerId(out aCustomerMaster);
        //    aCustPayment.MarketId = marketDropDownList.SelectedValue == "" ? 0 : Convert.ToInt32(marketDropDownList.SelectedValue);
        //    aCustPayment.ComUnitId = Convert.ToInt32(salesCenterDropDownList.SelectedValue);
        //    //   aCustPayment.PaymentDate = Convert.ToDateTime(paymentDtTextBox.Text);
        //    //   aCustPayment.PaymentAmount = Convert.ToDecimal(paymentAmountTextBox.Text);
        //    //   aCustPayment.PayType = payTypeDDL.SelectedItem.Text;
        //    // aCustPayment.RefNo = refNameTextBox.Text;

        //    //if (refDtTextBox.Text != "")
        //    //{
        //    //    aCustPayment.RefDate = Convert.ToDateTime(refDtTextBox.Text);
        //    //}

        //    aCustPayment.CreateBy = Session["LoginName"].ToString();
        //    aCustPayment.CreateDate = DateTime.Now;


        //    List<CustPaymentDetail> aCustPaymentDetails = new List<CustPaymentDetail>();

        //    for (int i = 0; i < orderGridView.Rows.Count; i++)
        //    {
        //        CheckBox ChkBoxRows = (CheckBox)orderGridView.Rows[i].Cells[0].FindControl("chkSelect");
        //        TextBox payAmountTextBox = (TextBox)orderGridView.Rows[i].Cells[7].FindControl("payAmountTextBox");
        //        decimal prevamount = 0;
        //        if (orderGridView.Rows[i].Cells[6].Text != "&nbsp;")
        //        {
        //            prevamount = Convert.ToDecimal(orderGridView.Rows[i].Cells[6].Text);
        //        }

        //        if (ChkBoxRows.Checked)
        //        {

        //            if ((Convert.ToDecimal(payAmountTextBox.Text) + prevamount) ==
        //                Convert.ToDecimal(orderGridView.Rows[i].Cells[5].Text))
        //            {
        //                decimal totalamount = 0;
        //                totalamount = (Convert.ToDecimal(payAmountTextBox.Text) + prevamount);
        //                aCustPaymentBll.UpdateInvoicePaymentAmount(totalamount.ToString(), "Full",
        //                    orderGridView.DataKeys[i][0].ToString());
        //            }
        //            else
        //            {
        //                decimal totalamount = 0;
        //                totalamount = (Convert.ToDecimal(payAmountTextBox.Text) + prevamount);
        //                aCustPaymentBll.UpdateInvoicePaymentAmount(totalamount.ToString(), "Partial",
        //                    orderGridView.DataKeys[i][0].ToString());
        //            }
        //            CustPaymentDetail aCustPaymentDetail = new CustPaymentDetail()
        //            {
        //                InvoiceId = Convert.ToInt32(orderGridView.DataKeys[i]["InvoiceId"].ToString()),
        //                PaymentAmount = Convert.ToDecimal(payAmountTextBox.Text),


        //            };

        //            aCustPaymentDetails.Add(aCustPaymentDetail);
        //        }
        //    }

        //    if (aCustPaymentBll.SaveCustPayment(aCustPayment, aCustPaymentDetails))
        //    {
        //        showMessageBox("Data Saved Successfully !!!!!");
        //        Clear();
        //    }
        //}
    }
    protected void payAmountTextBox_TextChanged(object sender, EventArgs e)
    {
        TextBox qtyTextBox = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)qtyTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        decimal prevamount = 0;
        TextBox payAmountTextBox = (TextBox)orderGridView.Rows[rowindex].Cells[7].FindControl("payAmountTextBox");
        decimal mainamount = string.IsNullOrEmpty(payAmountTextBox.Text) ? 0 : Convert.ToDecimal(payAmountTextBox.Text);
        decimal delamount = Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[5].Text);

        if (orderGridView.Rows[rowindex].Cells[6].Text != "&nbsp;")
        {
            prevamount = Convert.ToDecimal(orderGridView.Rows[rowindex].Cells[6].Text);
        }


        if ((mainamount + prevamount) > delamount)
        {
            payAmountTextBox.Text = "0";
            showMessageBox("Cannot Be Greater then Invoice Quantity ");

        }

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

       // paymentAmountTextBox.Text = prevamount.ToString(CultureInfo.InvariantCulture);
    }


    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }


    protected void btnUpload_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)orderGridView.Rows[rowID].FindControl("hfDetailsId");

        if (hfMasterId.Value !="")
        {
            Response.Redirect("PaymentAttachment.aspx?MID=" + hfMasterId.Value.Trim());
        }
        else
        {
            AlertMessageBoxShow("You Can not edit this.....");
        }
        
    }


    protected void btnPreview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)orderGridView.Rows[rowID].FindControl("hfDetailsId");

        if (hfMasterId.Value != "")
        {
            string url = "../SInventory_RPTVIEW/PayImageViewer.aspx?fromDate=" + hfMasterId.Value;
            // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }


    }

}