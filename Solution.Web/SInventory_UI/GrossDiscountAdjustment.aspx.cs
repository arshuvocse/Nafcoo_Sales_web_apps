using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_FrossDiscountAdjustment : System.Web.UI.Page
{

    GrossDiscountAdjustmentDal aDal = new GrossDiscountAdjustmentDal();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (tblDelivaryInvoiceNo.Text.Trim() != "")
        {
            if (tblGrossDiscountAmount.Text.Trim() != "")
            {
                if (Convert.ToDecimal(tblGrossDiscountAmount.Text) > 0)
                {
                    int val = 0;
                    val = aDal.UpdateInvoice(tblDelivaryInvoiceNo.Text, Convert.ToDecimal(tblGrossDiscountAmount.Text),Convert.ToInt32(Session["UserId"].ToString()));

                    if (val > 0)
                    {
                        tblDelivaryInvoiceNo.Text = "";
                        tblGrossDiscountAmount.Text = "";
                        showMessageBox("Updated successfully !!");
                    }
                    else
                    {
                        showMessageBox("Update Operation failed !!");
                    }

                }
                {
                    showMessageBox("Please discount amount should be greater than 0 !!");
                }
            }
            else
            {
                showMessageBox("Please select discount amount !!");
            }
        }
        else
        {
            showMessageBox("Please select DeliveryInvoice no !!");
        }
    }


    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("GrossDiscountAdjustment.aspx");
    }
}