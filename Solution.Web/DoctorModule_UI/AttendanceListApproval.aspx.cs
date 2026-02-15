using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoctorModule_UI_AttendanceListApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    static AttendanceDAL _AttendanceDAL = new AttendanceDAL();


    [WebMethod]
    public static string Get_AttendanceList_Approval()
    {
        DataTable dt = _AttendanceDAL.Get_AttendanceList_Approval();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return  (JSONresult);
    }


    [WebMethod]
    public static ResultInfo Approve_AttendanceList(string MyArry, bool? rbValue)
    {

        return  (_AttendanceDAL.ApprovalAttendanceListInfo(MyArry, rbValue, HttpContext.Current.Session["UserId"].ToString()));



    }
}