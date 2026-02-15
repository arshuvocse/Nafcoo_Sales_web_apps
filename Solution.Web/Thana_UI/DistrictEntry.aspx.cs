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
            LoadDropdownList();

            if (Request.QueryString["id"] != "")
            {
                hdfDistrictId.Value = Request.QueryString["id"];
                LoadDistrictById(hdfDistrictId.Value); 
            }
                        
        }
    }

    private void LoadDistrictById(string districtId)
    {
        DataTable aTable = aDal.GetDistrictById(Convert.ToInt32(districtId));

        if (aTable.Rows.Count > 0)
        {


            ddlDivision.SelectedValue = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
            tbxDistrict.Text = aTable.Rows[0].Field<String>("DistrictName");
            tbxLatitude.Text = aTable.Rows[0].Field<String>("Lat");
            tbxLongitude.Text = aTable.Rows[0].Field<String>("Long");

            //tbxLatitude.Text = aTable.Rows[0]["Latitude"] != DBNull.Value ? aTable.Rows[0].Field<Decimal>("Latitude").ToString(CultureInfo.InvariantCulture) : "";
            //tbxLongitude.Text = aTable.Rows[0]["Longitude"] != DBNull.Value ? aTable.Rows[0].Field<Decimal>("Longitude").ToString(CultureInfo.InvariantCulture) : "";
        }
    }

    private void LoadDropdownList()
    {
        aDal.LoadDivision(ddlDivision);
        //ddlDivision
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            DistrictDao aDao = new DistrictDao();

           
            aDao.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
            aDao.DistrictName = tbxDistrict.Text;
            aDao.Lat = tbxLatitude.Text.Trim();
            aDao.Long = tbxLatitude.Text.Trim();

            if (hdfDistrictId.Value == "")
            {
                aDao.EntryBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.EntryDate = DateTime.Now;
            }
            else
            {
                aDao.DistrictId = Convert.ToInt32(hdfDistrictId.Value);
                aDao.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                aDao.UpdateDate = DateTime.Now;
            }
            

            ResultInfo Res = aDal.SaveDistrict(aDao);

            if (Res.isSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + "Operation successful !" + "','Success','District_View.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "faildalert('" + "Operation Failed !" + "','Faild');", true);

            }
        }
    }

    private bool Validation()
    {
        if (ddlDivision.SelectedValue == "")
        {
            ShowMessageBox("Please select Division !!");
            ddlDivision.Focus();
            return false;
        }

        if (tbxDistrict.Text.Trim() == "")
        {
            ShowMessageBox("Please select District name !!");
            tbxDistrict.Focus();
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