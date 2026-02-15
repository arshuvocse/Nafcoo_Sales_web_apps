using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using Library.DAL.SInventory_DAL;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

public partial class SInventory_UI_BrandWisePromoSetup : System.Web.UI.Page
{
    BrandWisePromoDal aDal = new BrandWisePromoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoaddropdownList();
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                masterHiddenFieldId.Value = Request.QueryString["ID"];
                LoadWorkTypeById(masterHiddenFieldId.Value);
            }
        }
    }

    private void LoaddropdownList()
    {
        aDal.LoadBrandName(ddlBrand);
        aDal.LoadPromoProducte(ddlPromoName);
    }

    private void LoadWorkTypeById(string value)
    {
        DataTable aTable = aDal.GetBrandWisePromoById(Convert.ToInt32(value));

        ddlBrand.SelectedValue = aTable.Rows[0]["BrandId"].ToString();
        ddlPromoName.SelectedValue = aTable.Rows[0]["PromoProductId"].ToString();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            var aMasterDao = new BrandWisePromoDao();

            aMasterDao.PromoProductId = Convert.ToInt32(ddlPromoName.SelectedValue);
            aMasterDao.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            else
            {
                aMasterDao.PromoWiseBrandSetupId = Convert.ToInt32(masterHiddenFieldId.Value);
                aMasterDao.IsActive = chkIsActive.Checked ? true : false;
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.UpdateDate = DateTime.Now;
            }


            ResultInfo Res = aDal.SaveDeliveryDay(aMasterDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','BrandWisePromoList.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Already Exist!" + "','Faild');", true);

            }
        }
    }

    private void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private bool Validation()
    {
        if (ddlPromoName.SelectedValue == "")
        {
            ShowMessageBox("Please select promo  !!");
            return false;
        }

        if (ddlBrand.SelectedValue == "")
        {
            ShowMessageBox("Please select brand !!");
            return false;
        }

        return true;
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BrandWisePromoSetup.aspx");
    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BrandWisePromoList.aspx");
    }
}