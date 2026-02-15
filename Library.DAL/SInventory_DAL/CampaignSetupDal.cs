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

namespace Library.DAL.MasterSetup_DAL
{
    public class CampaignSetupDal
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable GetCustomerListActive()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                DataTable dt = accessManager.GetDataTable("sp_Get_Customer_Active");
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
        public DataTable GetDetailById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_Get_QuotedPriceDetailById", aSqlParameterlist);
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
        public DataTable GetQuotedPriceMasterById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_QuotedPriceMaster_ById", aSqlParameterlist);
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
        public DataTable GetProductListActive()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                DataTable dt = accessManager.GetDataTable("sp_Get_Product_Active");
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
        }public DataTable GetProductByPriceGroup(int customerMasterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@CustomerMasterId", customerMasterId.ToString() != "" ? customerMasterId : 0));

                DataTable dt = accessManager.GetDataTable("sp_Get_ProductByPriceGroup",aSqlParameterlist);
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
        public DataTable GetProductListActiveForALl()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                DataTable dt = accessManager.GetDataTable("sp_Get_Product_All");
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


        public DataTable GetQuotedPriceList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                DataTable dt = accessManager.GetDataTable("sp_Get_QuotedPriceMaster");
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


        public ResultInfo SaveMasterDetals(CampaignSetupMasterDao _Master, List<CampaignSetupDetailDao> _Dtls, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                
                gSqlParameterList.Add(new SqlParameter("@CustomerInformationId", _Master.CustomerInformationId));
                gSqlParameterList.Add(new SqlParameter("@SlabAmount", _Master.SlabAmount));
                gSqlParameterList.Add(new SqlParameter("@DiscountPercentage", _Master.DiscountPercentage));


                if (_Master.CampaignMasterId > 0)
                {
                   gSqlParameterList.Add(new SqlParameter("@CampaignMasterId", _Master.CampaignMasterId));
                   gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                   gSqlParameterList.Add(new SqlParameter("@UpdateDate", entryDtae));

                   bool result = accessManager.UpdateData("sp_Campaign_UpdateMasterInfo", gSqlParameterList);
                   aInformation.isSuccess = result;
                   pk = _Master.CampaignMasterId;
                   aInformation.Id = pk;
                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser ?? (object)DBNull.Value ?? (object)DBNull.Value));
                    gSqlParameterList.Add(new SqlParameter("@EntryDate", entryDtae));

                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Campaign_SaveMasterInfo", gSqlParameterList);

                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                        aInformation.Id = pk;
                    }
                }
                   
                //Deleted Previous
                List<SqlParameter> aDeleteParameters = new List<SqlParameter>();

                aDeleteParameters.Add(new SqlParameter("@CampaignMasterId", _Master.CampaignMasterId));
                accessManager.DeleteData("sp_Campaign_DeleteDetailInfoById", aDeleteParameters);

                foreach (var item in _Dtls)
                {

                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@CampaignMasterId", pk));
                    aParameters.Add(new SqlParameter("@ProductId", item.ProductId));

                    accessManager.SaveData("sp_Campaign_SaveDetailsInfo", aParameters);
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

     public void LoadPriceGroup(DropDownList ddl)
     {
         ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();

         string queryStr = "SELECT PriceGroupId,PriceGroupName FROM tblCustomerPriceGroups WHERE IsActive = 1";
         aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "PriceGroupName", "PriceGroupId", queryStr);
     }

     public void LoadCustomerByPriceGroup(DropDownList ddl, Int32 customerPriceGroupId)
     {
         ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();

         string queryStr = "SELECT CustomerMasterId,CustomerCode + ' : ' + CustomerName AS Customer FROM tblCustMaster WHERE CustomerTypeId = " + customerPriceGroupId + " ORDER BY CustomerName";
         aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "Customer", "CustomerMasterId", queryStr);
     }

     public void LoadACustomerByType(DropDownList ddl)
     {
         ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
         string queryStr = "SELECT CustomerMasterId, CustomerCode + ' : ' + CustomerName AS Customer FROM tblCustMaster WHERE CustomerTypeId = 5";
         aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "Customer", "CustomerMasterId", queryStr);
     }

        public DataTable GetSpecialDiscountData(string pram)
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            try
            {
                //accessManager.SqlConnectionOpen(DataBase.SalesDB);


                string query = @"SELECT QM.CampaignMasterId,CustomerCode,CustomerName,Address,ProductCode,ProductName,PackSize,DiscountPercentage FROM tbl_Campaign_MasterInformations AS QM
LEFT JOIN tbl_Campaign_DetailInformations AS QD ON QM.CampaignMasterId = QD.CampaignMasterId
LEFT JOIN tblCustMaster AS CM ON QM.CustomerInformationId = Cm.CustomerMasterId
LEFT JOIN tblProduct AS PD ON QD.ProductId = PD.ProductId  WHERE DiscountPercentage > 0 " + pram + " ORDER BY CustomerCode ";

                return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");

                //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                //aSqlParameterlist.Add(new SqlParameter("@Pram", pram));

                //DataTable dt = accessManager.GetDataTable("sp_Get_QuotedPriceReport", aSqlParameterlist);
                //return dt;
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

        public DataTable GetExistingDiscount(int customerId)
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            try
            {

                string query = @"SELECT QM.CampaignMasterId,CustomerCode,CustomerName,Address,ProductCode,ProductName,PackSize,QM.SlabAmount,DiscountPercentage FROM tbl_Campaign_MasterInformations AS QM
                                 LEFT JOIN tbl_Campaign_DetailInformations AS QD ON QM.CampaignMasterId = QD.CampaignMasterId
                                 LEFT JOIN tblCustMaster AS CM ON QM.CustomerInformationId = Cm.CustomerMasterId
                                 LEFT JOIN tblProduct AS PD ON QD.ProductId = PD.ProductId WHERE QM.CustomerInformationId = " + customerId;

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
