using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class ChalanDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveDataForChalanInfo(ChalanInfo aChalanInfo)
        {
            string insertQuery = @"insert into tblChalanInfo (ChalanId,ChalanDate,ChalanNo,TrackNo,DriverName,FromComUnitCode,FromComUnitName,FromComUnitAddress,ToComUnitCode,ToComUnitName,ToComUnitAddress,TotalValue,TotalVat,GrandTotal,ForwardBy,ForwardDate,ManufacId,IsDeliver,FromComUnitId) 
            values (" + aChalanInfo.ChalanId + ",'" + aChalanInfo.ChalanDate + "','" + aChalanInfo.ChalanNo + "','" + aChalanInfo.TrackNo + "','" + aChalanInfo.DriverName + "','" + aChalanInfo.FromComUnitCode + "','" + aChalanInfo.FromComUnitName + "','" + aChalanInfo.FromComUnitAddress + "','" + aChalanInfo.ToComUnitCode + "','" + aChalanInfo.ToComUnitName + "','" + aChalanInfo.ToComUnitAddress + "','" + aChalanInfo.TotalValue + "','" + aChalanInfo.TotalVat + "','" + aChalanInfo.GrandTotal + "','" + aChalanInfo.ForwardBy + "','" + aChalanInfo.ForwardDate + "','" + aChalanInfo.ManufacId + "','" + false + "','" + aChalanInfo.fromunitid + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool SaveDataForChalanDetail(ChalanDetail aChalanDetail)
        {
            string insertQuery = @"insert into tblChalanDetail (ChalanDetailsId,ProductCode,ProductName,Quantity,BatchNo,UnitPrice,Value,Vat,ValueWVat,ChalanId,DCStoreId) 
            values (" + aChalanDetail.ChalanDetailId + ",'" + aChalanDetail.ProductCode + "','" + aChalanDetail.ProductName + "','" + aChalanDetail.Quantity + "','" + aChalanDetail.BatchNo + "','" + aChalanDetail.UnitPrice + "','" + aChalanDetail.Value + "','" + aChalanDetail.Vat + "','" + aChalanDetail.ValueWVat + "'," + aChalanDetail.ChalanId + ",'" + aChalanDetail.DCStoreId + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool ChalanUpdate(string chalanId, int userId)
        {
            string query = @"UPDATE dbo.tblChalanInfo SET IsDeliver='True', ReceiveBy = '" + userId + "', ReceiveDate = '" + DateTime.Now + "' WHERE ChalanId='" + chalanId + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool SubChalanUpdate(string chalanId)
        {
            string query = @"UPDATE dbo.tblSubDepotChalanReturnInfo SET IsDeliver='True' WHERE SChalanId='" + chalanId + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool SaveDCStoreFreeze2(DCStoreFreezeDAO aDcStoreFreezeDao)
        {
            string insertQuery = @"insert into tblDCStoreFreeze (DCStoreFreezeId,StorageLocation,TotalQuantity,ProductCode,ProductName,PackSize,BatchNo,ExpDate,ReceiveDate,ChalanNo,ChalanDate,StockQty,DamageQty,StockRcvDate,StockCondition,ComUnitId,DCStoreId,ChalanDetailsId) 
            values (" + aDcStoreFreezeDao.DCStoreFreezeId + ",'" + aDcStoreFreezeDao.StorageLocation + "','" + aDcStoreFreezeDao.TotalQuantity + "','" + aDcStoreFreezeDao.ProductCode + "','" + aDcStoreFreezeDao.ProductName + "','" + aDcStoreFreezeDao.PackSize + "','" + aDcStoreFreezeDao.BatchNo + "','" + aDcStoreFreezeDao.ExpDate + "','" + aDcStoreFreezeDao.ReceiveDate + "','" + aDcStoreFreezeDao.ChalanNo + "','" + aDcStoreFreezeDao.ChalanDate + "','" + aDcStoreFreezeDao.StockQty + "','" + aDcStoreFreezeDao.DamageQty + "','" + aDcStoreFreezeDao.StockRcvDate + "','" + aDcStoreFreezeDao.StockCondition + "'," + aDcStoreFreezeDao.ComUnitId + "," + aDcStoreFreezeDao.DCStoreId + "," + aDcStoreFreezeDao.ChalanDetailsId + ")";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool DCStockInDALMainSub(DCStockNew aDcStockNew)
        {
            string query = @"INSERT INTO dbo.tblDCStore " +
       "  ( DCStoreId , " +
         "    StorageLocation , " +
         "    ProductCode , " +
         "    ProductName , " +
         "    PackSize , " +
         "    BatchNo , " +
         "    TotalQuantity , " +
         "    ExpDate , " +
          "    MfgDate , " +
         "    ReceiveDate , " +
         "    ChalanNo , " +
         "    ChalanDate , " +
          "   ComUnitId , " +
         "    StockQty , " +
         "    DamageQty , " +
         "    StockRcvDate , " +
         "    SChalanDetailsId , " +

          "    StockCondition " +

        "   ) " +
        "   VALUES  ( '" + aDcStockNew.DCStoreId + "' , " +
        "     '" + aDcStockNew.StorageLocation + "' , " +
        "     '" + aDcStockNew.ProductCode + "' ,  " +
        "    '" + aDcStockNew.ProductName + "' , " +
        "    '" + aDcStockNew.PackSize + "' , " +
        "    '" + aDcStockNew.BatchNo + "' , " +
         "    '" + aDcStockNew.TotalQuantity + "' ,  " +
         "    '" + aDcStockNew.ExpDate + "' ,  " +
            "    '" + aDcStockNew.mfgdate + "' ,  " +
        "     '" + aDcStockNew.ReceiveDate + "' , " +
        "    '" + aDcStockNew.ChalanNo + "' , " +
        "    '" + aDcStockNew.ChalanDate + "', " +
        "    '" + aDcStockNew.ComUnitId + "' , " +
         "    '" + aDcStockNew.StockQty + "', " +
         "    '" + aDcStockNew.DamageQty + "' , " +
         "   '" + aDcStockNew.StockRcvDate + "' , " +
         "   '" + aDcStockNew.ChalanDetailsId + "' , " +

           "    'Available' " +

      "     )";
            return aCommonInternalDal.SaveDataByInsertCommand(query, "SSIDB");
        }
        public bool DCStockInDALMain(DCStockNew aDcStockNew)
        {
            string query = @"INSERT INTO dbo.tblDCStore " +
       "  ( DCStoreId , " +
         "    StorageLocation , " +
         "    ProductCode , " +
         "    ProductName , " +
         "    PackSize , " +
         "    BatchNo , " +
         "    TotalQuantity , " +
         "    ExpDate , " +
          "    MfgDate , " +
         "    ReceiveDate , " +
         "    ChalanNo , " +
         "    ChalanDate , " +
          "   ComUnitId , " +
         "    StockQty , " +
         "    DamageQty , " +
         "    StockRcvDate , " +
         "    ChalanDetailsId , " +
         
          "    StockCondition " +
         
        "   ) " +
        "   VALUES  ( '" + aDcStockNew.DCStoreId + "' , " +
        "     '" + aDcStockNew.StorageLocation + "' , " +
        "     '" + aDcStockNew.ProductCode + "' ,  " +
        "    '" + aDcStockNew.ProductName + "' , " +
        "    '" + aDcStockNew.PackSize + "' , " +
        "    '" + aDcStockNew.BatchNo + "' , " +
         "    '" + aDcStockNew.TotalQuantity + "' ,  " +
         "    '" + aDcStockNew.ExpDate + "' ,  " +
            "    '" + aDcStockNew.mfgdate + "' ,  " +
        "     '" + aDcStockNew.ReceiveDate + "' , " +
        "    '" + aDcStockNew.ChalanNo + "' , " +
        "    '" + aDcStockNew.ChalanDate + "', " +
        "    '" + aDcStockNew.ComUnitId + "' , " +
         "    '" + aDcStockNew.StockQty + "', " +
         "    '" + aDcStockNew.DamageQty + "' , " +
         "   '" + aDcStockNew.StockRcvDate + "' , " +
         "   '" + aDcStockNew.ChalanDetailsId + "' , " +
        
           "    'Available' " +
       
      "     )";
            return aCommonInternalDal.SaveDataByInsertCommand(query, "SSIDB");
        }
        public DataTable DCStoreReport(string reqId)
        {
            string query = @"SELECT ISNULL(FUE.EmpName,FU.UserName) AS ForwardBy, ForwardDate, ISNULL(RVUE.EmpName,RVU.UserName) AS ReceiveByBy,tblChalanInfo.ReceiveDate AS ChalanReceiveDate ,CASE WHEN tblDCStore.ChalanDetailsId IS NULL THEN tblDCStore.StockQty ELSE tblChalanDetail.Quantity END StockQty,tblUnitPrice.UnitPrice,tblUnitPrice.VATAmountPerUnit,DS.MfgDate,DS.ExpDate,tblProduct.ProductName,tblChalanInfo.ToComUnitCode TotalQuantity,tblChalanInfo.ToComUnitName DataColumn1,* FROM dbo.tblDCStore
                            LEFT JOIN dbo.tblChalanDetail ON dbo.tblDCStore.ChalanDetailsId = dbo.tblChalanDetail.ChalanDetailsId
                            LEFT JOIN tblChalanInfo ON tblChalanDetail.ChalanId = tblChalanInfo.ChalanId
                            LEFT JOIN dbo.tblDCStore DS ON dbo.tblChalanDetail.DCStoreId = DS.DCStoreId
                            LEFT JOIN dbo.tblStockInTransfar ON DS.StockInTransfarId = dbo.tblStockInTransfar.StockInTransfarId
                            LEFT JOIN dbo.tblCentralStore ON dbo.tblStockInTransfar.ReceiveId = dbo.tblCentralStore.ReceiveId
                            LEFT JOIN dbo.tblMIGODetail ON dbo.tblCentralStore.MigoDetailID = dbo.tblMIGODetail.MigoDetailID
                            LEFT JOIN dbo.tblProduct ON dbo.tblDCStore.ProductCode = dbo.tblProduct.ProductCode
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDCStore.ComUnitId = dbo.tblCompanyUnit.ComUnitId 
                            LEFT JOIN dbo.tblUnitPrice ON dbo.tblDCStore.ProductCode = dbo.tblUnitPrice.ProductCode 
							LEFT JOIN tblUser AS FU ON tblChalanInfo.ForwardBy = FU.UserId
							LEFT JOIN tblEmpGeneralInfo AS FUE ON FU.EmpInfoId = FUE.EmpInfoId
							LEFT JOIN tblUser AS RVU ON tblChalanInfo.ReceiveBy = RVU.UserId
							LEFT JOIN tblEmpGeneralInfo AS RVUE ON RVU.EmpInfoId = RVUE.EmpInfoId 
                            WHERE tblUnitPrice.IsActive=1 AND tblDCStore.ChalanNo='" + reqId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool DCStockInDAL(DCStockNew aDcStockNew)
        {
            string query = @"INSERT INTO dbo.tblDCStoreFreeze " +
       "  ( DCStoreId , " +
         "    DCStoreFreezeId, " +
         "    ProductCode , " +
         "    ProductName , " +
         "    PackSize , " +
         "    BatchNo , " +
         "    TotalQuantity , " +
         "    ExpDate , " +
         "    ChalanNo , " +
         "    ChalanDate , " +
          "   ComUnitId , " +
         "    StockQty , " +
         "    DamageQty , " +
         "    StockCondition " +
        "   ) " +
        "   VALUES  ( '" + aDcStockNew.DCStoreId + "' , " +
        "     '" + aDcStockNew.DCStoreFreezeId + "' , " +
        "     '" + aDcStockNew.ProductCode + "' ,  " +
        "    '" + aDcStockNew.ProductName + "' , " +
        "    '" + aDcStockNew.PackSize + "' , " +
        "    '" + aDcStockNew.BatchNo + "' , " +
         "    '" + aDcStockNew.TotalQuantity + "' ,  " +
         "    '" + aDcStockNew.ExpDate + "' ,  " +
        "    '" + aDcStockNew.ChalanNo + "' , " +
        "    '" + aDcStockNew.ChalanDate + "', " +
        "    '" + aDcStockNew.ComUnitId + "' , " +
         "    '" + aDcStockNew.StockQty + "', " +
         "    '" + aDcStockNew.DamageQty + "' , " +
       "      'StockInTransfer'  " +
      "     )";
            return aCommonInternalDal.SaveDataByInsertCommand(query, "SSIDB");
        }
        public DataTable DCInfoWithDCId(string dcstoreId)
        {
            string query = "SELECT * FROM dbo.tblDCStore WHERE DCStoreId='" + dcstoreId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public void LoadManufac(DropDownList aDropDownList)
        {
            string query = @"SELECT * FROM dbo.tblManufacturer ";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManufacName", "ManufacId", query, "SSIDB");
        }
        public bool HasProductcode(ChalanDetail aChalanDetail)
        {
            string query = "select * from tblDCStock where ProductCode = '" + aChalanDetail.ProductCode + "' and BatchNo='"+aChalanDetail.BatchNo+"'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable LoadChalanView()
        {
            string query = @"SELECT *  FROM tblChalanInfo ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable ChalanLoadInReceive(string comunitId)
        {
            string query = @"SELECT * FROM dbo.tblChalanInfo
            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblCompanyUnit.ComUnitCode=dbo.tblChalanInfo.ToComUnitCode
            WHERE tblCompanyUnit.ComUnitId='" + comunitId + "' AND (IsDeliver='False' OR IsDeliver IS NULL)";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable SubdeportChalanLoadInReceive(string comunitId)
        {
            string query = @"SELECT * FROM dbo.tblSubDepotChalanReturnInfo
            WHERE (IsDeliver='False' OR IsDeliver IS NULL)";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable GetChalanReceieve(string id)
        {
            string query = @"SELECT tblProduct.ProductName,*,tblChalanInfo.ChalanDate AS ReceiveDate2 FROM dbo.tblChalanInfo
            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblCompanyUnit.ComUnitCode=dbo.tblChalanInfo.ToComUnitCode
            LEFT JOIN dbo.tblChalanDetail ON dbo.tblChalanInfo.ChalanId = dbo.tblChalanDetail.ChalanId
            LEFT JOIN dbo.tblProduct ON dbo.tblChalanDetail.ProductCode = dbo.tblProduct.ProductCode
            LEFT JOIN dbo.tblDCStore ON dbo.tblChalanDetail.DCStoreId=dbo.tblDCStore.DCStoreId
            WHERE tblChalanInfo.IsDeliver='False' and tblChalanInfo.ChalanId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable SubChalanReceieve(string id)
        {
            string query = @"SELECT tblProduct.ProductName,*,tblSubDepotChalanReturnInfo.ChalanDate AS ReceiveDate FROM dbo.tblSubDepotChalanReturnInfo
            LEFT JOIN dbo.tblSubDepotChalanRetuenDetail ON dbo.tblSubDepotChalanReturnInfo.SChalanId = tblSubDepotChalanRetuenDetail.SChalanId
            LEFT JOIN dbo.tblProduct ON dbo.tblSubDepotChalanRetuenDetail.ProductCode = dbo.tblProduct.ProductCode
            LEFT JOIN dbo.tblDCStore ON dbo.tblSubDepotChalanRetuenDetail.DCStoreId=dbo.tblDCStore.DCStoreId
            WHERE tblSubDepotChalanReturnInfo.IsDeliver='False' and tblSubDepotChalanReturnInfo.SChalanId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadChalanById(string id)
        {
            string query = @"SELECT * FROM dbo.tblChalanInfo WHERE ChalanId='"+id+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
//        public DataTable ChalanReport(string id)
//        {
//            string query = @"SELECT * FROM dbo.tblChalanInfo
//            LEFT JOIN dbo.tblChalanDetail ON dbo.tblChalanInfo.ChalanId = dbo.tblChalanDetail.ChalanId WHERE ChalanNo='"+id+"'";
//            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
//        }
        public DataTable LoadComunit(string comunitCode)
        {
            string query = @"SELECT * FROM dbo.tblCompanyUnit WHERE ComUnitCode='"+comunitCode+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public ChalanInfo ChalanEditLoad(string ChalanId)
        {
            string query = "select * from tblChalanInfo where ChalanId = '" + ChalanId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ChalanInfo aChalanInfo = new ChalanInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aChalanInfo.ChalanId = Int32.Parse(dataReader["ChalanId"].ToString());
                    aChalanInfo.ChalanDate = Convert.ToDateTime(dataReader["ChalanDate"].ToString());
                    aChalanInfo.ChalanNo = dataReader["ChalanNo"].ToString();
                    aChalanInfo.TrackNo = dataReader["TrackNo"].ToString();
                    aChalanInfo.DriverName = dataReader["DriverName"].ToString();
                    aChalanInfo.ToComUnitCode = dataReader["ToComUnitCode"].ToString();
                    aChalanInfo.ToComUnitName = dataReader["ToComUnitName"].ToString();
                    aChalanInfo.ToComUnitAddress = dataReader["ToComUnitAddress"].ToString();
                    aChalanInfo.FromComUnitCode = dataReader["FromComUnitCode"].ToString();
                    aChalanInfo.FromComUnitName = dataReader["FromComUnitName"].ToString();
                    aChalanInfo.FromComUnitAddress = dataReader["FromComUnitAddress"].ToString();
                    aChalanInfo.TotalValue = Convert.ToDecimal(dataReader["TotalValue"].ToString());
                    aChalanInfo.TotalVat = Convert.ToDecimal(dataReader["TotalVat"].ToString());
                    aChalanInfo.GrandTotal = Convert.ToDecimal(dataReader["GrandTotal"].ToString());
                    
                }
            }
            return aChalanInfo;
        }
         public ChalanDetail ChalanDetailEditLoad(string ChalanDetailId)
        {
            string query = "select * from tblChalanDetail where ChalanDetailId = '" + ChalanDetailId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ChalanDetail aChalanDetail = new ChalanDetail();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aChalanDetail.ChalanDetailId = Int32.Parse(dataReader["ChalanDetailId"].ToString());
                    aChalanDetail.ProductCode = dataReader["ProductCode"].ToString();
                    aChalanDetail.ProductName = dataReader["ProductName"].ToString();
                    aChalanDetail.Quantity = Convert.ToDecimal(dataReader["Quantity"].ToString());
                    aChalanDetail.BatchNo = dataReader["BatchNo"].ToString();
                    aChalanDetail.UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"].ToString());
                    aChalanDetail.Value = Convert.ToDecimal(dataReader["Value"].ToString());
                    aChalanDetail.Vat = Convert.ToDecimal(dataReader["Vat"].ToString());
                    aChalanDetail.ValueWVat = Convert.ToDecimal(dataReader["ValueWVat"].ToString());
                }
            }
            return aChalanDetail;
        }
        
        public bool UpdateaChalanInfo(ChalanInfo aChalanInfo)
        {
            string query = @"UPDATE tblChalanInfo SET ChalanDate='" + aChalanInfo.ChalanDate + "',ChalanNo='" + aChalanInfo.ChalanNo + "',FromComUnitCode='" + aChalanInfo.FromComUnitCode + "',FromComUnitName='" + aChalanInfo.FromComUnitName + "', FromComUnitAddress='" + aChalanInfo.FromComUnitAddress + "',ToComUnitCode='" + aChalanInfo.ToComUnitCode + "',ToComUnitName='" + aChalanInfo.ToComUnitName + "', ToComUnitAddress='" + aChalanInfo.ToComUnitAddress + "', TotalValue='" + aChalanInfo.TotalValue + "',TotalVat='" + aChalanInfo.TotalVat + "',GrandTotal='" + aChalanInfo.GrandTotal + "' WHERE ChalanId=" + aChalanInfo.ChalanId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool UpdateaChalanDetail(ChalanDetail aChalanDetail)
        {
            string query = @"UPDATE tblChalanInfo SET ProductCode='" + aChalanDetail.ProductCode + "',ProductName='" + aChalanDetail.ProductName + "',Quantity='" + aChalanDetail.Quantity + "',BatchNo='" + aChalanDetail.BatchNo + "',UnitPrice='" + aChalanDetail.UnitPrice + "',Value='" + aChalanDetail.Value + "',Vat'" + aChalanDetail.Vat + "',ValueWVat='" + aChalanDetail.ValueWVat + "' WHERE ChalanDetailId=" + aChalanDetail.ChalanDetailId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public DataTable LoadProduct(string productId)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT * FROM tblProduct where ProductCode='" + productId.Trim() + "' ";
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }
        public DataTable GetProductDcStore(string productCode,string comunitId)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT ISNULL(VATAmountPerUnit,0)VATAmountPerUnit,ISNULL(VATPercentage,0)VATPercentage,UnitPrice,*,
            tblProduct.ProductCode AS PCode , tblProduct.ProductName AS PName 
            FROM dbo.tblDCStore
            LEFT JOIN dbo.tblProduct ON dbo.tblDCStore.ProductCode = dbo.tblProduct.ProductCode 
            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDCStore.ComUnitId = dbo.tblCompanyUnit.ComUnitId 
            LEFT JOIN dbo.tblUnitPrice ON dbo.tblDCStore.ProductCode=dbo.tblUnitPrice.ProductCode
            WHERE  ComUnitCode='" + comunitId + "' AND tblDCStore.ProductCode='" + productCode + "' AND tblUnitPrice.IsActive='True' AND StockQty>0 ";
            //tblDCStore.StockInTransfarId is not null AND
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }
        public DataTable LoadComUnit(string ComUnitId)
        {
            DataTable aDataTable = new DataTable();
            string query = @"SELECT * FROM tblCompanyUnit where ComUnitCode='" + ComUnitId.Trim() + "' ";
            aDataTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTable;
        }

        public DataTable CentralStoreQuantity(CentralStore aCentralStore)
        {
            string query = "select * from tblCentralStore where ProductCode = '" + aCentralStore.ProductCode + "' and BatchNo='" + aCentralStore.BatchNo + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public void UpdateDCStockQuantity(string stockId, string Quantity)
        {
            string updateQuery = @"UPDATE tblDCStore SET StockQty='" + Quantity + "' WHERE DCStoreId='" + stockId.Trim() + "'   ";
            aCommonInternalDal.UpdateDataByUpdateCommand(updateQuery, "SSIDB");
        }
      
            public DataTable ChalanReport(string id)
                    {
                        string query = @"SELECT tblDCStore.MfgDate,dbo.tblProduct.ProductName,* 
            FROM dbo.tblChalanInfo
            LEFT JOIN dbo.tblChalanDetail ON dbo.tblChalanInfo.ChalanId = dbo.tblChalanDetail.ChalanId
            LEFT JOIN dbo.tblDCStore ON dbo.tblChalanDetail.DCStoreId = dbo.tblDCStore.DCStoreId
            LEFT JOIN dbo.tblProduct ON dbo.tblDCStore.ProductCode = dbo.tblProduct.ProductCode 

            LEFT JOIN dbo.tblStockInTransfar ON dbo.tblDCStore.StockInTransfarId = dbo.tblStockInTransfar.StockInTransfarId
            LEFT JOIN dbo.tblCentralStore ON dbo.tblStockInTransfar.ReceiveId = dbo.tblCentralStore.ReceiveId
            LEFT JOIN dbo.tblMIGODetail ON dbo.tblCentralStore.MigoDetailID = dbo.tblMIGODetail.MigoDetailID
            WHERE tblChalanInfo.ChalanNo='" + id + "'";
                        return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
                    }


            public DataTable SubDepotChalanReport(string id)
            {
                string query = @"SELECT tblDCStore.MfgDate,dbo.tblProduct.ProductName,SubDepotName AS ToComUnitName, SubDepotAddress AS ToComUnitAddress,SubDepotCode AS ToComUnitCode,* 
FROM dbo.tblSubDepotChalanInfo
LEFT JOIN dbo.tblSubDepotChalanDetail ON dbo.tblSubDepotChalanInfo.SChalanId = dbo.tblSubDepotChalanDetail.SChalanId
LEFT JOIN dbo.tblDCStore ON dbo.tblSubDepotChalanDetail.DCStoreId = dbo.tblDCStore.DCStoreId
LEFT JOIN dbo.tblProduct ON dbo.tblDCStore.ProductCode = dbo.tblProduct.ProductCode 

LEFT JOIN dbo.tblStockInTransfar ON dbo.tblDCStore.StockInTransfarId = dbo.tblStockInTransfar.StockInTransfarId
LEFT JOIN dbo.tblCentralStore ON dbo.tblStockInTransfar.ReceiveId = dbo.tblCentralStore.ReceiveId
LEFT JOIN dbo.tblMIGODetail ON dbo.tblCentralStore.MigoDetailID = dbo.tblMIGODetail.MigoDetailID
WHERE tblSubDepotChalanInfo.ChalanNo='" + id + "'";
                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }
        //Sub Deport

            public DataTable SubdeportDCStoreReport(string reqId)
            {
                string query = @"SELECT SubDepotCode AS ComUnitCode, SubDepotName AS ComUnitName,tblUnitPrice.UnitPrice,tblUnitPrice.VATAmountPerUnit,DS.MfgDate,DS.ExpDate,tblProduct.ProductName,* 
FROM dbo.tblSubDepotStore
LEFT JOIN dbo.tblSubDepotChalanDetail ON dbo.tblSubDepotStore.SChalanDetailsId = dbo.tblSubDepotChalanDetail.SChalanDetailsId
LEFT JOIN dbo.tblDCStore DS ON dbo.tblSubDepotChalanDetail.DCStoreId = DS.DCStoreId
LEFT JOIN dbo.tblStockInTransfar ON DS.StockInTransfarId = dbo.tblStockInTransfar.StockInTransfarId
LEFT JOIN dbo.tblCentralStore ON dbo.tblStockInTransfar.ReceiveId = dbo.tblCentralStore.ReceiveId
LEFT JOIN dbo.tblMIGODetail ON dbo.tblCentralStore.MigoDetailID = dbo.tblMIGODetail.MigoDetailID
LEFT JOIN dbo.tblProduct ON dbo.tblSubDepotStore.ProductCode = dbo.tblProduct.ProductCode
LEFT JOIN dbo.tblSubDepot ON dbo.tblSubDepotStore.SubDepotId = dbo.tblSubDepot.SubDepotId 
LEFT JOIN dbo.tblUnitPrice ON dbo.tblSubDepotStore.ProductCode = dbo.tblUnitPrice.ProductCode 
WHERE tblUnitPrice.IsActive=1 AND tblSubDepotStore.ChalanNo='" + reqId + "'";

                           // WHERE tblUnitPrice.IsActive=1 AND tblDCStore.ChalanNo='" + reqId + "'";
                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

        public DataTable GetUserWiseDepot(int userId)
        {
            string queryStr = "select ComUnitId,ComUnitCode,ComUnitName  from tblCompanyUnit WHERE ComUnitId IN (SELECT CompanyUnitId FROM dbo.tblUserCompanyUnit WHERE UserId='" + userId + "')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "SSIDB");
        }
    }
}
