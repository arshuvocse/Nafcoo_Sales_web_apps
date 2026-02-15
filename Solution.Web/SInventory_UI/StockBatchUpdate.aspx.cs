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

public partial class SInventory_UI_StockConditionFreeze : System.Web.UI.Page
{
    StockConditionFreezeBLL aStockConditionFreezeBll = new StockConditionFreezeBLL();
    StockBatchUpdateDal aDal = new StockBatchUpdateDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDown();
           
        }
    }
    public void LoadDropDown()
    {
        OtherStockActionBLL aOtherStockActionBLL = new OtherStockActionBLL();
        aOtherStockActionBLL.LoadmanufacturerName(manufacturerDropDownList);
        aOtherStockActionBLL.DCLoad(dcDropDownList1, Session["UserId"].ToString());
        manufacturerDropDownList.SelectedIndex = 1;
    }

    protected void dcDropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (dcDropDownList1.SelectedValue != "")
        {
            DataTable aTable = aDal.LoadStockByDcId(Convert.ToInt32(dcDropDownList1.SelectedValue));

            if (aTable.Rows.Count > 0)
            {
                loadGridView.DataSource = aTable;
                loadGridView.DataBind();
            }
            else
            {
                loadGridView.DataSource = null;
                loadGridView.DataBind();
            }
        }
        
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void submitButton0_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {

                var checkBox = (CheckBox) loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

                if (checkBox.Checked)
                {
                    int datakey = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                    var batch = ((TextBox)loadGridView.Rows[i].Cells[3].FindControl("batchTextBox")).Text;
                    var mfgdate = ((TextBox)loadGridView.Rows[i].Cells[4].FindControl("mfgDateTextBox")).Text;
                    var expDate = ((TextBox)loadGridView.Rows[i].Cells[5].FindControl("expDateDateTextBox")).Text;

                    TextBox txtStockQty = ((TextBox)loadGridView.Rows[i].Cells[5].FindControl("txtStockQty"));
                    decimal sQ = 0;
                    try
                    {
                        sQ = Convert.ToDecimal(txtStockQty.Text);
                    }
                    catch
                    {

                    }

                    aDal.UpdateStockBatch(datakey, batch, mfgdate, expDate, Session["LoginName"].ToString(), sQ, loadGridView.DataKeys[i][1].ToString(), Convert.ToInt32(dcDropDownList1.SelectedValue));
                }
            }

            dcDropDownList1_SelectedIndexChanged(null,null);
            ShowMessageBox("Stock Batch Updated Successfully!!!");
        }
    }

    private bool Validation()
    {
        int rowCount = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (checkBox.Checked)
            {
                rowCount = rowCount + 1;
            }

            if (rowCount > 0)
            {
                break;
            }
        }

        if (rowCount > 0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                var checkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
                var batch = (TextBox)loadGridView.Rows[i].Cells[3].FindControl("batchTextBox");
                var mfgdate = (TextBox)loadGridView.Rows[i].Cells[4].FindControl("mfgDateTextBox");
                var expDate = (TextBox)loadGridView.Rows[i].Cells[5].FindControl("expDateDateTextBox");

                if (checkBox.Checked)
                {

                    if (batch.Text == "")
                    {
                        ShowMessageBox("Please Select batch no !!!");
                        return false;
                    }

                    if (mfgdate.Text == "")
                    {
                        ShowMessageBox("Please Select MFG Date !!!");
                        return false;
                    }

                    if (expDate.Text == "")
                    {
                        ShowMessageBox("Please select EXP Date !!!");
                        return false;
                    }
                }
            }
        }
        else
        {
            ShowMessageBox("You must check at least one row!!!");
            return false;
        }


        return true;
    }


    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxHeader.Checked)
            {
                chkBoxRows.Checked = true;
            }
            else
            {
                chkBoxRows.Checked = false;
            }
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockBatchUpdate.aspx");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("SL");
        aDataTable.Columns.Add("DCStoreId");
        aDataTable.Columns.Add("ProductCode");
        aDataTable.Columns.Add("ProductName");
        aDataTable.Columns.Add("BatchNo");
        aDataTable.Columns.Add("MfgDate");
        aDataTable.Columns.Add("ExpDate");
        aDataTable.Columns.Add("StockQty");
        aDataTable.Columns.Add("TotalQuantity");



        DataRow dataRow;

        if (loadGridView.Rows.Count > 0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                

                TextBox tbxBatchNo = (TextBox)loadGridView.Rows[i].Cells[1].FindControl("batchTextBox");
                TextBox tbxmfgDate = (TextBox)loadGridView.Rows[i].Cells[1].FindControl("mfgDateTextBox");
                TextBox tbxExpDate = (TextBox)loadGridView.Rows[i].Cells[1].FindControl("expDateDateTextBox");
                TextBox tbxStockQty = (TextBox)loadGridView.Rows[i].Cells[1].FindControl("txtStockQty");



                if (i == (rowindex + 1))
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["SL"] = Convert.ToString(i + 1);
                    dataRow["DCStoreId"] = 0;
                    dataRow["ProductCode"] = loadGridView.DataKeys[i-1][1];
                    dataRow["ProductName"] = loadGridView.DataKeys[i-1][2];
                    dataRow["BatchNo"] = "";
                    dataRow["MfgDate"] = "";
                    dataRow["ExpDate"] = "";
                    dataRow["StockQty"] = 0;
                    dataRow["TotalQuantity"] = 0;

                    aDataTable.Rows.Add(dataRow);

                   
                }


                dataRow = aDataTable.NewRow();

                dataRow["SL"] = Convert.ToString(i + 1);
                dataRow["DCStoreId"] = loadGridView.DataKeys[i][0];
                dataRow["ProductCode"] = loadGridView.DataKeys[i][1];
                dataRow["ProductName"] = loadGridView.DataKeys[i][2];
                dataRow["TotalQuantity"] = loadGridView.DataKeys[i][3];
                dataRow["BatchNo"] = tbxBatchNo.Text;
                dataRow["MfgDate"] = tbxmfgDate.Text;
                dataRow["ExpDate"] = tbxExpDate.Text;
                dataRow["StockQty"] = tbxStockQty.Text;

                aDataTable.Rows.Add(dataRow);
            }

        }

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
        
    }

}