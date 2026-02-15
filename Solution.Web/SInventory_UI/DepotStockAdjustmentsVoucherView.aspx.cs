using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.SInventory_BLL;
using Library.DAO.SInventory_Entities;

public partial class SInventory_UI_DirectStockOutView : System.Web.UI.Page
{
    DeStockOutBLL aStockOutBll = new DeStockOutBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InvoiceDateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            todateTextBox.Text = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadDropDown();
            DcStockOutgrid(Parm());
        }
   
    }


    

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void gotoinvoiceButton_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        DropDownList statusDropDownList = ((DropDownList)loadGridView.Rows[rowindex].FindControl("statusDropDownList"));
       
        HiddenField hfDcStockOutMasterId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfDcStockOutMasterId"));
        HiddenField hfDcStoreId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfDcStoreId"));


        if (statusDropDownList.SelectedValue != "0") {
            masId.Value = hfDcStockOutMasterId.Value;
            masSta.Value = statusDropDownList.SelectedValue;
          
            if (statusDropDownList.SelectedValue == "Partial")
        {
            DataTable dt = aStockOutBll.getRecordEditMode(hfDcStockOutMasterId.Value);
            DerectStoctOutGridView.DataSource = dt;
            DerectStoctOutGridView.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#MDModal').modal('show')", true);

        }

        if (statusDropDownList.SelectedValue == "Full" || statusDropDownList.SelectedValue == "Reject")
        {
            //DcStockOutMasterDao aMasterDao = new DcStockOutMasterDao();
            //aMasterDao.DcStockOutMasterId = Convert.ToInt32(hfDcStockOutMasterId.Value);
            //aMasterDao.Status = statusDropDownList.SelectedValue;
            //aMasterDao.ApprovedBy = Session["LoginName"].ToString();
            //aMasterDao.ApprovedDate = DateTime.Today;
            //aStockOutBll.UpdateStockOutMasterDataForApprovalBll(aMasterDao);

                aStockOutBll.UpdateDcStockOutDetailsDelete(hfDcStockOutMasterId.Value, statusDropDownList.SelectedValue);

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation Successsfully Done!" + "','Success');", true);
                DcStockOutgrid(Parm());

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Please select Status!" + "','Faild');", true);
        }

    }


    protected void dQtyTextBox_TextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DerectStoctOutGridView.Rows.Count; i++)
        {
           

                Label lbl_StockQty = (Label)DerectStoctOutGridView.Rows[i].FindControl("lbl_StockQty");

                TextBox transferQtyTextBox = (TextBox)DerectStoctOutGridView.Rows[i].FindControl("transferQtyTextBox");
                int rowindex = i;
                decimal mainqty = 0;
                decimal delqty = 0;
                delqty =
                    string.IsNullOrEmpty(
                        (transferQtyTextBox.Text))
                        ? 0
                        : Convert.ToDecimal(
                            (transferQtyTextBox.Text));

                mainqty = string.IsNullOrEmpty(lbl_StockQty.Text)
                    ? 0
                    : Convert.ToDecimal(lbl_StockQty.Text);
                if (delqty <= mainqty)
                {

                }
                else
                {
                    showMessageBox("Stock Out Qty. cantbe more then Stock Quantity");
                    ((TextBox)DerectStoctOutGridView.Rows[rowindex].Cells[3].FindControl("transferQtyTextBox")).Text =
                        string.Empty;
                }
             

        }



    }


    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepotStockAdjustmentsVoucherView.aspx");
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        DcStockOutgrid(Parm());

    }


    private string Parm()
    {

        string param = "";

        param = param + " AND (LTRIM(RTRIM(UPPER(tblDeStockOutMaster.Status))) IN ('APPROVED', 'REJECT') OR (LTRIM(RTRIM(UPPER(tblDeStockOutMaster.EntryBy))) = LTRIM(RTRIM(UPPER('" + Session["LoginName"].ToString() + "'))) AND LTRIM(RTRIM(UPPER(tblDeStockOutMaster.Status))) = 'POSTED'))";

        if (dcDropDownList1.SelectedValue != "")
        {
            param = param + " AND tblDeStockOutMaster.ComUnitId='" + dcDropDownList1.SelectedValue + "' ";
        }

        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text != "")
        {
            param = param + " AND CONVERT(date,tblDeStockOutMaster.EntryDate)  BETWEEN '" + InvoiceDateTextBox.Text + "' AND '" + todateTextBox.Text + "' ";
        }
        if (InvoiceDateTextBox.Text != "" && todateTextBox.Text == "")
        {
            param = param + " AND CONVERT(date,tblDeStockOutMaster.EntryDate)  BETWEEN '" + InvoiceDateTextBox.Text + "' AND '" + DateTime.Now + "' ";
        }
 
        return param;
    }
    public void LoadDropDown()
    {
        try
        {
            OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
            aOtherStockActionBLL.DCLoad(dcDropDownList1);
        }
        catch { }
    }
    private void DcStockOutgrid(string prm)
    {
        DataTable dt = aStockOutBll.DcStockOutBll(prm);

        loadGridView.DataSource = dt;
        loadGridView.DataBind();

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            DropDownList statusDropDownList = ((DropDownList)loadGridView.Rows[i].FindControl("statusDropDownList"));
            
            string status = loadGridView.DataKeys[i][1].ToString();
            string EntryBy = loadGridView.DataKeys[i][3].ToString();

            HiddenField hfDepotStatus = ((HiddenField)loadGridView.Rows[i].Cells[1].FindControl("hfDepotStatus"));
            LinkButton gotoinvoiceButton = ((LinkButton)loadGridView.Rows[i].Cells[1].FindControl("gotoinvoiceButton"));
            ImageButton reportImageButton = ((ImageButton)loadGridView.Rows[i].Cells[1].FindControl("reportImageButton"));


            // File Preview

            if (status == "Approved" )
            {
                reportImageButton.Visible = true;
            }
            else
            {

                if (EntryBy.Trim() == (Session["LoginName"].ToString()).Trim() && (status == "Reject" || status == "Posted"))
                {
                    reportImageButton.Visible = true;
                }
                else
                {
                    reportImageButton.Visible = false;
                }
                
            }

            if (status != "Approved")
            {
                statusDropDownList.Enabled = false;
                gotoinvoiceButton.Visible = false;

               
            }
            else
            {
                
                
                if (hfDepotStatus.Value != "")
                {
                    statusDropDownList.SelectedValue = hfDepotStatus.Value;
                    statusDropDownList.Enabled = false;
                    gotoinvoiceButton.Visible = false;
                }
            }

        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string DcStockOutMasterId = loadGridView.DataKeys[rowindex][0].ToString();
            string status = loadGridView.DataKeys[rowindex][1].ToString();
            Session["Status"] = status;

            if (status == "Approved")
            {
                showMessageBox("Can not Delete Data!!!....");
            }
            else
            {
                if (aStockOutBll.DcStockOutMasterDelete(DcStockOutMasterId))
                {
                    aStockOutBll.DcStockOutDetailsDelete(DcStockOutMasterId);
                    showMessageBox("Data Delete successfully");
                    DcStockOutgrid(Parm());

                }
            }
            
        }

        if (e.CommandName == "ReportView")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            String Id = loadGridView.DataKeys[rowindex][0].ToString();
            Session["DcStockOutMasterId"] = Id;
     

                string url = "../SInventory_RPTVIEW/DcStockOutReportViewer.aspx?DcStockOutMasterId=" + Id;
                string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
           

        }
       
    }

    private void PopUp(string Id)
    {
        string url = "CustomerMasterEdit.aspx?ID=" + Id;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=700,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void CustMasterNewImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CustMasterEntry.aspx");
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
    protected void DcStockOutAddImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DepotStockAdjustmentsVoucher.aspx");
    }

    protected void rptImageButton_Click(object sender, ImageClickEventArgs e)
    {
        string url = "../SInventory_RPTVIEW/CustomerMasterViewer.aspx";
        // string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        string fullURL = "var Mleft = (screen.width/2)-(950/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=950,status=yes,toolbar=no,addressbar=no, scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DcStockOutgrid(Parm());
    }
    public bool ValidationQty()
    {

        for (int i = 0; i < DerectStoctOutGridView.Rows.Count; i++)
        {

            HiddenField hfDcStockOutDetailsId = ((HiddenField)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("hfDcStockOutDetailsId"));
            TextBox transferQtyTextBox = ((TextBox)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("transferQtyTextBox"));


            if (transferQtyTextBox.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Qty Can not be Empty!" + "','Faild');", true);

                return false;
            }
        }
        return true;
    }
        protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ValidationQty())
        {



            try
            {
                for (int i = 0; i < DerectStoctOutGridView.Rows.Count; i++)
                {

                    HiddenField hfDcStockOutDetailsId = ((HiddenField)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("hfDcStockOutDetailsId"));

                    HiddenField hfDcStoreId = ((HiddenField)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("hfDcStoreId"));
                    TextBox transferQtyTextBox = ((TextBox)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("transferQtyTextBox"));
                    Label lbl_StockQty = ((Label)DerectStoctOutGridView.Rows[i].Cells[1].FindControl("lbl_StockQty"));



                    int qttty = Convert.ToInt32(transferQtyTextBox.Text);
                    int  StockQty = Convert.ToInt32(lbl_StockQty.Text);
                    aStockOutBll.DcStockOutMasterPartialDal(hfDcStockOutDetailsId.Value, qttty);


                    aStockOutBll.UpdateDCStoreQuantity(hfDcStoreId.Value, StockQty - qttty);


                   

                    //}
                    //catch
                    //{
                    //    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Operation Faild!" + "','Faild');", true);

                    //}



                }
                
                aStockOutBll.UpdateDcStockOutDetailsDelete(masId.Value, masSta.Value);

                //DcStockOutMasterDao aMasterDao = new DcStockOutMasterDao();
                //aMasterDao.DcStockOutMasterId = Convert.ToInt32(masId.Value);
                //aMasterDao.Status = "Partial";
                //aMasterDao.ApprovedBy = Session["LoginName"].ToString();
                //aMasterDao.ApprovedDate = DateTime.Today;
                //aStockOutBll.UpdateStockOutMasterDataForApprovalBll(aMasterDao);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#MDModal').modal('hide')", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowSuccesalert('" + "Operation Successsfully Done!" + "','Success');", true);

                DcStockOutgrid(Parm());
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Operation Faild!" + "','Faild');", true);

            }
        }
    }
}