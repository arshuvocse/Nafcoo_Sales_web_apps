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

namespace Library.DAL.SInventory_DAL
{
    public class SalesReportDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        private DB_Manager aDbManager = new DB_Manager();

        public void LoadTargetCategory(DropDownList ddl)
        {
            string query = @"SELECT TargetId, TargetCategory FROM tbl_Target_CategoryMaster";
            aCommonInternalDal.LoadDropDownValue(ddl, "TargetCategory", "TargetId", query, "SSIDB");

        }

        public DataTable LoadClusterHead()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetClusterHeadInfo", aSqlParameters);
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

        public DataTable LoadRsmByClusterHead(Int32 clusterheadId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@RsmId", clusterheadId));
                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetRsmInfo", aSqlParameters);
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

        public DataTable LoadAsmByRsm(Int32 rsmId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@AsmId", rsmId));
                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetMioInfo", aSqlParameters);
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

        public DataTable GetSalesData(string parm, string datePram)
        {
            try
            {
                string query = @"SELECT * FROM ( SELECT DISTINCT EGI.EmpMasterCode,sr.SubterritoryCode,EGI.EmpName,
                OD.OrderValue,SLS.InvoiceValue,RTN.ReturnValue,(CAST(((RTN.ReturnValue * 100)/(ISNULL(SLS.InvoiceValue,0) + ISNULL(RTN.ReturnValue,0))) as decimal(16,2))) AS ReturnPercentage ,TNS.OnDelivery,
                (SLS.InvoiceValue - PMT.PaymentAmount) AS CreditAmount,PMT.PaymentAmount,TRGT.TotalTargetByTpVat,
                CASE WHEN ISNULL(PMT.PaymentAmount,0) > 0 AND ISNULL(TRGT.TotalTargetByTpVat,0) > 0 THEN CAST(((PMT.PaymentAmount *100)/TRGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement,
                TRGT.Month,TRGT.Year,ODR.NSMId,ODR.RSMId,ODR.ASMId,ODR.MIOId,MBEEmpInfoId,RG.RegionId,AR.AreaId,tr.TerritoryId,sr.SubTerritoryId  FROM tblOrder AS ODR with (nolock)
                LEFT JOIN dbo.tblEmpGeneralInfo EGI with (nolock)  ON ODR.MBEEmpInfoId = EGI.EmpInfoId
                LEFT JOIN tblMBEInfo AS MB ON EGI.EmpInfoId = MB.EmployeeId
                LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock)  ON ODR.NSMId = NSM.EmpInfoId 
                LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock)   ON ODR.RSMId=DZSM.EmpInfoId
                LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON ODR.ASMId=AM.EmpInfoId
                LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON ODR.MIOId=MIO.EmpInfoId
                left join tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = MB.SubTerritoryId
                left join tblTerritory tr  with (nolock) on sr.TerritoryId=tr.TerritoryId
                left join tblArea ar   with (nolock)  on ar.AreaId=tr.AreaId
                left join tblRegion rg  with (nolock) on ar.RegionId=rg.RegionId
                left join dbo.tbl_Group gr  with (nolock) on rg.GroupId=gr.GroupId 
                LEFT JOIN (SELECT TerritoryCode,TC.TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
                LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
                LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId WHERE TerritoryCode IS NOT NULL) AS TRGT ON sr.SubterritoryCode = TRGT.TerritoryCode
                LEFT JOIN (SELECT MBE.EmpMasterCode,SUM(MAS.TotalNetPayable) OrderValue FROM tblOrder AS MAS
                LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MAS.MBEEmpInfoId = MBE.EmpInfoId
                LEFT JOIN tblCustMaster C  with(nolock) ON C.CustomerMasterId = mas.CustomerMasterId 
                LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId
                WHERE MBE.EmpMasterCode IS NOT NULL" + datePram +" GROUP BY MBE.EmpMasterCode) AS OD ON EGI.EmpMasterCode = OD.EmpMasterCode"
                + " LEFT JOIN (SELECT MBE.EmpMasterCode,SUM(I.TpGrandTotal) OnDelivery FROM tblInvoice AS I"
                + " LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId "
                + " LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MAS.MBEEmpInfoId = MBE.EmpInfoId "
                + " LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId"
                + " WHERE MBE.EmpMasterCode IS NOT NULL" + datePram + " AND DeliveryInvoiceStatus IS NULL GROUP BY MBE.EmpMasterCode) AS TNS ON EGI.EmpMasterCode = TNS.EmpMasterCode"
                + " LEFT JOIN (SELECT MBE.EmpMasterCode,SUM(I.DeliveryTpGrandTotal) InvoiceValue FROM tblInvoice AS I"
                + " LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId "
                + " LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MAS.MBEEmpInfoId = MBE.EmpInfoId "
                + " LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId"
                + " WHERE MBE.EmpMasterCode IS NOT NULL" + datePram + "  AND DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY MBE.EmpMasterCode) AS SLS ON EGI.EmpMasterCode = SLS.EmpMasterCode"
                + " LEFT JOIN (SELECT MBE.EmpMasterCode,(SUM(I.TpGrandTotal) - SUM(I.DeliveryTpGrandTotal)) AS ReturnValue FROM tblInvoice AS I with(nolock)" 
                + " LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId" 
                + " LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId"
                + " LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock)  ON mas.MBEEmpInfoId = MBE.EmpInfoId"
                + " WHERE I.InvoiceId IS NOT NULL AND MBE.EmpMasterCode IS NOT NULL" + datePram + "  GROUP BY MBE.EmpMasterCode) AS RTN ON EGI.EmpMasterCode = RTN.EmpMasterCode"
                + " LEFT JOIN (SELECT MBE.EmpMasterCode,SUM(I.PaymentAmount) PaymentAmount FROM tblInvoice AS I"
                + " LEFT JOIN dbo.tblOrder mas  with(nolock) ON I.OrderId = mas.OrderId "
                + " LEFT JOIN dbo.tblEmpGeneralInfo MBE  with (nolock) ON MAS.MBEEmpInfoId = MBE.EmpInfoId "
                + " LEFT JOIN dbo.tblCompanyUnit CU  with(nolock) ON CU.ComUnitId = mas.ComUnitId"
                + " WHERE MBE.EmpMasterCode IS NOT NULL" + datePram + "  AND DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY MBE.EmpMasterCode) AS PMT ON EGI.EmpMasterCode = PMT.EmpMasterCode"
                + " WHERE EGI.EmpMasterCode IS NOT NULL ) AS SLS WHERE SLS.EmpMasterCode IS NOT NULL  " + parm + "  ORDER BY SLS.SubterritoryCode";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetProformaInvoListDAL(string parm)
        {
            try
            {
                string query = @"SELECT * FROM (SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo ,
mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryName AreaName,ar.AreaName RegionName, rg.RegionName ZoneName, gr.GroupName, 
NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,CONVERT(VARCHAR,MAS.SubmissionDate,101) Orderdate,
I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,101) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,ID.Quantity,
ID.Quantity*ID.UnitPrice AS GrossValue,ID.Quantity*ID.UnitVatAmount AS TotalVat,ID.DiscountAmount*-1 TotalDiscount,isnull(ID.AdjustmentAmount,0) AdjustmentAmount,
CASE WHEN  ID.IsGiftProduct = 1 THEN (ID.UnitPrice*ID.Quantity)*-1 ELSE 0 END AS FOC, 
CASE WHEN ID.IsGiftProduct = 1 THEN (ID.UnitVatAmount*ID.Quantity)*-1 ELSE 0 END AS VatOnFOC,
((ID.Quantity*ID.UnitPrice) - (ISNULL(ID.DiscountAmount,0) + (isnull(ID.AdjustmentAmount,0) + CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*ID.Quantity) ELSE 0 END))) AS NetTp,
CASE WHEN ID.IsGiftProduct = 1 THEN 0 WHEN ID.IsGiftProduct = 0 THEN 
CAST(((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount) as decimal(10,2))  END AS NetTPVat,
CASE 
WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
I.DelivaryInvoiceNo,CONVERT(VARCHAR,I.UpdateDate,101) UpdateDate, CASE WHEN ISNULL(PaidAmount,0) > 0  AND ID.IsGiftProduct = 0 THEN 'PY-' + RIGHT(I.DelivaryInvoiceNo,LEN(DelivaryInvoiceNo) - 4) END AS PaymentNo,
CASE WHEN ISNULL(PaidAmount,0) > 0 AND ID.IsGiftProduct = 0   THEN CONVERT(VARCHAR,LD.PaymentDate,101) END PaymentDate,
CASE WHEN ISNULL(PaidAmount,0) = 0 THEN  0 
WHEN Id.DeliveryStatus = 'Reject' THEN  0 
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IN ('Full','Partial') AND ISNULL(PaidAmount,0) > 0 THEN 
CAST(((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount as decimal(10,2))
END AS PayAmount,PaidAmount,
CASE WHEN Id.DeliveryStatus = 'Reject' THEN  0  
WHEN ID.IsGiftProduct = 1 THEN  0
WHEN Id.DeliveryStatus IS NULL THEN ((ID.Quantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.Quantity*ID.UnitVatAmount)
WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) = 0 THEN
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)
WHEN Id.DeliveryStatus IN ('Full','Partial') AND  ISNULL(PaidAmount,0) > 0
THEN (((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount)) - (((((((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DeliveryDiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount))*100)/TotalDeliveryQty)/100)*PaidAmount) 
END AS Due,
SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType,
MAS.NSMId,MAS.RSMId,MAS.ASMId,MAS.MIOId,MBEEmpInfoId  
FROM tblInvoice AS I with(nolock) 
LEFT JOIN tblUser AS USR ON I.UserId = USR.UserId
LEFT JOIN tblUser AS UUSR ON I.UpdateBy = UUSR.LoginName
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
LEFT JOIN (select D.InvoiceId,DS.PaymentDate,PayType from tblInvoice as d cross apply (select top 1 P.PaymentDate,PayType from tblCustPayDetail AS PD 
LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId
LEFT JOIN (SELECT InvoiceId,SUM(ISNULL(DeliveryQuantity,0)) TotalDelivery FROM tblInvoiceDetail WHERE IsGiftProduct = 0 GROUP BY InvoiceId) AS TTD ON I.InvoiceId = TTD.InvoiceId
WHERE I.InvoiceId IS NOT NULL AND ID.Quantity > 0 

UNION ALL

SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,ISNULL(OrderCode,'N/A') AS OrderNo, 
mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryName AreaName,ar.AreaName RegionName, rg.RegionName ZoneName, gr.GroupName,NSM,DZSM,AM,MIO,MBE,
ODR.SubmissionDate Orderdate,ISNULL(INV.InvoiceNo,'N/A') InvoiceNo,CASE WHEN INV.InvoiceDate IS NULL THEN M.ApprovedDate ELSE INV.InvoiceDate END AS InvoiceDate,
DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,
D.StackOutQty,D.StackOutQty*UP.UnitPrice AS GrossValue,D.StackOutQty*UP.VatAmountPerUnit AS TotalVat,0 TotalDiscount,0 AdjustmentAmount,
(D.StackOutQty*UP.UnitPrice)*-1 AS FOC, 
(D.StackOutQty*UP.VatAmountPerUnit)*-1 AS VatOnFOC,0 NetTp,0 NetTPVat,0 TotalDelivery,INV.DelivaryInvoiceNo,CONVERT(VARCHAR,INV.UpdateDate,101) UpdateDate,
'' PaymentNo,'' PaymentDate,0 PayAmount,0 PaidAmount,0 Due,
'' ProductOffer,M.EntryBy AS InvoiceBy,M.EntryBy ConfirmBy,DZSM DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'FOC/Sample Invoice'  AS InvoiceType,
NSM.NSMId,RSM.RSMId,AsmEmpId,MIO.MIOId,MBE.MBEInfoId     
FROM tblDeStockOutDetails AS D
LEFT JOIN tblDeStockOutMaster AS M with(nolock) ON D.DcStockOutMasterId = M.DcStockOutMasterId
LEFT JOIN tblInvoice AS INV with(nolock) ON M.InvoiceId = INV.InvoiceId 
LEFT JOIN tblOrder AS ODR ON INV.OrderId = ODR.OrderId
LEFT JOIN tblUnitPrice AS UP ON D.ProductCode = UP.ProductCode
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
LEFT JOIN (SELECT MBEI.MBEInfoId,MBEI.SubTerritoryId,MBE.EmpMasterCode + ' : ' + MBE.EmpName MBE FROM tblMBEInfo AS MBEI with (nolock)
LEFT JOIN tblSubTerritory sr  with (nolock) on sr.SubTerritoryId = MBEI.SubTerritoryId
LEFT JOIN tblEmpGeneralInfo MBE  with (nolock)  ON MBEI.EmployeeId = MBE.EmpInfoId 
WHERE MBEI.IsActive = 1) AS MBE ON SR.SubTerritoryId = MBE.SubTerritoryId

LEFT JOIN (SELECT MIOI.MIOId,MIOI.TerritoryId,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO FROM tblMioInfo AS MIOI with (nolock)
left join tblTerritory tr  with (nolock) on MIOI.TerritoryId=tr.TerritoryId
LEFT JOIN dbo.tblEmpGeneralInfo MIO  with (nolock)  ON MIOI.EmployeeId=MIO.EmpInfoId
WHERE MIOI.IsActive = 1) AS MIO ON TR.TerritoryId = MIO.TerritoryId

LEFT JOIN (SELECT ASMI.ASMId,ASMI.AreaId,AM.EmpMasterCode +' : '+ AM.EmpName AS AM FROM tblASMInfo AS ASMI with (nolock)
left join tblArea ar  with (nolock)  on ar.AreaId = ASMI.AreaId
LEFT JOIN dbo.tblEmpGeneralInfo AM  with (nolock)  ON AM.EmpInfoId = ASMI.EmployeeId 
WHERE ASMI.IsActive = 1) AS ASM ON AR.AreaId = ASM.AreaId

LEFT JOIN (SELECT RSMI.RSMId,RSMI.RegionId,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM FROM tblRSMInfo AS RSMI with (nolock)
left join tblRegion rg  with (nolock) on RSMI.RegionId = rg.RegionId
LEFT JOIN dbo.tblEmpGeneralInfo DZSM  with (nolock) ON RSMI.EmployeeId = DZSM.EmpInfoId 
WHERE RSMI.IsActive = 1) AS RSM ON rg.RegionId = RSM.RegionId

LEFT JOIN (SELECT NSMI.NSMId,NSMI.GroupId,NSM.EmpMasterCode +' : '+ NSM.EmpName NSM FROM tblNSMInfo AS NSMI with (nolock)
left join dbo.tbl_Group gr  with (nolock) on NSMI.GroupId=gr.GroupId
LEFT JOIN dbo.tblEmpGeneralInfo NSM  with (nolock) ON NSMI.EmployeeId = NSM.EmpInfoId 
WHERE NSMI.IsActive = 1) AS NSM ON NSM.GroupId = gr.GroupId

UNION ALL

SELECT CU.ComUnitId,CU.ComUnitCode,CU.ComUnitName,C.CustomerCode,C.CustomerName,C.Address,TermOfPayment AS PayType,CT.CustomerType IntransitDay,MAS.OrderCode OrderNo, 

mr.MarketCode, mr.MarketName, sr.SubTerritoryName TerritoryName,sr.SubterritoryCode,tr.TerritoryName AreaName,ar.AreaName RegionName, rg.RegionName ZoneName, gr.GroupName, 
NSM.EmpMasterCode +' : '+ NSM.EmpName NSM,DZSM.EmpMasterCode +' : '+ DZSM.EmpName DZSM,AM.EmpMasterCode +' : '+ AM.EmpName AM,MIO.EmpMasterCode +' : '+ MIO.EmpName MIO, 
MBE.EmpMasterCode +' : '+ MBE.EmpName MBE,

CONVERT(VARCHAR,MAS.SubmissionDate,101) Orderdate, I.InvoiceNo,CONVERT(VARCHAR,I.InvoiceDate,101) InvoiceDate,DS.ProductCode,DS.ProductName,DS.PackSize,DS.BatchNo,
CONVERT(VARCHAR,DS.ExpDate,101) ExpDate,(ID.Quantity - ID.DeliveryQuantity)*-1 Quantity,
((ID.Quantity - ID.DeliveryQuantity)*ID.UnitPrice)*-1 AS GrossValue,((ID.Quantity - ID.DeliveryQuantity)*ID.UnitVatAmount)*-1 AS TotalVat,CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0))) ELSE 0 END  TotalDiscount,Id.AdjustmentAmount*-1 AdjustmentAmount, 
CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitPrice*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS FOC,  
CASE WHEN (SUBSTRING(camp.CampaignName, 1, 14) IS NOT NULL or ID.IsGiftProduct = 1) AND ID.DiscountAmount = 0 THEN (ID.UnitVatAmount*(ID.Quantity - ID.DeliveryQuantity)) ELSE 0 END AS VatOnFOC, 

CASE WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
(((((ID.Quantity - ISNULL(DeliveryQuantity,0)) *ID.UnitPrice) - (ID.DiscountAmount/ID.Quantity)) - ISNULL(ID.AdjustmentAmount,0))) END *-1 AS NetTp, 

CASE WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
CAST((CASE WHEN ID.DiscountAmount > 0 THEN ((ID.DiscountAmount/ID.Quantity)*(ID.Quantity - ISNULL(ID.DeliveryQuantity,0)))*-1 ELSE 0 END) + (((((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitPrice) - isnull(ID.AdjustmentAmount,0)) + ((ID.Quantity - ISNULL(DeliveryQuantity,0))*ID.UnitVatAmount))) AS DECIMAL(18,2)) END *-1 AS NetTPVat,
CASE 
WHEN ID.IsGiftProduct = 1 THEN 0
WHEN ID.IsGiftProduct = 0 THEN 
((ID.DeliveryQuantity*ID.UnitPrice) - ISNULL(ID.DiscountAmount,0) - isnull(ID.AdjustmentAmount,0)) + (ID.DeliveryQuantity*ID.UnitVatAmount) END AS TotalDelivery,
I.DelivaryInvoiceNo, CONVERT(VARCHAR,I.UpdateDate,101) UpdateDate, NULL AS PaymentNo,NULL PaymentDate, 0 AS PayAmount,PaidAmount,
0 AS Due,
SUBSTRING(camp.CampaignName, 1, 14)  AS ProductOffer,
USR.UserName AS InvoiceBy,UUSR.UserName ConfirmBy,DZSM.EmpMasterCode+' : '+ DZSM.EmpName DZSMEmpName,GR.GroupId,rg.RegionId,ar.AreaId,tr.TerritoryId,sr.SubTerritoryId,mr.MarketId,'General Invoice'  AS InvoiceType,
MAS.NSMId,MAS.RSMId,MAS.ASMId,MAS.MIOId,MAS.MBEEmpInfoId   
FROM tblInvoice AS I with(nolock)  
LEFT JOIN tblUser AS USR ON I.UserId = USR.UserId LEFT JOIN tblUser AS UUSR ON I.UpdateBy = UUSR.LoginName 
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
LEFT JOIN (SELECT InvoiceId,SUM(IVD.DeliveryQuantity) AS TotalDeliveryQty FROM tblInvoiceDetail AS IVD WHERE ISNULL(DeliveryTotalPrice,0) > 0 GROUP BY InvoiceId)  AS IVDD ON I.InvoiceId = IVDD.InvoiceId  
LEFT JOIN (select D.InvoiceId,DS.PaymentDate from tblInvoice as d cross apply (select top 1 P.PaymentDate from tblCustPayDetail AS PD 
LEFT JOIN tblCustomerPay AS P ON PD.CustPayId = P.CustPayId where PD.InvoiceId = d.InvoiceId order by PaymentDate desc) as ds) AS LD ON I.InvoiceId = LD.InvoiceId 
LEFT JOIN (SELECT InvoiceId,SUM(PaymentAmount) PaidAmount FROM tblCustPayDetail GROUP BY InvoiceId) AS PMT ON I.InvoiceId = PMT.InvoiceId 
WHERE I.InvoiceId IS NOT NULL AND ID.DeliveryStatus IN ('Reject','Partial')) AS SLS WHERE SLS.InvoiceNo IS NOT NULL " + parm + "  ORDER BY InvoiceNo, ProductCode";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

            }
            catch (Exception e)
            {
                throw;
            }

        }

        public DataTable LoadMioByAsm(Int32 rsmId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@MioId", rsmId));
                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetMbeInfo", aSqlParameters);
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
        public DataTable LoadProductDetailsReport()
        {
            string query = @"SELECT d.TargetDetailsId, m.TargetCategory,m.TotalTargetByTp,m.TotalTargetByTpVat, d.ProductCode, P.Description, p.PackSize, d.TargetQty, d.TpPerPack, d.TargetValueByTp, uP.VATAmountPerUnit, d.TargetValueByTpVat FROM tbl_Target_CategoryDetails AS d
                            LEFT JOIN tbl_Target_CategoryMaster m ON d.TargetId=m.TargetId
                            LEFT JOIN tblProduct p ON d.ProductCode=p.ProductCode
                            LEFT JOIN tblUnitPrice uP ON p.ProductId=uP.ProductId
                            ORDER BY d.TargetDetailsId DESC";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable LoadProductDetailsReportById(string targetID)
        {
            string query = @"SELECT d.TargetDetailsId, m.TargetCategory,m.TotalTargetByTp,m.TotalTargetByTpVat, d.ProductCode, P.Description, p.PackSize, d.TargetQty, d.TpPerPack, d.TargetValueByTp, uP.VATAmountPerUnit, d.TargetValueByTpVat FROM tbl_Target_CategoryDetails AS d
                            LEFT JOIN tbl_Target_CategoryMaster m ON d.TargetId=m.TargetId
                            LEFT JOIN tblProduct p ON d.ProductCode=p.ProductCode
                            LEFT JOIN tblUnitPrice uP ON p.ProductId=uP.ProductId
                            WHERE m.TargetId IS NOT NULL " + targetID + " ORDER BY d.TargetDetailsId DESC";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        
        public DataTable LoadProductTargetMasterEdit(string targetID)
        {
            string query = @"SELECT * FROM tbl_Target_CategoryMaster
                            WHERE TargetId='" + targetID + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadProductTargetEdit(string targetID)
        {
            string query = @"SELECT d.TargetDetailsId as TargetDetailsId, m.TargetId as TargetId,m.TargetCategory as TargetCategory, m.TotalTargetByTp as TotalTargetByTp,m.TotalTargetByTpVat as TotalTargetByTpVat, p.ProductId as ProductId, d.ProductCode as ProductCode,p.Description as Description, d.TargetQty as TargetQty,p.PackSize as PackSize, d.TpPerPack as UnitPrice,d.TargetValueByTp as TargetValue,d.VatPerPack as VATAmountPerUnit,d.TargetValueByTpVat as TargetWithVAT FROM [dbo].[tbl_Target_CategoryDetails] AS d
                            LEFT JOIN [dbo].[tbl_Target_CategoryMaster] m on d.TargetId=m.TargetId
                            LEFT JOIN tblProduct p on d.ProductCode=p.ProductCode
                            WHERE d.TargetId='" + targetID + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadProductTargetDetailsEdit(string targetID)
        {
            string query = @"SELECT * FROM tbl_Target_CategoryMaster
                            WHERE TargetId='" + targetID + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable GetSalesReport(string parameter)
        {

            try
            {
                string query = @"SELECT EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,SUM(OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue, CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(DueAmount) CreditAmount,SUM(PaymentAmount) CollectionAmount,ISNULL(TGT.TotalTargetByTpVat,0) TargetValue,
CASE WHEN ISNULL(TGT.TotalTargetByTpVat,0) > 0 THEN CAST(((SUM(PaymentAmount) *100)/TGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement
FROM (SELECT MBE.EmpMasterCode, ODR.SubmissionDate,I.InvoiceDate,ODR.GroupId,CLS.RegionId,CLS.RegionCode AS ClusterCode,RGN.AreaId,RGN.AreaCode AS RegionCode,ARA.TerritoryId,ARA.TerritoryCode AS AreaCode,
TTR.SubTerritoryId,TTR.SubTerritoryCode AS TerritoryCode,RSM.EmpInfoId AS RSMId,RSM.EmpMasterCode +' : '+  RSM.EmpName AS ClusterHead,ASM.EmpInfoId AS ASMId,
ASM.EmpMasterCode +' : '+  ASM.EmpName AS RSM,MIO.EmpInfoId AS MIOId,MIO.EmpMasterCode +' : '+  MIO.EmpName AS ASM,
MBE.EmpInfoId AS MBEId,MBE.EmpMasterCode +' : '+  MBE.EmpName AS MBE,ISNULL(OV.OrderValue,0) AS OrderValue,ISNULL(PV.ProformaValue,0) AS ProformaValue,
ISNULL(IV.InvoiceValue,0) AS InvoiceValue,ISNULL(ODV.OnDelivery,0) AS OnDelivery, ISNULL(RV.ReturnValue,0) ReturnValue,(ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END) PaymentAmount,
(ISNULL(IV.InvoiceValue,0) - (ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END)) AS DueAmount FROM tblInvoice AS I with (nolock)
LEFT JOIN tblOrder AS ODR with (nolock) ON I.OrderId = ODR.OrderId
LEFT JOIN tblRegion AS CLS with (nolock) ON ODR.RegionCode_Ord = CLS.RegionCode
LEFT JOIN tblArea AS RGN with (nolock) ON ODR.AreaCode_Ord = RGN.AreaCode 
LEFT JOIN tblTerritory AS ARA with (nolock) ON ODR.TerritoryCode_Ord  = ARA.TerritoryCode
LEFT JOIN tblSubTerritory AS TTR with (nolock) ON ODR.SubTerritoryCode_Ord = TTR.SubTerritoryCode 
LEFT JOIN tblEmpGeneralInfo AS MBE with (nolock) ON ODR.MBEEmpInfoId = MBE.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS MIO with (nolock) ON ODR.MIOId = MIO.EmpInfoId 
LEFT JOIN tblEmpGeneralInfo AS ASM with (nolock) ON ODR.ASMId = ASM.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS RSM with (nolock) ON ODR.RSMId = RSM.EmpInfoId 
LEFT JOIN (SELECT T.InvoiceId,SUM(ODRD.TotalTradePrice) OrderValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId 
LEFT JOIN tblOrderDetail AS ODRD ON TID.OrderDetailsId = ODRD.OrderDetailId GROUP BY T.InvoiceId) AS OV ON I.InvoiceId = OV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) AS ProformaValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId GROUP BY T.InvoiceId) AS PV ON I.InvoiceId = PV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.DeliveryTotalPrice) InvoiceValue,SUM(TID.DeliveryTotalPriceVatAmount) InvoiceVat FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY T.InvoiceId) AS IV ON I.InvoiceId = IV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) OnDelivery FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IS NULL GROUP BY T.InvoiceId) AS ODV ON I.InvoiceId = ODV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice-ISNULL(TID.DeliveryTotalPrice,0)) ReturnValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Reject','Partial') GROUP BY T.InvoiceId) AS RV ON I.InvoiceId = RV.InvoiceId
LEFT JOIN (SELECT CP.InvoiceId,SUM(CP.PaymentAmount) AS PaymentAmount FROM tblCustPayDetail AS CP GROUP BY CP.InvoiceId) AS PT ON I.InvoiceId = PT.InvoiceId) AS SLS 
LEFT JOIN (SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL ) AS TGT ON SLS.TerritoryCode = TGT.TerritoryCode
WHERE  ClusterCode IS NOT NULL  " + parameter + "  GROUP BY EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,TGT.TotalTargetByTpVat ORDER BY ClusterHead,RSM,ASM,MBE";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }

        public DataTable GetSalesReportByClusterHead(string parameter,string pram2)
        {

            try
            {
                string query = @"SELECT CLSH.ClusterCode AS FieldName,CLSH.ClusterHead AS FieldForceName,SUM(CLSH.OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue,CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(CreditAmount) CreditAmount,SUM(CollectionAmount) CollectionAmount,SUM(ISNULL(TargetValue,0)) TargetValue,
CASE WHEN SUM(ISNULL(TargetValue,0)) > 0 THEN CAST(((SUM(CollectionAmount) *100)/SUM(ISNULL(TargetValue,0))) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement FROM (SELECT EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,SUM(OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue, CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(DueAmount) CreditAmount,SUM(PaymentAmount) CollectionAmount,ISNULL(TGT.TotalTargetByTpVat,0) TargetValue,
CASE WHEN ISNULL(TGT.TotalTargetByTpVat,0) > 0 THEN CAST(((SUM(PaymentAmount) *100)/TGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement
FROM (SELECT MBE.EmpMasterCode, ODR.SubmissionDate,I.InvoiceDate,ODR.GroupId,CLS.RegionId,CLS.RegionCode AS ClusterCode,RGN.AreaId,RGN.AreaCode AS RegionCode,ARA.TerritoryId,ARA.TerritoryCode AS AreaCode,
TTR.SubTerritoryId,TTR.SubTerritoryCode AS TerritoryCode,RSM.EmpInfoId AS RSMId,RSM.EmpMasterCode +' : '+  RSM.EmpName AS ClusterHead,ASM.EmpInfoId AS ASMId,
ASM.EmpMasterCode +' : '+  ASM.EmpName AS RSM,MIO.EmpInfoId AS MIOId,MIO.EmpMasterCode +' : '+  MIO.EmpName AS ASM,
MBE.EmpInfoId AS MBEId,MBE.EmpMasterCode +' : '+  MBE.EmpName AS MBE,ISNULL(OV.OrderValue,0) AS OrderValue,ISNULL(PV.ProformaValue,0) AS ProformaValue,
ISNULL(IV.InvoiceValue,0) AS InvoiceValue,ISNULL(ODV.OnDelivery,0) AS OnDelivery, ISNULL(RV.ReturnValue,0) ReturnValue,(ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END) PaymentAmount,
(ISNULL(IV.InvoiceValue,0) - (ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END)) AS DueAmount FROM tblInvoice AS I with (nolock)
LEFT JOIN tblOrder AS ODR with (nolock) ON I.OrderId = ODR.OrderId
LEFT JOIN tblRegion AS CLS with (nolock) ON ODR.RegionCode_Ord = CLS.RegionCode
LEFT JOIN tblArea AS RGN with (nolock) ON ODR.AreaCode_Ord = RGN.AreaCode 
LEFT JOIN tblTerritory AS ARA with (nolock) ON ODR.TerritoryCode_Ord  = ARA.TerritoryCode
LEFT JOIN tblSubTerritory AS TTR with (nolock) ON ODR.SubTerritoryCode_Ord = TTR.SubTerritoryCode 
LEFT JOIN tblEmpGeneralInfo AS MBE with (nolock) ON ODR.MBEEmpInfoId = MBE.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS MIO with (nolock) ON ODR.MIOId = MIO.EmpInfoId 
LEFT JOIN tblEmpGeneralInfo AS ASM with (nolock) ON ODR.ASMId = ASM.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS RSM with (nolock) ON ODR.RSMId = RSM.EmpInfoId 
LEFT JOIN (SELECT T.InvoiceId,SUM(ODRD.TotalTradePrice) OrderValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId 
LEFT JOIN tblOrderDetail AS ODRD ON TID.OrderDetailsId = ODRD.OrderDetailId GROUP BY T.InvoiceId) AS OV ON I.InvoiceId = OV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) AS ProformaValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId GROUP BY T.InvoiceId) AS PV ON I.InvoiceId = PV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.DeliveryTotalPrice) InvoiceValue,SUM(TID.DeliveryTotalPriceVatAmount) InvoiceVat FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY T.InvoiceId) AS IV ON I.InvoiceId = IV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) OnDelivery FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IS NULL GROUP BY T.InvoiceId) AS ODV ON I.InvoiceId = ODV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice-ISNULL(TID.DeliveryTotalPrice,0)) ReturnValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Reject','Partial') GROUP BY T.InvoiceId) AS RV ON I.InvoiceId = RV.InvoiceId
LEFT JOIN (SELECT CP.InvoiceId,SUM(CP.PaymentAmount) AS PaymentAmount FROM tblCustPayDetail AS CP GROUP BY CP.InvoiceId) AS PT ON I.InvoiceId = PT.InvoiceId) AS SLS 
LEFT JOIN (SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL ) AS TGT ON SLS.TerritoryCode = TGT.TerritoryCode
WHERE  ClusterCode IS NOT NULL " + parameter + " GROUP BY EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,TGT.TotalTargetByTpVat ) AS CLSH WHERE CLSH.ClusterCode IS NOT NULL " + pram2 + " GROUP BY CLSH.ClusterCode,CLSH.ClusterHead ";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }


        public DataTable CheckTargetMonth(string parameter)
        {

            try
            {
                string query = @"SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
                LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
                LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL "+parameter+" ";
                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }


        public DataTable GetSalesReportByRBM(string parameter,string clusterCode)
        {

            try
            {
                string query = @"SELECT CLSH.RegionCode AS FieldName,CLSH.RSM AS FieldForceName,SUM(CLSH.OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue,CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(CreditAmount) CreditAmount,SUM(CollectionAmount) CollectionAmount,SUM(ISNULL(TargetValue,0)) TargetValue,
CASE WHEN SUM(ISNULL(TargetValue,0)) > 0 THEN CAST(((SUM(CollectionAmount) *100)/SUM(ISNULL(TargetValue,0))) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement FROM (SELECT EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,SUM(OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue, CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(DueAmount) CreditAmount,SUM(PaymentAmount) CollectionAmount,ISNULL(TGT.TotalTargetByTpVat,0) TargetValue,
CASE WHEN ISNULL(TGT.TotalTargetByTpVat,0) > 0 THEN CAST(((SUM(PaymentAmount) *100)/TGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement
FROM (SELECT MBE.EmpMasterCode, ODR.SubmissionDate,I.InvoiceDate,ODR.GroupId,CLS.RegionId,CLS.RegionCode AS ClusterCode,RGN.AreaId,RGN.AreaCode AS RegionCode,ARA.TerritoryId,ARA.TerritoryCode AS AreaCode,
TTR.SubTerritoryId,TTR.SubTerritoryCode AS TerritoryCode,RSM.EmpInfoId AS RSMId,RSM.EmpMasterCode +' : '+  RSM.EmpName AS ClusterHead,ASM.EmpInfoId AS ASMId,
ASM.EmpMasterCode +' : '+  ASM.EmpName AS RSM,MIO.EmpInfoId AS MIOId,MIO.EmpMasterCode +' : '+  MIO.EmpName AS ASM,
MBE.EmpInfoId AS MBEId,MBE.EmpMasterCode +' : '+  MBE.EmpName AS MBE,ISNULL(OV.OrderValue,0) AS OrderValue,ISNULL(PV.ProformaValue,0) AS ProformaValue,
ISNULL(IV.InvoiceValue,0) AS InvoiceValue,ISNULL(ODV.OnDelivery,0) AS OnDelivery, ISNULL(RV.ReturnValue,0) ReturnValue,(ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END) PaymentAmount,
(ISNULL(IV.InvoiceValue,0) - (ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END)) AS DueAmount FROM tblInvoice AS I with (nolock)
LEFT JOIN tblOrder AS ODR with (nolock) ON I.OrderId = ODR.OrderId
LEFT JOIN tblRegion AS CLS with (nolock) ON ODR.RegionCode_Ord = CLS.RegionCode
LEFT JOIN tblArea AS RGN with (nolock) ON ODR.AreaCode_Ord = RGN.AreaCode 
LEFT JOIN tblTerritory AS ARA with (nolock) ON ODR.TerritoryCode_Ord  = ARA.TerritoryCode
LEFT JOIN tblSubTerritory AS TTR with (nolock) ON ODR.SubTerritoryCode_Ord = TTR.SubTerritoryCode 
LEFT JOIN tblEmpGeneralInfo AS MBE with (nolock) ON ODR.MBEEmpInfoId = MBE.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS MIO with (nolock) ON ODR.MIOId = MIO.EmpInfoId 
LEFT JOIN tblEmpGeneralInfo AS ASM with (nolock) ON ODR.ASMId = ASM.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS RSM with (nolock) ON ODR.RSMId = RSM.EmpInfoId 
LEFT JOIN (SELECT T.InvoiceId,SUM(ODRD.TotalTradePrice) OrderValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId 
LEFT JOIN tblOrderDetail AS ODRD ON TID.OrderDetailsId = ODRD.OrderDetailId GROUP BY T.InvoiceId) AS OV ON I.InvoiceId = OV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) AS ProformaValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId GROUP BY T.InvoiceId) AS PV ON I.InvoiceId = PV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.DeliveryTotalPrice) InvoiceValue,SUM(TID.DeliveryTotalPriceVatAmount) InvoiceVat FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY T.InvoiceId) AS IV ON I.InvoiceId = IV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) OnDelivery FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IS NULL GROUP BY T.InvoiceId) AS ODV ON I.InvoiceId = ODV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice-ISNULL(TID.DeliveryTotalPrice,0)) ReturnValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Reject','Partial') GROUP BY T.InvoiceId) AS RV ON I.InvoiceId = RV.InvoiceId
LEFT JOIN (SELECT CP.InvoiceId,SUM(CP.PaymentAmount) AS PaymentAmount FROM tblCustPayDetail AS CP GROUP BY CP.InvoiceId) AS PT ON I.InvoiceId = PT.InvoiceId) AS SLS 
LEFT JOIN (SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL ) AS TGT ON SLS.TerritoryCode = TGT.TerritoryCode
WHERE  ClusterCode IS NOT NULL " + parameter + " GROUP BY EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,TGT.TotalTargetByTpVat ) AS CLSH WHERE  CLSH.RegionCode IS NOT NULL " + clusterCode + " GROUP BY CLSH.RegionCode,CLSH.RSM ";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }


        public DataTable GetSalesReportByABM(string parameter, string regionCode)
        {

            try
            {
                string query = @"SELECT CLSH.AreaCode AS FieldName,CLSH.ASM AS FieldForceName,SUM(CLSH.OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue,CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(CreditAmount) CreditAmount,SUM(CollectionAmount) CollectionAmount,SUM(ISNULL(TargetValue,0)) TargetValue,
CASE WHEN SUM(ISNULL(TargetValue,0)) > 0 THEN CAST(((SUM(CollectionAmount) *100)/SUM(ISNULL(TargetValue,0))) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement FROM (SELECT EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,SUM(OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue, CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(DueAmount) CreditAmount,SUM(PaymentAmount) CollectionAmount,ISNULL(TGT.TotalTargetByTpVat,0) TargetValue,
CASE WHEN ISNULL(TGT.TotalTargetByTpVat,0) > 0 THEN CAST(((SUM(PaymentAmount) *100)/TGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement
FROM (SELECT MBE.EmpMasterCode, ODR.SubmissionDate,I.InvoiceDate,ODR.GroupId,CLS.RegionId,CLS.RegionCode AS ClusterCode,RGN.AreaId,RGN.AreaCode AS RegionCode,ARA.TerritoryId,ARA.TerritoryCode AS AreaCode,
TTR.SubTerritoryId,TTR.SubTerritoryCode AS TerritoryCode,RSM.EmpInfoId AS RSMId,RSM.EmpMasterCode +' : '+  RSM.EmpName AS ClusterHead,ASM.EmpInfoId AS ASMId,
ASM.EmpMasterCode +' : '+  ASM.EmpName AS RSM,MIO.EmpInfoId AS MIOId,MIO.EmpMasterCode +' : '+  MIO.EmpName AS ASM,
MBE.EmpInfoId AS MBEId,MBE.EmpMasterCode +' : '+  MBE.EmpName AS MBE,ISNULL(OV.OrderValue,0) AS OrderValue,ISNULL(PV.ProformaValue,0) AS ProformaValue,
ISNULL(IV.InvoiceValue,0) AS InvoiceValue,ISNULL(ODV.OnDelivery,0) AS OnDelivery, ISNULL(RV.ReturnValue,0) ReturnValue,(ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END) PaymentAmount,
(ISNULL(IV.InvoiceValue,0) - (ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END)) AS DueAmount FROM tblInvoice AS I with (nolock)
LEFT JOIN tblOrder AS ODR with (nolock) ON I.OrderId = ODR.OrderId
LEFT JOIN tblRegion AS CLS with (nolock) ON ODR.RegionCode_Ord = CLS.RegionCode
LEFT JOIN tblArea AS RGN with (nolock) ON ODR.AreaCode_Ord = RGN.AreaCode 
LEFT JOIN tblTerritory AS ARA with (nolock) ON ODR.TerritoryCode_Ord  = ARA.TerritoryCode
LEFT JOIN tblSubTerritory AS TTR with (nolock) ON ODR.SubTerritoryCode_Ord = TTR.SubTerritoryCode 
LEFT JOIN tblEmpGeneralInfo AS MBE with (nolock) ON ODR.MBEEmpInfoId = MBE.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS MIO with (nolock) ON ODR.MIOId = MIO.EmpInfoId 
LEFT JOIN tblEmpGeneralInfo AS ASM with (nolock) ON ODR.ASMId = ASM.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS RSM with (nolock) ON ODR.RSMId = RSM.EmpInfoId 
LEFT JOIN (SELECT T.InvoiceId,SUM(ODRD.TotalTradePrice) OrderValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId 
LEFT JOIN tblOrderDetail AS ODRD ON TID.OrderDetailsId = ODRD.OrderDetailId GROUP BY T.InvoiceId) AS OV ON I.InvoiceId = OV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) AS ProformaValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId GROUP BY T.InvoiceId) AS PV ON I.InvoiceId = PV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.DeliveryTotalPrice) InvoiceValue,SUM(TID.DeliveryTotalPriceVatAmount) InvoiceVat FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY T.InvoiceId) AS IV ON I.InvoiceId = IV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) OnDelivery FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IS NULL GROUP BY T.InvoiceId) AS ODV ON I.InvoiceId = ODV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice-ISNULL(TID.DeliveryTotalPrice,0)) ReturnValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Reject','Partial') GROUP BY T.InvoiceId) AS RV ON I.InvoiceId = RV.InvoiceId
LEFT JOIN (SELECT CP.InvoiceId,SUM(CP.PaymentAmount) AS PaymentAmount FROM tblCustPayDetail AS CP GROUP BY CP.InvoiceId) AS PT ON I.InvoiceId = PT.InvoiceId) AS SLS 
LEFT JOIN (SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL ) AS TGT ON SLS.TerritoryCode = TGT.TerritoryCode
WHERE  ClusterCode IS NOT NULL " + parameter + " GROUP BY EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBE,TGT.TotalTargetByTpVat ) AS CLSH WHERE CLSH.AreaCode IS NOT NULL " + regionCode + " GROUP BY CLSH.AreaCode,CLSH.ASM ";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }


        public DataTable GetSalesReportByMBE(string parameter, string regionCode,string targetMonth)
        {

            try
            {
                string query = @"SELECT ClusterCode,RegionCode,AreaCode,CLSH.TerritoryCode AS FieldName,MBECode,CLSH.MBE AS FieldForceName,SUM(CLSH.OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue,CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(CreditAmount) CreditAmount,SUM(CollectionAmount) CollectionAmount,SUM(ISNULL(TargetValue,0)) TargetValue,
CASE WHEN SUM(ISNULL(TargetValue,0)) > 0 THEN CAST(((SUM(CollectionAmount) *100)/SUM(ISNULL(TargetValue,0))) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement FROM (SELECT EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBECode,MBE,SUM(OrderValue) AS OrderValue,SUM(ProformaValue) AS ProformaValue,SUM(InvoiceValue) InvoiceValue,
SUM(ReturnValue) ReturnValue, CASE WHEN SUM(ReturnValue) > 0 THEN (CAST(((SUM(ReturnValue) * 100)/(SUM(InvoiceValue) + ISNULL(SUM(ReturnValue),0))) as decimal(16,2))) 
ELSE CAST(0 as decimal(16,2)) END AS ReturnPercentage,SUM(OnDelivery) OnDelivery,SUM(DueAmount) CreditAmount,SUM(PaymentAmount) CollectionAmount,ISNULL(TGT.TotalTargetByTpVat,0) TargetValue,
CASE WHEN ISNULL(TGT.TotalTargetByTpVat,0) > 0 THEN CAST(((SUM(PaymentAmount) *100)/TGT.TotalTargetByTpVat) as decimal(16,2))  ELSE CAST(0 as decimal(16,2)) END AS Achivement
FROM (SELECT MBE.EmpMasterCode, ODR.SubmissionDate,I.InvoiceDate,ODR.GroupId,CLS.RegionId,CLS.RegionCode AS ClusterCode,RGN.AreaId,RGN.AreaCode AS RegionCode,ARA.TerritoryId,ARA.TerritoryCode AS AreaCode,
TTR.SubTerritoryId,TTR.SubTerritoryCode AS TerritoryCode,RSM.EmpInfoId AS RSMId,RSM.EmpMasterCode +' : '+  RSM.EmpName AS ClusterHead,ASM.EmpInfoId AS ASMId,
ASM.EmpMasterCode +' : '+  ASM.EmpName AS RSM,MIO.EmpInfoId AS MIOId,MIO.EmpMasterCode +' : '+  MIO.EmpName AS ASM,
MBE.EmpInfoId AS MBEId,MBE.EmpMasterCode AS MBECode,MBE.EmpName AS MBE,ISNULL(OV.OrderValue,0) AS OrderValue,ISNULL(PV.ProformaValue,0) AS ProformaValue,
ISNULL(IV.InvoiceValue,0) AS InvoiceValue,ISNULL(ODV.OnDelivery,0) AS OnDelivery, ISNULL(RV.ReturnValue,0) ReturnValue,(ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END) PaymentAmount,
(ISNULL(IV.InvoiceValue,0) - (ISNULL(PT.PaymentAmount,0) - CASE WHEN  ISNULL(PT.PaymentAmount,0) > 0 THEN InvoiceVat ELSE 0 END)) AS DueAmount FROM tblInvoice AS I with (nolock)
LEFT JOIN tblOrder AS ODR with (nolock) ON I.OrderId = ODR.OrderId
LEFT JOIN tblRegion AS CLS with (nolock) ON ODR.RegionCode_Ord = CLS.RegionCode
LEFT JOIN tblArea AS RGN with (nolock) ON ODR.AreaCode_Ord = RGN.AreaCode 
LEFT JOIN tblTerritory AS ARA with (nolock) ON ODR.TerritoryCode_Ord  = ARA.TerritoryCode
LEFT JOIN tblSubTerritory AS TTR with (nolock) ON ODR.SubTerritoryCode_Ord = TTR.SubTerritoryCode 
LEFT JOIN tblEmpGeneralInfo AS MBE with (nolock) ON ODR.MBEEmpInfoId = MBE.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS MIO with (nolock) ON ODR.MIOId = MIO.EmpInfoId 
LEFT JOIN tblEmpGeneralInfo AS ASM with (nolock) ON ODR.ASMId = ASM.EmpInfoId
LEFT JOIN tblEmpGeneralInfo AS RSM with (nolock) ON ODR.RSMId = RSM.EmpInfoId 
LEFT JOIN (SELECT T.InvoiceId,SUM(ODRD.TotalTradePrice) OrderValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId 
LEFT JOIN tblOrderDetail AS ODRD ON TID.OrderDetailsId = ODRD.OrderDetailId GROUP BY T.InvoiceId) AS OV ON I.InvoiceId = OV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) AS ProformaValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId GROUP BY T.InvoiceId) AS PV ON I.InvoiceId = PV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.DeliveryTotalPrice) InvoiceValue,SUM(TID.DeliveryTotalPriceVatAmount) InvoiceVat FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Full','Partial') GROUP BY T.InvoiceId) AS IV ON I.InvoiceId = IV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice) OnDelivery FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IS NULL GROUP BY T.InvoiceId) AS ODV ON I.InvoiceId = ODV.InvoiceId
LEFT JOIN (SELECT T.InvoiceId,SUM(TID.TotalPrice-ISNULL(TID.DeliveryTotalPrice,0)) ReturnValue FROM tblInvoice AS T
LEFT JOIN tblInvoiceDetail AS TID ON T.InvoiceId = TID.InvoiceId
LEFT JOIN tblOrder AS TODR ON T.OrderId = TODR.OrderId WHERE DeliveryInvoiceStatus IN ('Reject','Partial') GROUP BY T.InvoiceId) AS RV ON I.InvoiceId = RV.InvoiceId
LEFT JOIN (SELECT CP.InvoiceId,SUM(CP.PaymentAmount) AS PaymentAmount FROM tblCustPayDetail AS CP GROUP BY CP.InvoiceId) AS PT ON I.InvoiceId = PT.InvoiceId) AS SLS 
LEFT JOIN (SELECT DISTINCT TerritoryCode,TC.TotalTargetByTp TotalTargetByTpVat,TG.Year,TG.Month FROM tbl_Target_MIOWiseTargetSetup AS TGD
LEFT JOIN tbl_Target_MIOTargetSetupMaster AS TG ON TGD.MioTargetMasterId = TG.MioTargetMasterId
LEFT JOIN tbl_Target_CategoryMaster AS TC ON TGD.TargetCategoryId = TC.TargetId  WHERE TerritoryCode IS NOT NULL " + targetMonth + " ) AS TGT ON SLS.TerritoryCode = TGT.TerritoryCode WHERE  ClusterCode IS NOT NULL " + parameter + " GROUP BY EmpMasterCode,ClusterCode,ClusterHead,RegionCode,RSM,AreaCode,ASM,SLS.TerritoryCode,MBECode,MBE,TGT.TotalTargetByTpVat ) AS CLSH WHERE CLSH.TerritoryCode IS NOT NULL " + regionCode + " GROUP BY ClusterCode,RegionCode,AreaCode,CLSH.TerritoryCode,MBECode,CLSH.MBE";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                //accessManager.SqlConnectionClose();
            }

        }
    }
}
