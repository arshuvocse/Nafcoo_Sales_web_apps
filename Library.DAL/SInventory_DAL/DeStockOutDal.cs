using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class DeStockOutDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable Load_InvoiceType()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_InvoiceType_For_DDL");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public void ProductLoadDal(DropDownList aDownList)
        {
            string dc = "SELECT (ProductCode+':'+ProductName)Pro,* FROM dbo.tblProduct";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Pro", "ProductId", dc, "SSIDB");
        }

        public void DistributionCenterLoadDal(DropDownList aDownList)
        {
            string dc = "select ComUnitId, (ComUnitCode+':'+ComUnitName) as Com from dbo.tblCompanyUnit";
            aCommonInternalDal.LoadDropDownValue(aDownList, "Com", "ComUnitId", dc, "SSIDB");
        }


        public void ProformaInvoiceNumberDal(DropDownList ddl, string companyId)
        {
            string queryStr =
                @"Select InvoiceId, InvoiceNo from tblInvoice  with (nolock)  where  InvoiceId Is NOT NULL  And cast( InvoiceDate as date) between '2020/07/01' and CURRENT_TIMESTAMP And ComUnitId=" + companyId+ @"   union all
Select top 1 0 InvoiceId, 'N/A' InvoiceNo from tblInvoice  with (nolock) ";
            aCommonInternalDal.LoadDropDownValue(ddl, "InvoiceNo", "InvoiceId", queryStr, "SSIDB");

        }



        public DataTable DeStockOutNoMasterCount()
        {
            string query = @"SELECT (isnull(MAX(DcStockOutMasterId),0)+1)CountNo FROM dbo.tblDeStockOutMaster";

            //string query = @"SELECT  (ISNULL(MAX(CAST((SUBSTRING(InvoiceNo,10,11)) AS INT)),0)+1) CountNo FROM dbo.tblInvoice WHERE ComUnitId ='" + comUnitId.Trim() + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public bool SaveDataForDcStockOutMaster(DcStockOutMasterDao aMasterDao)
        {
            string insertQuery =
                @"insert into tblDeStockOutMaster (DcStockOutMasterId,ComUnitId,InvoiceId,StockOutDate,Reason,EntryBy,EntryDate,CustomerCode,Status,DoctorCode,invoiceTypeId,DcStockOutCode) 
            values (" + aMasterDao.DcStockOutMasterId + ",'" + aMasterDao.ComUnitId + "','" + aMasterDao.InvoiceId +
                "','" + aMasterDao.StockOutDate + "','" + aMasterDao.Reason + "','" + aMasterDao.EntryBy + "','"
                + aMasterDao.EntryDate + "','" + aMasterDao.CustomerCode + "','" + aMasterDao.Status + "','" + aMasterDao.DoctorCode + "', '" + aMasterDao.invoiceTypeId + "' , '" + aMasterDao.DcStockOutCode + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool SaveDataForStockOutDetailDal(DcStockOutDetailsDao aDetailsDao)
        {
            string insertQuery =
                @"insert into tblDeStockOutDetails (DcStockOutDetailsId,DcStockOutMasterId,DCStoreId,ProductCode,ProductName,StackOutQty,PackSize,BatchNo,ExpDate,ReceiveDate,UnitPrice,UnitVat,TotalUnitPrice,TotalUnitVat,TotalPrice) 
            values (" + aDetailsDao.DcStockOutDetailsId + ",'" + aDetailsDao.DcStockOutMasterId + "','" +
                aDetailsDao.DcStoreId + "','" + aDetailsDao.ProductCode + "','" + aDetailsDao.ProductName + "','" +
                aDetailsDao.StackOutQty + "','" + aDetailsDao.PackSize + "','" + aDetailsDao.BatchNo + "','" +
                aDetailsDao.ExpDate + "','" + aDetailsDao.ReceiveDate + "','" + aDetailsDao.UnitPrice + "','" + aDetailsDao.UnitVat + "'" +
                ",'" + aDetailsDao.TotalUnitPrice + "','" + aDetailsDao.TotalUnitVat + "','" + aDetailsDao.TotalPrice + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }


        public DataTable DcStockOutViewDal( string prm)
        {
//            string query =
//                @"Select   tblDeStockOutMaster.DcStockOutMasterId,tblCompanyUnit.ComUnitName, case when tblDeStockOutMaster.InvoiceId=0 then 'N/A'  else   tblInvoice.InvoiceNo end InvoiceNo , tblDeStockOutMaster.Reason,tblDeStockOutMaster.StockOutDate,tblDeStockOutMaster.Status
//from tblDeStockOutMaster  with (nolock)
//Left join tblCompanyUnit   with (nolock) ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
//Left join tblInvoice   with (nolock) On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
//where tblDeStockOutMaster.DcStockOutMasterId IS NOT NULL Order by StockOutDate Desc";


            string query =
                @"SELECT   tblDeStockOutMaster.DepotStatus,   tblDeStockOutMaster.DcStockOutMasterId,tblCompanyUnit.ComUnitName,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN D.DoctorName  
ELSE C.CustomerName END Name,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN 'Doctor'
ELSE 'Customer' END Isdoctor,
CASE WHEN tblDeStockOutMaster.InvoiceId=0 then 'N/A' else tblInvoice.InvoiceNo end InvoiceNo, tblDeStockOutMaster.Reason,tblDeStockOutMaster.StockOutDate,tblDeStockOutMaster.Status,tblDeStockOutMaster.DcStockOutCode,tblDeStockOutMaster.EntryBy
from tblDeStockOutMaster  with (nolock)
Left join tblCompanyUnit   with (nolock) ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
Left join tblInvoice   with (nolock) On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
left join tblDoctorMaster D with (nolock) On tblDeStockOutMaster.DoctorCode = D.DoctorCode
left join tblCustMaster C with (nolock) On tblDeStockOutMaster.CustomerCode = C.CustomerCode
where tblDeStockOutMaster.DcStockOutMasterId IS NOT NULL " + prm  + " order by tblDeStockOutMaster.EntryDate desc";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool UpdateDCStoreQuantity(string dCStoreId, decimal Quantity)
        {
            string updateQuery = @"UPDATE tblDCStore SET StockQty =  StockQty + " + Quantity + "  WHERE DCStoreId='" + dCStoreId.Trim() + "'  ";
            return aCommonInternalDal.DeleteDataByDeleteCommand(updateQuery, "SSIDB");
        }

        public DataTable getRecordEditMode(string prm)
        {
            //            string query =
            //                @"Select   tblDeStockOutMaster.DcStockOutMasterId,tblCompanyUnit.ComUnitName, case when tblDeStockOutMaster.InvoiceId=0 then 'N/A'  else   tblInvoice.InvoiceNo end InvoiceNo , tblDeStockOutMaster.Reason,tblDeStockOutMaster.StockOutDate,tblDeStockOutMaster.Status
            //from tblDeStockOutMaster  with (nolock)
            //Left join tblCompanyUnit   with (nolock) ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
            //Left join tblInvoice   with (nolock) On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
            //where tblDeStockOutMaster.DcStockOutMasterId IS NOT NULL Order by StockOutDate Desc";


            string query =
                @"select ProductCode PCode,ProductName PName, StackOutQty StockQty, BatchNo, format(ExpDate,'dd-MMM-yyyy') ExpDate,  format(ReceiveDate,'dd-MMM-yyyy') ReceiveDate, *  from tblDeStockOutDetails  where DcStockOutMasterId= " + prm  ;

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public bool DcStockOutMasterDeleteDal(string Id)
        {
            string query =
                @"Delete from tblDeStockOutMaster where tblDeStockOutMaster.DcStockOutMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool DcStockOutMasterPartialDal(string Id, int  Qty)
        {
            string query =
                @"UPDATE [dbo].[tblDeStockOutDetails]
   SET [StackOutQty] =  '"+ Qty + @"'
 WHERE DcStockOutDetailsId=" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool DcStockOutDetailsDeleteDal(string Id)
        {
            string query =
                @"Delete from tblDeStockOutDetails where tblDeStockOutDetails.DcStockOutMasterId =" + Id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }


        public bool UpdateDcStockOutDetailsDelete(string Id, string Status)
        {
           

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DcStockOutMasterId", Id));
            aSqlParameterlist.Add(new SqlParameter("@Status", Status));

            return aCommonInternalDal.UpdateAction("sp_UD_DcStockOutStatus", aSqlParameterlist);

            //string query = "";

//            if (Status == "Full" || Status == "Partial")
//            {
//                query = @"UPDATE [dbo].[tblDeStockOutMaster]
//   SET [DepotStatus] = '"+ Status + "'  WHERE DcStockOutMasterId  = " + Id;
//            }
//            else
//            {
//                query = @" UPDATE [dbo].[tblDeStockOutDetails]
//   SET  
// [StackOutQty] = 0
// WHERE DcStockOutMasterId ='" + Id + @"'  UPDATE [dbo].[tblDeStockOutMaster]
//   SET [DepotStatus] = '" + Status + "'  WHERE DcStockOutMasterId  = " + Id;

//            }

            //return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }

        //GetManu for approval 
        public DataTable GetMenuIdByMenuName(string menuname)
        {
            string query = @"SELECT * FROM tblMainMenu WHERE URL like '%" + menuname + "%' ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        //GetAssignAppUser
        public DataTable GetAssignedAppUser(string menuid, string userId)
        {
            string query = @"SELECT * FROM tblAppSetup WHERE SL='" + menuid + "' AND UserId='" + userId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        //sUpdateApproval
        public void ApprovalUpdateDal(DcStockOutMasterDao aMasterDao)
        {
            string query = @"UPDATE tblDeStockOutMaster SET Status = '" + aMasterDao.Status + "',ApprovedBy = '"
                           + aMasterDao.ApprovedBy + "',ApprovedDate = '" + aMasterDao.ApprovedDate +
                           "' WHERE DcStockOutMasterId = " + aMasterDao.DcStockOutMasterId + "";
            aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }


        //GetStockOutApproval

        public DataTable DcStockOutAppDal()
        {
            string query =
                @"Select tblDeStockOutMaster.DcStockOutMasterId,tblCompanyUnit.ComUnitName,tblInvoice.InvoiceNo, tblDeStockOutMaster.Reason,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN tblDoctorMaster.DoctorName  
ELSE tblCustMaster.CustomerName END Name,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN 'Doctor'
ELSE 'Customer' END Isdoctor,
tblDeStockOutMaster.StockOutDate,tblDeStockOutMaster.Status from tblDeStockOutMaster 
Left join tblCompanyUnit ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
Left join tblInvoice On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
left join tblCustMaster on tblCustMaster.CustomerCode =tblDeStockOutMaster.CustomerCode
left join tblDoctorMaster On tblDeStockOutMaster.DoctorCode= tblDoctorMaster.DoctorCode
where tblDeStockOutMaster.DcStockOutMasterId IS NOT NULL And  tblDeStockOutMaster.Status='Posted' ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }



        public DataTable DcStockOutReportDal(string id)
        {
            //            string query =
            //                @"Select tblunitprice.UnitPrice*StackOutQty as Total,tblunitprice.VATAmountPerUnit,tblunitprice.UnitPrice,
            //CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN tblDoctorMaster.DoctorCode  
            //ELSE tblCustMaster.CustomerCode END CustomerCode,
            //CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN tblDoctorMaster.DoctorName  
            //ELSE tblCustMaster.CustomerName END CustomerName,
            //tblCustMaster.Address,tblCompanyUnit.ComUnitName, case when tblDeStockOutMaster.InvoiceId=0 then 'N/A'  else   tblInvoice.InvoiceNo end  InvoiceNo, tblDeStockOutMaster.Reason,FORMAT(Cast(tblDeStockOutMaster.StockOutDate As date),'dd-MMM-yyyy') As StockOutDate,tblDeStockOutDetails.ProductCode,tblDeStockOutDetails.ProductName,
            //tblDeStockOutDetails.BatchNo,tblDeStockOutDetails.StackOutQty
            //from tblDeStockOutMaster 
            //Left join tblDeStockOutDetails On tblDeStockOutDetails.DcStockOutMasterId = tblDeStockOutMaster.DcStockOutMasterId
            //Left join tblCompanyUnit ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
            //Left join tblInvoice On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
            //left join tblCustMaster on tblCustMaster.CustomerCode =tblDeStockOutMaster.CustomerCode
            //left join tblDoctorMaster On tblDeStockOutMaster.DoctorCode= tblDoctorMaster.DoctorCode
            //left join tblunitprice on tblDeStockOutDetails.ProductCode= tblunitprice.ProductCode
            //where tblDeStockOutMaster.DcStockOutMasterId=" + id;

            string query = @"Select CASE WHEN tblDeStockOutDetails.TotalPrice IS NULL THEN (tblunitprice.UnitPrice*tblDeStockOutDetails.StackOutQty) + (tblunitprice.VATAmountPerUnit*tblDeStockOutDetails.StackOutQty)ELSE tblDeStockOutDetails.TotalPrice END Total,
case when tblDeStockOutDetails.UnitVat Is null then tblunitprice.VATAmountPerUnit ELSE tblDeStockOutDetails.UnitVat  END VATAmountPerUnit,
case when tblDeStockOutDetails.UnitPrice Is null then tblunitprice.UnitPrice ELSE tblDeStockOutDetails.UnitPrice  END UnitPrice,
case When tblDeStockOutDetails.TotalUnitPrice IS null Then tblunitprice.UnitPrice*tblDeStockOutDetails.StackOutQty ELSE tblDeStockOutDetails.TotalUnitPrice  END TotalUnitPrice,
case When tblDeStockOutDetails.TotalUnitVat IS null Then tblunitprice.VATAmountPerUnit*tblDeStockOutDetails.StackOutQty ELSE tblDeStockOutDetails.TotalUnitVat  END TotalUnitVat,
tblDeStockOutDetails.TotalUnitVat,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN tblDoctorMaster.DoctorCode  
ELSE tblCustMaster.CustomerCode END CustomerCode,
CASE WHEN  tblDeStockOutMaster.DoctorCode !='' THEN tblDoctorMaster.DoctorName  
ELSE tblCustMaster.CustomerName END CustomerName,
tblCustMaster.Address,tblCompanyUnit.ComUnitName,tblCustomerType.CustomerType,    CASE WHEN tblCustMaster.CellNo !='Not Available' THEN '0'+tblCustMaster.CellNo ELSE tblCustMaster.CellNo END CellNo,  
case when tblDeStockOutMaster.InvoiceId=0 then tblDeStockOutMaster.DcStockOutCode  else   tblInvoice.InvoiceNo end  InvoiceNo,
tblDeStockOutMaster.Reason,FORMAT(Cast(tblDeStockOutMaster.StockOutDate As date),'dd-MMM-yyyy') As StockOutDate,tblDeStockOutDetails.ProductCode,tblDeStockOutDetails.ProductName,
tblDeStockOutDetails.BatchNo,tblDeStockOutDetails.StackOutQty
from tblDeStockOutMaster 
Left join tblDeStockOutDetails On tblDeStockOutDetails.DcStockOutMasterId = tblDeStockOutMaster.DcStockOutMasterId
Left join tblCompanyUnit ON tblCompanyUnit.ComUnitId = tblDeStockOutMaster.ComUnitId
Left join tblInvoice On tblInvoice.InvoiceId = tblDeStockOutMaster.InvoiceId
left join tblCustMaster on tblCustMaster.CustomerCode =tblDeStockOutMaster.CustomerCode
LEFT JOIN tblCustomerType On tblCustMaster.CustomerTypeId = tblCustomerType.CustomerTypeId
left join tblDoctorMaster On tblDeStockOutMaster.DoctorCode= tblDoctorMaster.DoctorCode
left join tblunitprice on tblDeStockOutDetails.ProductCode= tblunitprice.ProductCode
where tblDeStockOutMaster.DcStockOutMasterId=" + id;

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        //RND Update Approval

        public bool UpdateStockOutMasterDataForApprovalDal(DcStockOutMasterDao aMasterDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DcStockOutMasterId", aMasterDao.DcStockOutMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Status", aMasterDao.Status));
            aSqlParameterlist.Add(new SqlParameter("@ApprovedBy", aMasterDao.ApprovedBy));
            aSqlParameterlist.Add(new SqlParameter("@ApprovedDate", aMasterDao.ApprovedDate));
            return aCommonInternalDal.UpdateAction("sp_UD_DcStockOutApproval", aSqlParameterlist);
        }


        public DataTable GetDcStoreIdDal(string id)
        {
            string query = @"Select tblDeStockOutMaster.DcStockOutMasterId,tblDeStockOutDetails.DcStoreId from tblDeStockOutMaster 
Left join tblDeStockOutDetails ON tblDeStockOutDetails.DcStockOutMasterId = tblDeStockOutMaster.DcStockOutMasterId
where tblDeStockOutMaster.DcStockOutMasterId IS NOT NULL And  tblDeStockOutMaster.Status='Posted' And tblDeStockOutMaster.DcStockOutMasterId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }



     



    }
}
