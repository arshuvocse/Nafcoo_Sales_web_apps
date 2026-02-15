using Library.DAL.MasterSetup_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_PriceGroupView : System.Web.UI.Page
{

    private PriceGroupDal aGroupDal = new PriceGroupDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadData();

            }
            catch (Exception ex) { }
    
        }

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
    protected void EmpCetegoryAddImageButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceGroupSetup.aspx");
    }


    private void LoadData()
    {
        DataTable aDataTable = aGroupDal.PriceList("");
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string PriceId = loadGridView.DataKeys[rowindex][0].ToString();

            Response.Redirect("PriceGroupSetup.aspx?MID=" + PriceId);
        }

    }

    protected void resetBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceGroupSetup.aspx");
    }
}