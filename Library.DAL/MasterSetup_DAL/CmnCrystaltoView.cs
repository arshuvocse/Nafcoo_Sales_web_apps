using Library.DAL.DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.MasterSetup_DAL
{
  public  class CmnCrystaltoView
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetMoneyReceiptDAL(string parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Parm", parm));


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
        public DataTable GetDCReportListDAL(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                DataTable dt = accessManager.GetDataTable("sp_Get_DCStockReportList", aSqlParameterlist);
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



        public DataTable SalesRejecionReportDAl(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                DataTable dt = accessManager.GetDataTable("sp_Get_alesReectionReportList", aSqlParameterlist);
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


        public DataTable BusinessSummaryReportDAl(DateTime fromdate, DateTime todate)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@fromdate", fromdate));
                aSqlParameterlist.Add(new SqlParameter("@todate", todate));

                DataTable dt = accessManager.GetDataTable("sp_Get_BusinessSummaryReportList", aSqlParameterlist);
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

        public DataTable GetProformaInvoListDAL(string parm)
        {
            try
            {
                string query = @"SELECT  ComUnitCode AS 'Sales Center',
    ComUnitName AS 'Sales Center Name',
    CustomerCode AS 'Customer ID',
    CustomerName AS 'Customer Name',
    IntransitDay AS 'Customer Type',
    PayType AS 'Mode of Payment',
    OrderNo AS 'Order Code',
    OrderDate AS 'Order / Submission Date',
    InvoiceNo AS 'Proforma Number',
    InvoiceDate AS 'Proforma Date',
    InvoiceBy AS 'Proforma By',
    DelivaryInvoiceNo AS 'Invoice No',
    UpdateDate AS 'Invoice Date',
    ConfirmBy AS 'Confirm By',
    ProductCode AS 'Product Code',
    ProductName AS 'Product Name',
    PackSize AS 'Pack Size',
    BatchNo AS 'Batch No',
    ExpDate AS 'Exp Date',
    Quantity AS 'Invoice Qty',
    GrossValue AS 'TP',
    TotalVat AS 'VAT',
    TotalDiscount AS 'Discount',
    FOC AS 'FOC',
    VatOnFOC AS 'Vat On FOC',
    NetTp AS 'Net TP',
    NetTPVat AS 'Net Amount',
    AdjustmentAmount AS 'Adjustment',
    PaymentNo AS 'Payment No',
    PaymentDate AS 'PaymentDate',
    PayAmount AS 'Pay Amount',
    Due AS 'Due',
    SalesReturn AS 'Sales Return',
    InvoiceType AS 'Invoice Type',
    MarketCode AS 'Market Code',
    MarketName AS 'Market Name',
    SubterritoryCode AS 'Territory Code',
    TerritoryName AS 'Territory',
    MBE AS 'MBE',
    AreaCode AS 'Area Code',
    AreaName AS 'Area',
    MIO AS 'ABM',
    RegionCode AS 'Region Code',
    RegionName AS 'Region',
    AM AS 'RBM',
    ZoneCode AS 'Cluster Code',
    ZoneName AS 'Cluster',
    DZSM AS 'Cluster Head',
    GroupCode AS 'Group Code',
    GroupName AS 'Group',
    NSM AS 'NSM' FROM (SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo ,
mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,  
NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,CONVERT(VARCHAR,MAS.EntryDate,109) Orderdate,
I.InvoiceNo,CONVERT(VARCHAR,I.CreateDate,109) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,ID.Quantity,
ID.Quantity*ID.UnitPrice AS GrossValue,ID.Quantity*ID.UnitVatAmount AS TotalVat,ID.DiscountAmount*-1 TotalDiscount,isnull(ID.AdjustmentAmount,0) AdjustmentAmount,
CASE WHEN  ID.IsGiftProduct = 1 THEN (ID.UnitPrice*ID.Quantity)*-1 ELSE 0 END AS FOC, 
CASE WHEN ID.IsGiftProduct = 1 THEN (ID.UnitVatAmount*ID.Quantity)*-1 ELSE 0 END AS VatOnFOC,
((ID.Quantity*ID.UnitPrice) - (ISNULL(ID.DiscountAmount,0) + (isnull(ID.AdjustmentAmount,0) + CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*ID.Quantity) ELSE 0 END))) AS NetTp,
CASE 
WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
CAST(((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount) as decimal(10,2))  END AS NetTPVat,
CASE 
WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
I.DelivaryInvoiceNo,CONVERT(VARCHAR,I.UpdateDatetime,109) UpdateDate, CASE WHEN ISNULL(PaidAmount,0) > 0  AND ID.IsGiftProduct = 0 THEN 'PY-' + RIGHT(I.DelivaryInvoiceNo,LEN(DelivaryInvoiceNo) - 4) END AS PaymentNo,
CASE WHEN ISNULL(PaidAmount,0) > 0 AND ID.IsGiftProduct = 0   THEN CONVERT(VARCHAR,LD.PaymentDate,101) END PaymentDate,

CASE WHEN ISNULL(PaidAmount,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(PaidAmount,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount as decimal(10,2))
END AS PayAmount,

CASE WHEN ISNULL(TotalDiscountAmount,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(TotalDiscountAmount,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*TotalDiscountAmount as decimal(10,2))
END *-1 AS DiscountOnPayment,

PaidAmount, TotalDiscountAmount ,SRTN.SalesReturn as TotalSalesReturn,
CASE WHEN ISNULL(SRTN.SalesReturn,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(SRTN.SalesReturn,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/DeliveryTpGrandTotal)*(DeliveryTpGrandTotal -  ISNULL(PaidAmount,0)))/100  as decimal(10,2))
ELSE 0 END AS SalesReturn,

CASE WHEN Id.DeliveryStatus = 'Reject' THEN  0  
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IS NULL THEN ((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount)
WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) = 0 THEN
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)
WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) > 0
THEN (((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)) - (((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount)  - isnull(ID.AdjustmentAmount,0)
END - ((CASE WHEN ISNULL(SRTN.SalesReturn,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(SRTN.SalesReturn,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/DeliveryTpGrandTotal)*(DeliveryTpGrandTotal -  ISNULL(PaidAmount,0)))/100  as decimal(10,2))
ELSE 0 END + CASE WHEN ISNULL(TotalDiscountAmount,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(TotalDiscountAmount,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*TotalDiscountAmount as decimal(10,2))
END )) AS Due,
SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType   
FROM tblInvoice AS I with(nolock) 
LEFT JOIN tblUser AS USR with(nolock) ON I.UserId = USR.UserId
LEFT JOIN tblUser AS UUSR with(nolock) ON I.UpdateBy = UUSR.LoginName
LEFT JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId
LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId 
LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId 
LEFT JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId 
LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId 
LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId
LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId
LEFT JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType
LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON mas.NSMId = NSM.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock)  ON mas.MBEEmpInfoId = MBE.EmpInfoId 
left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId
LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryNetAmount) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId) AS IVDD ON I.InvoiceId = IVDD.InvoiceId 
LEFT JOIN (select D.InvoiceId,DS.PaymentDate,PayType from tblInvoice as d cross apply (select top 1 P.PaymentDate,PayType from tblCustPayDetail AS PD with(nolock)
LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount,SUM(ISNULL(DiscountAmount,0)) TotalDiscountAmount  FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId
LEFT JOIN (SELECT InvoiceId,SUM(ISNULL(DeliveryQuantity,0)) TotalDelivery FROM tblInvoiceDetail WHERE IsGiftProduct = 0 GROUP BY InvoiceId) AS TTD ON I.InvoiceId = TTD.InvoiceId
LEFT JOIN (SELECT InvoiceId,SUM(NetAmount) AS SalesReturn FROM tblReturnInvoiceDetail AS RTND
LEFT JOIN tblReturnInvoice AS RTNM ON RTND.ReturnInvoiceId = RTNM.ReturnInvoiceId WHERE InvoiceId != 0 GROUP BY InvoiceId) AS SRTN ON I.InvoiceId = SRTN.InvoiceId 
WHERE I.InvoiceId IS NOT NULL AND ID.Quantity > 0 

UNION ALL

SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,ISNULL(OrderCode,'N/A') AS OrderNo, 
mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,NSM,DZSM,AM,MIO,MBE,
 CONVERT(VARCHAR, ODR.EntryDate,109) Orderdate,ISNULL(INV.InvoiceNo,'N/A') InvoiceNo,CASE WHEN INV.InvoiceDate IS NULL THEN M.ApprovedDate ELSE INV.CreateDate END AS InvoiceDate,
DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,
D.StackOutQty,D.StackOutQty*UP.UnitPrice AS GrossValue,D.StackOutQty*UP.VatAmountPerUnit AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
(D.StackOutQty*UP.UnitPrice)*-1 AS FOC, 
(D.StackOutQty*UP.VatAmountPerUnit)*-1 AS VatOnFOC,0 NetTp,0 NetTPVat,0 TotalDelivery,INV.DelivaryInvoiceNo,CONVERT(VARCHAR,INV.UpdateDatetime,109) UpdateDate,
'' PaymentNo,'' PaymentDate,0 PayAmount,0 DiscountOnPayment,0 PaidAmount,0 TotalDiscountAmount,0 TotalSalesReturn, 0 SalesReturn,0 Due,
'' ProductOffer,M.EntryBy AS InvoiceBy,M.EntryBy ConfirmBy,DZSM DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'FOC/Sample Invoice'  AS InvoiceType    
FROM tblDeStockOutDetails AS D
LEFT JOIN tblDeStockOutMaster AS M with(nolock) ON D.DcStockOutMasterId = M.DcStockOutMasterId
LEFT JOIN tblInvoice AS INV with(nolock) ON M.InvoiceId = INV.InvoiceId 
LEFT JOIN tblOrder AS ODR with(nolock) ON INV.OrderId = ODR.OrderId
LEFT JOIN tblUnitPrice AS UP with(nolock) ON D.ProductCode = UP.ProductCode
LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerCode = M.CustomerCode
LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON C.CustomerTypeId = ct.CustomerTypeId 
LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = M.ComUnitId 
left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = D.DCStoreId 
LEFT JOIN (SELECT MBEI.SubTerritoryId,MBE.EmpMasterCode + ' : ' + MBE.EmpName MBE FROM tblMBEInfo AS MBEI with (nolock)
LEFT JOIN tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = MBEI.SubTerritoryId
LEFT JOIN tblEmpGeneralInfo MBE  with (nolock)  ON MBEI.EmployeeId = MBE.EmpInfoId 
WHERE MBEI.IsActive = 1) AS MBE ON SR.SubTerritoryId = MBE.SubTerritoryId

LEFT JOIN (SELECT MIOI.TerritoryId,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO FROM tblMioInfo AS MIOI with (nolock)
left join tblTerritory tr  with (nolock) on MIOI.TerritoryId=tr.TerritoryId
LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON MIOI.EmployeeId=MIO.EmpInfoId
WHERE MIOI.IsActive = 1) AS MIO ON TR.TerritoryId = MIO.TerritoryId

LEFT JOIN (SELECT ASMI.AreaId,AM.EmpMasterCode +' : '+ AM.EmpName AS AM FROM tblASMInfo AS ASMI with (nolock)
left join tblArea ar  with (nolock)  on ar.AreaId = ASMI.AreaId
LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON AM.EmpInfoId = ASMI.EmployeeId 
WHERE ASMI.IsActive = 1) AS ASM ON AR.AreaId = ASM.AreaId

LEFT JOIN (SELECT RSMI.RegionId,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM FROM tblRSMInfo AS RSMI with (nolock)
left join tblRegion rg  with (nolock) on RSMI.RegionId = rg.RegionId
LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock) ON RSMI.EmployeeId = DZSM.EmpInfoId 
WHERE RSMI.IsActive = 1) AS RSM ON rg.RegionId = RSM.RegionId

LEFT JOIN (SELECT NSMI.GroupId,NSM.EmpMasterCode +' : '+ NSM.EmpName NSM FROM tblNSMInfo AS NSMI with (nolock)
left join dbo.tbl_Group gr  with (nolock) on NSMI.GroupId=gr.GroupId
LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock) ON NSMI.EmployeeId = NSM.EmpInfoId 
WHERE NSMI.IsActive = 1) AS NSM ON NSM.GroupId = gr.GroupId
WHERE M.Status = 'Approved'

UNION ALL

SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo, 

mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,  
NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,

CONVERT(VARCHAR,MAS.SubmissionDate,109) Orderdate, I.InvoiceNo,CONVERT(VARCHAR,I.CreateDate,109) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,(ID.Quantity - ID.DeliveryQuantity)*-1 Quantity,
((ID.Quantity - ID.DeliveryQuantity)*ID.UnitPrice)*-1 AS GrossValue,((ID.Quantity - ID.DeliveryQuantity)*ID.UnitVatAmount)*-1 AS TotalVat,CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0))) ELSE 0 END  TotalDiscount,Id.AdjustmentAmount*-1 AdjustmentAmount, 
CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS FOC,  
CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitVatAmount*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS VatOnFOC, 

CASE WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
(((((ID.Quantity - ISNULL(DeliveryQuantity,0)) *ID.UnitPrice) - (ID.DiscountAmount- (ID.DiscountAmount/ID.Quantity)*ISNULL(ID.DeliveryQuantity,0))) - ISNULL(ID.AdjustmentAmount,0))) END *-1 AS NetTp,

CASE WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
CAST((CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0)))*-1 ELSE 0 END) + (((((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitPrice) - isnull(ID.AdjustmentAmount,0)) + ((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitVatAmount))) AS DECIMAL(18,2)) END *-1 AS NetTPVat,
CASE 
WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
I.DelivaryInvoiceNo, CONVERT(VARCHAR,I.UpdateDatetime,109) UpdateDate, NULL AS PaymentNo,NULL PaymentDate, 0 AS PayAmount,0 DiscountOnPayment,PaidAmount,0 TotalDiscountAmount,0 TotalSalesReturn, 0 SalesReturn,
0 AS Due,
SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,
USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType  
FROM tblInvoice AS I with(nolock)  
LEFT JOIN tblUser AS USR with(nolock) ON I.UserId = USR.UserId LEFT JOIN tblUser AS UUSR ON I.UpdateBy = UUSR.LoginName 
LEFT JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId 
LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId  
LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId  
LEFT JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId  
LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId  
LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId 
LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId 
LEFT JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType 
LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON mas.NSMId = NSM.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock)  ON mas.MBEEmpInfoId = MBE.EmpInfoId 
left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId 
LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryQuantity) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId)  AS IVDD  ON I.InvoiceId = IVDD.InvoiceId  
LEFT JOIN (select D.InvoiceId,DS.PaymentDate from tblInvoice as d cross apply (select top 1 P.PaymentDate from tblCustPayDetail AS PD with(nolock)
LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId 
WHERE I.InvoiceId IS NOT NULL AND ID.DeliveryStatus IN ('Reject','Partial') AND ISNULL(ID.Quantity,0) > 0


UNION ALL


SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,'N/A' AS OrderNo, 
mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,NSM.EmpName,DZSM.EmpName,AM.EmpName,MIO.EmpName,MBE.EmpName,
'' Orderdate,ISNULL(RM.ReturnInvoiceNo,'N/A') InvoiceNo,RM.ReturnInvoiceDate InvoiceDate,
RD.ProductCode,RD.ProductName,RD.PackSize,RD.BatchNo,
CONVERT(VARCHAR,RD.ExpDate,101) ExpDate,
RD.Quantity*-1 AS Quantity,(RD.Quantity * RD.UnitPrice)*-1 AS GrossValue,(RD.Quantity*RD.UnitVatAmount)*-1 AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitPrice ELSE 0 END AS FOC, 
CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitVatAmount ELSE 0 END AS VatOnFOC,CASE WHEN RD.IsFoc = 0 THEN ((RD.Quantity * RD.UnitPrice)*-1) ELSE 0 END AS NetTp,
CASE WHEN RD.IsFoc = 0 THEN (((RD.Quantity * RD.UnitPrice)*-1) + ((RD.Quantity*RD.UnitVatAmount)*-1)) ELSE 0 END AS NetTPVat,0 TotalDelivery,'',CONVERT(VARCHAR,RM.AprovedDate,101) UpdateDate,
'' PaymentNo,'' PaymentDate,0 PayAmount,0 DiscountOnPayment,0 PaidAmount,0 TotalDiscountAmount,ISNULL(PTR.CustomerGet,0) TotalSalesReturn,ISNULL(PTR.SalesReturn,0)*-1 SalesReturn,0 Due,
'' ProductOffer,USR.UserName AS InvoiceBy,RM.ApprovedBy ConfirmBy,DZSM.EmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'Return Invoice'  AS InvoiceType FROM tblReturnInvoiceDetail AS RD with(nolock)
LEFT JOIN tblReturnInvoice AS RM with(nolock) ON RD.ReturnInvoiceId = RM.ReturnInvoiceId
LEFT JOIN tblUser AS USR with(nolock) ON RM.UserId = USR.UserId
LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = RM.CustomerMasterId  
LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON C.CustomerTypeId = ct.CustomerTypeId 
LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = RM.ComUnitId 
left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
left join tblArea ar   with (nolock)  on ar.AreaId = tr.AreaId
left join tblRegion rg  with (nolock) on ar.RegionId = rg.RegionId
left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId

LEFT JOIN (SELECT NM.GroupId,EGI.EmpName FROM tblNsmInfo AS NM
LEFT JOIN tblEmpGeneralInfo AS EGI ON NM.EmployeeId = EGI.EmpInfoId
WHERE NM.IsActive = 1) AS NSM ON GR.GroupId = NSM.GroupId

LEFT JOIN (SELECT CLSI.RegionId,DSM.Empname FROM tblRSMInfo AS CLSI with (nolock) 
LEFT JOIN dbo.tblEmpGeneralInfo DSM  with (nolock) ON CLSI.EmployeeId = DSM.EmpInfoId 
WHERE CLSI.IsActive = 1) AS DZSM ON rg.RegionId = DZSM.RegionId

LEFT JOIN (SELECT RSMI.AreaId,AM2.EmpName FROM tblASMInfo AS RSMI with (nolock)
LEFT JOIN dbo.tblEmpGeneralInfo AM2  with (nolock)  ON RSMI.EmployeeId = AM2.EmpInfoId 
WHERE RSMI.IsActive = 1) AS AM ON ar.AreaId = AM.AreaId

LEFT JOIN (SELECT ABMI.TerritoryId,MIO1.EmpName FROM tblMIOInfo AS ABMI with (nolock)
LEFT JOIN dbo.tblEmpGeneralInfo MIO1  with (nolock) ON ABMI.EmployeeId = MIO1.EmpInfoId 
WHERE ABMI.IsActive = 1) AS MIO ON tr.TerritoryId = MIO.TerritoryId

LEFT JOIN (SELECT MBEI.SubTerritoryId,MBE1.EmpName FROM tblMBEInfo AS MBEI with (nolock)
LEFT JOIN dbo.tblEmpGeneralInfo MBE1  with (nolock) ON MBEI.EmployeeId = MBE1.EmpInfoId
WHERE MBEI.IsActive = 1) AS MBE ON sr.SubTerritoryId = MBE.SubTerritoryId

LEFT JOIN (SELECT RD.ReturnInvoiceDetailId,ISNULL(ReturnAmount,0) ReturnAmount,INV.DeliveryTpGrandTotal,
ISNULL(TotalPaid,0) AS TotalPaid,ReturnAmount -(ISNULL(INV.DeliveryTpGrandTotal,0) - ISNULL(TotalPaid,0)) AS CustomerGet,NetAmount,
CASE WHEN ReturnAmount > 0 THEN  (((100*NetAmount)/ReturnAmount)*(ReturnAmount -(ISNULL(INV.DeliveryTpGrandTotal,0) - ISNULL(TotalPaid,0))))/100 ELSE 0 END AS SalesReturn FROM tblReturnInvoice RM
LEFT JOIN tblReturnInvoiceDetail AS RD ON RM.ReturnInvoiceId = RD.ReturnInvoiceId
INNER JOIN tblInvoice AS INV ON RM.InvoiceId = INV.InvoiceId
INNER JOIN (SELECT ReturnInvoiceId,SUM(NetAmount) AS ReturnAmount FROM tblReturnInvoiceDetail GROUP BY ReturnInvoiceId) AS D ON RM.ReturnInvoiceId = D.ReturnInvoiceId
INNER JOIN (SELECT InvoiceId,SUM(PaymentAmount) TotalPaid FROM tblCustpayDetail GROUP BY InvoiceId) AS PM ON INV.InvoiceId = PM.InvoiceId ) AS PTR ON RD.ReturnInvoiceDetailId = PTR.ReturnInvoiceDetailId

WHERE  RM.ApprovalStatus = 'Approved' 

) AS SLS WHERE SLS.InvoiceNo IS NOT NULL " + parm + " ORDER BY InvoiceNo, ProductCode";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");


