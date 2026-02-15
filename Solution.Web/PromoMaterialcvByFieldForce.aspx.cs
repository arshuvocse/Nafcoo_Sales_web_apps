using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.Panal_BLL;
using Library.DAL.SInventory_DAL;

public partial class PromoMaterialcvByFieldForce : System.Web.UI.Page
{
    PromoMaterialReportDal aDal = new PromoMaterialReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if empID is present in the query string
            LoadInfo();
        }
    }

    private void LoadInfo()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["empID"]))
        {
            int empID;
            if (int.TryParse(Request.QueryString["empID"], out empID))
            {
                DataTable aTable = aDal.PromoMaterialcvByFieldForceApp(empID);
                if (aTable != null && aTable.Rows.Count > 0)
                {
                    RepeaterPromo.DataSource = aTable;
                    RepeaterPromo.DataBind();
                }
                else
                {
                    // Handle empty data (optional)
                    RepeaterPromo.DataSource = null;
                    RepeaterPromo.DataBind();
                }

                // You can bind aTable to a control if needed, e.g., GridView1.DataSource = aTable;
            }
            else
            {
                // Optional: handle invalid empID format
                // e.g., show error message or log
            }
        }
        else
        {
            // Optional: handle missing empID
            // e.g., show error message or redirect
        }
    }

    protected void RepeaterPromo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string PromoChallanId = e.CommandArgument.ToString();
        TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
        string remarks = txtRemarks != null ? txtRemarks.Text.Trim() : "";
        lblMessage.Text = "";
        if (string.IsNullOrEmpty(remarks))
        {
            lblMessage.CssClass = "alert alert-danger d-inline-block";
            lblMessage.Text = "Please enter remarks before proceeding.";
            lblMessage.Visible = true;




            // ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please enter remarks before proceeding." + "','Faild');", true);
            return;
        }

        if (e.CommandName == "Receive")
        {
            if (PromoChallanId != "")
            {
                int empID;
                if (int.TryParse(Request.QueryString["empID"], out empID))
                {

                    if (aDal.ApproveChallanListFS(PromoChallanId, empID, true, remarks))
                    {
                        // Show alert using JavaScript if rem
                        lblMessage.CssClass = "alert alert-success d-inline-block";
                        lblMessage.Text = "Operation successfully Done.";
                        lblMessage.Visible = true;


                        LoadInfo();
                    }
                    else
                    {
                        lblMessage.CssClass = "alert alert-danger d-inline-block";
                        lblMessage.Text = "Something Went Wrong.";
                        lblMessage.Visible = true;


                    }
                }
            }
        }
        else if (e.CommandName == "Reject")
        {
            if (PromoChallanId != "")
            {
                int empID;
                if (int.TryParse(Request.QueryString["empID"], out empID))
                {

                    if (aDal.ApproveChallanListFS(PromoChallanId, empID, false, remarks))
                    {
                        lblMessage.CssClass = "alert alert-success d-inline-block";
                        lblMessage.Text = "Operation successfully Done.";
                        lblMessage.Visible = true;
                        LoadInfo();
                    }
                }
                else
                {
                    lblMessage.CssClass = "alert alert-danger d-inline-block";
                    lblMessage.Text = "Something Went Wrong.";
                    lblMessage.Visible = true;


                }
            }
        }

        // Optional: reload or rebind data after action
        // BindRepeater();
    }




}