using Library.DAL.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using SalesSolution.Web.Models;
using Library.DAO.DoctorModule_DAO;

namespace Library.DAL.MasterSetup_DAL
{
    public class TourPlanAppsDAL
    {

        private DataAccessManager accessManager = new DataAccessManager();


        public DataTable GetTourTypeListDAL()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_TourTypeActive");
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public DataTable GetSubTerritoryListDAL(string STerritoryId, string RoleType)
        {
            try
            { 
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@STerritoryId", STerritoryId));
                aSqlParameterlist.Add(new SqlParameter("@RoleType", RoleType));
                DataTable dt = accessManager.GetDataTable("sp_GET_SubTerritoryInfoByFFIdRoleType", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public DataTable GetByMonthTourPlanDataWithDataDAL(int empId, int month, int year)
        {
            try
            { 
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@empId", empId));
                aSqlParameterlist.Add(new SqlParameter("@month", month));
                aSqlParameterlist.Add(new SqlParameter("@year", year));
                DataTable dt = accessManager.GetDataTable("sp_GET_ByMonthTourPlanDataWithData", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        //public ResultInfo SaveTourPlanList_vThree(TourPlanInfoMasterDAO aInfo)
        //{
        //    var aInformation = new ResultInfo();

        //    try
        //    {
        //        if (aInfo == null || aInfo.aTourPlanInfo == null || aInfo.aTourPlanInfo.Count == 0)
        //        {
        //            aInformation.isSuccess = false;
        //            aInformation.Msd = "No data to save.";
        //            return aInformation;
        //        }

        //        accessManager.SqlConnectionOpen(DataBase.SalesDB);

        //        // 1) Distinct (EmpId, TourDate) pair অনুযায়ী আগের ডাটা ডিলিট
        //        var seen = new System.Collections.Generic.HashSet<string>();
        //        for (int i = 0; i < aInfo.aTourPlanInfo.Count; i++)
        //        {
        //            var item = aInfo.aTourPlanInfo[i];
        //            if (item == null || !item.TourPlanDate || !item.EmpInfoId.HasValue)
        //                continue;

        //            string key = item.EmpInfoId.Value + "|" + item.TourPlanDate.Value.ToString("yyyy-MM-dd");
        //            if (!seen.Add(key)) continue;

        //            var sqlDel = new List<SqlParameter>();
        //            sqlDel.Add(new SqlParameter("@TourDate", (object)item.TourPlanDate.Value));
        //            sqlDel.Add(new SqlParameter("@empId", (object)item.EmpInfoId.Value));
        //            accessManager.DeleteData("sp_Webapi_Del_TourPlanInfoForEmpDate", sqlDel);
        //        }

        //        // 2) Insert loop (Morning / Evening—দু’টাই আলাদা আলাদা রো)
        //        int saved = 0;
        //        for (int i = 0; i < aInfo.aTourPlanInfo.Count; i++)
        //        {
        //            var item = aInfo.aTourPlanInfo[i];
        //            if (item == null) continue;

        //            // তোমার SP signature অনুযায়ী প্যারাম বসালাম
        //            var p = new List<SqlParameter>();
        //            p.Add(new SqlParameter("@TerritoryId", (object)item.MarketId ?? DBNull.Value));
        //            p.Add(new SqlParameter("@TerritoryIdEnd", (object)item.MarketIdEnd ?? DBNull.Value));
        //            p.Add(new SqlParameter("@marketName", (object)item.marketName ?? DBNull.Value));
        //            p.Add(new SqlParameter("@marketNameEnd", (object)item.marketNameEnd ?? DBNull.Value));
        //            p.Add(new SqlParameter("@PurposeId", (object)item.TPId ?? DBNull.Value));
        //            p.Add(new SqlParameter("@TourDate", (object)item.TourPlanDate ?? DBNull.Value));
        //            p.Add(new SqlParameter("@empId", (object)item.EmpInfoId ?? DBNull.Value));
        //            p.Add(new SqlParameter("@SerialNo", (object)item.SerialNo ?? DBNull.Value));

        //            p.Add(new SqlParameter("@IsMorning", (object)item.IsMorning ?? DBNull.Value));
        //            p.Add(new SqlParameter("@IsEvening", (object)item.IsEvening ?? DBNull.Value));

        //            p.Add(new SqlParameter("@Starttime", (object)item.Starttime ?? DBNull.Value));
        //            p.Add(new SqlParameter("@Endtime", (object)item.Endtime ?? DBNull.Value));

        //            p.Add(new SqlParameter("@IsMarketVisit", (object)item.IsMarketVisit ?? DBNull.Value));
        //            p.Add(new SqlParameter("@IsOtherVisit", (object)item.IsOtherVisit ?? DBNull.Value));
        //            p.Add(new SqlParameter("@Objective", (object)item.Objective ?? DBNull.Value));
        //            p.Add(new SqlParameter("@VisitedWithEmpInfoId", (object)item.VisitedWithEmpInfoId ?? DBNull.Value));

        //            int pk = accessManager.SaveDataReturnPrimaryKey("sp_Webapi_Save_TourPlanInfo_vThree", p);
        //            if (pk > 0) saved++;
        //        }

        //        aInformation.isSuccess = true;
        //        aInformation.Msd = "Saved " + saved + " row(s).";
        //        aInformation.ErrorMessage = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        accessManager.SqlConnectionClose(true);
        //        aInformation.isSuccess = false;
        //        aInformation.ErrorMessage = ex.Message;
        //        aInformation.Msd = "Failed to save.";
        //    }
        //    finally
        //    {
        //        accessManager.SqlConnectionClose();
        //    }

        //    return aInformation;
        //}
        // C# 4 safe helpers (no ?. etc.)
        private static object ToDb(DateTime? v) { return v.HasValue ? (object)v.Value.Date : DBNull.Value; }
        private static object ToDb(int? v) { return v.HasValue ? (object)v.Value : DBNull.Value; }
        private static object ToDb(string s) { return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : (object)s; }

        private static SqlParameter P(string name, SqlDbType type, object value, int size)
        {
            var p = new SqlParameter(name, type);
            p.IsNullable = true;
            if (size != 0) p.Size = size;          // -1 => NVARCHAR(MAX)
            p.Value = (value == null) ? (object)DBNull.Value : value;
            return p;
        }
        private static SqlParameter P(string name, SqlDbType type, object value)
        {
            return P(name, type, value, 0);
        }


        public ResultInfo SaveTourPlanList_vThree(TourPlanInfoMasterDAO aInfo)
        {
            accessManager.SqlConnectionOpen(DataBase.SalesDB);
            var info = new ResultInfo();
            try
            {
                if (aInfo == null || aInfo.aTourPlanInfo == null || aInfo.aTourPlanInfo.Count == 0)
                {
                    info.isSuccess = false;
                    info.Msd = "No data to save.";
                    return info;
                }

          

                // 1) delete-once-per-(Emp,Date)
                var seen = new HashSet<string>(StringComparer.Ordinal);
              

                foreach (var item in aInfo.aTourPlanInfo)
                {
                    if (item == null || !item.TourPlanDate.HasValue || !item.EmpId.HasValue)
                        continue;

                    // Duplicate check key: EmpId + Month-Year
                    string key = item.EmpId.Value + "|" + item.TourPlanDate.Value.ToString("yyyyMM");
                    if (!seen.Add(key)) continue;

                    int month = item.TourPlanDate.Value.Month;
                    int year = item.TourPlanDate.Value.Year;

                    var delParams = new List<SqlParameter>
    {
        new SqlParameter("@month",   month),
        new SqlParameter("@year",    year),
        new SqlParameter("@empId",   item.EmpId.Value) 
    };

                    accessManager.DeleteData("sp_Webapi_Del_TourPlanInfoForEmpDate", delParams);
                }




                // 2) insert ONE ROW per (EmpId, TourDate) with both Morning+Evening fields
                int saved = 0;
                foreach (var item in aInfo.aTourPlanInfo)
                {
                    if (item == null || !item.TourPlanDate.HasValue || !item.EmpId.HasValue) continue;

                    var prm = new List<SqlParameter>
{
    // Required (if nullable, still safe)
    P("@TourDate",  SqlDbType.Date,      ToDb(item.TourPlanDate)),
    P("@empId",     SqlDbType.Int,       ToDb(item.EmpId)),
    P("@SerialNo",  SqlDbType.Int,       ToDb(item.SerialNo)),

    // Morning
    P("@MorTourTypeId",  SqlDbType.Int,       ToDb(item.MorTourTypeId)),
    P("@MorTerritoryId", SqlDbType.Int,       ToDb(item.MorTerritoryId)),
    P("@MorMarketId",    SqlDbType.NVarChar,  ToDb(item.MorMarketId), -1), // NVARCHAR(MAX)

    // Evening
    P("@EveTourTypeId",  SqlDbType.Int,       ToDb(item.EveTourTypeId)),
    P("@EveTerritoryId", SqlDbType.Int,       ToDb(item.EveTerritoryId)),
    P("@EveMarketId",    SqlDbType.NVarChar,  ToDb(item.EveMarketId), -1),

    // Control flags (note: property name casing)
    P("@Type",     SqlDbType.NVarChar,  (aInfo != null ? ToDb(aInfo.Type)    : (object)DBNull.Value), 50),
    P("@Remarks",  SqlDbType.NVarChar,  (aInfo != null ? ToDb(aInfo.remarks) : (object)DBNull.Value), -1),
};


                    // single-row save SP (one insert with both shift columns)
                    int pk = accessManager.SaveDataReturnPrimaryKey("sp_Webapi_Save_TourPlanInfo_BothShifts", prm);
                    if (pk > 0) saved++;
                }
                if (saved > 0)
                {
                    var typeRaw = (aInfo != null && aInfo.Type != null) ? aInfo.Type : string.Empty;
                    // NBSP → normal space, then Trim()
                    var typeNorm = typeRaw.Replace('\u00A0', ' ').Trim();

                    if (string.Equals(typeNorm, "final", StringComparison.OrdinalIgnoreCase))
                    {
                        var seen2 = new HashSet<string>();

                        if (aInfo != null && aInfo.aTourPlanInfo != null)
                        {
                            foreach (var item in aInfo.aTourPlanInfo)
                            {
                                if (item == null || !item.TourPlanDate.HasValue || !item.EmpId.HasValue)
                                    continue;

                                // Duplicate key: EmpId|yyyyMM
                                string key = item.EmpId.Value.ToString() + "|" + item.TourPlanDate.Value.ToString("yyyyMM");
                                if (!seen2.Add(key))
                                    continue;

                                int month = item.TourPlanDate.Value.Month;
                                int year = item.TourPlanDate.Value.Year;

                                var delParams = new List<SqlParameter>();
                                delParams.Add(new SqlParameter("@month", month));
                                delParams.Add(new SqlParameter("@year", year));
                                delParams.Add(new SqlParameter("@empId", item.EmpId.Value));
                                delParams.Add(new SqlParameter("@remarks",
                                    (object)((aInfo != null) ? aInfo.remarks : null) ?? DBNull.Value));

                                accessManager.DeleteData("sp_Webapi_FinalSubmitSend_TourPlan", delParams);
                            }
                        }
                    }

                }



                info.isSuccess = true;
                info.Msd = "Saved " + saved + " row(s).";
                info.ErrorMessage = null;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                info.isSuccess = false;
                info.Msd = "Failed to save.";
                info.ErrorMessage = ex.Message;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return info;
        }

        public DataTable EmpHiarcharcyInfoByIdDAL(string EmpInfoId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
                DataTable dt = accessManager.GetDataTable("sp_GET_EmpHiarcharcyInfoById", aSqlParameterlist);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

    }
}