//                     string query = @"SELECT * FROM (SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo ,
//mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,  
//NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
//MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,CONVERT(VARCHAR,MAS.SubmissionDate,101) Orderdate,
//I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,101) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,ID.Quantity,
//ID.Quantity*ID.UnitPrice AS GrossValue,ID.Quantity*ID.UnitVatAmount AS TotalVat,ID.DiscountAmount*-1 TotalDiscount,isnull(ID.AdjustmentAmount,0) AdjustmentAmount,
//CASE WHEN  ID.IsGiftProduct = 1 THEN (ID.UnitPrice*ID.Quantity)*-1 ELSE 0 END AS FOC, 
//CASE WHEN ID.IsGiftProduct = 1 THEN (ID.UnitVatAmount*ID.Quantity)*-1 ELSE 0 END AS VatOnFOC,
//((ID.Quantity*ID.UnitPrice) - (ISNULL(ID.DiscountAmount,0) + (isnull(ID.AdjustmentAmount,0) + CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*ID.Quantity) ELSE 0 END))) AS NetTp,
//CASE 
//WHEN ID.IsGiftProduct = 1 THEN 0
//WHEN ID.IsGiftProduct = 0 THEN 
//CAST(((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount) as decimal(10,2))  END AS NetTPVat,
//CASE 
//WHEN ID.IsGiftProduct = 1 THEN 0
//WHEN ID.IsGiftProduct = 0 THEN 
//((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
//I.DelivaryInvoiceNo,CONVERT(VARCHAR,I.UpdateDate,101) UpdateDate, CASE WHEN ISNULL(PaidAmount,0) > 0  AND ID.IsGiftProduct = 0 THEN 'PY-' + RIGHT(I.DelivaryInvoiceNo,LEN(DelivaryInvoiceNo) - 4) END AS PaymentNo,
//CASE WHEN ISNULL(PaidAmount,0) > 0 AND ID.IsGiftProduct = 0   THEN CONVERT(VARCHAR,LD.PaymentDate,101) END PaymentDate,
//CASE WHEN ISNULL(PaidAmount,0) = 0 THEN  0 
//WHEN Id.DeliveryStatus = 'Reject' THEN  0 
//WHEN ID.IsGiftProduct = 1 THEN  0
//WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(PaidAmount,0) > 0 THEN 
//CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount as decimal(10,2))
//END AS PayAmount,PaidAmount,
//CASE WHEN Id.DeliveryStatus = 'Reject' THEN  0  
//WHEN ID.IsGiftProduct = 1 THEN  0
//WHEN Id.DeliveryStatus IS NULL THEN ((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount)
//WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) = 0 THEN
//((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)
//WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) > 0
//THEN (((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)) - (((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount) 
//END AS Due,
//SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType   
//FROM tblInvoice AS I with(nolock) 
//LEFT JOIN tblUser AS USR with(nolock) ON I.UserId = USR.UserId
//LEFT JOIN tblUser AS UUSR with(nolock) ON I.UpdateBy = UUSR.LoginName
//LEFT JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId
//LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId 
//LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId 
//LEFT JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId 
//LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId 
//LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId
//LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId
//LEFT JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType
//LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON mas.NSMId = NSM.EmpInfoId 
//LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId
//LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId
//LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId
//LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock)  ON mas.MBEEmpInfoId = MBE.EmpInfoId 
//left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
//left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
//left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
//left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
//left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
//left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
//left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId
//LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryNetAmount) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId) AS IVDD ON I.InvoiceId = IVDD.InvoiceId 
//LEFT JOIN (select D.InvoiceId,DS.PaymentDate,PayType from tblInvoice as d cross apply (select top 1 P.PaymentDate,PayType from tblCustPayDetail AS PD with(nolock)
//LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
//LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId
//LEFT JOIN (SELECT InvoiceId,SUM(ISNULL(DeliveryQuantity,0)) TotalDelivery FROM tblInvoiceDetail WHERE IsGiftProduct = 0 GROUP BY InvoiceId) AS TTD ON I.InvoiceId = TTD.InvoiceId
//WHERE I.InvoiceId IS NOT NULL AND ID.Quantity > 0 
//
//UNION ALL
//
//SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,ISNULL(OrderCode,'N/A') AS OrderNo, 
//mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,NSM,DZSM,AM,MIO,MBE,
//ODR.SubmissionDate Orderdate,ISNULL(INV.InvoiceNo,'N/A') InvoiceNo,CASE WHEN INV.InvoiceDate IS NULL THEN M.ApprovedDate ELSE INV.InvoiceDate END AS InvoiceDate,
//DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
//CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,
//D.StackOutQty,D.StackOutQty*UP.UnitPrice AS GrossValue,D.StackOutQty*UP.VatAmountPerUnit AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
//(D.StackOutQty*UP.UnitPrice)*-1 AS FOC, 
//(D.StackOutQty*UP.VatAmountPerUnit)*-1 AS VatOnFOC,0 NetTp,0 NetTPVat,0 TotalDelivery,INV.DelivaryInvoiceNo,CONVERT(VARCHAR,INV.UpdateDate,101) UpdateDate,
//'' PaymentNo,'' PaymentDate,0 PayAmount,0 PaidAmount,0 Due,
//'' ProductOffer,M.EntryBy AS InvoiceBy,M.EntryBy ConfirmBy,DZSM DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'FOC/Sample Invoice'  AS InvoiceType    
//FROM tblDeStockOutDetails AS D
//LEFT JOIN tblDeStockOutMaster AS M with(nolock) ON D.DcStockOutMasterId = M.DcStockOutMasterId
//LEFT JOIN tblInvoice AS INV with(nolock) ON M.InvoiceId = INV.InvoiceId 
//LEFT JOIN tblOrder AS ODR with(nolock) ON INV.OrderId = ODR.OrderId
//LEFT JOIN tblUnitPrice AS UP with(nolock) ON D.ProductCode = UP.ProductCode
//LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerCode = M.CustomerCode
//LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON C.CustomerTypeId = ct.CustomerTypeId 
//LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = M.ComUnitId 
//left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
//left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
//left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
//left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
//left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
//left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
//LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = D.DCStoreId 
//LEFT JOIN (SELECT MBEI.SubTerritoryId,MBE.EmpMasterCode + ' : ' + MBE.EmpName MBE FROM tblMBEInfo AS MBEI with (nolock)
//LEFT JOIN tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = MBEI.SubTerritoryId
//LEFT JOIN tblEmpGeneralInfo MBE  with (nolock)  ON MBEI.EmployeeId = MBE.EmpInfoId 
//WHERE MBEI.IsActive = 1) AS MBE ON SR.SubTerritoryId = MBE.SubTerritoryId
//
//LEFT JOIN (SELECT MIOI.TerritoryId,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO FROM tblMioInfo AS MIOI with (nolock)
//left join tblTerritory tr  with (nolock) on MIOI.TerritoryId=tr.TerritoryId
//LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON MIOI.EmployeeId=MIO.EmpInfoId
//WHERE MIOI.IsActive = 1) AS MIO ON TR.TerritoryId = MIO.TerritoryId
//
//LEFT JOIN (SELECT ASMI.AreaId,AM.EmpMasterCode +' : '+ AM.EmpName AS AM FROM tblASMInfo AS ASMI with (nolock)
//left join tblArea ar  with (nolock)  on ar.AreaId = ASMI.AreaId
//LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON AM.EmpInfoId = ASMI.EmployeeId 
//WHERE ASMI.IsActive = 1) AS ASM ON AR.AreaId = ASM.AreaId
//
//LEFT JOIN (SELECT RSMI.RegionId,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM FROM tblRSMInfo AS RSMI with (nolock)
//left join tblRegion rg  with (nolock) on RSMI.RegionId = rg.RegionId
//LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock) ON RSMI.EmployeeId = DZSM.EmpInfoId 
//WHERE RSMI.IsActive = 1) AS RSM ON rg.RegionId = RSM.RegionId
//
//LEFT JOIN (SELECT NSMI.GroupId,NSM.EmpMasterCode +' : '+ NSM.EmpName NSM FROM tblNSMInfo AS NSMI with (nolock)
//left join dbo.tbl_Group gr  with (nolock) on NSMI.GroupId=gr.GroupId
//LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock) ON NSMI.EmployeeId = NSM.EmpInfoId 
//WHERE NSMI.IsActive = 1) AS NSM ON NSM.GroupId = gr.GroupId
//
//UNION ALL
//
//SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo, 
//
//mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,  
//NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
//MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,
//
//CONVERT(VARCHAR,MAS.SubmissionDate,101) Orderdate, I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,101) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
//CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,(ID.Quantity - ID.DeliveryQuantity)*-1 Quantity,
//((ID.Quantity - ID.DeliveryQuantity)*ID.UnitPrice)*-1 AS GrossValue,((ID.Quantity - ID.DeliveryQuantity)*ID.UnitVatAmount)*-1 AS TotalVat,CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0))) ELSE 0 END  TotalDiscount,Id.AdjustmentAmount*-1 AdjustmentAmount, 
//CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS FOC,  
//CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitVatAmount*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS VatOnFOC, 
//
//CASE WHEN ID.IsGiftProduct = 1 THEN 0
//WHEN ID.IsGiftProduct = 0 THEN 
//(((((ID.Quantity - ISNULL(DeliveryQuantity,0)) *ID.UnitPrice) - (ID.DiscountAmount)) - ISNULL(ID.AdjustmentAmount,0))) END *-1 AS NetTp, 
//
//CASE WHEN ID.IsGiftProduct = 1 THEN 0
//WHEN ID.IsGiftProduct = 0 THEN 
//CAST((CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0)))*-1 ELSE 0 END) + (((((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitPrice) - isnull(ID.AdjustmentAmount,0)) + ((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitVatAmount))) AS DECIMAL(18,2)) END *-1 AS NetTPVat,
//CASE 
//WHEN ID.IsGiftProduct = 1 THEN 0
//WHEN ID.IsGiftProduct = 0 THEN 
//((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
//I.DelivaryInvoiceNo, CONVERT(VARCHAR,I.UpdateDate,101) UpdateDate, NULL AS PaymentNo,NULL PaymentDate, 0 AS PayAmount,PaidAmount,
//0 AS Due,
//SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,
//USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType  
//FROM tblInvoice AS I with(nolock)  
//LEFT JOIN tblUser AS USR with(nolock) ON I.UserId = USR.UserId LEFT JOIN tblUser AS UUSR ON I.UpdateBy = UUSR.LoginName 
//LEFT JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId 
//LEFT JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId  
//LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId  
//LEFT JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId  
//LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId  
//LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId 
//LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId 
//LEFT JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType 
//LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON mas.NSMId = NSM.EmpInfoId 
//LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId 
//LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId 
//LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId 
//LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock)  ON mas.MBEEmpInfoId = MBE.EmpInfoId 
//left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
//left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
//left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
//left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
//left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
//left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
//left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId 
//LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryQuantity) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId)  AS IVDD  ON I.InvoiceId = IVDD.InvoiceId  
//LEFT JOIN (select D.InvoiceId,DS.PaymentDate from tblInvoice as d cross apply (select top 1 P.PaymentDate from tblCustPayDetail AS PD with(nolock)
//LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
//LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId 
//WHERE I.InvoiceId IS NOT NULL AND ID.DeliveryStatus IN ('Reject','Partial')
//
//UNION ALL
//
//SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,'N/A' AS OrderNo, 
//mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,NSM.EmpName,DZSM.EmpName,AM.EmpName,MIO.EmpName,MBE.EmpName,
//'' Orderdate,ISNULL(RM.ReturnInvoiceNo,'N/A') InvoiceNo,RM.ReturnInvoiceDate InvoiceDate,
//RD.ProductCode,RD.ProductName,RD.PackSize,RD.BatchNo,
//CONVERT(VARCHAR,RD.ExpDate,101) ExpDate,
//RD.Quantity,RD.Quantity * RD.UnitPrice AS GrossValue,RD.Quantity*RD.UnitVatAmount AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
//CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitPrice ELSE 0 END AS FOC, 
//CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitVatAmount ELSE 0 END AS VatOnFOC,0 NetTp,0 NetTPVat,0 TotalDelivery,'',CONVERT(VARCHAR,RM.AprovedDate,101) UpdateDate,
//'' PaymentNo,'' PaymentDate,0 PayAmount,0 PaidAmount,0 Due,
//'' ProductOffer,USR.UserName AS InvoiceBy,RM.ApprovedBy ConfirmBy,DZSM.EmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'Return Invoice'  AS InvoiceType  FROM tblReturnInvoiceDetail AS RD
//LEFT JOIN tblReturnInvoice AS RM with(nolock) ON RD.ReturnInvoiceId = RM.ReturnInvoiceId
//LEFT JOIN tblUser AS USR  with(nolock) ON RM.UserId = USR.UserId
//LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = RM.CustomerMasterId  
//LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON C.CustomerTypeId = ct.CustomerTypeId 
//LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = RM.ComUnitId 
//left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
//left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
//left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
//left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
//left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
//left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
//LEFT JOIN tblNSMInfo AS NSMI with (nolock) ON GR.GroupId = NSMI.GroupId
//LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON NSMI.EmployeeId = NSM.EmpInfoId 
//LEFT JOIN tblRSMInfo AS CLSI with (nolock) ON RG.RegionId = CLSI.RegionId
//LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock) ON CLSI.EmployeeId = DZSM.EmpInfoId 
//LEFT JOIN tblASMInfo AS RSMI ON AR.AreaId = RSMI.AreaId
//LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON RSMI.EmployeeId = AM.EmpInfoId 
//LEFT JOIN tblMIOInfo AS ABMI ON TR.TerritoryId = ABMI.TerritoryId
//LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock) ON ABMI.EmployeeId = MIO.EmpInfoId 
//LEFT JOIN tblMBEInfo AS MBEI ON SR.SubTerritoryId = MBEI.SubTerritoryId 
//LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MBEI.EmployeeId = MBE.EmpInfoId
//
//UNION ALL
//
//SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,'N/A' AS OrderNo, 
//mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryCode AS AreaCode,tr.TerritoryName AreaName,ar.AreaCode AS RegionCode,ar.AreaName RegionName,rg.RegionCode AS ZoneCode, rg.RegionName ZoneName,gr.GroupCode ,gr.GroupName,NSM.EmpName,DZSM.EmpName,AM.EmpName,MIO.EmpName,MBE.EmpName,
//'' Orderdate,ISNULL(RM.ReturnInvoiceNo,'N/A') InvoiceNo,RM.ReturnInvoiceDate InvoiceDate,
//RD.ProductCode,RD.ProductName,RD.PackSize,RD.BatchNo,
//CONVERT(VARCHAR,RD.ExpDate,101) ExpDate,
//RD.Quantity*-1 AS Quantity,(RD.Quantity * RD.UnitPrice)*-1 AS GrossValue,(RD.Quantity*RD.UnitVatAmount)*-1 AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
//CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitPrice ELSE 0 END AS FOC, 
//CASE WHEN RD.IsFoc = 1 THEN RD.Quantity*RD.UnitVatAmount ELSE 0 END AS VatOnFOC,0 NetTp,0 NetTPVat,0 TotalDelivery,'',CONVERT(VARCHAR,RM.AprovedDate,101) UpdateDate,
//'' PaymentNo,'' PaymentDate,0 PayAmount,0 PaidAmount,0 Due,
//'' ProductOffer,USR.UserName AS InvoiceBy,RM.ApprovedBy ConfirmBy,DZSM.EmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'Return Invoice'  AS InvoiceType  FROM tblReturnInvoiceDetail AS RD
//LEFT JOIN tblReturnInvoice AS RM with(nolock) ON RD.ReturnInvoiceId = RM.ReturnInvoiceId
//LEFT JOIN tblUser AS USR with(nolock) ON RM.UserId = USR.UserId
//LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = RM.CustomerMasterId  
//LEFT JOIN dbo.tblCustomerType ct  with(nolock) ON C.CustomerTypeId = ct.CustomerTypeId 
//LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = RM.ComUnitId 
//left join tblmarket mr with (nolock) on mr.MarketId = C.MarketId
//left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = mr.SubTerritoryId
//left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
//left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
//left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
//left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId
//LEFT JOIN tblNSMInfo AS NSMI with (nolock) ON GR.GroupId = NSMI.GroupId
//LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON NSMI.EmployeeId = NSM.EmpInfoId 
//LEFT JOIN tblRSMInfo AS CLSI with (nolock) ON RG.RegionId = CLSI.RegionId
//LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock) ON CLSI.EmployeeId = DZSM.EmpInfoId 
//LEFT JOIN tblASMInfo AS RSMI ON AR.AreaId = RSMI.AreaId
//LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON RSMI.EmployeeId = AM.EmpInfoId 
//LEFT JOIN tblMIOInfo AS ABMI ON TR.TerritoryId = ABMI.TerritoryId
//LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock) ON ABMI.EmployeeId = MIO.EmpInfoId 
//LEFT JOIN tblMBEInfo AS MBEI ON SR.SubTerritoryId = MBEI.SubTerritoryId 
//LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MBEI.EmployeeId = MBE.EmpInfoId
//
//) AS SLS WHERE SLS.InvoiceNo IS NOT NULL " + parm + " ORDER BY InvoiceNo, ProductCode";

