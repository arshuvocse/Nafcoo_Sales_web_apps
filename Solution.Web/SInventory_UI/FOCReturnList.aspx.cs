using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_FOCReturnList : System.Web.UI.Page
{
    FOCReturnDal aDal = new FOCReturnDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime currentDate = DateTime.Now;

            tbxFromDate.Text = currentDate.ToString("dd-MMM-yyyy");
            tbxToDate.Text = currentDate.ToString("dd-MMM-yyyy");

            this.LoadInitialGrid(itemsGridView);
        }
    }

    private void LoadInitialGrid(GridView gridView)
    {
        DataTable aTable = new DataTable();

        aTable = aDal.GetFOCReturnList(GenerateParam());

        if (aTable.Rows.Count > 0)
        {
            gridView.DataSource = aTable;
            gridView.DataBind();


            //for (int i = 0; i < gridView.Rows.Count; i++)
            //{
            //    string activeStatus = gridView.DataKeys[i][1].ToString();

            //    if (activeStatus == "Inactive")
            //    {
            //        ImageButton editImageButton = ((ImageButton)gridView.Rows[i].Cells[0].FindControl("editImageButton"));

            //        editImageButton.Visible = false;
            //    }
            //}


        }
        else
        {
            gridView.DataSource = null;
            gridView.DataBind();
        }
    }

    private string GenerateParam()
    {
        string param = "";

        if (tbxFromDate.Text.Trim() != "" && tbxToDate.Text.Trim() != "")
        {
            param = param + " AND CONVERT(date,ReturnDate) BETWEEN '" + tbxFromDate.Text + "' AND '" + tbxToDate.Text  + "'";
        }

        return param;
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        itemsGridView.PageIndex = e.NewPageIndex;
        this.LoadInitialGrid(itemsGridView);
    }
    protected void itemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            Session["MBEId"] = e.CommandArgument.ToString();
            Response.Redirect("MBESetupNew.aspx");
        }

    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        this.LoadInitialGrid(itemsGridView);
    }

    protected void cancelButton_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}