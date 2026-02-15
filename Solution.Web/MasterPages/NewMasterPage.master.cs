using Library.BLL.SInventory_BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_NewMasterPage : System.Web.UI.MasterPage
{
    UserBLL aUserBll = new UserBLL();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
            {
        if (!IsPostBack)
        {
            

                if (Session["UserId"] != null)
                {
                    //  Menu();
                    UserNameTime();

                    lblName.Text = Session["EmpName"].ToString();
                    lblDgs.Text = Session["DesigName"].ToString();
                    MenuHtml();
                }
                else
                {
                    Session["UserId"] = "";
                    Session["LoginName"] = "";
                    Session["UserType"] = "";
                    Session["ComUnitId"] = "";
                    Response.Redirect("../Login.aspx");
                }

                
            
        }
            }
        catch
        {
            Session["UserId"] = "";
            Session["LoginName"] = "";
            Session["UserType"] = "";
            Session["ComUnitId"] = "";
            Response.Redirect("../Login.aspx");

        }
    }
    public void UserNameTime()
    {
        //Label1.Text = Session["LoginName"].ToString();
        //Label2.Text = Session["UserTime"].ToString();
    }
    public void MenuHtml()
    {
        DataTable aDataTable = new DataTable();
        aDataTable = aUserBll.LoadMenu(Session["UserId"].ToString(), Session["UserRoleID"].ToString());
        menu.InnerHtml = aDataTable.Rows[0][0].ToString();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Session["UserId"] = "1";
        Session["LoginName"] = "admin";
        Session["UserType"] = "Admin";
        Session["ComUnitId"] = 1;
        Session.Abandon();
        Response.Redirect("../Login.aspx");
    }

    protected void logOut_Click(object sender, EventArgs e)
    {
        Session["UserId"] = "";
        Session["LoginName"] = "";
        Session["UserType"] = "";
        Session["ComUnitId"] = "";
        Response.Redirect("../Login.aspx");
    }
}
