using Library.DAL.MasterSetup_DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_EmployeeRecords : System.Web.UI.Page
{

    private static EmployeeInformationDaL _EmployeeInformationDaL = new EmployeeInformationDaL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetEmployeeInformationList()
    {
        DataTable dt = _EmployeeInformationDaL.GetEmployeeInformationList();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return  (JSONresult);
    }
}