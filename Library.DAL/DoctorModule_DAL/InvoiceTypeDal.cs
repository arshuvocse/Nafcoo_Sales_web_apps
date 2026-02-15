using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Library.DAL.DataManager;
using Library.DAO.DoctorModule_DAO;
using SalesSolution.Web.Models;

namespace Library.DAL.DoctorModule_DAL
{
   public class InvoiceTypeDal
    {
        
        private DataAccessManager accessManager = new DataAccessManager();

        public InvoiceTypeDao GetInvoiceTypeForEdit(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                InvoiceTypeDao master = new InvoiceTypeDao();
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_Get_InvoiceType_ById", aSqlParameters);
                while (dr.Read())
                {
                    master.InvoiceTypeId = (int)dr["InvoiceTypeId"];
                    master.TypeName = dr["TypeName"].ToString();
                    master.Activedate = (DateTime)dr["Activedate"];
                    master.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }
                return master;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

        }
        public ResultInfo SaveInvoiceType(InvoiceTypeDao doctorDesignation, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                gSqlParameterList.Add(new SqlParameter("@InvoiceTypeId ", doctorDesignation.InvoiceTypeId));
                gSqlParameterList.Add(new SqlParameter("@TypeName", doctorDesignation.TypeName));
                gSqlParameterList.Add(new SqlParameter("@IsActive ", doctorDesignation.IsActive));
                gSqlParameterList.Add(new SqlParameter("@Activedate", doctorDesignation.Activedate));

                if (doctorDesignation.InvoiceTypeId > 0)
                {
                    aSqlParameterlist.Add(new SqlParameter("@InvoiceTypeId ", doctorDesignation.InvoiceTypeId));
                    aSqlParameterlist.Add(new SqlParameter("@TypeName", doctorDesignation.TypeName));
                    DataTable dt = accessManager.GetDataTable("sp_check_InvoiceType", aSqlParameterlist);
                    if (dt.Rows.Count == 0)
                    {
                        gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser));
                        aInformation.isSuccess = accessManager.UpdateData("sp_Update_InvoiceType", gSqlParameterList);
                    }
                    else
                    {
                        aInformation.isSuccess = false;
                    }

                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser));
                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_InvoiceType", gSqlParameterList);
                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                    }
                    else
                    {

                        aInformation.isSuccess = false;
                    }

                }
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                aInformation.isSuccess = false;
                aInformation.ErrorMessage = exception.Message;

                throw exception;
            }
            finally
            {

                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }
        public DataTable GetInvoiceTypeList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_InVoiceType");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultInfo DeleteInvoiceType(Int32 DeleteId, string sessionUser)
        {
            int pk = 0;

            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                gSqlParameterList.Add(new SqlParameter("@DesignationId", DeleteId));
                gSqlParameterList.Add(new SqlParameter("@DeleteBy", sessionUser));
                bool result = accessManager.DeleteData("sp_Delete_InvoiceType", gSqlParameterList);
                pk = DeleteId;
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                aInformation.isSuccess = false;
                aInformation.ErrorMessage = exception.Message;

                throw exception;
            }
            finally
            {
                aInformation.isSuccess = true;
                accessManager.SqlConnectionClose();
            }

            return aInformation;
        }
    }
    
}
