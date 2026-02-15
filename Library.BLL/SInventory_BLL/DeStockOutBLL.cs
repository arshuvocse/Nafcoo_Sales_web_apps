using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

namespace Library.BLL.SInventory_BLL
{

    public class DeStockOutBLL
    {
        ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();

        DeStockOutDal aDeStockOutDal = new DeStockOutDal();

        public DataTable Get_InvoiceType()
        {
            return aDeStockOutDal.Load_InvoiceType();
        }

        
        public void ProductLoadBll(DropDownList aDownList)
        {
            aDeStockOutDal.ProductLoadDal(aDownList);
        }


        public void DistributionCenterLoadBll(DropDownList aDownList)
        {
            aDeStockOutDal.DistributionCenterLoadDal(aDownList);
        }


        public void ProformaInvoiceNumberBll(DropDownList ddl, string id)
        {
            aDeStockOutDal.ProformaInvoiceNumberDal(ddl, id);
        }


        public bool SaveDataForDcStockOutMasterBll(DcStockOutMasterDao aMasterDao, out int DcStockOutMasterId)
        {
            DcStockOutMasterId = aClsPrimaryKeyFind.PrimaryKeyMax("DcStockOutMasterId", "tblDeStockOutMaster");
            aMasterDao.DcStockOutMasterId = DcStockOutMasterId;
            aMasterDao.DcStockOutCode = InvoiceNoGenerator(aMasterDao.Reason, DcStockOutMasterId);
            return aDeStockOutDal.SaveDataForDcStockOutMaster(aMasterDao);
        }

        public string InvoiceNoGenerator(string comCode, int id)
        {
           // DataTable aDataTable = new DataTable();
          //  aDataTable = aDeStockOutDal.DeStockOutNoMasterCount();

            string code = string.Empty;
            string Id = Convert.ToString(id);

            if (Id.Length == 1)
            {
                Id = "000000000" + Id;
            }
            if (Id.Length == 2)
            {
                Id = "00000000" + Id;
            }
            if (Id.Length == 3)
            {
                Id = "0000000" + Id;
            }
            if (Id.Length == 4)
            {
                Id = "000000" + Id;
            }
            if (Id.Length == 5)
            {
                Id = "00000" + Id;
            }
            if (Id.Length == 6)
            {
                Id = "0000" + Id;
            }
            if (Id.Length == 7)
            {
                Id = "000" + Id;
            }
            if (Id.Length == 8)
            {
                Id = "00" + Id;
            }
            if (Id.Length == 9)
            {
                Id = "0" + Id;
            }
            if (comCode == "FOC")
            {
                code = "FOC-" + Id;
            }
            else
            {
                code = "SAM-" + Id;
            }
            return code;
        }

        public bool SaveDataForStockOutDetailBll(List<DcStockOutDetailsDao> aStockOutDetailsDaos)
        {
            foreach (var stockOutDetail in aStockOutDetailsDaos)
            {
                stockOutDetail.DcStockOutDetailsId = aClsPrimaryKeyFind.PrimaryKeyMax("DcStockOutDetailsId", "tblDeStockOutDetails");
                aDeStockOutDal.SaveDataForStockOutDetailDal(stockOutDetail);
            }
            return true;
        }

        public DataTable DcStockOutBll(string prm)
        {
            return aDeStockOutDal.DcStockOutViewDal(prm);
        }

        public DataTable getRecordEditMode(string prm)
        {
            return aDeStockOutDal.getRecordEditMode(prm);
        }

        public bool DcStockOutDetailsDelete(string Id )
        {
            return aDeStockOutDal.DcStockOutDetailsDeleteDal(Id);
        }

        public bool UpdateDcStockOutDetailsDelete(string Id, string Status)
        {
            return aDeStockOutDal.UpdateDcStockOutDetailsDelete(Id, Status);
        }

        public bool DcStockOutMasterDelete(string Id)
        {
            return aDeStockOutDal.DcStockOutMasterDeleteDal(Id);
        }
        public bool UpdateDCStoreQuantity(string dCStoreId, decimal Quantity)
        {
            return aDeStockOutDal.UpdateDCStoreQuantity(dCStoreId, Quantity);
        }


        public bool DcStockOutMasterPartialDal(string Id, int Qty)
        {
            return aDeStockOutDal.DcStockOutMasterPartialDal(Id, Qty);
        }

        //getManu
        public DataTable GetMenuIdByMenuName(string menuname)
        {
            return aDeStockOutDal.GetMenuIdByMenuName(menuname);
        }

        //GetAssignAppuer
        public DataTable GetAssignedAppUser(string menuid, string userId)
        {
            return aDeStockOutDal.GetAssignedAppUser(menuid, userId);
        }

        //UpdateApproVal

        public string ApprovalUpdateBLL(DcStockOutMasterDao aMasterDao)
        {
            aDeStockOutDal.ApprovalUpdateDal(aMasterDao);
            return "Weldone! Stock In approved successfully!!!";
        }

        //GetDcStockOut for approval
        public DataTable DcStockOutAppBll()
        {
            return aDeStockOutDal.DcStockOutAppDal();
        }

        //getdata for ReportView

        public DataTable DcStockOutReportViewBll(string id)
        {
            return aDeStockOutDal.DcStockOutReportDal(id);
        }


        //RND update Approval
        public bool UpdateStockOutMasterDataForApprovalBll( DcStockOutMasterDao aMasterDao)
        {
            return aDeStockOutDal.UpdateStockOutMasterDataForApprovalDal(aMasterDao);
           
        }

        public DataTable GetDcStoreIdBll(string id)
        {
            return aDeStockOutDal.GetDcStoreIdDal(id);
        }


      

 
        
    }
}