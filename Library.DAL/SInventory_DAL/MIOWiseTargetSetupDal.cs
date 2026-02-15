using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAL.MAIN_FUNCTION;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class MIOWiseTargetSetupDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        private DB_Manager aDbManager = new DB_Manager();


        public void CreateConnection_DAL()
        {
            aDbManager.CreateConnection("SalesDisDB_New3");
        }

        public void CloseAllConnection_DAL()
        {
            aDbManager.CloseConnection();
        }

        public int SaveFullInvoice(string InvoiceNo, string updateby, string updatedate)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@InvoiceNo", InvoiceNo));
            aSqlParameterList.Add(new SqlParameter("@UpdateBy", updateby));
            aSqlParameterList.Add(new SqlParameter("@UpdateDate", updatedate));
            return aCommonInternalDal.RunStoreProcedure("sp_DeliveryConformationFull", aSqlParameterList, "SSIDB");
        }


        public DataTable LoadMIOInfo()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Get_MoneyReceiptReportList", aSqlParameters);
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

        public DataTable ValidateCategoryList(string categoryName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@CategoryName", categoryName));

                DataTable dt = accessManager.GetDataTable("sp_Target_CheckCategory", aSqlParameters);
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

        public int SaveMIOTarget(MIOWiseTargetSetupDao aDao)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@AreaCode", aDao.AreaCode));
            aSqlParameterList.Add(new SqlParameter("@TerritoryName", aDao.TerritoryName));
            aSqlParameterList.Add(new SqlParameter("@MioName", aDao.MioName));
            aSqlParameterList.Add(new SqlParameter("@TargetCategory", aDao.TargetCategory));
            aSqlParameterList.Add(new SqlParameter("@MioTargetId", aDao.MioTargetId));


            return aCommonInternalDal.RunStoreProcedure("sp_Target_MIOWiseTargetInsert", aSqlParameterList, "SolutionConnectionStringSSIDB");
        }



        public ResultInfo SaveTopSheet(MIOWiseTargetSetupMasterDao aMasterDao, List<MIOWiseTargetSetupDao> aList)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@monthName", aMasterDao.Month));

                if (aMasterDao.MioTargetMasterId > 0)
                {

                    //masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    //masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_Update_TopSheetMaster", masterParameters);
                    pk = aMasterDao.MioTargetMasterId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Target_MIOWiseTargetMasterInsert", masterParameters);

                }


                if (pk > 0)
                {
                    List<SqlParameter> deleteId = new List<SqlParameter>();

                    deleteId.Add(new SqlParameter("@MioTargetId", pk));

                    if (accessManager.DeleteData("sp_Target_MIOWiseTargetDetailsDelete", deleteId))
                    {
                        foreach (var aDao in aList)
                        {
                            List<SqlParameter> aSQL = new List<SqlParameter>();

                            aSQL.Add(new SqlParameter("@AreaCode", aDao.AreaCode));
                            aSQL.Add(new SqlParameter("@TerritoryName", aDao.TerritoryName));
                            aSQL.Add(new SqlParameter("@MioName", aDao.MioName));
                            aSQL.Add(new SqlParameter("@TargetCategory", aDao.TargetCategory));
                            aSQL.Add(new SqlParameter("@MioTargetId", pk));

                            aInformation.isSuccess = accessManager.SaveData("sp_Target_MIOWiseTargetInsert", aSQL);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }

        public int SaveMioTargetMaster(string monthName, int entryBy, DateTime entryDate)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@monthName", monthName));
            aSqlParameterList.Add(new SqlParameter("@EntryBy", entryBy));
            aSqlParameterList.Add(new SqlParameter("@EntryDate", entryDate));

            return aCommonInternalDal.RunStoreProcedure("sp_Target_MIOWiseTargetMasterInsert", aSqlParameterList, "SolutionConnectionStringSSIDB");
        }

        public DataTable GetMIOTarget(string param)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Pram", param));

                DataTable dt = accessManager.GetDataTable("sp_Target_GetMIOWiseTarget", aSqlParameters);
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

        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        public void LoadTargetCategory(DropDownList ddl)
        {
            string queryStr = @"SELECT TargetId,TargetCategory FROM tbl_Target_CategoryMaster";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "TargetCategory", "TargetId", queryStr);
        }


        public DataTable GetTargetData(string parm)
        {
            try
            {
                string query = @"SELECT DISTINCT YEAR,Month,RegionCode AS CLS,AreaCode AS Region,
TTR.TerritoryCode AS Team,SubTerritoryCode AS Territory,PD.ProductCode,PD.ProductName,PD.PackSize,TpPerPack,TargetQty,TargetValueByTp,VatPerPack,TargetValueByTpVat FROM tbl_Target_MIOWiseTargetSetup AS MT WITH (NOLOCK)
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TSM WITH (NOLOCK) ON MT.MioTargetMasterId = TSM.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryDetails AS TD WITH (NOLOCK) ON MT.TargetCategoryId = TD.TargetId
LEFT JOIN tblSubTerritory AS STTR WITH (NOLOCK) ON MT.TerritoryCode = STTR.SubTerritoryCode 
LEFT JOIN tblTerritory AS TTR WITH (NOLOCK) ON STTR.TerritoryId = TTR.TerritoryId
LEFT JOIN tblArea AS ARA WITH (NOLOCK) ON TTR.AreaId = ARA.AreaId
LEFT JOIN tblRegion AS RGN WITH (NOLOCK) ON ARA.RegionId = RGN.RegionId
LEFT JOIN tbl_Group AS GRP WITH (NOLOCK) ON RGN.GroupId = GRp.GroupId
LEFT JOIN tblProduct AS PD  WITH (NOLOCK) ON TD.ProductCode = PD.ProductCode
WHERE PD.ProductCode IS NOT NULL " + parm + " ORDER BY RegionCode,AreaCode,TTR.TerritoryCode,PD.ProductCode";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            }
            catch (Exception e)
            {
                throw;
            }
//            try
//            {
//                string query = @"SELECT * FROM (SELECT YEAR(M.InvoiceDate) YearName,DATENAME(month,M.InvoiceDate) AS MonthName,RegionCode_Ord AS CLS,AreaCode_Ord AS Region,
//		TerritoryCode_Ord AS Team,SubTerritoryCode_Ord AS Territory,D.ProductCode,ProductName,PackSize,
//		TargetQty,SUM(DeliveryQuantity) AS SalesQuantity,UnitPrice,TRG.TargetValueByTp,UnitVatAmount,TRG.TargetValueByTpVat,
//		SubTerritoryId,TerritoryId,AreaId,RegionId,GroupId FROM tblInvoiceDetail AS D WITH(NOLOCK)
//		LEFT JOIN tblInvoice AS M WITH(NOLOCK) ON D.InvoiceId = M.InvoiceId
//		LEFT JOIN tblOrder AS ODR WITH(NOLOCK) ON M.OrderId = ODR.OrderId
//		LEFT JOIN (SELECT Year,CAST(Month AS NVARCHAR) Month,TerritoryCode,ProductCode,TargetQty,TargetValueByTp,TargetValueByTpVat FROM tbl_Target_MIOWiseTargetSetup AS MT
//		LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TSM ON MT.MioTargetMasterId = TSM.MioTargetMasterId
//		LEFT JOIN tbl_Target_CategoryDetails AS TD ON MT.TargetCategoryId = TD.TargetId WHERE ProductCode IS NOT NULL) AS TRG 
//		ON D.ProductCode = TRG.ProductCode AND ODR.SubTerritoryCode_Ord = TRG.TerritoryCode AND YEAR(M.InvoiceDate) = TRG.Year 
//		AND DATENAME(month,M.InvoiceDate) = TRG.Month 
//		GROUP BY YEAR(M.InvoiceDate),DATENAME(month,M.InvoiceDate),RegionCode_Ord,AreaCode_Ord,TerritoryCode_Ord,SubTerritoryCode_Ord,D.ProductCode,ProductName,PackSize,
//		TargetQty,UnitPrice,TRG.TargetValueByTp,UnitVatAmount,TRG.TargetValueByTpVat,SubTerritoryId,TerritoryId,AreaId,RegionId,GroupId ) AS TRGT WHERE ProductCode IS NOT NULL " + parm + " ORDER BY TRGT.CLS,TRGT.Region,TRGT.Team,TRGT.Territory,TRGT.ProductCode";

//                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

//            }
//            catch (Exception e)
//            {
//                throw;
//            }
        }


        //public DataTable GetTargetData(string param)
        //{
        //    try
        //    {
        //        accessManager.SqlConnectionOpen(DataBase.SalesDB);

        //        List<SqlParameter> aSqlParameters = new List<SqlParameter>();

        //        aSqlParameters.Add(new SqlParameter("@Pram", param));

        //        DataTable dt = accessManager.GetDataTable("sp_Target_GetReport", aSqlParameters);
        //        return dt;

        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        accessManager.SqlConnectionClose();
        //    }
        //}
    }
}
