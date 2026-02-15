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
    public class PatientPriorityDal
    {

        ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        public ResultInfo SaveWorkType(PatientPriorityDao aMasterDao)
        {

            int pk = 0;
            ResultInfo aInformation = new ResultInfo();

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> masterParameters = new List<SqlParameter>();

                masterParameters.Add(new SqlParameter("@PatientPriorityId", aMasterDao.PatientPriorityId));
                masterParameters.Add(new SqlParameter("@PatientStartPoint", aMasterDao.PatientStartPoint));
                masterParameters.Add(new SqlParameter("@PatientEndPoint", aMasterDao.PatientEndPoint));
                masterParameters.Add(new SqlParameter("@RxStartPoint", aMasterDao.RxStartPoint));
                masterParameters.Add(new SqlParameter("@RxEndPoint", aMasterDao.RxEndPoint));
                masterParameters.Add(new SqlParameter("@Patientstatus", aMasterDao.Patientstatus));
                masterParameters.Add(new SqlParameter("@ColourCodeForNote", aMasterDao.ColourCodeForNote));

                if (aMasterDao.PatientPriorityId > 0)
                {

                    masterParameters.Add(new SqlParameter("@UpdateBy", aMasterDao.UpdateBy));
                    masterParameters.Add(new SqlParameter("@UpdateDate", aMasterDao.UpdateDate));

                    accessManager.UpdateData("sp_PatientPriority_Update", masterParameters);
                    pk = aMasterDao.PatientPriorityId;
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

        public DataTable GetWorkTypes(string prameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", prameter));
                DataTable dt = accessManager.GetDataTable("sp_PatientPriority_GetList", aSqlParameters);

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

        public DataTable GetWorkTypeById(int masterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@masterId", masterId));
                DataTable dt = accessManager.GetDataTable("sp_PatientPriority_ById", aSqlParameters);

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

        public bool ApproveById(int masterId, int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@masterId", masterId));
                aSqlParameters.Add(new SqlParameter("@userId", userId));

                return accessManager.UpdateData("sp_WorkSchedule_ApproveById", aSqlParameters);

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
