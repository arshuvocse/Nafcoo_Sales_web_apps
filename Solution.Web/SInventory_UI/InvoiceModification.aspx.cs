using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_InvoiceModification : System.Web.UI.Page
{
    private InvoiceDAL aInvoiceDal = new InvoiceDAL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_DLL();
        }
        
    }


    private void Get_DLL()
    {
        try
        {
            using (DataTable dt = aInvoiceDal.InvoiceNo())
            {
                ddlInvoiceNo.DataSource = dt;
                ddlInvoiceNo.DataValueField = "InvoiceId";
                ddlInvoiceNo.DataTextField = "InvoiceNo";
                ddlInvoiceNo.DataBind();
                ddlInvoiceNo.Items.Insert(0, new ListItem("Please Select From List", String.Empty));
                ddlInvoiceNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex) { }

        //aInvoiceDal.InvoiceNumber(ddlInvoiceNo);
    
    }


    private void Load_InvoiceDetails()
    {

       // ddlInvoiceNo.SelectedItem.Text
        //INV-BD030000013982
        DataTable dt = aInvoiceDal.InvoiceModification(ddlInvoiceNo.SelectedItem.Text);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }



    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        if (ddlInvoiceNo.SelectedValue != "")
        {
            Load_InvoiceDetails();
        }
    }


    private void SaveInvoiceDetail()
    {
        List<InvoiceDetail> aInvoiceDetailsList = new List<InvoiceDetail>();
        if (GridView1.Rows.Count > 0)
        {
           
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                InvoiceDetail aInvoiceDetail = new InvoiceDetail();
                aInvoiceDetail.InvoiceDetailId = Convert.ToInt32(GridView1.DataKeys[i][0].ToString());
                aInvoiceDetail.TotalPrice = Convert.ToDecimal(((TextBox)GridView1.Rows[i].FindControl("tpTextBox")).Text);
                aInvoiceDetail.DiscountPercentage = Convert.ToDecimal(((TextBox)GridView1.Rows[i].FindControl("dpTextBox")).Text);
                aInvoiceDetail.DiscountAmount = Convert.ToDecimal(((TextBox)GridView1.Rows[i].FindControl("dpAmtTextBox")).Text);
                aInvoiceDetail.TotalPriceVatAmount = Convert.ToDecimal(((TextBox)GridView1.Rows[i].FindControl("tpVatTextBox")).Text);
                TextBox npTextBox = (TextBox)GridView1.Rows[i].FindControl("npTextBox");
                aInvoiceDetail.NetAmount = Convert.ToDecimal(npTextBox.Text);
                aInvoiceDetailsList.Add(aInvoiceDetail);
            }
        }

     bool status = aInvoiceDal.SaveDataForInvoiceDetails(aInvoiceDetailsList);

     if (status)
     {
         GridView1.DataSource = null;
         GridView1.DataBind();
         showMessageBox("Updated Successfully");
     }

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveInvoiceDetail();
    }
}