using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;

public partial class MasterSetup_UI_PriceGroupSetup : System.Web.UI.Page
{

    private PriceGroupDal aGroupDal = new PriceGroupDal();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                btnUpdate.Visible = true;
                id_mastetID.Value = Request.QueryString["MID"];

                GetOneRecord(Request.QueryString["MID"]);
            }
            else
            {
                btnSave.Visible = true;
            }
        }
    }




    private void GetOneRecord(string Id)
    {
        try
        {
            using (DataTable dt = aGroupDal.PriceListById(Id))
            {
                txtGroupName.Text = dt.Rows[0]["PriceGroupName"].ToString();
            }

        }
        catch (Exception ex) { }
    }
 

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            priceSetupDao aSetupDao = new priceSetupDao();

            if (id_mastetID.Value == "")
            {
                aSetupDao.PriceGroupId = 0;
            }
            else
            {
                aSetupDao.PriceGroupId = Convert.ToInt32(id_mastetID.Value);
            }
            
            aSetupDao.CheckPriceGroupName = String.Concat(txtGroupName.Text.Where(c => !Char.IsWhiteSpace(c)));
            aSetupDao.PriceGroupName = txtGroupName.Text;
            aSetupDao.EntryBy = int.Parse(Session["UserId"].ToString());
            ResultInfo aInfo = new ResultInfo();
            aInfo= aGroupDal.SaveInfo(aSetupDao);
            if (aInfo.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','PriceGroupView.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);
            }
        }
    }



    private bool Validation()
    {
        if (txtGroupName.Text.Trim() == "")
        {
            showMessageBox("Please Input Price Group");
            txtGroupName.Focus();
            return false;
        }
        return true;
    }


    private static readonly Regex sWhitespace = new Regex(@"\s+");
    public static string ReplaceWhitespace(string input, string replacement)
    {
        return sWhitespace.Replace(input, replacement);
    }



    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceGroupView.aspx");
    }

    protected void txtGroupName_OnTextChanged(object sender, EventArgs e)
    {
        if (txtGroupName.Text.Trim() != "")
        {

        }
    }
}