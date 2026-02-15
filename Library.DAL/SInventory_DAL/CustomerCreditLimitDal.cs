using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAO.SInventory_Entities;
using SalesSolution.Web.Models;

namespace Library.DAL.SInventory_DAL
{
    public class CustomerCreditLimitDal
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public ResultInfo SaveData(CreditLimitDAO aMasterDao)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@LimitAmount", aMasterDao.LimitAmount));
                masterParameters.Add(new SqlParameter("@DayLimit", aMasterDao.DayLimit));
                masterParameters.Add(new SqlParameter("@CustomerMasterId", aMasterDao.CustomerMasterId));
                masterParameters.Add(new SqlParameter("@CreditLimitId", aMasterDao.CreditLimitId));
                masterParameters.Add(new SqlParameter("@CompanyId", aMasterDao.CompanyId));

                if (aMasterDao.CreditLimitId > 0)
                {
                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    aInformation.isSuccess = accessManager.UpdateData("sp_Update_CustomerCreditLimit", masterParameters);
                    pk = aMasterDao.CreditLimitId;
                }
                else
                {
                    masterParameters.Add(new SqlParameter("@EntryBy", aMasterDao.EntryBy));
                    masterParameters.Add(new SqlParameter("@EntryDate", aMasterDao.EntryDate));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_CustomerCreditLimit", masterParameters);

                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
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

        public DataTable GetData(string pram)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", pram));
                DataTable dt = accessManager.GetDataTable("sp_GET_CustomerCreditLimit", aSqlParameters);

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

        public DataTable GetDataById(int masterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", masterId));
                DataTable dt = accessManager.GetDataTable("sp_GET_CreditLimitInfoById", aSqlParameters);

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
