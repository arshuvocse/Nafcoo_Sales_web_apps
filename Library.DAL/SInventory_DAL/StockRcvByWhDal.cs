using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class StockRcvByWhDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable LoadMasterInfo(string reqId)
        {
            string query = @"SELECT DISTINCT ChalanNo AS IssueChalanNo,ChalanDate AS IssuChalanDate,DriverName,TrackNo AS TruckNo,ComUnitId FROM tblDepotToWHChalanInfo
                             INNER JOIN dbo.tblCompanyUnit ON tblDepotToWHChalanInfo.FromComUnitCode = ComUnitCode WHERE SChalanId = " + reqId + "";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetChallanDetailByReqId(string reqId)
        {
            string query = @"SELECT PD.ProductId,CLND.SChalanDetailsId,CLND.SChalanId,CLND.DCStoreFreezeId,CLND.DCStoreId,CLND.ProductCode,CLND.ProductName,DS.PackSize,P.Purpose,ISNULL(P.StockConditionId,0) AS StockConditionId,
                             ST.UnitPrice,CAST((ST.VATAmount/ST.Quantity) as decimal(10,2)) AS VatPerUnit,(CLND.Quantity*ST.UnitPrice) AS TotalPrice,
                             (CAST((ST.VATAmount/ST.Quantity) as decimal(10,2))*CLND.Quantity) AS TotalVat,
                             ((CLND.Quantity*ST.UnitPrice) + (CAST((ST.VATAmount/ST.Quantity) as decimal(10,2))*CLND.Quantity)) AS TotalAmount,
                             CLND.BatchNo,CLND.Quantity,DS.ExpDate,DS.ReceiveDate,DS.MfgDate FROM tblDepotToWHChalanDetail AS CLND 
                             INNER JOIN tblDepotToWHChalanInfo AS CLN ON CLN.SChalanId = CLND.SChalanId
                             INNER JOIN dbo.tblProduct AS PD ON PD.ProductCode = CLND.ProductCode
                             INNER JOIN dbo.tblDCStore AS DS ON DS.DCStoreId = CLND.DCStoreId
                             LEFT JOIN tblStockInTransfar AS ST ON ST.StockInTransfarId = DS.StockInTransfarId 
                             LEFT JOIN tblPurpose AS P ON CLND.PurposeId = P.PurposeId WHERE CLN.IsDeliver = 'False' AND CLND.SChalanId = " + reqId + "";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public bool CentralStorStockIn(CentralStoreDao aDcStoreFreezeDao)
        {
            string insertQuery = @"INSERT INTO dbo.tblCentralStore (ProductId,ProductCode,ProductName,PackSize,BatchNo,Quantity,MfgDate,ExpDate,ReceiveDate,ChalanNo,ChalanDate,StockInQty,UnitPrice,TotalPrice,VATPerUnit,TotalVAT,TotalAmount,StockCondition,MigoDetailID,ProductStockType, DCStoreFreezeId,DCStoreId) 
            values (" + aDcStoreFreezeDao.ProductId + ",'" + aDcStoreFreezeDao.ProductCode + "','" + aDcStoreFreezeDao.ProductName + "','"
                      + aDcStoreFreezeDao.PackSize + "','" + aDcStoreFreezeDao.BatchNo + "','" + aDcStoreFreezeDao.Quantity + "','" + aDcStoreFreezeDao.MfgDate + "','"
                      + aDcStoreFreezeDao.ExpDate + "','" + aDcStoreFreezeDao.ReceiveDate + "','" + aDcStoreFreezeDao.ChalanNo + "','" + aDcStoreFreezeDao.ChalanDate + "','" + aDcStoreFreezeDao.StockInQty + "','" + aDcStoreFreezeDao.UnitPrice + "','"
                      + aDcStoreFreezeDao.TotalPrice + "','" + aDcStoreFreezeDao.VATPerUnit + "','" + aDcStoreFreezeDao.TotalVAT + "','" + aDcStoreFreezeDao.TotalAmount + "','" + aDcStoreFreezeDao.StockCondition + "','" + aDcStoreFreezeDao.MigoDetailID + "','" + aDcStoreFreezeDao.ProductStockType + "','" + aDcStoreFreezeDao.DCStoreFreezeId + "'," + aDcStoreFreezeDao.DCStoreId + ")";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool updateChallanStatus(int scId)
        {
            string query = @"UPDATE tblDepotToWHChalanInfo SET IsDeliver='OK' WHERE SChalanId = " + scId;
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
