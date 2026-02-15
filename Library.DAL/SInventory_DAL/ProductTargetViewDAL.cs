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
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class ProductTargetViewDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();
        private DB_Manager aDbManager = new DB_Manager();

        public DataTable LoadProductTargetView(string pram)
        {
            string query = @"SELECT M.TargetId,M.TargetCategory, M.TotalTargetByTp, M.TotalTargetByTpVat, M.EntryDate,
                             EGI.EmpName + ' (' + EGI.EmpMasterCode + ')' AS EntryBy,EGI2.EmpName + ' (' + EGI2.EmpMasterCode + ')' AS UpdateBy,
                             M.UpdatedDate FROM tbl_Target_CategoryMaster AS M
                             LEFT JOIN tblUser AS USR ON M.EntryBy = USR.UserId
                             LEFT JOIN tblUser AS USR2 ON M.UpdatedBy = USR2.UserId
                             LEFT JOIN tblEmpGeneralinfo AS EGI ON USR.EmpInfoid = EGI.EmpInfoId
                             LEFT JOIN tblEmpGeneralinfo AS EGI2 ON USR2.EmpInfoid = EGI2.EmpInfoId
                             WHERE M.TargetId IS NOT NULL " + pram + " ORDER BY M.EntryDate DESC"; ;

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        
        public DataTable LoadProductTargetDetailView(string pram)
        {
            string query = @"SELECT M.TargetCategory,PD.ProductCode,PD.ProductName,PKS.PackSizeName,D.TargetQty,
                             D.TpPerPack,D.VatPerPack,D.TargetValueByTp,D.TargetValueByTpVat
                             FROM tbl_Target_CategoryMaster AS M
                             LEFT JOIN tbl_Target_CategoryDetails AS D ON M.TargetId = D.TargetId
                             LEFT JOIN tblProduct AS PD ON D.ProductCode = PD.ProductCode
                             LEFT JOIN tblPackSize AS PKS ON PD.PackSizeId = PKS.PackSizeId
                             LEFT JOIN tblUser AS USR ON M.EntryBy = USR.UserId
                             LEFT JOIN tblUser AS USR2 ON M.UpdatedBy = USR2.UserId
                             LEFT JOIN tblEmpGeneralinfo AS EGI ON USR.EmpInfoid = EGI.EmpInfoId
                             LEFT JOIN tblEmpGeneralinfo AS EGI2 ON USR2.EmpInfoid = EGI2.EmpInfoId
                             WHERE M.TargetId IS NOT NULL  " + pram + " ORDER BY M.EntryDate DESC";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
