using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.BLL.Panal_BLL;

public partial class Login : System.Web.UI.Page
{
    DataTable aTableLogin = new DataTable();
    PanalBLL aPanalBll = new PanalBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable aTable = new DataTable();

            //aTable = aPanalBll.GetExpiryInfo();

            msgLabel.Text = "";
            msgLabel.CssClass = "";

            //if (aTable.Rows.Count > 0)
            //{
            //     DateTime nowdate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            //     DateTime expdate = Convert.ToDateTime(aTable.Rows[0]["LicenseExpiryDate"].ToString());

            //     if (aTable.Rows[0]["Remarks"].ToString() != "")
            //     {
            //         ExpireMsg.Text = aTable.Rows[0]["Remarks"].ToString();
            //         ExpireMsg.CssClass = "alert alert-danger";
            //         ExpireMsg.Visible = true;
            //     }

            //     //if (nowdate <= expdate)
            //     //{
            //     //    msgLabel.Text = aTable.Rows[0]["Remarks"].ToString();
            //     //    msgLabel.CssClass = "alert alert-warning";

            //     //}
            //     //else
            //     //{
            //     //    msgLabel.Text = aTable.Rows[0]["RemarksAfterExpire"].ToString();
            //     //    msgLabel.CssClass = "alert alert-danger";
            //     //    msgLabel.Visible = true;
            //     //}
            //}
        }
    }
    protected void loginButton_Click(object sender, EventArgs e)
    {

        DataTable aTable = new DataTable();

        aTable = aPanalBll.GetExpiryInfo();

        string loginName = string.Empty;
        string passwordText = string.Empty;

        if (aTable.Rows.Count > 0)
        {

            DateTime nowdate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            DateTime expdate = Convert.ToDateTime(aTable.Rows[0]["LicenseExpiryDate"].ToString());

            if (nowdate <= expdate)
            {
                if (!string.IsNullOrEmpty(userNameTextBox.Text.Trim()))
                {
                    loginName = userNameTextBox.Text.Trim();
                    if (!string.IsNullOrEmpty(passwordTextBox.Text.Trim()))
                    {
                        passwordText = passwordTextBox.Text.Trim();
                        aTableLogin = aPanalBll.Login(loginName, passwordText);
                        if (aTableLogin.Rows.Count > 0)
                        {

                            Session["UserId"] = aTableLogin.Rows[0]["UserId"].ToString().Trim();
                            Session["UserRoleID"] = aTableLogin.Rows[0]["UserRoleID"].ToString().Trim();
                            Session["RoleTypeId"] = aTableLogin.Rows[0]["RoleTypeId"].ToString().Trim();
                            Session["RoleTypeName"] = aTableLogin.Rows[0]["RoleTypeName"].ToString().Trim();

                            Session["LoginName"] = aTableLogin.Rows[0]["LoginName"].ToString().Trim();
                            Session["UserType"] = aTableLogin.Rows[0]["UserType"].ToString().Trim();
                            Session["CentralWareHouse"] = aTableLogin.Rows[0]["CentralWareHouse"].ToString().Trim();
                            if (aTableLogin.Rows[0]["UserType"].ToString().Trim() != "Admin")
                            {
                                Session["ComUnitId"] = aTableLogin.Rows[0]["CompanyUnitId"].ToString().Trim();
                            }
                            aPanalBll.LoginLog(Session["UserId"].ToString(), Session["LoginName"].ToString(), DateTime.Now);
                            Session["UserTime"] = DateTime.Now.ToString("f");
                            Session["EmpInfoId"] = aTableLogin.Rows[0]["EmpInfoId"].ToString().Trim();
                            Session["EmpName"] = aTableLogin.Rows[0]["EmpName"].ToString().Trim();
                            Session["DesigName"] = aTableLogin.Rows[0]["DesigName"].ToString().Trim();
                            Session["DICCompanyUnitId"] = aTableLogin.Rows[0]["DICCompanyUnitId"].ToString().Trim();

                            bool IsMainDashboard = false;
                            try
                            {

                                IsMainDashboard = Convert.ToBoolean(aTableLogin.Rows[0]["IsMainDashboard"].ToString().Trim());

                            }
                            catch (Exception ex)
                            {

                            }
                            if (IsMainDashboard == true)
                            {

                                Response.Redirect("Dashboard_UI/AdminDashboard.aspx");

                            }
                            else
                            {
                                Response.Redirect("Dashboard_UI/DashboardOne.aspx");

                            }
                        }
                        else
                        {
                            msgLabel.Text = "User name or password incourect !!!!";
                            //msgLabel.CssClass = "alert alert-warning";
                        }
                    }
                    else
                    {
                        msgLabel.Text = "Input Password Please!!!";
                        //msgLabel.CssClass = "alert alert-warning";

                    }
                }
                else
                {
                    msgLabel.Text = "Input Use Name Please!!!";
                    //msgLabel.CssClass = "alert alert-warning";

                }
            }
            else
            {
                msgLabel.Text = aTable.Rows[0]["RemarksAfterExpire"].ToString();
                //msgLabel.CssClass = "alert alert-danger";
            }

            
        }
    }
}