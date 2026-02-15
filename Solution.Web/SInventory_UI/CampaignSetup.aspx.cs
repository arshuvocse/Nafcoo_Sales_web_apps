using System.Globalization;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_QuotedPriceSetup : System.Web.UI.Page
{

    private CampaignSetupDal _Dal = new CampaignSetupDal();
    private int mid = 0;
    private string _userId;

    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;
    protected void Page_Load(object sender, EventArgs e)
    {

       


        if (!IsPostBack)
        {
            LoadInitialInfo();
            LoadDropdownlist();
     
            SetDiscountPercentaze();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                btnUpdate.Visible = true;

                id_mastetID.Value = Request.QueryString["MID"];
                GetOneRecord(id_mastetID.Value);
            }
            else
            {

                //var chkBoxHeader = (CheckBox)gv_ProductList.HeaderRow.FindControl("chkSelectAll");
                //chkBoxHeader.Checked = true;
                //chkSelectAll_CheckedChanged(null, null);
            
                btnSave.Visible = true;
            }
        }
    }

    private void SetDiscountPercentaze()
    {

        //for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        //{
        //    Label txtDiscountShow = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountShow");
        //    Label txtUnitPrice = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtUnitPrice");
        //    TextBox txtVat = (TextBox)gv_ProductList.Rows[i].Cells[1].FindControl("txtVat");
        //    TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountPercent");
        //    txtVat.Text = 0.ToString();

        //    try
        //    {

        //        decimal UnitPrice = 0, Vat = 0;

        //        try
        //        {
        //            UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
        //        }

        //        catch (Exception ex)
        //        {

        //        }

        //        try
        //        {
        //            Vat = Convert.ToDecimal(txtVat.Text);
        //        }

        //        catch (Exception ex)
        //        {

        //        }


        //        decimal res = 0;
        //        decimal first = UnitPrice * Vat;
        //        decimal secend = first / 100;

        //        res = UnitPrice - secend;
        //        txtDiscountPercent.Text = Math.Round(secend, 3).ToString();
        //        txtDiscountShow.Text = (Math.Round(UnitPrice - (UnitPrice - res), 3)).ToString();


        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
  
    private void LoadDropdownlist()
    {
        //try
        //{
        //    using (DataTable dt = _Dal.GetCustomerListActive())
        //    {
        //        ddlCustomer.DataSource = dt;
        //        ddlCustomer.DataValueField = "Value";
        //        ddlCustomer.DataTextField = "TextField";
        //        ddlCustomer.DataBind();
        //        ddlCustomer.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
        //        ddlCustomer.SelectedIndex = 0;
        //    }


        //}
        //catch (Exception ex) { }
    }


    protected void custCodeTextBox_TextChanged(object sender, EventArgs e)
    {
        tbxSlabAmount.Text = "";
        tbxDiscountPercent.Text = "";
        hfSpecialCampaignId.Value = "";

        string empName = custCodeTextBox.Text.Trim();
        if (empName.Contains(':'))
        {
            string[] emp = empName.Split('|');

            hfCustomerId.Value = emp[1].Trim();
            custCodeTextBox.Text = emp[0].Trim();


            if(hfCustomerId.Value != "")
            {
                DataTable aTable = new DataTable();

                aTable = _Dal.GetExistingDiscount(Convert.ToInt32(hfCustomerId.Value));

                if (aTable.Rows.Count > 0)
                {
                    
                        tbxSlabAmount.Text = aTable.Rows[0].Field<Decimal>("SlabAmount").ToString(CultureInfo.InvariantCulture);
                        tbxDiscountPercent.Text = aTable.Rows[0].Field<Decimal>("DiscountPercentage").ToString(CultureInfo.InvariantCulture);
                        hfSpecialCampaignId.Value = aTable.Rows[0].Field<Int32>("CampaignMasterId").ToString(CultureInfo.InvariantCulture);


                        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
                        {
                            string productCode = gv_ProductList.DataKeys[i][0].ToString();
                            var chkBoxRows = (CheckBox)gv_ProductList.Rows[i].Cells[0].FindControl("chkSelect");

                             for (int j = 0; j < aTable.Rows.Count; j++)
                             {
                                 
                                 if (aTable.Rows[j]["ProductCode"].ToString() == productCode)
                                 {
                                     chkBoxRows.Checked = true;
                                     break;
                                 }
                                 
                             }
                        }
                }
            }
        }
        else
        {

            custCodeTextBox.Text = "";
            hfCustomerId.Value = "";
            showMessageBox("Input Correct Data !!");
        }

    }

    private void GetOneRecord(string Id)
    {
        //try
        //{
        //    using (DataTable dt = _Dal.GetQuotedPriceMasterById(Id))
        //    {
        //        txtDescription.Text = dt.Rows[0]["Description"].ToString();
        //        txtPolicy.Text = dt.Rows[0]["Policy"].ToString();
        //        txtFromDate.Text = dt.Rows[0]["ActiveFromDate"].ToString();
        //        txtToDate.Text = dt.Rows[0]["ActiveToDate"].ToString();
        //        hfCustomerId.Value= dt.Rows[0]["CustomerMasterId"].ToString();
        //        custNameTextBox.Text= dt.Rows[0]["CustomerName"].ToString();


        //        string PriceGroupId = dt.Rows[0]["PriceGroupId"].ToString();

        //        ddlCustomerPriceGroup.SelectedValue = PriceGroupId;

        //        ddlCustomerPriceGroup_OnSelectedIndexChanged(null,null);
        //        ddlCustomer.SelectedValue = dt.Rows[0]["CustomerMasterId"].ToString();
        //    }

        //    using (DataTable dtDetail = _Dal.GetDetailById(Id))
        //    {
        //        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        //        {
        //            HiddenField hfProductId = (HiddenField)gv_ProductList.Rows[i].FindControl("hfProductId");
        //    var chkBoxRows = (CheckBox)gv_ProductList.Rows[i].Cells[0].FindControl("chkSelect");
        //            TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[i].FindControl("txtDiscountPercent");
        //            TextBox txtVat = (TextBox)gv_ProductList.Rows[i].FindControl("txtVat");
        //            Label txtDiscountShow = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountShow");
        //            Label txtUnitPrice = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtUnitPrice");

        //            for (int k = 0; k < dtDetail.Rows.Count; k++)
        //            {
        //                //var pId = hfProductId.Value;
        //                //var dId = dtDetail.Rows[k]["ProductId"].ToString();

        //                    if (hfProductId.Value == dtDetail.Rows[k]["ProductId"].ToString())
        //                    {
        //                        chkBoxRows.Checked = true;
        //                        txtDiscountShow.Text = dtDetail.Rows[k]["UnitPrice"].ToString();
        //                        txtVat.Text = dtDetail.Rows[k]["Vat"].ToString();
        //                        Cal_UnitPrice_to_Percent(i);

        //                    }
        //                    //else
        //                    //{
        //                    //      chkBoxRows.Checked = false;

        //                    //}
                        


                     
        //            }
        //        }
        //            //gv_ProductList.DataSource = dtDetail;
        //            //gv_ProductList.DataBind();
        //        }
        //}
        //catch (Exception ex) { }
    }
        private void LoadInitialInfo()
    {
        //try
        //{
        //    using (DataTable dt = _Dal.GetCustomerListActive())
        //    {
        //        ddlCustomer.DataSource = dt;
        //        ddlCustomer.DataValueField = "Value";
        //        ddlCustomer.DataTextField = "TextField";
        //        ddlCustomer.DataBind();
        //        ddlCustomer.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
        //        ddlCustomer.SelectedIndex = 0;
        //    }


        //}
        //catch (Exception ex) { }


        try
        {
            using (DataTable dt = _Dal.GetProductListActive())
            {
                gv_ProductList.DataSource = dt;
                gv_ProductList.DataBind();
            }


        }
        catch (Exception ex) { }
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_ProductList.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_ProductList.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
            //chkBoxRows.Enabled = false;
            //chkBoxHeader.Enabled = false;
        }
    }
    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divCus.Visible = false;
        //divMarket.Visible = false;
        //if (rbType.Items[0].Selected == true)
        //{
        //    divCus.Visible = true;
        //}
        //else
        //{
        //    divMarket.Visible = true;

        //}
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void custNameTextBox_TextChanged(object sender, EventArgs e)
    {


        //string empName = custNameTextBox.Text.Trim();
        //if (empName.Contains(':'))
        //{
        //    string[] emp = empName.Split('|');

        //    hfCustomerId.Value = emp[1].Trim();
        //    custNameTextBox.Text = emp[0].Trim();



        //}
        //else
        //{

        //    custNameTextBox.Text = "";
        //    hfCustomerId.Value = "";
        //    showMessageBox("Input Correct Data !!");
        //}

     
    }

    public bool Validation()
    {

        if (hfCustomerId.Value == "")
        {
            ddlCustomer.ToolTip = "Please select customer !";
            ddlCustomer.Focus();
            return false;
        }

        if (tbxSlabAmount.Text.Trim() == "")
        {
            tbxSlabAmount.ToolTip = "Please Select Slab Amount !";
            tbxSlabAmount.Focus();
            return false;
        }

        if (tbxDiscountPercent.Text.Trim() == "")
        {
            tbxDiscountPercent.ToolTip = "Please Select Discount Amount !";
            tbxDiscountPercent.Focus();
            return false;
        }

        
        Int32 count = 0;

        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)gv_ProductList.Rows[i].FindControl("chkSelect");

            if (chkSelect.Checked)
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
            ShowMessageBox("Please Select at least one row !!!");
            return false;
        }

        return true;
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Validation())
        {
            CampaignSetupMasterDao aMasterDao = new CampaignSetupMasterDao();

            if (hfSpecialCampaignId.Value != "")
            {
                aMasterDao.CampaignMasterId = Convert.ToInt32(hfSpecialCampaignId.Value);
            }

            aMasterDao.CustomerInformationId = Convert.ToInt32(hfCustomerId.Value);
            aMasterDao.SlabAmount = Convert.ToDecimal(tbxSlabAmount.Text.Trim());
            aMasterDao.DiscountPercentage = Convert.ToDecimal(tbxDiscountPercent.Text);

            List<CampaignSetupDetailDao> DtlList = new List<CampaignSetupDetailDao>();


            for (int i = 0; i < gv_ProductList.Rows.Count; i++)
            {
                HiddenField hfProductId = (HiddenField)gv_ProductList.Rows[i].FindControl("hfProductId");
                CheckBox chkSelect = (CheckBox)gv_ProductList.Rows[i].FindControl("chkSelect");

                if (chkSelect.Checked==true)
                {

                    CampaignSetupDetailDao _DAO = new CampaignSetupDetailDao();

                    _DAO.ProductId = Convert.ToInt32(hfProductId.Value);

                    DtlList.Add(_DAO);
                }
            }    

            bool result = false;

            ResultInfo Res= _Dal.SaveMasterDetals(aMasterDao, DtlList, Session["UserId"].ToString());

            if (Res.isSuccess==true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','CampaignSetup.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }

    }



    protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
    {

        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        try
        {


            Cal_UnitPrice_to_Percent(rowIndex);
            
        }
        catch(Exception ex)
        {

        }
    }

    private void Cal_UnitPrice_to_Percent(int rowIndex)
    {

        try
        {

            Label txtDiscountShow = (Label)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtDiscountShow");
            Label txtUnitPrice = (Label)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtUnitPrice");
            TextBox txtVat = (TextBox)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtVat");
            TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtDiscountPercent");



            decimal UnitPrice = 0, Vat=0;

            try
            {
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            }

            catch (Exception ex)
            {

            }

            try
            {
                Vat = Convert.ToDecimal(txtVat.Text);
            }

            catch (Exception ex)
            {

            }



            decimal res = 0;

            decimal first = UnitPrice * Vat;

            decimal secend = first / 100;

            res = UnitPrice - secend;

            txtDiscountPercent.Text = Math.Round(secend, 3).ToString();


            txtDiscountShow.Text = (Math.Round(UnitPrice - (UnitPrice - res),3)).ToString();
          //  txtDiscountShow.Text = ((UnitPrice - Convert.ToDecimal(txtDiscountPercent.Text))).ToString();


        }

        catch (Exception ex)
        {

        }
    }

    protected void lblDiscountPercent_TextChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        try
        {


            Cal_Percent_to_UnitPrice(rowIndex);

        }
        catch (Exception ex)
        {

        }
    }


    private void Cal_Percent_to_UnitPrice(int rowIndex)
    {

        try
        {
            Label txtDiscountShow = (Label)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtDiscountShow");

            Label txtUnitPrice = (Label)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtUnitPrice");
            TextBox txtVat = (TextBox)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtVat");
            TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[rowIndex].Cells[1].FindControl("txtDiscountPercent");



            decimal UnitPrice = 0, DiscountPercent = 0;

            try
            {
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            }

            catch (Exception ex)
            {

            }

            try
            {
                DiscountPercent = Convert.ToDecimal(txtDiscountPercent.Text);
            }

            catch (Exception ex)
            {

            }



            decimal res = 0;

            decimal first =  DiscountPercent/ UnitPrice;

            decimal secend = first * 100;



            txtVat.Text = Math.Round(secend, 3).ToString();




            txtDiscountShow.Text = (Math.Round(UnitPrice - Convert.ToDecimal(txtDiscountPercent.Text),3)).ToString();
            //txtDiscountShow.Text = (UnitPrice - (UnitPrice - DiscountPercent)).ToString();
        }

        catch (Exception ex)
        {

        }
    }


    protected void txtCmnPercent_TextChanged(object sender, EventArgs e)
    {


        
        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        {
            var txtVat = (TextBox)gv_ProductList.Rows[i].Cells[0].FindControl("txtVat");
            

            Cal_UnitPrice_to_Percent(i);
        }
    }

    protected void restbtn_Click(object sender, EventArgs e)
    {

    }

    protected void ddlCustomerPriceGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCustomerPriceGroup.SelectedValue != "")
        //{
        //    _Dal.LoadCustomerByPriceGroup(ddlCustomer, Convert.ToInt32(ddlCustomerPriceGroup.SelectedValue));
        //}
    }

    protected void ddlCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (DataTable dt = _Dal.GetProductByPriceGroup(Convert.ToInt32(ddlCustomer.SelectedValue)))
            {
                gv_ProductList.DataSource = dt;
                gv_ProductList.DataBind();

                //var chkBoxHeader = (CheckBox)gv_ProductList.HeaderRow.FindControl("chkSelectAll");
                //chkBoxHeader.Checked = true;

                //chkSelectAll_CheckedChanged(null, null);

            }
        }
        catch
        {
            
        }
    }
}