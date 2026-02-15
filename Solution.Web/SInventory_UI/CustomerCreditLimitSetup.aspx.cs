using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

public partial class SInventory_UI_CustomerCreditLimitSetup : System.Web.UI.Page
{

    CustomerCreditLimitDal aDal = new CustomerCreditLimitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["MasterId"] != null)
            {
                creditHiddenField.Value = Session["MasterId"].ToString();
                GetOneRecord(Convert.ToInt32(creditHiddenField.Value));
                Session["MasterId"] = null;
            }
        }
    }

    private void GetOneRecord(int masterId)
    {
        DataTable aTable = aDal.GetDataById(masterId);

        if (aTable.Rows.Count > 0)
        {
            custCodeTextBox.Text = aTable.Rows[0].Field<String>("Customer");
            tbxLimitAmount.Text = aTable.Rows[0].Field<Decimal>("LimitAmount").ToString();
            tbxDayLimit.Text = aTable.Rows[0].Field<Int32>("DayLimit").ToString();
            hfCustomerId.Value = aTable.Rows[0].Field<Int32>("CustomerMasterId").ToString();
            hfCustomerPriceGroupId.Value = aTable.Rows[0].Field<Int32>("PriceGroupId").ToString();
            creditHiddenField.Value = aTable.Rows[0].Field<Int32>("CreditLimitId").ToString();

            submitButton.Text = "Update";

        }
        else
        {
            ShowMessageBox("No dat found !!");
        }
    }

    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {

        string empName = custCodeTextBox.Text.Trim();
        if (empName.Contains(':'))
        {
            string[] emp = empName.Split('|');

            hfCustomerId.Value = emp[1].Trim();
            hfCustomerPriceGroupId.Value = emp[2].Trim();
            custCodeTextBox.Text = emp[0].Trim();
        }
        else
        {

            custCodeTextBox.Text = "";
            hfCustomerId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            CreditLimitDAO aMasterDao = new CreditLimitDAO();

            aMasterDao.CustomerMasterId = Convert.ToInt32(hfCustomerId.Value);
            aMasterDao.LimitAmount = Convert.ToDecimal(tbxLimitAmount.Text.Trim());
            aMasterDao.DayLimit = Convert.ToInt32(tbxDayLimit.Text);
            aMasterDao.CompanyId = Convert.ToInt32(1);

            if (creditHiddenField.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            else
            {
                aMasterDao.CreditLimitId = Convert.ToInt32(creditHiddenField.Value);
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.UpdateDate = DateTime.Now;
            }
            
            ResultInfo Res = aDal.SaveData(aMasterDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','CustomerCreditLimitView.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }
        }
    }

    protected void resetbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerCreditLimitSetup.aspx");
    }

    private bool Validation()
    {
        if (hfCustomerId.Value == "")
        {
            ShowMessageBox("Please Select a customer !!");
            return false;
        }

       
        if (tbxLimitAmount.Text == string.Empty)
        {
            ShowMessageBox("Please select Customer Credit Limit");
            return false;
        }

        //if (tbxDayLimit.Text == string.Empty)
        //{
        //    ShowMessageBox("Please select Customer Day Limit");
        //    return false;
        //}


        return true;
    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerCreditLimitView.aspx");
    }
}