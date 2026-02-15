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
    public class SampleStockForWHDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();



        public void SalesCenterLoadDal(DropDownList aDownList)
        {
            string dc = @"select WearhouseId, (WearhouseCode+':'+WearhouseName) as Com from dbo.tblWearhouse";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "WearhouseId", dc, "SSIDB");
        }

        public void ProductLoadDal(DropDownList aDownList)
        {
            string dc = "SELECT (ProductCode+':'+ProductName)Pro,* FROM dbo.tblProduct";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Pro", "ProductId", dc, "SSIDB");
        }

        public DataTable GetProductDcStore(string productCode)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT *,
            tblProduct.ProductCode AS PCode , tblProduct.ProductName AS PName 
            FROM dbo.tblCentralStore
            LEFT JOIN dbo.tblProduct ON dbo.tblCentralStore.ProductCode = dbo.tblProduct.ProductCode    	    
            WHERE tblProduct.ProductId='" + productCode + "' AND StockInQty>0 order by tblProduct.ProductCode";

            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }

        public bool SaveDataForSubDcStockOutMaster(SampleStockForWareHouseMaster aMasterDao)
        {
            string insertQuery =
                @"insert into tblSampleStockForWareHouseMaster (SampleStockForWHMasterId,SampleStockForWareHouseMstCode,WareHouseId,Action,Date,EntryBy,EntryDate,Status) 
            values (" + aMasterDao.SampleStockForWareHouseMstId + ",'" + aMasterDao.SampleStockForWareHouseMstCode + "','" + aMasterDao.WareHouseId + "','" + aMasterDao.Action +
                "','" + aMasterDao.Date + "','" + aMasterDao.EntryBy + "','" +
                aMasterDao.EntryDate + "','" + aMasterDao.Status + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool SaveDataForStockOutDetailDal(SampleStockForWHDetails aDetailsDao)
        {
            string insertQuery =
                @"insert into tblSampleStockForWareHouseDetails (SampleStockForWHDetailsId,SampleStockForWHMasterId,ReceiveId,ProductCode,ProductName,BatchNo,ReceiveDate,ExpDate,SampleStock) 
            values (" + aDetailsDao.SampleStockForWHDetailsId + ",'" + aDetailsDao.SampleStockForWHMasterId + "','" +
                aDetailsDao.ReceiveId + "','" + aDetailsDao.ProductCode + "','" + aDetailsDao.ProductName + "','" + aDetailsDao.BatchNo + "','" +
                aDetailsDao.ReceiveDate + "','" + aDetailsDao.ExpDate + "','" + aDetailsDao.SampleStock + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");

        }

        public bool StockOutCentral(SampleStockForWHDetails aDetails)
        {
            string query = @"Update tblCentralStore Set StockInQty=((Select StockInQty from tblCentralStore where ReceiveId='"+aDetails.ReceiveId+"')-'"+aDetails.SampleStock+"')  where  ReceiveId='"+aDetails.ReceiveId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public bool StockInCentral(SampleStockForWHDetails aDetails)
        {
            string query = @"Update tblCentralStore Set StockInQty=((Select StockInQty from tblCentralStore where ReceiveId='"+aDetails.ReceiveId+"')+'"+aDetails.SampleStock+"')  where  ReceiveId='"+aDetails.ReceiveId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }


        public DataTable DcStockOutViewDal()
        {
            string query = @"Select SSFDM.SampleStockForWHMasterId,tblWearhouse.WearhouseName,SSFDM.Date,SSFDM.Action
                           from tblSampleStockForWareHouseMaster  SSFDM
                           Left join tblWearhouse ON tblWearhouse.WearhouseId = SSFDM.WareHouseId
                           where SSFDM.SampleStockForWHMasterId IS NOT NULL";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public bool DcStockOutMasterDeleteDal(string Id)
        {
            string query =
                @"Delete from tblSampleStockForWareHouseMaster where SampleStockForWHMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool DcStockOutDetailsDeleteDal(string Id)
        {
            string query =
                @"Delete from tblSampleStockForWareHouseDetails where SampleStockForWHMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }

        public DataTable GetStock(string Id)
        {
            string query = @"Select ReceiveId,SampleStock from tblSampleStockForWareHouseDetails where SampleStockForWHMasterId="+Id;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
