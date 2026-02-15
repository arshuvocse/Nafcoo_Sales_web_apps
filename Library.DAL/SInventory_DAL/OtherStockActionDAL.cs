using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public  class OtherStockActionDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable LoadData(int unitID, int ManuID)
        {
            string query = @"SELECT F.DCStoreFreezeId,F.DCStoreId,F.ProductCode,F.ProductName,F.BatchNo,F.ExpDate,F.StockQty,
                           (U.UnitPrice*F.StockQty) AS Amount,F.StockCondition,F.Remarks
                           FROM dbo.tblDCStoreFreeze F
                           INNER JOIN dbo.tblUnitPrice U ON F.ProductCode = U.ProductCode
                           INNER JOIN dbo.tblProduct P ON F.ProductCode = P.ProductCode
                           WHERE StockQty>0 AND F.ComUnitId ='" + unitID + "' AND P.ManufacId = '" + ManuID + "'ORDER BY F.ExpDate ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public void LoadmanufacturerName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblManufacturer";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }

        public void LoadTerritory(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblArea";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "AreaCode", "AreaId", queryStr);
        }

        public DataTable LoadStockData(int DCStoreFreezeId)
        {
            string query = @"SELECT StockQty FROM dbo.tblDCStoreFreeze where DCStoreFreezeId='" + DCStoreFreezeId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadStockDCStockData(int DCStoreId)
        {
            string query = @"SELECT StockQty FROM dbo.tblDCStore where DCStoreId='" + DCStoreId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public void LoadSC(DropDownList ddl, string userId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            //            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
            //                                LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
            //                                WHERE UserId='"+userId+"'";


            string queryStr = "select ComUnitId, ComUnitName  from tblCompanyUnit WHERE " +
                               " ComUnitId IN (SELECT CompanyUnitId FROM dbo.tblUserCompanyUnit WHERE UserId='" + userId.Trim() + "')";


            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }
        public void LoadMIO(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            //            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
            //                                LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
            //                                WHERE UserId='"+userId+"'";


            string queryStr = "SELECT MiaCode+':'+MiaName AS MIO,MiaId FROM dbo.tblMIAInfo";


            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MIO", "MiaId", queryStr);
        }

        public void LoadMIOMIOreceivable(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            //            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
            //                                LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
            //                                WHERE UserId='"+userId+"'";


            string queryStr = "SELECT MiaCode+':'+MiaName AS MIO,MiaId,MiaCode FROM dbo.tblMIAInfo";


            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MIO", "MiaCode", queryStr);
        }


        public void DCLoad(DropDownList aDownList, string userId)
        {
            //string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            //aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
                                            LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
                                            WHERE UserId='" + userId + "'";

            aInternalDal.LoadDropDownValueWithoutDataBase(aDownList, "ComUnitName", "ComUnitId", queryStr);
        }
        public void DCLoad(DropDownList aDownList)
        {
            string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
//            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
//            string queryStr = @"SELECT * FROM dbo.tblCompanyUnit
//                                            LEFT JOIN dbo.tblUserCompanyUnit ON dbo.tblCompanyUnit.ComUnitId=dbo.tblUserCompanyUnit.CompanyUnitId
//                                            WHERE UserId='" + userId + "'";

          //  aInternalDal.LoadDropDownValueWithoutDataBase(aDownList, "ComUnitName", "ComUnitId", queryStr);
        }
        public void MarketLoad(DropDownList aDownList)
        {
            string dc = "select * from dbo.tblMarket";
            aCommonInternalDal.LoadDropDownValue(aDownList, "MarketName", "MarketId", dc, "SSIDB");
        }
        public bool UpdateDCStoreFreezeStock(decimal StockQty, int DCStoreFreezeId)
        {
            string query = @"UPDATE tblDCStoreFreeze SET StockQty='" + StockQty + "', LastFreezeRcvDate = '" + DateTime.Now + "' WHERE DCStoreFreezeId=" + DCStoreFreezeId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool UpdateDCStock(decimal StockQty, int DCStoreId)
        {
            string query = @"UPDATE tblDCStore SET StockQty='" + StockQty + "' WHERE DCStoreId=" + DCStoreId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        //Sub Deport

        public DataTable SubDeportLoadData(int unitID, int ManuID)
        {
            string query = @"SELECT F.SDStoreFreezeId,F.SubDCStoreId,F.ProductCode,F.ProductName,F.BatchNo,F.ExpDate,F.StockQty,
                           (U.UnitPrice*F.StockQty) AS Amount,F.StockCondition,F.Remarks
                           FROM dbo.tblSubDepotStoreFreeze F
                           INNER JOIN dbo.tblUnitPrice U ON F.ProductCode = U.ProductCode
                           INNER JOIN dbo.tblProduct P ON F.ProductCode = P.ProductCode
                           WHERE StockQty>0 AND F.SubDepotId ='" + unitID + "'  AND P.ManufacId = '1'ORDER BY F.ExpDate ";


                         //  WHERE StockQty>0 AND F.ComUnitId ='" + unitID + "' AND P.ManufacId = '" + ManuID + "'ORDER BY F.ExpDate ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable LoadSubdeportStockData(int DCStoreFreezeId)
        {
            string query = @"SELECT StockQty FROM dbo.tblSubDepotStoreFreeze where SDStoreFreezeId='" + DCStoreFreezeId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool SubdeportUpdateDCStoreFreezeStock(decimal StockQty, int DCStoreFreezeId)
        {
            string query = @"UPDATE tblSubDepotStoreFreeze SET StockQty='" + StockQty + "' WHERE SDStoreFreezeId=" + DCStoreFreezeId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public DataTable SubdeportLoadStockDCStockData(int DCStoreId)
        {
            string query = @"SELECT StockQty FROM dbo.tblSubDepotStore where SubDCStoreId='" + DCStoreId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool SubdeportUpdateDCStock(decimal StockQty, int DCStoreId)
        {
            string query = @"UPDATE tblSubDepotStore SET StockQty='" + StockQty + "' WHERE SubDCStoreId=" + DCStoreId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public DataTable CheckDuplicateEntry(int dCStoreFreezeId)
        {
            string query = @"SELECT COUNT(*) Total FROM tblDCStoreFreeze WHERE DCStoreFreezeId = '" + dCStoreFreezeId + "' AND CONVERT(DATE,LastFreezeRcvDate) = CONVERT(DATE,GETDATE()) AND LastFreezeRcvDate BETWEEN DATEADD(MINUTE,-1,GETDATE()) AND DATEADD(SECOND,30,GETDATE())";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
