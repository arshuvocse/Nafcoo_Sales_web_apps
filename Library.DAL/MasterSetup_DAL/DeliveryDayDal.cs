using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.MasterSetup_DAO;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.MasterSetup_DAL
{
    public class DeliveryDayDal
    {

        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public ResultInfo SaveDeliveryDay(DeliveryDayDao aMasterDao)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@DeliveryDayId", aMasterDao.DeliveryDayId));
                masterParameters.Add(new SqlParameter("@DeliveryDay", aMasterDao.DeliveryDay));

                if (aMasterDao.DeliveryDayId > 0)
                {

                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_Delivery_Update", masterParameters);
                    pk = aMasterDao.DeliveryDayId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Delivery_Save", masterParameters);

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

        public DataTable GetDeliveryDays(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_DeliveryDay_GetList", aSqlParameters);

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

        public DataTable GetDeliveryDayById(int masterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@masterId", masterId));
                DataTable dt = accessManager.GetDataTable("sp_Delivery_ById", aSqlParameters);

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
