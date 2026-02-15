using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class ExcelUpForOrderListDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadmanufacturerName(DropDownList ddl)
        {
            string queryStr = "select * from tblManufacturer";
            aCommonInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }
        public bool XLDataGridToDbByRow(String SalesCentre, String SalesCentreName, String MIOName, String TerritoryCode, String FECode,
                                                        String DZSMCode, String CustomerID, String CustomerName, String ProductCode, String ProductName, decimal OrderQty, decimal GrossValue, String OrderCode, DateTime SubmissionDate,
                                                       String MIOCode, int OrderMasterID)
        {
            string insertQuery = @"insert into tblOrderListDetail (OrderMasterID,SalesCentre, SalesCentreName, MIOName, TerritoryCode, FECode,DZSMCode, CustomerID, CustomerName, ProductCode, ProductName, OrderQty, GrossValue, OrderCode, SubmissionDate,MIOCode) 
            values (" + OrderMasterID + ",'" + SalesCentre + "','" + SalesCentreName + "','" + MIOName + "','" + TerritoryCode + "','" + FECode + "','" + DZSMCode + "','" + CustomerID + "','" + CustomerName + "','" + ProductCode + "','" + ProductName + "','" + OrderQty + "','" + GrossValue + "','" + OrderCode + "','" + SubmissionDate + "','" + MIOCode + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool SaveOrderDAL(OrderListMasterDAO aOrderListMasterDAO)
        {
            string insertQuery = @"insert into tblOrderListMaster (OrderMasterID,ManufacId,DocumentDate,GenerateOrder,EntryBy,EntryDate) 
            values (" + aOrderListMasterDAO.OrderMasterID + "," + aOrderListMasterDAO.ManufacId + ",'" + aOrderListMasterDAO.DocumentDate + "','" + aOrderListMasterDAO.GenerateOrder + "','" + aOrderListMasterDAO.EntryBy + "','" + aOrderListMasterDAO.EntryDate  + "')";
                                 

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public DataTable LoadOrder()
        {
            string query = @"SELECT TOP 10 * FROM dbo.tblOrderListMaster
                INNER JOIN dbo.tblManufacturer ON tblOrderListMaster.ManufacId = tblManufacturer.ManufacId
                where GenerateOrder=0 
                ORDER BY DocumentDate DESC ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadMigobyID(int OrderMasterID)
        {
            string query = @"SELECT GenerateOrder,OrderMasterID FROM dbo.tblOrderListMaster
           INNER JOIN dbo.tblManufacturer ON tblOrderListMaster.ManufacId = tblManufacturer.ManufacId
           where OrderMasterID = '" + OrderMasterID + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool DeleteData(int OrderMasterID)
        {
            string query = @"DELETE FROM dbo.tblOrderListMaster WHERE OrderMasterID = '" + OrderMasterID + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool DeleteDetailData(int OrderMasterID)
        {
            string query = @"DELETE FROM dbo.tblOrderListDetail WHERE OrderMasterID = '" + OrderMasterID + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public DataTable LoadOrder(string id)
        {
            string query = @"SELECT * FROM dbo.tblOrderListDetail
                
                WHERE OrderMasterID = '" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadMigoDate(string parameter)
        {
            string query = @"SELECT  * FROM dbo.tblOrderListMaster  left JOIN dbo.tblManufacturer ON tblOrderListMaster.ManufacId = tblManufacturer.ManufacId Where GenerateOrder=0 and" + parameter + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public int TransfarOrderID_DAL(int id)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@OrderMasterID_In", id));
            return aCommonInternalDal.RunStoreProcedure("sp_OrderGenerationFromUploadOrder", aSqlParameterList, "SSIDB");
        }
    }
}
