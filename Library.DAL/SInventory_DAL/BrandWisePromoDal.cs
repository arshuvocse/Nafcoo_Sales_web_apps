using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class BrandWisePromoDal
    {

        public void LoadBrandName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "	SELECT ProductBrandId,ProductSQName FROM tblProductSQ ORDER BY ProductSQName";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ProductSQName", "ProductBrandId", queryStr);
        }

        public void LoadPromoProducte(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT PromoProductId,PromoProductName FROM tblPromoProductName";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "PromoProductName", "PromoProductId", queryStr);
        }

        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public ResultInfo SaveDeliveryDay(BrandWisePromoDao aMasterDao)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@PromoWiseBrandSetupId", aMasterDao.PromoWiseBrandSetupId));
                masterParameters.Add(new SqlParameter("@PromoProductId", aMasterDao.PromoProductId));
                masterParameters.Add(new SqlParameter("@BrandId", aMasterDao.BrandId));

                if (aMasterDao.PromoWiseBrandSetupId > 0)
                {

                    masterParameters.Add(new SqlParameter("@IsActive", aMasterDao.IsActive));
                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_PromoWiseBrand_Update", masterParameters);
                    pk = aMasterDao.PromoWiseBrandSetupId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_PromoWiseBrand_Save", masterParameters);

                }


                if (pk > 0)
                {
                    aInformation.isSuccess = true;

                }
                else
                {
                    aInformation.isSuccess = false;
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

        public DataTable GetBrandWisePromoList(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_BrandWisePromo_GetList", aSqlParameters);

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

        public DataTable GetBrandWisePromoById(int masterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@masterId", masterId));
                DataTable dt = accessManager.GetDataTable("sp_BrandWisePromo_ById", aSqlParameters);

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
