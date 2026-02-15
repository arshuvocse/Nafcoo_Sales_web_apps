using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;
using Library.DAO.SubDepot_DAO;

namespace Library.DAL.SInventory_DAL
{
   public class SampleTypeConventionDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public void SalesCenterLoadDal(DropDownList aDownList)
        {
            string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
        }

        public void ProductLoadDal(DropDownList aDownList)
        {
            string dc = "SELECT (ProductCode+':'+ProductName)Pro,* FROM dbo.tblProduct";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Pro", "ProductId", dc, "SSIDB");
        }


        public DataTable GetProductDcStore(string productCode,string ComUnit)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT *,
            tblProduct.ProductCode AS PCode , tblProduct.ProductName AS PName 
            FROM dbo.tblDCStore
            LEFT JOIN dbo.tblProduct ON dbo.tblDCStore.ProductCode = dbo.tblProduct.ProductCode    	    
            WHERE tblDCstore.ComUnitId='"+ComUnit+"' And tblProduct.ProductId='"+productCode+"' AND StockQty>0 order by tblProduct.ProductCode";
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }

        public bool SaveDataForSubDcStockOutMaster(SampleStockForDcMaster aMasterDao)
        {
            string insertQuery =
                @"insert into tblSampleStockForDcMaster (SampleStockForDcMasterId,SampleStockForDcMasterCode,ComUnitId,Action,Date,EntryBy,EntryDate,Status) 
            values (" + aMasterDao.SampleStockForDcMasterId + ",'" + aMasterDao.SampleStockForDcMasterCode + "','" + aMasterDao.ComUnitId + "','" + aMasterDao.Action +
                "','" + aMasterDao.Date + "','" + aMasterDao.EntryBy + "','" +
                aMasterDao.EntryDate + "','" + aMasterDao.Status + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool SaveDataForStockOutDetailDal(SampleStockForDcDetails aDetailsDao)
        {
            string insertQuery =
                @"insert into tblSampleStockForDcDetails (SampleStockForDcDetailsId,SampleStockForDcMasterId,DCStoreId,ProductCode,ProductName,BatchNo,ReceiveDate,ExpDate,SampleStock) 
            values (" + aDetailsDao.SampleStockForDcDetailsId + ",'" + aDetailsDao.SampleStockForDcMasterId + "','" +
                aDetailsDao.DCStoreId + "','" + aDetailsDao.ProductCode + "','" + aDetailsDao.ProductName + "','" + aDetailsDao.BatchNo + "','" +
                aDetailsDao.ReceiveDate + "','" + aDetailsDao.ExpDate + "','" + aDetailsDao.SampleStock + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public DataTable DcStockOutViewDal()
        {
            string query = @"Select SSFDM.SampleStockForDcMasterId,tblCompanyUnit.ComUnitName,SSFDM.Date,SSFDM.Action
                  from tblSampleStockForDcMaster SSFDM
                  Left join tblCompanyUnit ON tblCompanyUnit.ComUnitId = SSFDM.ComUnitId
                  where SSFDM.SampleStockForDcMasterId IS NOT NULL";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public bool DcStockOutMasterDeleteDal(string Id)
        {
            string query =
                @"Delete from tblSampleStockForDcMaster where tblSampleStockForDcMaster.SampleStockForDcMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool DcStockOutDetailsDeleteDal(string Id)
        {
            string query =
                @"Delete from tblSampleStockForDcDetails where tblSampleStockForDcDetails.SampleStockForDcMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool StockInCentral(SampleStockForDcDetails aDetails)
        {
            string query = @"Update tblDCStore Set  StockQty=((Select StockQty from tblDCStore where  DCStoreId='"+aDetails.DCStoreId+"') + '"+aDetails.SampleStock+"') Where DCStoreId='"+aDetails.DCStoreId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public bool StockOuTCentral(SampleStockForDcDetails aDetails)
        {
            string query = @"Update tblDCStore Set  StockQty=((Select StockQty from tblDCStore where  DCStoreId='" + aDetails.DCStoreId + "') - '" + aDetails.SampleStock + "') Where DCStoreId='"+aDetails.DCStoreId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public DataTable LoadSampleStock(string Id)
        {
            string query = @"Select DCStoreId, SampleStock from tblSampleStockForDcDetails where SampleStockForDcMasterId="+Id;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
