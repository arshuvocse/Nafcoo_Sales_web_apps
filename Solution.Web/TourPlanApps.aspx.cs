using DocumentFormat.OpenXml.Drawing.Charts;
using Library.DAL.MasterSetup_DAL;
using Library.DAO.DoctorModule_DAO;
using Newtonsoft.Json;
using SalesSolution.Web.DataLayer;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;

public partial class TourPlanApps : System.Web.UI.Page
{

    private static TourPlanAppsDAL _TourPlanDAL = new TourPlanAppsDAL();
    protected void Page_Load(object sender, EventArgs e)
    {

    } 
     private static string SafeStr(string s)
    {
        return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    }

    

    // ==== helpers ====
    private static bool IsEmpty(string s) { return string.IsNullOrWhiteSpace(s); }
    private static string nvl(string s) { return IsEmpty(s) ? null : s.Trim(); }

    private static int? ResolveEmpId()
    {
        try
        {
            var ctx = HttpContext.Current;
            if (ctx == null) return null;

            int tmp;

            // 1) QueryString
            var qs = ctx.Request.QueryString["EmpId"];
            if (!string.IsNullOrEmpty(qs) && int.TryParse(qs, out tmp)) return tmp;

            // 2) Session
            if (ctx.Session != null && ctx.Session["EmpInfoId"] != null && int.TryParse(ctx.Session["EmpInfoId"].ToString(), out tmp))
                return tmp;

            // 3) UrlReferrer (front page had ?EmpId=...)
            if (ctx.Request.UrlReferrer != null)
            {
                var nv = System.Web.HttpUtility.ParseQueryString(ctx.Request.UrlReferrer.Query);
                var r = nv["EmpId"];
                if (!string.IsNullOrEmpty(r) && int.TryParse(r, out tmp)) return tmp;
            }
        }
        catch { }
        return null;
    }

    [WebMethod]
    public static string GetTourTypeList()
    {
        DataTable dt = _TourPlanDAL.GetTourTypeListDAL();
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }


    [WebMethod]
    public static string GetSubTerritoryList(string STerritoryId, string RoleType)
    {
        DataTable dt = _TourPlanDAL.GetSubTerritoryListDAL(STerritoryId, RoleType);
        string JSONresult;
        JSONresult = JsonConvert.SerializeObject(dt);
        return (JSONresult);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TourPlanDto> GetByMonthTourPlanDataWithData(int empId, int month, int year)
    {
        DataTable dt = _TourPlanDAL.GetByMonthTourPlanDataWithDataDAL(empId, month, year);

        var data = new List<TourPlanDto>();

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                var dto = new TourPlanDto
                {
                    TourPlanDate = row["TourPlanDate"] != DBNull.Value ? Convert.ToDateTime(row["TourPlanDate"]).ToString("yyyy-MM-dd") : null,
                    Session = row["Session"] != DBNull.Value ? row["Session"].ToString() : null,
                    TourTypeId = row["TourTypeId"] != DBNull.Value ? Convert.ToInt32(row["TourTypeId"]) : 0,
                    SubTerritoryId = row["SubTerritoryId"] != DBNull.Value ? Convert.ToInt32(row["SubTerritoryId"]) : 0,
                    Market = row["Market"] != DBNull.Value ? row["Market"].ToString() : "",
                    ApprovalStatus = row["ApprovalStatus"] != DBNull.Value ? row["ApprovalStatus"].ToString() : "",
                    ApprovalStatusOthers = row["ApprovalStatusOthers"] != DBNull.Value ? row["ApprovalStatusOthers"].ToString() : ""
                };
                data.Add(dto);
            }
        }

        return data;
    }

  

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object SubmitTourPlan(TourPlanInfoMasterDAO aInfo)
    {
        try
        {
            // Server-side EmpId fallback (QS/Session) যদি চান, এখানে বসাতে পারেন
            // foreach (var r in aInfo.aTourPlanInfo) if (!r.EmpInfoId.HasValue) r.EmpInfoId = ResolveEmpId();

              var res = _TourPlanDAL.SaveTourPlanList_vThree(aInfo);

            return new
            {
                success = res.isSuccess,
                message = res.Msd,
                ErrorMessage = res.ErrorMessage,
                isSuccess = res.isSuccess, // backward-compat
                Msd = res.Msd
            };
        }
        catch (Exception ex)
        {
            return new
            {
                success = false,
                message = "Server error: " + ex.Message
            };
        }
    }

     

    [WebMethod]
    public static object GetEmployeeHierarchy(string empId)
    {
        try
        {
            var employeeData = _TourPlanDAL.EmpHiarcharcyInfoByIdDAL(empId);

            if (employeeData != null && employeeData.Rows.Count > 0)
            {
                var result = employeeData.Rows[0];

                // Convert DataRow to a Dictionary to ensure JSON serialization
                var employee = new Dictionary<string, object>();
                foreach (DataColumn column in employeeData.Columns)
                {
                    employee[column.ColumnName] = result[column];
                }
                
                return new { success = true, data = employee };
            }
            else
            {
                return new { success = false, message = "No employee data found." };
            }
        }
        catch (Exception ex)
        {
            // Log the error (you can log the error details to a file or database)
            return new { success = false, message = "Error occurred while fetching employee data." };
        }
    }




}