using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.DAO.UserRoleDAO;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;

public partial class UserPermission_UserPermission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static MenuDAL aMenuDal = new MenuDAL();
    // GET: MenuRole

    //[WebMethod]
    //public static string SaveMenu(List<MenuRoleDAO> MenuRoles)
    //{

    //    int Count = 0;

    //    int id = 0;
    //    foreach (var menuRoleDao in MenuRoles)
    //    {
    //        if (Count == 0)
    //        {
    //            Count++;
    //            aMenuDal.Delete_Prevoiusmenu(menuRoleDao.RoleId);
    //        }

    //        id = (aMenuDal.SaveMenuRole(menuRoleDao));
    //    }

    //    return "True";
    //}
    [WebMethod]
    public static string SaveMenu(MenuRoleMasterDAO arole, string roleSelectid, string typeSelectId)
    {

        int Count = 0;

        aMenuDal.Delete_PrevoiusmenuAll(roleSelectid, typeSelectId);
        int id = 0;
        foreach (var menuRoleDao in arole.MenuRoles)
        {
            //if (Count == 0)
            //{
            //    Count++;
               //aMenuDal.Delete_Prevoiusmenu(menuRoleDao.RoleId);
            //}
         
            id = (aMenuDal.SaveMenuRole(menuRoleDao));
        }

        return "True";
    }

    [WebMethod]
    public static dynamic GetUserRole()
    {
        var list = aMenuDal.GetUserRoleDropDown();
        return list;
    }
    [WebMethod]
    public static dynamic LoadMenu(string roleId,string typeId)
    {
        var list = aMenuDal.LoadMenu(roleId,typeId);
        return list;
    }

    [WebMethod]
    public static dynamic LoadMenuIsApp(string roleId, string typeId)
    {
        var list = aMenuDal.LoadMenuIsApp(roleId, typeId);
        return list;
    }
}