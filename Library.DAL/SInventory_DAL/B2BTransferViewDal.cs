using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class B2BTransferViewDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       

        public DataTable GetB2bTransferInfo(string prameter)
        {
            string query = @"SELECT ISNULL(FUE.EmpMasterCode,'') EmpMasterCode, ISNULL(FUE.EmpName,FU.UserName) AS ForwardBy, FORMAT(ForwardDate, 'dd-MMM-yyyy hh:mm:ss tt') ForwardDate, ISNULL(RVUE.EmpName,RVU.UserName) AS ReceiveByBy,
ISNULL(RVUE.EmpMasterCode,'') ReceiveEmpMasterCode,FORMAT(tblChalanInfo.ReceiveDate, 'dd-MMM-yyyy hh:mm:ss tt')  AS ChalanReceiveDate ,ChalanId,ChalanNo,ChalanDate,FromComUnitName,ToComUnitName,TotalValue,TotalVat,GrandTotal,
CASE WHEN IsDeliver = 'True' THEN 'Received' ELSE 'Pending' END AS Status
FROM dbo.tblChalanInfo 
LEFT JOIN tblUser AS FU ON tblChalanInfo.ForwardBy = FU.UserId
LEFT JOIN tblEmpGeneralInfo AS FUE ON FU.EmpInfoId = FUE.EmpInfoId
LEFT JOIN tblUser AS RVU ON tblChalanInfo.ReceiveBy = RVU.UserId
LEFT JOIN tblEmpGeneralInfo AS RVUE ON RVU.EmpInfoId = RVUE.EmpInfoId							 
WHERE ChalanId IS NOT NULL " + prameter + " ORDER BY Status";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }


        public DataTable GetB2bTransferInfoDetails(string prameter)
        {
            string query = @"SELECT ISNULL(FUE.EmpMasterCode,'') EmpMasterCode, ISNULL(FUE.EmpName,FU.UserName) AS ForwardBy, FORMAT(ForwardDate, 'dd-MMM-yyyy hh:mm:ss tt') ForwardDate, ISNULL(RVUE.EmpName,RVU.UserName) AS ReceiveByBy,
ISNULL(RVUE.EmpMasterCode,'') ReceiveEmpMasterCode,
FORMAT(CM.ReceiveDate, 'dd-MMM-yyyy hh:mm:ss tt')  AS ChalanReceiveDate ,CM.ChalanId,ChalanNo,ChalanDate,FromComUnitName,ToComUnitName,TotalValue,TotalVat,GrandTotal,
CASE WHEN IsDeliver = 'True' THEN 'Received' ELSE 'Pending' END AS Status, CD.ProductCode,CD.ProductName,BatchNo,Quantity,UOM.StockUOMName
FROM dbo.tblChalanInfo  CM
LEFT JOIN tblChalanDetail AS CD ON CM.ChalanId = CD.ChalanId
LEFT JOIN tblProduct AS PD ON CD.ProductCode = PD.ProductCode
LEFT JOIN tblStockUOM AS UOM ON PD.StockUOMId = UOM.StockUOMId
LEFT JOIN tblUser AS FU ON CM.ForwardBy = FU.UserId 
LEFT JOIN tblEmpGeneralInfo AS FUE ON FU.EmpInfoId = FUE.EmpInfoId
LEFT JOIN tblUser AS RVU ON CM.ReceiveBy = RVU.UserId
LEFT JOIN tblEmpGeneralInfo AS RVUE ON RVU.EmpInfoId = RVUE.EmpInfoId							 
WHERE CM.ChalanId IS NOT NULL  " + prameter + " ORDER BY Status";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public bool DeleteChallanMasterById(int chalanId)
        {
            string query = @"DELETE FROM dbo.tblChalanInfo WHERE ChalanId = " + chalanId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        }

        //public bool DeleteChallanDetailById(int chalanId)
        //{
        //    string query = @"DELETE FROM dbo.tblChalanDetail WHERE ChalanId = " + chalanId;
        //    return aCommonInternalDal.DeleteDataByDeleteCommand(query, "SSIDB");
        //}


        public bool DeleteChallanDetailById(int chalanId)
        {
            var aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@chalanId", chalanId));

            return aCommonInternalDal.DeleteAction("sp_Del_B2BDelete", aSqlParameters);
        }
    }
}
