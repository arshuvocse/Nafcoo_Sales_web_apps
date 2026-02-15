using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class PromoMaterialReportDal
    {
        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public void LoadSC(DropDownList ddl)
        {
            string queryStr = "SELECT * FROM dbo.tblCompanyUnit";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }

        public DataTable GetPromoMaterialSummery(string pram)
        {
            string query = @"SELECT UNT.ComUnitId,ComUnitName,Year,Month,P.PromoProductName,SUM(MAS.TransactionQTY) AS Qty FROM tblGroupWisePromoQty AS MAS 
                             LEFT JOIN tblPromoGroup AS PGP ON MAS.PromoGroupId = PGP.PromoGroupId
                             LEFT JOIN tblProductSQ AS B ON MAS.BrandId = B.ProductBrandId
							 LEFT JOIN tblPromoProductName AS P ON MAS.promoId = P.PromoProductId
                             LEFT JOIN tblRouteInformationMarketDetail AS RD ON MAS.TerritoryId = RD.SubTerritoryId
                             LEFT JOIN tblSubTerritory tr with (nolock) ON RD.SubTerritoryId=tr.SubTerritoryId 
                             LEFT JOIN tblRouteInformationMaster AS RM ON RD.RouteInformationMasterId = RM.RouteInformationMasterId
                             LEFT JOIN tblCompanyUnit AS UNT ON RM.DcId = UNT.ComUnitId
                             WHERE mas.GWPromoQtyId IS NOT NULL " + pram + " GROUP BY UNT.ComUnitId,ComUnitName,Year,Month,P.PromoProductName ORDER BY UNT.ComUnitId,P.PromoProductName";

            return aInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public DataTable PromoMaterialcvByFieldForceApp(int empID)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", empID));

            return aInternalDal.GetDataTableAction("sp_Promo_ChallanReportDetailsFieldForce", aSqlParameterlist, "SSIDB");
        }


        public bool ApproveChallanListFS(string masterId, int ForwardBy, bool ApprovalStatus, string Remarks)
        {
            bool Status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                var detailSqlParameterlist = new List<SqlParameter>();

                detailSqlParameterlist.Add(new SqlParameter("@MasterId", (object)masterId ?? DBNull.Value));
                detailSqlParameterlist.Add(new SqlParameter("@ForwardBy", (object)ForwardBy ?? DBNull.Value));

                detailSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", (object)ApprovalStatus ?? DBNull.Value));

                detailSqlParameterlist.Add(new SqlParameter("@Remarks", (object)Remarks ?? DBNull.Value));

                Status = accessManager.UpdateData("sp_Promo_ReceiveChallanFS", detailSqlParameterlist);
            }
            catch (Exception)
            {
                Status = false;
                accessManager.SqlConnectionClose();
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return Status;
        }

        public DataTable GetDetail(string pram)
        {
            string query = @"SELECT mas.GWPromoQtyId,UNT.ComUnitId,ComUnitName,Year,Month,PromoGroupName,B.ProductSQName,P.PromoProductName,
                             tr.SubTerritoryCode + ' : ' + tr.SubTerritoryName TerritoryName,emp.EmpMasterCode+' : '+emp.EmpName MioName,SUM(MAS.TransactionQTY) AS Qty,
                             CASE WHEN PCL.GWPromoQtyId IS NULL THEN 'Yes' ELSE 'No' END isForwardAble FROM tblGroupWisePromoQty AS MAS 
                             LEFT JOIN (SELECT GWPromoQtyId FROM tbl_Promo_ChallanDetails) AS PCL ON MAS.GWPromoQtyId = PCL.GWPromoQtyId
                             LEFT JOIN tblPromoGroup AS PGP ON MAS.PromoGroupId = PGP.PromoGroupId
                             LEFT JOIN tblProductSQ AS B ON MAS.BrandId = B.ProductBrandId
							 LEFT JOIN tblPromoProductName AS P ON MAS.promoId = P.PromoProductId
                             LEFT JOIN tblRouteInformationMarketDetail AS RD ON MAS.TerritoryId = RD.SubTerritoryId
                             LEFT JOIN tblSubTerritory tr with (nolock) ON RD.SubTerritoryId=tr.SubTerritoryId 
                             LEFT JOIN tblRouteInformationMaster AS RM ON RD.RouteInformationMasterId = RM.RouteInformationMasterId
                             LEFT JOIN tblCompanyUnit AS UNT ON RM.DcId = UNT.ComUnitId
                             LEFT JOIN tblMBEInfo mio  with (nolock) ON mas.MIOId=MIO.MBEInfoId
                             LEFT JOIN dbo.tblEmpGeneralInfo emp  with (nolock) ON mio.EmployeeId=emp.EmpInfoId
                             WHERE mas.GWPromoQtyId IS NOT NULL " + pram + " GROUP BY mas.GWPromoQtyId,UNT.ComUnitId,ComUnitName,Year,Month,PromoGroupName,B.ProductSQName,P.PromoProductName,tr.SubTerritoryCode,tr.SubTerritoryName,emp.EmpMasterCode,emp.EmpName,PCL.GWPromoQtyId ORDER BY UNT.ComUnitId,tr.SubTerritoryCode,PromoGroupName";

            return aInternalDal.DataContainerDataTable(query, "SSIDB");
        }



        public Int32 SaveProductionReport(PromoChallanMasterDao aInfo, List<PromoChallanDetailsDao> aDetailDaos)
        {

            int masterId = 0;
            int detailId = 0;

            bool status = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@PromoChallanId", (object)aInfo.PromoChallanId ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@ComUnitId", (object)aInfo.ComUnitId ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@ChallanBy", (object)aInfo.ChallanBy ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@ChallanDate", aInfo.ChallanDate));

                if (aInfo.PromoChallanId == 0)
                {
                    masterId = accessManager.SaveDataReturnPrimaryKey("sp_PromoChallan_MasterInsert", aSqlParameterlist);
                }
                else
                {
                    status = accessManager.UpdateData("sp_ProductionReport_MasterUpdate", aSqlParameterlist);

                    if (status)
                    {
                        masterId = aInfo.PromoChallanId;
                    }
                }

                foreach (var aDaos in aDetailDaos)
                {
                    var detailSqlParameterlist = new List<SqlParameter>();

                    detailSqlParameterlist.Add(new SqlParameter("@PromoChallanId", (object)masterId ?? DBNull.Value));
                    detailSqlParameterlist.Add(new SqlParameter("@GWPromoQtyId", (object)aDaos.GWPromoQtyId ?? DBNull.Value));

                    detailId = accessManager.SaveDataReturnPrimaryKey("sp_PromoChallan_DetailsInsert", detailSqlParameterlist);
                }

            }
            catch (Exception e)
            {
                masterId = 0;
                accessManager.SqlConnectionClose();
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return masterId;
        }

        public DataTable LoadPromoChallanReport(string pram)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Pram", pram));

            return aInternalDal.GetDataTableAction("sp_Promo_ChallanReport", aSqlParameterlist, "SSIDB");
        }

        public DataTable GetChallanDetailList(string pram)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Pram", pram));

            return aInternalDal.GetDataTableAction("sp_Promo_ChallanReportDetails", aSqlParameterlist, "SSIDB");
        }

        public bool ForwardChallanList(string masterId)
        {
            bool Status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                var detailSqlParameterlist = new List<SqlParameter>();

                detailSqlParameterlist.Add(new SqlParameter("@MasterId", (object)masterId ?? DBNull.Value));
                detailSqlParameterlist.Add(new SqlParameter("@ForwardBy", HttpContext.Current.Session["UserId"].ToString()));

                Status = accessManager.UpdateData("sp_Promo_SendChallanToDepot", detailSqlParameterlist);
            }
            catch (Exception)
            {
                Status = false;
                accessManager.SqlConnectionClose();
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return Status;
        }

        public bool ApproveChallanList(string masterId)
        {
            bool Status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                var detailSqlParameterlist = new List<SqlParameter>();

                detailSqlParameterlist.Add(new SqlParameter("@MasterId", (object)masterId ?? DBNull.Value));
                detailSqlParameterlist.Add(new SqlParameter("@ForwardBy", HttpContext.Current.Session["UserId"].ToString()));

                Status = accessManager.UpdateData("sp_Promo_ReceiveChallan", detailSqlParameterlist);
            }
            catch (Exception)
            {
                Status = false;
                accessManager.SqlConnectionClose();
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return Status;
        }

        public DataTable LoadChallanReport(string pram)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Pram", pram));

            return aInternalDal.GetDataTableAction("sp_Promo_GetChallanReportDropDown", aSqlParameterlist, "SSIDB");
        }
    }
}
