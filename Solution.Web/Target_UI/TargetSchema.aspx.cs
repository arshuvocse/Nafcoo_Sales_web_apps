using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAO.Target_DAO;
using Library.DAL.TargetDAL;
using Library.DAL.MasterSetup_DAL;

public partial class Target_UI_TargetSchema : System.Web.UI.Page
{

    private CampaignSetupDal _Dal = new CampaignSetupDal();
    private TargetSchemaDAL _tDal = new TargetSchemaDAL();
    private int mid = 0;
    private string _userId;

    private DropDownList GroupSelect, ZoneSelect, AreaSelect, TeritorySelect, SubTeritory, MarketSelect;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadInitialInfo();

            if (!string.IsNullOrEmpty(Request.QueryString["SchemaMasterId"]))
            {
                btnUpdate.Visible = true;

                var SchemaMasterId = Request.QueryString["SchemaMasterId"];
                GetOneRecord(SchemaMasterId);
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


    public void GetOneRecord(string Id)
    {
        hfSchemaId.Value = Id;
        try
        {
            DataTable dt = _tDal.GetDataById(Id);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Fill master data
                tbxSchemaName.Text = dt.Rows[0]["SchemaName"].ToString();
                tbxSchemaAmount.Text = dt.Rows[0]["SchemaAmount"].ToString();

            }

            // Filter only detail rows (assuming ProductId and Percentage are in all rows)
            DataView dvDetails = new DataView(dt);
            dvDetails.RowFilter = "ProductId IS NOT NULL"; // Adjust this if needed

            for (int i = 0; i < gv_ProductList.Rows.Count; i++)
            {
                HiddenField hfProductId = gv_ProductList.Rows[i].FindControl("hfProductId") as HiddenField;
                CheckBox chkBoxRows = gv_ProductList.Rows[i].FindControl("chkSelect") as CheckBox;
                TextBox tbxSchemaPercentage = gv_ProductList.Rows[i].FindControl("tbxSchemaPercentage") as TextBox;

                if (hfProductId == null || chkBoxRows == null || tbxSchemaPercentage == null)
                    continue;

                // Find matching row in detail data
                DataRow[] match = dt.Select("ProductId = '" + hfProductId.Value + "'");

                if (match.Length > 0)
                {
                    chkBoxRows.Checked = true;
                    tbxSchemaPercentage.Text = match[0]["Percentage"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Basic error feedback — replace with proper logging
            Response.Write("Error loading record: " + ex.Message);
        }
    }


    private void LoadInitialInfo()
    {
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
    
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {


        if (tbxSchemaName.Text.Trim() == "")
        {
            tbxSchemaName.ToolTip = "Please Select Schema Name!";
            tbxSchemaName.Focus();
            return false;
        }

        if (tbxSchemaAmount.Text.Trim() == "")
        {
            tbxSchemaAmount.ToolTip = "Please Select Schema Amount !";
            tbxSchemaAmount.Focus();
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
            TargetSchemaMasterDAO aMasterDao = new TargetSchemaMasterDAO();
            string SchemaMasterId = " ";
            if (hfSchemaId.Value != null)
            {
                SchemaMasterId = hfSchemaId.Value;
            }
            else {
                SchemaMasterId = "0";
            }
            
            var SchemaName = tbxSchemaName.Text.ToString();
            var SchemaAmt = Convert.ToDecimal(tbxSchemaAmount.Text); 

            List<TargetSchemaDetailDAO> DtlList = new List<TargetSchemaDetailDAO>();


            for (int i = 0; i < gv_ProductList.Rows.Count; i++)
            {
                HiddenField hfProductId = (HiddenField)gv_ProductList.Rows[i].FindControl("hfProductId");
                CheckBox chkSelect = (CheckBox)gv_ProductList.Rows[i].FindControl("chkSelect");
                TextBox percentage = (TextBox)gv_ProductList.Rows[i].FindControl("tbxSchemaPercentage");
                if (chkSelect.Checked==true)
                {

                    TargetSchemaDetailDAO _DAO = new TargetSchemaDetailDAO();

                    _DAO.ProductId = Convert.ToInt32(hfProductId.Value);
                    _DAO.Percentage = string.IsNullOrWhiteSpace(percentage.Text) ? 0 : Convert.ToDecimal(percentage.Text);

                    DtlList.Add(_DAO);
                }
            }    

            bool result = false;
            int masterId = string.IsNullOrEmpty(SchemaMasterId) ? 0 : Convert.ToInt32(SchemaMasterId);
            ResultInfo Res= _tDal.SaveMasterDetals(Convert.ToInt32(masterId),SchemaName, SchemaAmt, DtlList, Session["UserId"].ToString());

            if (Res.isSuccess==true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','TargetSchemeView.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }

        }

    }

    protected void restbtn_Click(object sender, EventArgs e)
    {

    }
}