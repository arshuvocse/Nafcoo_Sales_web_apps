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
    public class ProductTargetDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        private DB_Manager aDbManager = new DB_Manager();
        public DataTable LoadProductTarget()
        {
            string query = @"SELECT PD.ProductId, PD.ProductCode, PD.Description, PD.PackSize, UP.UnitPrice,UP.VATAmountPerUnit FROM tblProduct AS PD
                             LEFT JOIN tblUnitPrice AS UP ON PD.ProductId = UP.ProductId WHERE UP.IsActive = 1 ORDER BY PD.ProductCode";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
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
        public void LoadTargetCategory(DropDownList ddl)
        {
            string query = @"SELECT TargetId, TargetCategory FROM tbl_Target_CategoryMaster";

            aCommonInternalDal.LoadDropDownValue(ddl, "TargetCategory", "TargetId", query, "SSIDB");
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
        
      //  public DataTable CheckTargetCategory(string aMasterDao)
      //  {
      //      string query = @"select TargetCategory from tbl_Target_CategoryMaster 
      //                          where TargetCategory='" + aMasterDao + "'";
      //                         
      //
      //      return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
      //  }
        public bool HasTargetCategory(string aMasterDao)
        {

            string query = @"SELECT TargetCategory FROM tbl_Target_CategoryMaster 
                                WHERE TargetCategory='" + aMasterDao + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query,"SSIDB");

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }
        
        public ResultInfo SaveProductTarget(TargetCategoryMasterDAO aMasterDao, List<TargetCategoryDetailsDAO> aList)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@TargetCategory", aMasterDao.TargetCategory));
                masterParameters.Add(new SqlParameter("@TotalTargetByTp", aMasterDao.TotalTargetByTp));
                masterParameters.Add(new SqlParameter("@TotalTargetByTpVat", aMasterDao.TotalTargetByTpVat));

                if (aMasterDao.TargetId > 0)
                {
                    masterParameters.Add(new SqlParameter("@UpdatedBy", aMasterDao.UpdatedBy));
                    masterParameters.Add(new SqlParameter("@TargetId", aMasterDao.TargetId));
                    
                    accessManager.UpdateData("sp_Target_UpdateTargetCategoryMaster", masterParameters);
                    pk = aMasterDao.TargetId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Target_SaveTargetCategoryMaster", masterParameters);

                }


                if (pk > 0)
                {
                    List<SqlParameter> deleteId = new List<SqlParameter>();

                    deleteId.Add(new SqlParameter("@TargetId", aMasterDao.TargetId));
                    accessManager.DeleteData("sp_Target_DeleteTargetCategoryDetailsById", deleteId);

                    foreach (var aDao in aList)
                    {
                        List<SqlParameter> aSQL = new List<SqlParameter>();

                        aSQL.Add(new SqlParameter("@ProductCode", aDao.ProductCode));
                        aSQL.Add(new SqlParameter("@TargetQty", aDao.TargetQty));
                        aSQL.Add(new SqlParameter("@TpPerPack", aDao.TpPerPack));
                        aSQL.Add(new SqlParameter("@VatPerPack", aDao.VatPerPack));
                        aSQL.Add(new SqlParameter("@TargetValueByTp", aDao.TargetValueByTp));
                        aSQL.Add(new SqlParameter("@TargetValueByTpVat", aDao.TargetValueByTpVat));
                        aSQL.Add(new SqlParameter("@TargetId", pk));

                        aInformation.isSuccess = accessManager.SaveData("sp_Target_SaveTargetCategoryDetails", aSQL);

                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }

        public void LoadExistingCategory(DropDownList ddl)
        {
            string query = @"SELECT TargetId,TargetCategory + ' ( TP Value:' + CONVERT(NVARCHAR,TotalTargetByTp) + ')' AS TargetCategory FROM tbl_Target_CategoryMaster";
            aCommonInternalDal.LoadDropDownValue(ddl, "TargetCategory", "TargetId", query, "SSIDB");
        }
    }
}
