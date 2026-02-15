using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared.Json;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_FOCReturn : System.Web.UI.Page
{
    FOCReturnDal aDal = new FOCReturnDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdownlist();
        }
    }

    private void LoadDropdownlist()
    {
        aDal.LoadFOC(ddlFOC);
    }


    protected void ddlFOC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFOC.SelectedValue != "")
        {
            string masterId = ddlFOC.SelectedValue;

            DataTable aTable = aDal.LoadFOCById(masterId);

            if (aTable.Rows.Count > 0)
            {
                loadGridView.DataSource = aTable;
                loadGridView.DataBind();
            }
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            CheckBox checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            TextBox returnQtyTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("returnQtyTextBox");
            TextBox remarksTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("remarksTextBox");

            if (ChkBoxHeader.Checked)
            {
                checkBox.Checked = true;
                string returnAbleStock = loadGridView.DataKeys[i][8].ToString();

                returnQtyTextBox.Text = returnAbleStock;

            }
            else
            {
                checkBox.Checked = false;

                returnQtyTextBox.Text = "";
                remarksTextBox.Text = "";

            }
        }

        //CalculateGrandTotal();
    }


    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    { 
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void myListDropDown_Change(object sender, EventArgs e)
    {
       
    }


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            int focId = Convert.ToInt32(ddlFOC.SelectedValue);
            DateTime returnDate = Convert.ToDateTime(tbxReturnDate.Text);

            int masterid = aDal.SaveFOCReturnMaster(userId, returnDate, focId);

            if (masterid > 0)
            {

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    CheckBox checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
                    TextBox returnQtyTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("returnQtyTextBox");
                    TextBox remarksTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("remarksTextBox");
                    Int32 DcStockOutDetailsId = Convert.ToInt32(loadGridView.DataKeys[i][1]);

                    if (checkBox.Checked)
                    {

                        aDal.SaveFOCReturnDetails(masterid, DcStockOutDetailsId, Convert.ToDecimal(returnQtyTextBox.Text), remarksTextBox.Text);
                        
                    }

                }


                // Approve data

                bool ststus = aDal.ApproveReturn(masterid);

                if (ststus)
                {
                    ddlFOC.SelectedValue = "";
                    ddlFOC_SelectedIndexChanged(null, null);

                    ShowMessageBox("Operation Successful !!");
                }
                else
                {
                    ShowMessageBox("Operation Failed !!");
                }

                

            }
        }
    }


    private bool Validation()
    {

        if (ddlFOC.SelectedValue == "")
        {
            ddlFOC.Focus();
            ShowMessageBox("Please enter return FOC !!");
            return false;
        }

        if (tbxReturnDate.Text == "")
        {
            tbxReturnDate.Focus();
            ShowMessageBox("Please enter return Return Date !!");
            return false;
        }

        if (loadGridView.Rows.Count > 0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {

                CheckBox checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

                if (checkBox.Checked)
                {
                    if (((TextBox)loadGridView.Rows[i].FindControl("returnQtyTextBox")).Text.Trim() == "")
                    {
                        ((TextBox)loadGridView.Rows[i].FindControl("returnQtyTextBox")).Focus();
                        ShowMessageBox("Please enter return quantity !!");
                        return false;
                    }
                }

                

            }
        }

        return true;
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        ddlFOC.SelectedValue = "";
        ddlFOC_SelectedIndexChanged(null, null);
    }

    protected void buttonListPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("FOCReturnList.aspx");
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox button = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int i = 0;
        i = currentRow.RowIndex;


        CheckBox checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
        TextBox returnQtyTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("returnQtyTextBox");


        if (checkBox.Checked)
        {
            decimal returnQty = string.IsNullOrEmpty(returnQtyTextBox.Text) ? 0 : Convert.ToDecimal(returnQtyTextBox.Text);
            decimal returnAbleStock = loadGridView.DataKeys[i][8] == null || loadGridView.DataKeys[i][8] == DBNull.Value
                                      ? 0
                                      : Convert.ToDecimal(loadGridView.DataKeys[i][8]);

            if (returnQty > returnAbleStock)
            {
                returnQtyTextBox.Text = returnAbleStock.ToString();
            }
            else if (returnQty == 0)
            {
                returnQtyTextBox.Text = returnAbleStock.ToString();
            }


        }
        else
        {
            returnQtyTextBox.Text = "";
        }


    }

    protected void returnQtyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox button = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int i = 0;
        i = currentRow.RowIndex;


        CheckBox checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
        TextBox returnQtyTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("returnQtyTextBox");


        if (checkBox.Checked)
        {
            // Assign 0 if null or empty

            decimal returnQty = string.IsNullOrEmpty(returnQtyTextBox.Text) ? 0 : Convert.ToDecimal(returnQtyTextBox.Text);
            decimal returnAbleStock = loadGridView.DataKeys[i][8] == null || loadGridView.DataKeys[i][8] == DBNull.Value
                                      ? 0
                                      : Convert.ToDecimal(loadGridView.DataKeys[i][8]);

            // Now you can safely use returnQty and returnAbleStock
   

            if (returnQty > returnAbleStock)
            {
                returnQtyTextBox.Text = returnAbleStock.ToString();
            }
            else if (returnQty == 0)
            {
                returnQtyTextBox.Text = returnAbleStock.ToString();
            }

        }
        else
        {
            returnQtyTextBox.Text = "";
        }
    }
}