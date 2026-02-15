using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAL.MasterSetup_DAL;
using Library.DAL.SInventory_DAL;

public partial class SInventory_UI_BrandWisePromoList : System.Web.UI.Page
{
    BrandWisePromoDal aDal = new BrandWisePromoDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadWorkType();
        }

    }

    private void LoadWorkType()
    {
        DataTable aTable = aDal.GetBrandWisePromoList("");

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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BrandWisePromoSetup.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string CetegoryId = loadGridView.DataKeys[rowindex][0].ToString();

            string url = "BrandWisePromoSetup.aspx?ID=" + CetegoryId;
            Response.Redirect(url);
            // PopUp(custCetegoryId);
        }
    }
}