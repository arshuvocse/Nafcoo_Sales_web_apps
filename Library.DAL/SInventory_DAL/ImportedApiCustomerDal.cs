using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class ImportedApiCustomerDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        //API Customer
        public DataTable LoadNewCustomer()
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tbltempCustMaster WHERE AddtoMainCustomer = 'False' ";
            return aInternalDal.DataContainerDataTable(queryStr);
        }


        public void LoadCompanyUnit(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT ComUnitId,ComUnitCode,ComUnitName +':'+ ComUnitCode as ComUnitName FROM dbo.tblCompanyUnit  WHERE ComUnitId IN (SELECT ComUnitId FROM dbo.tblCompanyUnit) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitCode", queryStr);
        }

        public void GetDZSMname(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT RegionId,RegionCode,RegionName +':'+ RegionCode as RegionName FROM dbo.tblRegion  WHERE RegionId IN (SELECT RegionId FROM dbo.tblRegion)";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "RegionName", "RegionCode", queryStr);
        }

        public void LoadCategoryName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT CategoryId,CategoryCode,CategoryName FROM dbo.tblCustCategory WHERE CategoryId IN (SELECT CategoryId FROM dbo.tblCustCategory) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "CategoryName", "CategoryId", queryStr);
        }

        public void GetFEInfo(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT DistrictId,DistrictCode,DistrictName  +':'+ DistrictCode as DistrictName FROM dbo.tblDistrict WHERE DistrictId IN (SELECT DistrictId FROM dbo.tblDistrict) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "DistrictName", "DistrictCode", queryStr);
        }

        public void GetTerritoryInfo(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT AreaId,AreaCode,AreaName +':'+ AreaCode as AreaName FROM dbo.tblArea WHERE AreaId IN (SELECT AreaId FROM dbo.tblArea) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "AreaName", "AreaCode", queryStr);
        }

        public void GetMiaInfo(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT MiaId,MiaCode,MiaName +':'+ MiaCode as MiaName FROM dbo.tblMIAInfo WHERE MiaId IN (SELECT MiaId FROM dbo.tblMIAInfo) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MiaName", "MiaCode", queryStr); ;
        }

        public void GetMaketInfo(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DISTINCT MarketId,MarketCode,MarketName +':'+ MarketCode as MarketName FROM dbo.tblMarket WHERE MarketId IN (SELECT MarketId FROM dbo.tblMarket) ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MarketName", "MarketCode", queryStr);
        }



        public CustomerMaster CustomerMasterEditLoad(string customerId)
        {
            string query = "SELECT * FROM [dbo].[tbltempCustMaster] WHERE [tempCustomerMasterId] = '" + customerId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            CustomerMaster aCustomerMaster = new CustomerMaster();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {

                    aCustomerMaster.CustomerMasterId = Int32.Parse(dataReader["tempCustomerMasterId"].ToString());
                    aCustomerMaster.CustomerName = dataReader["CustomerName"].ToString();
                    aCustomerMaster.CustomerCode = dataReader["CustomerCode"].ToString();
                    aCustomerMaster.Address = dataReader["Address"].ToString();
                    aCustomerMaster.CellNo = dataReader["CellNo"].ToString();
                    //aCustomerMaster.CategoryId = Convert.ToInt32(dataReader["CategoryId"].ToString());
                    aCustomerMaster.Addrees2 = dataReader["Addrees2"].ToString();
                    aCustomerMaster.City = dataReader["City"].ToString();
                    aCustomerMaster.ConPerson = dataReader["ConPerson"].ToString();
                    //aCustomerMaster.ShippingCond = dataReader["ShippingCond"].ToString();
                    aCustomerMaster.MarketCode = dataReader["MarketCode"].ToString();
                    aCustomerMaster.MarketName = dataReader["MarketName"].ToString();
                    aCustomerMaster.MIACode = dataReader["MIACode"].ToString();
                    aCustomerMaster.MiaName = dataReader["MIAName"].ToString();
                    aCustomerMaster.AreaCode = dataReader["AreaCode"].ToString();
                    aCustomerMaster.DisCode = dataReader["DisCode"].ToString();
                    aCustomerMaster.FEName = dataReader["FEName"].ToString();
                    aCustomerMaster.ComUnitCode = dataReader["ComUnitCode"].ToString();
                    aCustomerMaster.ComUnitName = dataReader["ComUnitName"].ToString();
                    aCustomerMaster.RegionCode = dataReader["RegionCode"].ToString();
                    aCustomerMaster.DZSMName = dataReader["DZSMName"].ToString();
                    aCustomerMaster.TermOfPayment = dataReader["TermOfPayment"].ToString();
                    //aCustomerMaster.CustomerCodeOld = dataReader["CustomerCodeOld"].ToString();
                    aCustomerMaster.FixedCustomer = (bool)dataReader["FixedCustomer"];
                }

            }
            return aCustomerMaster;
        }


        public DataTable GetDZSMnameById(string dzsmId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT RegionId,RegionName FROM dbo.tblRegion WHERE RegionCode = '" + dzsmId + "'";
            return aInternalDal.DataContainerDataTable(queryStr);
        }

        public DataTable GetFEnameById(string feId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT DistrictId,DistrictName FROM dbo.tblDistrict WHERE DistrictCode = '" + feId + "'";
            return aInternalDal.DataContainerDataTable(queryStr);
        }

        public DataTable GetTeritorynameById(string teritoryId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT AreaId,AreaName FROM dbo.tblArea WHERE AreaCode = '" + teritoryId + "'";
            return aInternalDal.DataContainerDataTable(queryStr);
        }

        public DataTable GetMiaNameById(string miaId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT MiaId,MiaName FROM dbo.tblMIAInfo WHERE MiaCode = '" + miaId + "'";
            return aInternalDal.DataContainerDataTable(queryStr);
        }

        public DataTable GetMarketNameById(string marketId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT MarketId,MarketName FROM dbo.tblMarket WHERE MarketCode = '" + marketId + "'";
            return aInternalDal.DataContainerDataTable(queryStr);
        }

        public bool UpdateApiCustomerInformation(CustomerMaster aCustomerMaster)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@CategoryId", aCustomerMaster.CategoryId));
            aSqlParameterlist.Add(new SqlParameter("@ApiCustomerId", aCustomerMaster.CustomerMasterId));
            //aSqlParameterlist.Add(new SqlParameter("@CustomerCode", aCustomerMaster.CustomerCode));
            aSqlParameterlist.Add(new SqlParameter("@CustomerName", aCustomerMaster.CustomerName));
            aSqlParameterlist.Add(new SqlParameter("@Address", aCustomerMaster.Address));
            aSqlParameterlist.Add(new SqlParameter("@CellNo", aCustomerMaster.CellNo));
            aSqlParameterlist.Add(new SqlParameter("@Addrees2", aCustomerMaster.Addrees2));
            aSqlParameterlist.Add(new SqlParameter("@City", aCustomerMaster.City));
            aSqlParameterlist.Add(new SqlParameter("@ConPerson", aCustomerMaster.ConPerson));
            //aSqlParameterlist.Add(new SqlParameter("@ShippingCond", aCustomerMaster.ShippingCond));
            aSqlParameterlist.Add(new SqlParameter("@MarketCode", aCustomerMaster.MarketCode));
            aSqlParameterlist.Add(new SqlParameter("@MarketName", aCustomerMaster.MarketName));
            aSqlParameterlist.Add(new SqlParameter("@MIACode", aCustomerMaster.MIACode));
            aSqlParameterlist.Add(new SqlParameter("@MiaName", aCustomerMaster.MiaName));
            aSqlParameterlist.Add(new SqlParameter("@AreaCode", aCustomerMaster.AreaCode));
            aSqlParameterlist.Add(new SqlParameter("@DisCode", aCustomerMaster.DisCode));
            aSqlParameterlist.Add(new SqlParameter("@FEName", aCustomerMaster.FEName));
            aSqlParameterlist.Add(new SqlParameter("@ComUnitCode", aCustomerMaster.ComUnitCode));
            aSqlParameterlist.Add(new SqlParameter("@ComUnitName", aCustomerMaster.ComUnitName));
            aSqlParameterlist.Add(new SqlParameter("@RegionCode", aCustomerMaster.RegionCode));
            aSqlParameterlist.Add(new SqlParameter("@DZSMName", aCustomerMaster.DZSMName));
            aSqlParameterlist.Add(new SqlParameter("@TermOfPayment", aCustomerMaster.TermOfPayment));
            aSqlParameterlist.Add(new SqlParameter("@FixedCustomer", aCustomerMaster.FixedCustomer));
           // aSqlParameterlist.Add(new SqlParameter("@LoginName", HttpContext.Current.Session["LoginName"].ToString()));

            return aCommonInternalDal.UpdateAction("sp_UD_ApiCustomerMaster", aSqlParameterlist);
        }

        public CustomerMaster ApiCustomerInformation(string customermasterid)
        {
            string query = "SELECT * FROM [dbo].[tbltempCustMaster] WHERE [tempCustomerMasterId] = '" + customermasterid + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            CustomerMaster aCustomerMaster = new CustomerMaster();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCustomerMaster.CustomerMasterId = Int32.Parse(dataReader["tempCustomerMasterId"].ToString());
                    aCustomerMaster.CustomerName = dataReader["CustomerName"].ToString();
                    aCustomerMaster.CustomerCode = dataReader["CustomerCode"].ToString();
                    aCustomerMaster.Address = dataReader["Address"].ToString();
                    aCustomerMaster.CellNo = dataReader["CellNo"].ToString();
                    aCustomerMaster.Addrees2 = dataReader["Addrees2"].ToString();
                    aCustomerMaster.City = dataReader["City"].ToString();
                    aCustomerMaster.ConPerson = dataReader["ConPerson"].ToString();
                    aCustomerMaster.MarketCode = dataReader["MarketCode"].ToString();
                    aCustomerMaster.MarketName = dataReader["MarketName"].ToString();
                    aCustomerMaster.MIACode = dataReader["MIACode"].ToString();
                    aCustomerMaster.MiaName = dataReader["MIAName"].ToString();
                    aCustomerMaster.AreaCode = dataReader["AreaCode"].ToString();
                    aCustomerMaster.DisCode = dataReader["DisCode"].ToString();
                    aCustomerMaster.FEName = dataReader["FEName"].ToString();
                    aCustomerMaster.ComUnitCode = dataReader["ComUnitCode"].ToString();
                    aCustomerMaster.ComUnitName = dataReader["ComUnitName"].ToString();
                    aCustomerMaster.RegionCode = dataReader["RegionCode"].ToString();
                    aCustomerMaster.DZSMName = dataReader["DZSMName"].ToString();
                    aCustomerMaster.TermOfPayment = dataReader["TermOfPayment"].ToString();
                    aCustomerMaster.FixedCustomer = (bool)dataReader["FixedCustomer"];
                }

            }
            return aCustomerMaster;
        }

        public bool HasCustomerMastername(CustomerMaster aMaster)
        {
            string query = "select * from tblCustMaster where CustomerCode = '" + aMaster.CustomerCode + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }


        public bool SaveApiCustomerInformation(CustomerMaster aCustomerMaster)
        {
            string insertQuery = @"INSERT INTO dbo.tblCustMaster
                        ( 
                          CustomerCode ,                          
                          CustomerName ,
                          CategoryId ,
                          Address ,
                          CellNo ,
                          Addrees2 ,
                          City ,
                          ConPerson ,
                          ShippingCond ,
                          MarketCode ,
                          MarketName ,
                          MIACode ,
                          MIAName ,
                          AreaCode ,
                          DisCode ,
                          FEName ,
                          ComUnitCode ,
                          ComUnitName ,
                          RegionCode ,
                          DZSMName ,
                          TermOfPayment ,
                          CustomerCodeOld,
                          FixedCustomer
                        )
                VALUES  ( '" + aCustomerMaster.CustomerCode + "' ,  " +
                        "  '" + aCustomerMaster.CustomerName + "' ,  " +
                        "  '" + aCustomerMaster.CategoryId + "' ,  " +
                        "  '" + aCustomerMaster.Address + "' ," +
                        "  '" + aCustomerMaster.CellNo + "' ," +
                        "  '" + aCustomerMaster.Addrees2 + "' ," +
                        "  '" + aCustomerMaster.City + "' , " +
                        "  '" + aCustomerMaster.ConPerson + "' , " +
                        "  '" + aCustomerMaster.ShippingCond + "' ,  " +
                        "  '" + aCustomerMaster.MarketCode + "' , " +
                        "  '" + aCustomerMaster.MarketName + "' ,  " +
                        "  '" + aCustomerMaster.MIACode + "' , " +
                        "  '" + aCustomerMaster.MiaName + "' ,  " +
                        "  '" + aCustomerMaster.AreaCode + "' , " +
                        " '" + aCustomerMaster.DisCode + "' ,  " +
                        "  '" + aCustomerMaster.FEName + "' , " +
                        "  '" + aCustomerMaster.ComUnitCode + "' ," +
                        "  '" + aCustomerMaster.ComUnitName + "' ,  " +
                        "  '" + aCustomerMaster.RegionCode + "' , " +
                        "  '" + aCustomerMaster.DZSMName + "' , " +
                        "  '" + aCustomerMaster.TermOfPayment + "' ,  " +
                        "  '" + aCustomerMaster.CustomerCodeOld + "' ,  " +
                        "  '" + aCustomerMaster.FixedCustomer + "'  " +
                       " )";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool UpdateApiCustomerInfo(int customerMasterId)
        {
            string query = @"UPDATE [dbo].[tbltempCustMaster] SET [AddtoMainCustomer] = 'True' WHERE [tempCustomerMasterId] = '" + customerMasterId + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
