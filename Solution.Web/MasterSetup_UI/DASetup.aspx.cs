using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;

public partial class MasterSetup_UI_DASetup : System.Web.UI.Page
{
    DAInfoDal aDal = new DAInfoDal();
    OrderInfoBLL aOrderInfoBll = new OrderInfoBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            aOrderInfoBll.LoadSC(salesCenterDropDownList, Session["UserId"].ToString());

            try
            {
                salesCenterDropDownList.SelectedIndex = 1;
            }
            catch
            {

            }
            if (Session["DAEdit"] != null)
            {
                btnUpdate.Visible = true;

                GetOneRecord(Convert.ToInt32(Session["DAEdit"].ToString()));
                Session["DAEdit"] = null;
            }
            else
            {
                btnSave.Visible = true;
            }
        }

    }

    private void GetOneRecord(int daId)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetDAInfoById(daId);

        if (aTable.Rows.Count > 0)
        {
            salesCenterDropDownList.SelectedValue = aTable.Rows[0]["SalesCenterId"].ToString();
            txtNID.Text = aTable.Rows[0]["NID"].ToString();
            txtName.Text = aTable.Rows[0]["Name"].ToString();
            txtPhone.Text = aTable.Rows[0]["PhoneNo"].ToString();
            txtAddress.Text = aTable.Rows[0]["Address"].ToString();
            txtEmergencyContactNo.Text = aTable.Rows[0]["EmergencyContactNo"].ToString();
            txtReferenceName.Text = aTable.Rows[0]["ReferenceName"].ToString();
            txtReferencePhone.Text = aTable.Rows[0]["ReferencePhone"].ToString();
            txtRemarks.Text = aTable.Rows[0]["Remarks"].ToString();
            hiddenField.Value = aTable.Rows[0]["DAId"].ToString();


        }
        else
        {
            txtNID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtEmergencyContactNo.Text = "";
            txtReferenceName.Text = "";
            txtReferencePhone.Text = "";
            txtRemarks.Text = "";
        }
    }


    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private bool Validation()
    {

        txtNID.CssClass = "form-control form-control-sm";
        txtName.CssClass = "form-control form-control-sm";
        txtPhone.CssClass = "form-control form-control-sm";
        txtEmergencyContactNo.CssClass = "form-control form-control-sm";
        txtReferenceName.CssClass = "form-control form-control-sm";
        txtReferencePhone.CssClass = "form-control form-control-sm";



        if (salesCenterDropDownList.SelectedValue == "")
        {
            salesCenterDropDownList.ToolTip = "please fill out this field";
            salesCenterDropDownList.CssClass = "form-control form-control-sm is-invalid";
            salesCenterDropDownList.Focus();
            return false;
        }

            if (txtName.Text == "")
        {

            txtName.ToolTip = "please fill out this field";
            txtName.CssClass = "form-control form-control-sm is-invalid";
            txtName.Focus();

            return false;
        }


        //if (txtNID.Text != "")
        //{
        //    if (txtNID.Text.Length != 17)
        //    {

        //        string text6 = "Delivery Man NID must be 11 digits!";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showpop6('" + text6 + "')</script>", false);
        //        txtNID.CssClass = "form-control form-control-sm is-invalid";
        //        txtNID.Focus();

        //        return false;
        //    }
        //}

        if (txtPhone.Text == "")
        {


            txtPhone.ToolTip = "please fill out this field";
            txtPhone.CssClass = "form-control form-control-sm is-invalid";
            txtPhone.Focus();


            return false;
        }

        if (txtPhone.Text != "")
        {
            if (txtPhone.Text.Length != 11)
            {

                string text6 = "Phone No must be 11 digits!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showpop6('" + text6 + "')</script>", false);
                txtPhone.CssClass = "form-control form-control-sm is-invalid";
                txtPhone.Focus();

                return false;
            }
        }

        if (txtEmergencyContactNo.Text == "")
        {
            txtEmergencyContactNo.ToolTip = "please fill out this field";
            txtEmergencyContactNo.CssClass = "form-control form-control-sm is-invalid";
            txtEmergencyContactNo.Focus();
            return false;
        }

        if (txtEmergencyContactNo.Text != "")
        {
            if (txtEmergencyContactNo.Text.Length != 11)
            {

                string text6 = "Emergency Contact No must be 11 digits!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showpop6('" + text6 + "')</script>", false);
                txtEmergencyContactNo.CssClass = "form-control form-control-sm is-invalid";
                txtEmergencyContactNo.Focus();

                return false;
            }
        }


        if (txtReferencePhone.Text != "")
        {
            if (txtReferencePhone.Text.Length != 11)
            {

                string text6 = "Reference Phone must be 11 digits!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showpop6('" + text6 + "')</script>", false);
                txtEmergencyContactNo.Focus();
                txtReferencePhone.CssClass = "form-control form-control-sm is-invalid";

                return false;
            }
        }


        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Validation())
        {

            if (hiddenField.Value == null)
            {
                var aDao = new DAInfoDao();

                aDao.NID = txtNID.Text.Trim();
                aDao.SalesCenterId = Convert.ToInt32(salesCenterDropDownList.SelectedValue);
                aDao.Name = txtName.Text;
                aDao.PhoneNo = txtPhone.Text;
                aDao.Address = txtAddress.Text;
                aDao.EmergencyContactNo = txtEmergencyContactNo.Text;
                aDao.ReferenceName = txtReferenceName.Text;
                aDao.ReferencePhone = txtReferencePhone.Text;
                aDao.Remarks = txtRemarks.Text;
                aDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.EntryDate = DateTime.Now;

                if (aDal.SaveDAInfo(aDao) > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','DAList.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

                }
            }
            else
            {
                var aDao = new DAInfoDao();
                aDao.DAId = hiddenField.Value == "" ? 0 : Convert.ToInt32(hiddenField.Value);
                aDao.SalesCenterId = Convert.ToInt32(salesCenterDropDownList.SelectedValue);
                aDao.NID = txtNID.Text.Trim();
                aDao.Name = txtName.Text;
                aDao.PhoneNo = txtPhone.Text;
                aDao.Address = txtAddress.Text;
                aDao.EmergencyContactNo = txtEmergencyContactNo.Text;
                aDao.ReferenceName = txtReferenceName.Text;
                aDao.ReferencePhone = txtReferencePhone.Text;
                aDao.Remarks = txtRemarks.Text;
                aDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.EntryDate = DateTime.Now;

                if (aDal.SaveDAInfo(aDao) > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','DAList.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

                }
            }

        }
    }

    private void Clear()
    {
        txtNID.Text = "";
        txtName.Text = "";
        txtPhone.Text = "";
        txtAddress.Text = "";
        txtEmergencyContactNo.Text = "";
        txtReferenceName.Text = "";
        txtReferencePhone.Text = "";
        txtRemarks.Text = "";

        btnSave.Text = "Save";
    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("DASetup.aspx");
    }
}