//                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");





                //                string query = @"SELECT  pt. ProgramTypeName as Type, ct.CustomerType   IntransitDay,  isnull(ID.AdjustmentAmount,0) AdjustmentAmount, 
                //(ID.UnitPrice * ID.Quantity) + (ID.UnitVatAmount * ID.Quantity) as TotalNetPayable, (ID.UnitPrice *ID.Quantity) AS GrossValue,
                //(ID.UnitVatAmount * ID.Quantity) AS TotalVat,ID.DiscountAmount  AS TotalDiscount, CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName, I.OrderNo,I.FixedCustomer,
                //SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer ,((ID.UnitPrice * ID.Quantity) - isnull(ID.AdjustmentAmount,0)) TotalTP,
                //(((ID.UnitPrice * ID.Quantity) - isnull(ID.AdjustmentAmount,0)) + (ID.UnitVatAmount * ID.Quantity)) AS NetTPVat,
                // CASE WHEN SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL THEN (ID.UnitPrice*ID.Quantity)*-1 ELSE 0 END AS FOC, 
                // CASE WHEN SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL THEN (ID.UnitVatAmount*ID.Quantity)*-1 ELSE 0 END AS VatOnFOC, CASE WHEN LD.PaymentDate IS NOT NULL THEN 'PY-' + RIGHT(I.DelivaryInvoiceNo,
                //  LEN(DelivaryInvoiceNo) - 4) END AS PaymentNo,CONVERT(VARCHAR,LD.PaymentDate,103) PaymentDate, DelivaryInvoiceNo,ISNULL(DeliveryQuantity,0) DeliveryQuantity, ISNULL(ID.DeliveryNetAmount,0) DeliveryNetAmount,
                //  ISNULL(ID.DeliveryTotalPrice,0) DeliveryTotalPrice,ISNULL(ID.DeliveryTotalPriceVatAmount,0) DeliveryTotalPriceVatAmount, 
                //  ISNULL(ID.DeliveryDiscountAmount,0) DeliveryDiscountAmount,CASE WHEN DeliveryQuantity IS NULL THEN 0 ELSE ID.Quantity -ID.DeliveryQuantity END AS ReturnQty ,
                //  CASE WHEN ID.DeliveryNetAmount IS NOT NULL THEN ID.NetAmount - ISNULL(ID.DeliveryNetAmount,0) ELSE 0 END AS ReturnAmount, IVDD.TotalDeliveryQty,
                //  CASE WHEN ISNULL(I.PaymentAmount,0) > 0  THEN CASE WHEN ISNULL(I.DeliveryTpGrandTotal,0) = ISNULL(PaymentAmount,0) THEN ID.DeliveryNetAmount END ELSE 0 END  AS PayAmount ,
                //  CASE WHEN ISNULL(I.PaymentAmount,0) > 0  THEN CASE WHEN ISNULL(I.DeliveryTpGrandTotal,0) = ISNULL(PaymentAmount,0) THEN 0  END ELSE ID.DeliveryNetAmount END  AS Due,
                //  CONVERT(VARCHAR,I.OrderDate,103) OrderDate,I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,103) InvoiceDate, ID.ProductCode,ID.ProductName,ID.PackSize,ID.BatchNo, 
                //  CONVERT(VARCHAR,DS.ExpDate,103) ExpDate,ID.Quantity,DZSM.EmpMasterCode+' : '+DZSM.EmpName DZSMEmpName, ar.AreaCode AMEmpCode,ar.AreaName AMEmpName,MIO.EmpMasterCode  MIOEmpCode,  
                //  MIO.EmpName MIOEmpName , gr.GroupCode  GroupName,rg.RegionCode   RegionName,ar.AreaCode  AreaName,tr.TerritoryCode,tr.TerritoryName TerritoryName, sr.SubTerritoryCode+' : '+  sr.SubTerritoryName SubTerritoryName, mr.MarketCode, mr.MarketName MarketName,rt.RouteName FROM dbo.tblInvoice I with(nolock) INNER JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId
                //left JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId INNER JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId left JOIN dbo.tblProgramType pt  with(nolock) ON mas.ProgramTypeId = pt.ProgramTypeId
                //left JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId INNER JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId INNER JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId INNER JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId
                //left JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType
                //LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId
                //LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId
                //LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId
                //left join tblmarket mr   with (nolock) on mr.MarketId=mas.MarketId
                //left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId=mas.SubTerritoryId
                //left join tblTerritory tr  with (nolock) on mas.TerritoryId=tr.TerritoryId
                //left join tblArea ar   with (nolock)  on ar.AreaId=mas.AreaId
                //left join tblRegion rg  with (nolock) on mas.RegionId=rg.RegionId
                //left join dbo.tbl_Group gr  with (nolock) on mas.GroupId=gr.GroupId
                //left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId
                //LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryQuantity) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId) 
                //AS IVDD ON I.InvoiceId = IVDD.InvoiceId LEFT JOIN (select top 1 with ties D.InvoiceId,PaymentDate from tblCustPayDetail AS D LEFT JOIN tblCustomerPay AS M ON D.CustPayId = M.CustPayId 
                //order by row_number() over (partition by D.CustPayId order by M.Paymentdate desc)) AS LD ON I.InvoiceId = LD.InvoiceId 
                //WHERE  I.InvoiceId IS NOT NULL   " + parm + " UNION ALL SELECT  pt. ProgramTypeName as Type, ct.CustomerType   IntransitDay,  isnull(ID.AdjustmentAmount,0) AdjustmentAmount,"
                //+" (((id.Quantity*id.UnitPrice)-(ID.UnitPrice*ID.DeliveryQuantity)) + ((id.Quantity*id.UnitVatAmount)-(ID.UnitVatAmount*ID.DeliveryQuantity)))*-1 as TotalNetPayable,((id.Quantity*id.UnitPrice)-(ID.UnitPrice*ID.DeliveryQuantity))*-1 AS GrossValue,((id.Quantity*id.UnitVatAmount)-(ID.UnitVatAmount*ID.DeliveryQuantity))*-1 AS TotalVat,ID.DiscountAmount  AS TotalDiscount, CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName, I.OrderNo,I.FixedCustomer,"
                //+" SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer, ((id.Quantity*id.UnitPrice)-(ID.UnitPrice*ID.DeliveryQuantity))*-1 TotalTP, "
                //+" (((id.Quantity*id.UnitPrice)-(ID.UnitPrice*ID.DeliveryQuantity)) + ((id.Quantity*id.UnitVatAmount)-(ID.UnitVatAmount*ID.DeliveryQuantity)))*-1 AS NetTPVat,"
                //+" CASE WHEN SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL THEN (ID.UnitPrice*ID.Quantity)*-1 ELSE 0 END AS FOC, "
                //+" CASE WHEN SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL THEN (ID.UnitVatAmount*ID.Quantity)*-1 ELSE 0 END AS VatOnFOC, CASE WHEN LD.PaymentDate IS NOT NULL THEN 'PY-' + RIGHT(I.DelivaryInvoiceNo,"
                //+" LEN(DelivaryInvoiceNo) - 4) END AS PaymentNo,LD.PaymentDate, DelivaryInvoiceNo,ISNULL(DeliveryQuantity,0) DeliveryQuantity, ISNULL(ID.DeliveryNetAmount,0) DeliveryNetAmount,"
                //+" ISNULL(ID.DeliveryTotalPrice,0) DeliveryTotalPrice,ISNULL(ID.DeliveryTotalPriceVatAmount,0) DeliveryTotalPriceVatAmount, "
                //+" ISNULL(ID.DeliveryDiscountAmount,0) DeliveryDiscountAmount,CASE WHEN DeliveryQuantity IS NULL THEN 0 ELSE ID.Quantity -ID.DeliveryQuantity END AS ReturnQty ,"
                //+" CASE WHEN ID.DeliveryNetAmount IS NOT NULL THEN ID.NetAmount - ISNULL(ID.DeliveryNetAmount,0) ELSE 0 END AS ReturnAmount, IVDD.TotalDeliveryQty,"
                //+ " CASE WHEN ISNULL(I.PaymentAmount,0) > 0  THEN CASE WHEN ISNULL(I.DeliveryTpGrandTotal,0) = ISNULL(PaymentAmount,0) THEN ID.DeliveryNetAmount END ELSE 0 END  AS PayAmount ,0  AS Due ,"
                //+" CONVERT(VARCHAR,I.OrderDate,103) OrderDate,I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,103) InvoiceDate, ID.ProductCode,ID.ProductName,ID.PackSize,ID.BatchNo, "
                //+" CONVERT(VARCHAR,DS.ExpDate,103) ExpDate,ID.Quantity,DZSM.EmpMasterCode+' : '+DZSM.EmpName DZSMEmpName, ar.AreaCode AMEmpCode,ar.AreaName AMEmpName,MIO.EmpMasterCode  MIOEmpCode, " 
                //+" MIO.EmpName MIOEmpName , gr.GroupCode  GroupName,rg.RegionCode   RegionName,ar.AreaCode  AreaName,tr.TerritoryCode,tr.TerritoryName TerritoryName, sr.SubTerritoryCode+' : '+  sr.SubTerritoryName SubTerritoryName, mr.MarketCode, mr.MarketName MarketName,rt.RouteName FROM dbo.tblInvoice I with(nolock) INNER JOIN dbo.tblInvoiceDetail ID  with(nolock) ON ID.InvoiceId = I.InvoiceId"
                //+" left JOIN dbo.tblDCStore DS  with(nolock) ON DS.DCStoreId = ID.DCStoreId INNER JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId left JOIN dbo.tblProgramType pt  with(nolock) ON mas.ProgramTypeId = pt.ProgramTypeId"
                //+" left JOIN dbo.tblCustomerType ct  with(nolock) ON mas.CustTypeId = ct.CustomerTypeId INNER JOIN dbo.tblOrderDetail masdtl  with(nolock) ON ID.OrderDetailsId = masdtl.OrderDetailId INNER JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId INNER JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId"
                //+" left JOIN dbo.[tbl_BonusCampaignNewDetail] camp  with(nolock) ON camp.CampaignDetailId = masdtl.CampaignType "
                //+" LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON mas.RSMId=DZSM.EmpInfoId "
                //+" LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON mas.ASMId=AM.EmpInfoId"
                //+" LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON mas.MIOId=MIO.EmpInfoId"
                //+" left join tblmarket mr   with (nolock) on mr.MarketId=mas.MarketId"
                //+" left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId=mas.SubTerritoryId"
                //+" left join tblTerritory tr  with (nolock) on mas.TerritoryId=tr.TerritoryId"
                //+" left join tblArea ar   with (nolock)  on ar.AreaId=mas.AreaId"
                //+" left join tblRegion rg  with (nolock) on mas.RegionId=rg.RegionId"
                //+" left join dbo.tbl_Group gr  with (nolock) on mas.GroupId=gr.GroupId"
                //+" left join dbo.tblRouteInformationMaster rt  with (nolock) on mas.DistributionRouteId=rt.RouteInformationMasterId"
                //+" LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryQuantity) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId) "
                //+" AS IVDD ON I.InvoiceId = IVDD.InvoiceId LEFT JOIN (select top 1 with ties D.InvoiceId,PaymentDate from tblCustPayDetail AS D LEFT JOIN tblCustomerPay AS M ON D.CustPayId = M.CustPayId "
                //+ " order by row_number() over (partition by D.CustPayId order by M.Paymentdate desc)) AS LD ON I.InvoiceId = LD.InvoiceId "
                //+" where ID.DeliveryStatus IN ('Reject','Partial') AND  I.InvoiceId IS NOT NULL " + parm;


                //accessManager.SqlConnectionOpen(DataBase.SalesDB);
                //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                //aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                //DataTable dt = accessManager.GetDataTable("sp_Get_ProformaInvoiceReportList", aSqlParameterlist);
                //return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            //finally
            //{
            //    accessManager.SqlConnectionClose();
            //}
        }




        public DataTable Get_MIOWiseReceiveableReport(string  FrmDate, string ToDate, string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@FrmDate", FrmDate));
                aSqlParameterlist.Add(new SqlParameter("@ToDate", ToDate));
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                DataTable dt = accessManager.GetDataTable("sp_Get_MIOWiseReceiveableReport", aSqlParameterlist);
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

        public DataTable GetDeliveryReturnReportListDAL(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                DataTable dt = accessManager.GetDataTable("sp_Get_DeliveryReturnReport", aSqlParameterlist);
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


        public DataTable GetDeliveryPaymentDAL(string Parm, string Parm2)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));
                aSqlParameterlist.Add(new SqlParameter("@Parm2", Parm2));

                DataTable dt = accessManager.GetDataTable("sp_Get_AllSalesReportListParam2", aSqlParameterlist);
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
        public DataTable GetAllSalesListDAL(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));
                //aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));
                
                DataTable dt = accessManager.GetDataTable("sp_Get_AllSalesReportList", aSqlParameterlist);
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


        public DataTable GetAllPaymentReportListDAL(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", Parm));

                DataTable dt = accessManager.GetDataTable("sp_Get_AllPaymentReportList", aSqlParameterlist);
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

        public DataTable GetMonthlyExpenseList(string EmpId, string From, string To)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@EmpId", EmpId));
                aSqlParameters.Add(new SqlParameter("@frmDate", From));
                aSqlParameters.Add(new SqlParameter("@ToDate", To));
                DataTable dt = accessManager.GetDataTable("sp_Get_EmployyeMonthlyExpense", aSqlParameters);
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


        public DataTable GetDoctorWiseDayList(string Type, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                //aSqlParameters.Add(new SqlParameter("@Month", Month));
                //aSqlParameters.Add(new SqlParameter("@Year", Year));
                //aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                //aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                //DataTable dt = new DataTable();

                //dt = accessManager.GetDataTable("DynamicPivotDoctorWiseDCR", aSqlParameters);


                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@Type", Type));
                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("sp_GetDCRRXDoctorWiseRptView", aSqlParameters);

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



        public DataTable GetRXDoctorWiseDayList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotDoctorWiseRX", aSqlParameters);


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


        public DataTable GetDoctorProductWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotProductdWiseDCR", aSqlParameters);


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

        public DataTable GetDCRUserWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotUserWiseDCR", aSqlParameters);


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


        public DataTable GetRXUserWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotUserWiseRX", aSqlParameters);


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
        public DataTable GetRXDoctorProductWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotProductdWiseRX", aSqlParameters);


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


        public DataTable GetDWSPMonthlyList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DWSPDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotDWSP", aSqlParameters);


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


        public DataTable GetDWSPMonthlyList_Mew(int MonthValue, string Month, string Year, string ApprovalStatus, string RegionId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@MonthValue", MonthValue)); 
                aSqlParameters.Add(new SqlParameter("@ApprovalStatus", ApprovalStatus)); 
                aSqlParameters.Add(new SqlParameter("@RegionId", RegionId));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("sp_Process_DWSPReport", aSqlParameters);


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

        public DataTable GetDoctorBrandWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotBrandWiseDCR", aSqlParameters);


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


        public DataTable GetRXDoctorBrandWiseList(string mainDate, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));
                aSqlParameters.Add(new SqlParameter("@ListToPivot", mainDate));
                aSqlParameters.Add(new SqlParameter("@ColumnToPivot", "DcrDate"));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicPivotBrandWiseRX", aSqlParameters);


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

        public DataTable GetDatefromMonthYear(string Type, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicDatebyMonthYear", aSqlParameters);


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

        public DataTable GetDatefromMonthYearStuff(string Type, string Month, string Year)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@Month", Month));
                aSqlParameters.Add(new SqlParameter("@Year", Year));

                DataTable dt = new DataTable();

                dt = accessManager.GetDataTable("DynamicDatebyMonthYearForStuff", aSqlParameters);


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

        public DataTable GetSampleStockRptList(string EmpId, string From, string To)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@EmpId", EmpId));
                aSqlParameters.Add(new SqlParameter("@Month", To ));
                aSqlParameters.Add(new SqlParameter("@Year", From));
                DataTable dt = accessManager.GetDataTable("sp_Get_SampleStockReport", aSqlParameters);
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


        public DataTable GetEmployee_YearlyLeaveBalanceRptList(string Parm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Parm", Parm));


                DataTable dt = accessManager.GetDataTable("sp_Webapi_LeaveReport_New", aSqlParameters);
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
