using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAO.Thana_DAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using DataTable = System.Data.DataTable;

public partial class Thana_UI_DistrictEntry : System.Web.UI.Page
{
    ThanaDal aDal = new ThanaDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadDropdownList();

            if (Request.QueryString["id"] != "")
            {
                hdfDivision.Value = Request.QueryString["id"];
                LoadDistrictById(hdfDivision.Value); 
            }
                        
        }
    }

    private void LoadDistrictById(string districtId)
    {
        DataTable aTable = aDal.GetDivisionById(Convert.ToInt32(districtId));

        if (aTable.Rows.Count > 0)
        {


            //ddlDivision.SelectedValue = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
            tbxDivision.Text = aTable.Rows[0].Field<String>("DivisionName");
            tbxLatitude.Text = aTable.Rows[0].Field<String>("Lat");
            tbxLongitude.Text = aTable.Rows[0].Field<String>("Long");

            //tbxLatitude.Text = aTable.Rows[0]["Latitude"] != DBNull.Value ? aTable.Rows[0].Field<Decimal>("Latitude").ToString(CultureInfo.InvariantCulture) : "";
            //tbxLongitude.Text = aTable.Rows[0]["Longitude"] != DBNull.Value ? aTable.Rows[0].Field<Decimal>("Longitude").ToString(CultureInfo.InvariantCulture) : "";
        }
    }

   

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            DivisionDao aDao = new DivisionDao();

          
            aDao.DivisionName = tbxDivision.Text;
            aDao.Lat = tbxLatitude.Text.Trim();
            aDao.Long = tbxLatitude.Text.Trim();

            if (hdfDivision.Value == "")
            {
                aDao.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.CreateDate = DateTime.Now;
            }
            else
            {
                aDao.DivisionId = Convert.ToInt32(hdfDivision.Value);
                aDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.UpdateDate = DateTime.Now;
            }
            

            ResultInfo Res = aDal.SaveDivision(aDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful !" + "','Success','Division_View.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Operation Failed !" + "','Faild');", true);

            }
        }
    }

    private bool Validation()
    {

        if (tbxDivision.Text.Trim() == "")
        {
            ShowMessageBox("Please select Division Name !!");
            tbxDivision.Focus();
            return false;
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
        throw new NotImplementedException();
    }
}