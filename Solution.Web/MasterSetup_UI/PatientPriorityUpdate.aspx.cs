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

    PatientPriorityDal aDal = new PatientPriorityDal();
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
        DataTable aTable = aDal.GetWorkTypeById(Convert.ToInt32(value));

        tbxPriorityStartPoint.Text = aTable.Rows[0]["PatientStartPoint"].ToString();
        tbxPriorityEndPoint.Text = aTable.Rows[0]["PatientEndPoint"].ToString();
        tbxRxStartPoint.Text = aTable.Rows[0]["RXStartPoint"].ToString();
        tbxRxEbdPoint.Text = aTable.Rows[0]["RXEndPoint"].ToString();
        ddlPatientstatus.SelectedValue = aTable.Rows[0]["PatientStatus"].ToString();
        ddlColourCode.SelectedValue = aTable.Rows[0]["ColourCodeForNote"].ToString();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            PatientPriorityDao aMasterDao = new PatientPriorityDao();

            aMasterDao.PatientStartPoint = Convert.ToInt32(tbxPriorityStartPoint.Text.Trim());
            aMasterDao.PatientEndPoint = Convert.ToInt32(tbxPriorityEndPoint.Text.Trim());
            aMasterDao.RxStartPoint = Convert.ToInt32(tbxRxStartPoint.Text.Trim());
            aMasterDao.RxEndPoint = Convert.ToInt32(tbxRxEbdPoint.Text.Trim());
            aMasterDao.Patientstatus = ddlPatientstatus.SelectedValue;
            aMasterDao.ColourCodeForNote = ddlColourCode.SelectedValue;

            if (masterHiddenFieldId.Value == "")
            {
                aMasterDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.EntryDate = DateTime.Now;
            }
            else
            {
                aMasterDao.PatientPriorityId = Convert.ToInt32(masterHiddenFieldId.Value);
                aMasterDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aMasterDao.UpdateDate = DateTime.Now;
            }


            ResultInfo Res = aDal.SaveWorkType(aMasterDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful!" + "','Success','PatientPriorityView.aspx');", true);

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
        if (tbxPriorityStartPoint.Text.Trim() == "")
        {
            ShowMessageBox("Please select Priority Start Point !!");
            return false;
        }

        if (tbxPriorityEndPoint.Text.Trim() == "")
        {
            ShowMessageBox("Please select Priority End Point !!");
            return false;
        }

        if (tbxRxStartPoint.Text.Trim() == "")
        {
            ShowMessageBox("Please select  RX start Point !!");
            return false;
        }

        if (tbxRxEbdPoint.Text.Trim() == "")
        {
            ShowMessageBox("Please select RX end Point !!");
            return false;
        }
        
        if (ddlPatientstatus.SelectedValue == "")
        {
            ShowMessageBox("Please select ststus !!");
            return false;
        }

        if (ddlColourCode.SelectedValue == "")
        {
            ShowMessageBox("Please select colour codee !!");
            return false;
        }

        return true;
    }
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkTypeEntry.aspx");
    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientPriorityView.aspx");
    }
}