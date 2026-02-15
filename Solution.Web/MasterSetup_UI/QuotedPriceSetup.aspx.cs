using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_QuotedPriceSetup : System.Web.UI.Page
{

    private QuotedPriceSetupDAL _Dal = new QuotedPriceSetupDAL();
    private int mid = 0;
    private string _userId;

    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;
    protected void Page_Load(object sender, EventArgs e)
    {

        GroupSelect = (DropDownList)IVMarketStructure.FindControl("GroupSelect") as DropDownList;
        ZoneSelect = (DropDownList)IVMarketStructure.FindControl("ZoneSelect") as DropDownList;
        AreaSelect = (DropDownList)IVMarketStructure.FindControl("AreaSelect") as DropDownList;
        TeritorySelect = (DropDownList)IVMarketStructure.FindControl("TeritorySelect") as DropDownList;
        SubTeritory = (DropDownList)IVMarketStructure.FindControl("SubTeritory") as DropDownList;
        MarketSelect = (DropDownList)IVMarketStructure.FindControl("MarketSelect") as DropDownList;


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

        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        {
            Label txtDiscountShow = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountShow");
            Label txtUnitPrice = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtUnitPrice");
            TextBox txtVat = (TextBox)gv_ProductList.Rows[i].Cells[1].FindControl("txtVat");
            TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountPercent");
            txtVat.Text = 0.ToString();

            try
            {

                decimal UnitPrice = 0, Vat = 0;

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
                txtDiscountShow.Text = (Math.Round(UnitPrice - (UnitPrice - res), 3)).ToString();


            }

            catch (Exception ex)
            {

            }
        }
    }
  
    private void LoadDropdownlist()
    {
        _Dal.LoadPriceGroup(ddlCustomerPriceGroup);
    }

    private void GetOneRecord(string Id)
    {
        try
        {
            using (DataTable dt = _Dal.GetQuotedPriceMasterById(Id))
            {
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtPolicy.Text = dt.Rows[0]["Policy"].ToString();
                txtFromDate.Text = dt.Rows[0]["ActiveFromDate"].ToString();
                txtToDate.Text = dt.Rows[0]["ActiveToDate"].ToString();
                hfCustomerId.Value= dt.Rows[0]["CustomerMasterId"].ToString();
                custNameTextBox.Text= dt.Rows[0]["CustomerName"].ToString();


                string PriceGroupId = dt.Rows[0]["PriceGroupId"].ToString();

                ddlCustomerPriceGroup.SelectedValue = PriceGroupId;

               // ddlCustomerPriceGroup_OnSelectedIndexChanged(null,null);
               // ddlCustomer.SelectedValue = dt.Rows[0]["CustomerMasterId"].ToString();
            }

            using (DataTable dtDetail = _Dal.GetDetailById(Id))
            {
                for (int i = 0; i < gv_ProductList.Rows.Count; i++)
                {
                    HiddenField hfProductId = (HiddenField)gv_ProductList.Rows[i].FindControl("hfProductId");
                    var chkBoxRows = (CheckBox)gv_ProductList.Rows[i].Cells[0].FindControl("chkSelect");
                    TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[i].FindControl("txtDiscountPercent");
                    TextBox txtVat = (TextBox)gv_ProductList.Rows[i].FindControl("txtVat");
                    Label txtDiscountShow = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtDiscountShow");
                    Label txtUnitPrice = (Label)gv_ProductList.Rows[i].Cells[1].FindControl("txtUnitPrice");

                    for (int k = 0; k < dtDetail.Rows.Count; k++)
                    {
                        //var pId = hfProductId.Value;
                        //var dId = dtDetail.Rows[k]["ProductId"].ToString();

                            if (hfProductId.Value == dtDetail.Rows[k]["ProductId"].ToString())
                            {
                                chkBoxRows.Checked = true;
                                txtDiscountShow.Text = dtDetail.Rows[k]["UnitPrice"].ToString();
                                txtVat.Text = dtDetail.Rows[k]["Vat"].ToString();
                                Cal_UnitPrice_to_Percent(i);
                            }
                            //else
                            //{
                            //      chkBoxRows.Checked = false;
                            //}
                    }
                }
                    //gv_ProductList.DataSource = dtDetail;
                    //gv_ProductList.DataBind();
            }
        }
        catch (Exception ex) { }
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
        divCus.Visible = false;
        divMarket.Visible = false;
        if (rbType.Items[0].Selected == true)
        {
            divCus.Visible = true;
        }
        else
        {
            divMarket.Visible = true;

        }
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
        //    string[] emp = empName.Split(':');

        //    hfCustomerId.Value = emp[1].Trim();
        //    custNameTextBox.Text = emp[0].Trim();

        //    using (DataTable dt = _Dal.GetProductByPriceGroup(Convert.ToInt32(hfCustomerId.Value)))
        //    {
        //        gv_ProductList.DataSource = dt;
        //        gv_ProductList.DataBind();

        //    }

        //}
        //else
        //{
        //    custNameTextBox.Text = "";
        //    hfCustomerId.Value = "";
        //    showMessageBox("Input Correct Data !!");
        //}

    }



    //[WebMethod]
    //public static List<string> GetEmployeeName(string empName)
    // {
    //    List<string> empResult = new List<string>();

    //    using (SqlConnection con = new SqlConnection(@"Data Source=194.233.66.180\MSSQLSERVER2014;Initial Catalog=SalesDisDB_UniWorld_DB;Integrated Security=false; User ID=sa; Password =sa1234;"))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "SELECT convert(varchar,CustomerMasterId)+':'+CustomerCode + ':' + CustomerName AS Customer FROM tblCustMaster WHERE CustomerCode+':'+CustomerName LIKE ''+@SearchEmpName+'%'";
    //            cmd.Connection = con;
    //            con.Open();
    //            cmd.Parameters.AddWithValue("@SearchEmpName", empName);
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            while (dr.Read())
    //            {
    //                empResult.Add(dr["Customer"].ToString());
    //            }
    //            con.Close();
    //            return empResult;
    //        }
    //    }
    //}  


    public bool Validation()
    {

        custNameTextBox.CssClass = "form-control form-control-sm";
        txtDescription.CssClass = "form-control form-control-sm";
        txtPolicy.CssClass = "form-control form-control-sm";
        txtFromDate.CssClass = "form-control form-control-sm mb-3 datepicker";
        txtToDate.CssClass = "form-control form-control-sm mb-3 datepicker";

        if (txtDescription.Text == "")
        {
            txtDescription.ToolTip = "please fill out this field";
            txtDescription.CssClass = "form-control form-control-sm is-invalid";
            txtDescription.Focus();
            return false;
        }


        if (txtPolicy.Text == "")
        {
            txtPolicy.ToolTip = "please fill out this field";
            txtPolicy.CssClass = "form-control form-control-sm is-invalid";
            txtPolicy.Focus();
            return false;
        }


        if (txtFromDate.Text == "")
        {
            txtFromDate.ToolTip = "please fill out this field";
            txtFromDate.CssClass = "form-control form-control-sm mb-3 datepicker is-invalid";
            txtFromDate.Focus();
            return false;
        }


        if (txtToDate.Text == "")
        {
            txtToDate.ToolTip = "please fill out this field";
            txtToDate.CssClass = "form-control form-control-sm mb-3 datepicker is-invalid";
            txtToDate.Focus();
            return false;
        }

        if (txtDescription.Text == "")
        {
            txtDescription.ToolTip = "please fill out this field";
            //txtDescription.CssClass = "form-control form-control-sm is-invalid";
            txtDescription.Focus();
            return false;
        }


        if (ddlCustomerPriceGroup.SelectedValue == "")
        {
            ddlCustomerPriceGroup.ToolTip = "please fill out this field";
            ddlCustomerPriceGroup.Focus();
            return false;
        }


        //if (hfCustomerId.Value == "")
        //{
        //    custNameTextBox.ToolTip = "please fill out this field";
        //    custNameTextBox.CssClass = "form-control form-control-sm mb-3  is-invalid";
        //    custNameTextBox.Focus();
        //    return false;
        //}





        for (int i = 0; i < gv_ProductList.Rows.Count; i++)
        {

            CheckBox chkSelect = (CheckBox)gv_ProductList.Rows[i].FindControl("chkSelect");
            Label txtDiscountShow = (Label)gv_ProductList.Rows[i].FindControl("txtDiscountShow");

            if (chkSelect.Checked)
            {
                if (txtDiscountShow.Text.Trim() == "")
                {
            showMessageBox("Input Discount Percent (%) Or	Discount Amount !!");
                    txtDiscountShow.Focus();
                    return false;
                }
                
                    }
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
            List<QuotedPriceDetailDAO> DtlList = new List<QuotedPriceDetailDAO>();


            for (int i = 0; i < gv_ProductList.Rows.Count; i++)
            {
                HiddenField hfProductId = (HiddenField)gv_ProductList.Rows[i].FindControl("hfProductId");
             
                Label txtDiscountShow = (Label)gv_ProductList.Rows[i].FindControl("txtDiscountShow");
                TextBox txtVat = (TextBox)gv_ProductList.Rows[i].FindControl("txtVat");
                CheckBox chkSelect = (CheckBox)gv_ProductList.Rows[i].FindControl("chkSelect");
                TextBox txtDiscountPercent = (TextBox)gv_ProductList.Rows[i].Cells[1].FindControl("txtVat");

                if (chkSelect.Checked==true)
                {

                    QuotedPriceDetailDAO _DAO = new QuotedPriceDetailDAO();

                    _DAO.ProductId = Convert.ToInt32(hfProductId.Value.ToString());

                    _DAO.UnitPrice = Convert.ToDecimal(txtDiscountShow.Text.Trim());
                    _DAO.DiscountPercentage = Convert.ToDecimal(txtDiscountPercent.Text.Trim());

                    if (txtVat.Text != "") { 
                    _DAO.Vat = Convert.ToDecimal(txtVat.Text.Trim());
                    }
                    else
                    {
                        _DAO.Vat = null;
                    }

                    DtlList.Add(_DAO);
                }
            }


            QuotedPriceMasterDAO aMaster = new QuotedPriceMasterDAO();

            aMaster.QuotedPriceMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.PriceGroupId = Convert.ToInt32(ddlCustomerPriceGroup.SelectedValue);

            aMaster.Description =   string.IsNullOrEmpty(txtDescription.Text) ? null : txtDescription.Text;
            aMaster.Policy = string.IsNullOrEmpty(txtPolicy.Text) ? null : txtPolicy.Text;
            aMaster.CustomerMasterId = Convert.ToInt32(hfCustomerId.Value.Trim());
          
            aMaster.IsCustomerWise = false;
            aMaster.IsMarketWise = false;

            if (rbType.Items[0].Selected == true)
            {
                aMaster.IsCustomerWise = true;
            }
            else
            {
                aMaster.IsMarketWise = true;
 
            }


            //if (hfCustomerId.Value.Trim() != "")
            //{
            //    aMaster.CustomerMasterId =  Convert.ToInt32(hfCustomerId.Value.Trim());

            //}
            //else
            //{
            //    aMaster.CustomerMasterId = null;
            //}

            if (txtFromDate.Text.Trim() != "")
            {
                aMaster.ActiveFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());

            }
            else
            {
                aMaster.ActiveFromDate = null;
            }

            if (txtToDate.Text.Trim() != "")
            {
                aMaster.ActiveToDate = Convert.ToDateTime(txtToDate.Text.Trim());

            }
            else
            {
                aMaster.ActiveToDate = null;
            }

            aMaster.GroupId = GroupSelect.SelectedIndex > 0 ? int.Parse(GroupSelect.SelectedValue) : (int?)null;
            aMaster.RegionId = ZoneSelect.SelectedIndex > 0 ? int.Parse(ZoneSelect.SelectedValue) : (int?)null;
            aMaster.AreaId = AreaSelect.SelectedIndex > 0 ? int.Parse(AreaSelect.SelectedValue) : (int?)null;
            aMaster.TerritoryId = TeritorySelect.SelectedIndex > 0 ? int.Parse(TeritorySelect.SelectedValue) : (int?)null;
            aMaster.SubTerritoryId = SubTeritory.SelectedIndex > 0 ? int.Parse(SubTeritory.SelectedValue) : (int?)null;
            aMaster.MarketId = MarketSelect.SelectedIndex > 0 ? int.Parse(MarketSelect.SelectedValue) : (int?)null;

            bool result = false;

            ResultInfo Res= _Dal.SaveMasterDetals(aMaster, DtlList, Session["UserId"].ToString());

            if (Res.isSuccess==true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','QuotedPriceView.aspx');", true);

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
            txtVat.Text = txtCmnPercent.Text;
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
        //try
        //{
        //    using (DataTable dt = _Dal.GetProductByPriceGroup(Convert.ToInt32(ddlCustomer.SelectedValue)))
        //    {
        //        gv_ProductList.DataSource = dt;
        //        gv_ProductList.DataBind();

               

        //    }
        //}
        //catch
        //{
            
        //}
    }

    protected void custNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (custNameTextBox.Text.Trim() != "")
        {
            DataTable aTable = _Dal.Get_Customer(custNameTextBox.Text);

            if (aTable.Rows.Count > 0)
            {
                custNameTextBox.Text = aTable.Rows[0]["CustomerName"].ToString();

                hfCustomerId.Value = aTable.Rows[0]["CustomerMasterId"].ToString();

                try
                {
                    using (DataTable dt = _Dal.GetProductByPriceGroup(Convert.ToInt32(hfCustomerId.Value)))
                    {
                        gv_ProductList.DataSource = dt;
                        gv_ProductList.DataBind();

                    }
                }
                catch
                {

                }
            }
            else
            {
                ShowMessageBox("Data Not Found");
            }
        }
    }
}