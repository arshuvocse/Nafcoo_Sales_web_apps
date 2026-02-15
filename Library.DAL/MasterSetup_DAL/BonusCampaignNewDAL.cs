using Library.DAL.DataManager;
using Library.DAO.MasterSetup_DAO;
using SalesSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Library.DAL.MasterSetup_DAL
{
  public  class BonusCampaignNewDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public DataTable GetBonusCampaignList(string prm)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@Parm", prm));
                DataTable dt = accessManager.GetDataTable("sp_Get_BonusCampaignNewMasterList", aSqlParameterlist);
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

        public DataTable GetActiveCustomerType()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("Sp_Get_ActiveCustomerType");
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
        public DataTable GetProductForDDL()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Get_Product_ForTargetSetup_DDL");
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
        public ResultInfo SaveBonusCampaign(BonusCampaignNewMasterDAO master, List<BonusCampaignNewDetailDAO> _Dtls, List<BonusCampaignMarketDetailDAO> _MarketDtls, List<CampaignCustomerDetailDAO> _CamCustomerDtls, string sessionUser)
        {
            int pk = 0;  
             ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@CampgainMasterId", master.CampgainMasterId));
                gSqlParameterList.Add(new SqlParameter("@CampaignName", master.CampaignName ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@CustomerTypeId", master.CustomerTypeId ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@CampainTypeId", master.CampainTypeId ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@Amount", master.Amount ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@ProductQty", master.ProductQty ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@MaxAmount", master.MaxAmount ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@FromDate", master.FromDate ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@Todate", master.Todate ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IsActive", master.IsActive ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@IsTradePolicy", master.IsTradePolicy ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@ProductLineID", master.ProductLineID ?? (object)DBNull.Value));
                gSqlParameterList.Add(new SqlParameter("@BonusProductId", master.BonusProductId ?? (object)DBNull.Value));

                if (master.CampgainMasterId > 0)
                {
                    gSqlParameterList.Add(new SqlParameter("@UpdateBy", sessionUser));
                    aInformation.isSuccess = accessManager.UpdateData("sp_Update_BonusCampaignNewMaster", gSqlParameterList);

                    pk = master.CampgainMasterId;

                }
                else
                {
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser));
                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_BonusCampaignNewMaster", gSqlParameterList);
                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                    }
                    else
                    {
                        aInformation.isSuccess = false;

                    }
                }

                if (pk > 0)
                {
                    foreach (var item in _Dtls)
                    {
                        
                        List<SqlParameter> aSQL = new List<SqlParameter>();
                        aSQL.Add(new SqlParameter("@CampaignMasterId", pk ));
                    
                        aSQL.Add(new SqlParameter("@DiscountPercentage", item.DiscountPercentage ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@ProductId", item.ProductId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@Quantity", item.Quantity ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@BonusProductId", item.BonusProductId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@BonusQuantity", item.BonusQuantity ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@QuantityDteail", item.QuantityDteail ?? (object)DBNull.Value));

                        aSQL.Add(new SqlParameter("@BonusTypeId", item.BonusTypeId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@CampaignName", master.CampaignName ?? (object)DBNull.Value));
                        aInformation.isSuccess = accessManager.SaveData("sp_Save_BonusCampaignNewDetail", aSQL);

                    }

                    foreach (var item in _MarketDtls)
                    {

                        List<SqlParameter> aSQL = new List<SqlParameter>();
                        aSQL.Add(new SqlParameter("@CampaignMasterId", pk ));
                        aSQL.Add(new SqlParameter("@GroupId", item.GroupId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@RegionId", item.RegionId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@AreaId", item.AreaId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@TerritoryId", item.TerritoryId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@SubTerritoryId", item.SubTerritoryId ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@MarketId", item.MarketId ?? (object)DBNull.Value));

                        aInformation.isSuccess = accessManager.SaveData("sp_Save_BonusCampaignMarketDetail", aSQL);

                    }


                    foreach (var item in _CamCustomerDtls)
                    {

                        List<SqlParameter> aSQL = new List<SqlParameter>();
                        aSQL.Add(new SqlParameter("@CampaignMasterId", pk));
                       
                        aSQL.Add(new SqlParameter("@CustomerMasterId", item.CustomerMasterId ?? (object)DBNull.Value));

                        aInformation.isSuccess = accessManager.SaveData("sp_Save_BonusCampaignCustomerDetail", aSQL);

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




        public ResultInfo SaveDoctorInfoForExcel(CustomerPropUpdateMasterDAO master, List<CustomerPropUpdateDetailDAO> _Dtls, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@CustPropMasterId", master.CustPropMasterId));
                gSqlParameterList.Add(new SqlParameter("@TypeId", master.TypeId ?? (object)DBNull.Value));


                gSqlParameterList.Add(new SqlParameter("@ConvertType", master.ConvertType ?? (object)DBNull.Value));

                gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser));
                pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_DoctorPropUpdateMaster", gSqlParameterList);
                if (pk > 0)
                {
                    aInformation.isSuccess = true;
                }
                else
                {
                    aInformation.isSuccess = false;

                }


                if (pk > 0)
                {
                    foreach (var item in _Dtls)
                    {

                        List<SqlParameter> aSQL = new List<SqlParameter>();
                        aSQL.Add(new SqlParameter("@CustPropMasterId", pk));

                        aSQL.Add(new SqlParameter("@CustPropUpdateDetailId", item.CustPropUpdateDetailId ?? (object)DBNull.Value));

                        aSQL.Add(new SqlParameter("@CustCode", item.CustCode ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@ProviderType", item.ProviderType ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@CustTypeCode", item.CustTypeCode ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@MarketCode", item.MarketCode ?? (object)DBNull.Value));


                        aInformation.isSuccess = accessManager.SaveData("sp_Save_DoctorPropUpdateDetail", aSQL);

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

        public ResultInfo SaveCustomerInfoForExcel(CustomerPropUpdateMasterDAO master, List<CustomerPropUpdateDetailDAO> _Dtls, string sessionUser)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;

                gSqlParameterList.Add(new SqlParameter("@CustPropMasterId", master.CustPropMasterId));
                gSqlParameterList.Add(new SqlParameter("@TypeId", master.TypeId ?? (object)DBNull.Value));
                 

                gSqlParameterList.Add(new SqlParameter("@ConvertType", master.ConvertType ?? (object)DBNull.Value));
 
                    gSqlParameterList.Add(new SqlParameter("@EntryBy", sessionUser));
                    pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_CustomerPropUpdateMaster", gSqlParameterList);
                    if (pk > 0)
                    {
                        aInformation.isSuccess = true;
                    }
                    else
                    {
                        aInformation.isSuccess = false;

                    }
             

                if (pk > 0)
                {
                    foreach (var item in _Dtls)
                    {

                        List<SqlParameter> aSQL = new List<SqlParameter>();
                        aSQL.Add(new SqlParameter("@CustPropMasterId", pk));

                        aSQL.Add(new SqlParameter("@CustPropUpdateDetailId", item.CustPropUpdateDetailId ?? (object)DBNull.Value));
                       
                        aSQL.Add(new SqlParameter("@CustCode", item.CustCode ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@ProviderType", item.ProviderType ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@CustTypeCode", item.CustTypeCode ?? (object)DBNull.Value));
                        aSQL.Add(new SqlParameter("@MarketCode", item.MarketCode ?? (object)DBNull.Value));

                      
                        aInformation.isSuccess = accessManager.SaveData("sp_Save_CustomerPropUpdateDetail", aSQL);

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
        public DataTable GetCampaignSetupById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_CampaignMaster_ById", aSqlParameterlist);
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


        public DataTable GetCampaignSetupDetailById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_CampaignDetail_ById", aSqlParameterlist);
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

        public DataTable GetCampaignSetupDetailMarketById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_CampaignDetailMarket_ById", aSqlParameterlist);
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


        public DataTable GetCampaignSetupDetailCustomerById(string Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));

                DataTable dt = accessManager.GetDataTable("sp_GET_CampaignDetailCustomer_ById", aSqlParameterlist);
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

        public bool SaveImage(string path, string name, int id, int ImageId)
        {
            bool status = false;
            string meesage = "";

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                gSqlParameterList.Add(new SqlParameter("@NoticeImageId", ImageId));
                gSqlParameterList.Add(new SqlParameter("@NoticeId", id));
                gSqlParameterList.Add(new SqlParameter("@ImageName", name));
                gSqlParameterList.Add(new SqlParameter("@ImagePath", path));
                status = accessManager.SaveData("sp_Save_NoticeImage", gSqlParameterList, false);
            }
            catch (Exception exception)
            {
                accessManager.SqlConnectionClose(true);
                status = false;
                meesage = exception.Message;

                throw exception;
            }
            finally
            {


                accessManager.SqlConnectionClose();
            }

            return status;
        }




        public ResultInfo SaveNoticeImage(string path, string name, int id)
        {
            int pk = 0;
            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                gSqlParameterList.Add(new SqlParameter("@NoticeImageId", 0));
                gSqlParameterList.Add(new SqlParameter("@NoticeImageId", id));
                gSqlParameterList.Add(new SqlParameter("@ImageName", name));
                gSqlParameterList.Add(new SqlParameter("@ImagePath", path));

                pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_NoticeImage", gSqlParameterList);
                if (pk > 0)
                {
                    aInformation.isSuccess = true;
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


        public DataTable GetNoticeMasterList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DataTable dt = accessManager.GetDataTable("sp_Get_NoticeMaster");
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


        public bool Delete_NoticeImage(int Id)
        {
            bool status = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@NoticeId", Id));
                status = accessManager.DeleteData("sp_DEL_NoticeImage", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return status;
        }

        public ResultInfo Delete_NoticeMaster(Int32 DeleteId)
        {
            int pk = 0;

            ResultInfo aInformation = new ResultInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> gSqlParameterList = new List<SqlParameter>();
                DateTime entryDtae = DateTime.Now;
                gSqlParameterList.Add(new SqlParameter("@NoticeId", DeleteId));

                bool result = accessManager.DeleteData("sp_Delete_NoticeMaster", gSqlParameterList);
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


        public NoticeMaster GetNoticeMasterForEdit(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                NoticeMaster master = new NoticeMaster();
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_Get_NoticeMaster_ById", aSqlParameters);
                while (dr.Read())
                {
                    master.NoticeId = (int)dr["NoticeId"];
                    master.NoticeTitle = dr["NoticeTitle"].ToString();
                    master.Announcement = dr["Announcement"].ToString();
                    master.FromDate = (DateTime)dr["FromDate"];
                    master.ToDate = (DateTime)dr["ToDate"];
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


        public DataTable Get_NoticeImageByNoticeId(int Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));
                DataTable dt = accessManager.GetDataTable("sp_Get_NoticeImage_By_NoticeId", aSqlParameterlist);
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


        public DataTable Get_NoticeDetailsByNoticeId(int Id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@id", Id));
                DataTable dt = accessManager.GetDataTable("sp_Get_Noticedetails_By_NoticeId", aSqlParameterlist);
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
