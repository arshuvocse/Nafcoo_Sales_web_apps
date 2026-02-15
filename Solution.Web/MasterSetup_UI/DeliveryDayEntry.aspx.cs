using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;
using DataTable = System.Data.DataTable;

public partial class MasterSetup_UI_WorkTypeEntry : System.Web.UI.Page
{

    DeliveryDayDal aDal = new DeliveryDayDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                masterHiddenFieldId.Value = Request.QueryString["ID"];
                LoadWorkTypeById(masterHiddenFieldId.Value);
            }
        }
    }

    private void LoadWorkTypeById(string value)
    {
        DataTable aTable = aDal.GetDeliveryDayById(Convert.ToInt32(value));

        tbxWorkType.Text = aTable.Rows[0]["DeliveryDayName"].ToString();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            DeliveryDayDao aMasterDao = new DeliveryDayDao();

            aMasterDao.DeliveryDay = tbxWorkType.Text;

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            else
            {
                aMasterDao.DeliveryDayId = Convert.ToInt32(masterHiddenFieldId.Value);
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.UpdateDate = DateTime.Now;
            }


            ResultInfo Res = aDal.SaveDeliveryDay(aMasterDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','DeliveryDayView.aspx');", true);

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
        if (tbxWorkType.Text.Trim() == "")
        {
            ShowMessageBox("Please select delivery day !!");
            return false;
        }

        return true;
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryDayEntry.aspx");
    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryDayView.aspx");
    }
}