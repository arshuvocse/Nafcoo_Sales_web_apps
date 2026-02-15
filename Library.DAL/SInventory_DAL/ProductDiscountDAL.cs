using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class ProductDiscountDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveProductDiscount(ProductDiscount aProductDiscount)
        {
            string insertQuery = @"insert into tblProductDiscount (DiscountId,ProductCode,CustomerMasterId,DiscountPercentage,Status,ActiveDate,InactiveDate) 
            values (" + aProductDiscount.DiscountId + ",'" + aProductDiscount.ProductCode + "','" + aProductDiscount.CustomerMasterId + "','" + aProductDiscount.DiscountPercentage + "','" + aProductDiscount.Status + "','" + aProductDiscount.ActiveDate + "','" + aProductDiscount.InactiveDate + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasProductDiscountName(ProductDiscount aProductDiscount)
        {
            string query = "select * from tblProductDiscount where ProductCode = '" + aProductDiscount.ProductCode + "'";
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

        public DataTable LoadProductDiscount()
        {
            string query = @"SELECT * from tblProductDiscount
            LEFT JOIN dbo.tblCustMaster ON dbo.tblProductDiscount.CustomerMasterId=dbo.tblCustMaster.CustomerMasterId
            LEFT JOIN dbo.tblProduct ON dbo.tblProductDiscount.ProductCode=dbo.tblProduct.ProductCode";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable LoadProductDiscount(string fromdate,string todate)
        {
            string query = @"SELECT (MIACode+'-'+MIAName)MIAName,* from tblProductDiscount
            LEFT JOIN dbo.tblCustMaster ON dbo.tblProductDiscount.CustomerMasterId=dbo.tblCustMaster.CustomerMasterId
            LEFT JOIN dbo.tblProduct ON dbo.tblProductDiscount.ProductCode=dbo.tblProduct.ProductCode WHERE ActiveDate BETWEEN '" + fromdate + "' AND '" + todate + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public void LoadCustomerMaster(DropDownList ddl,string marketId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblCustMaster WHERE CustomerMasterId IN (SELECT DISTINCT CustomerMasterId FROM dbo.View_CustomerMaster WHERE MarketId='"+marketId+"')";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "CustomerName", "CustomerMasterId", queryStr);
        }
        public void LoadSalesCenter(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblCompanyUnit";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }
        public void LoadArea(DropDownList ddl,string comUnitId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblArea WHERE AreaId IN (SELECT AreaId FROM dbo.View_CustomerMaster WHERE ComUnitId='"+comUnitId+"')";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "AreaName", "AreaId", queryStr);
        }
        public void LoadMarket(DropDownList ddl, string areaId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblMarket WHERE MarketId IN (SELECT DISTINCT MarketId FROM dbo.View_CustomerMaster WHERE AreaId='"+areaId+"')";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MarketName", "MarketId", queryStr);
        }
        

        public ProductDiscount ProductDiscountEditLoad(string ID)
        {
            string query = "select * from tblProductDiscount where DiscountId = '" + ID + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ProductDiscount aProductDiscount = new ProductDiscount();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aProductDiscount.DiscountId = Int32.Parse(dataReader["DiscountId"].ToString());
                    aProductDiscount.ProductCode = dataReader["ProductCode"].ToString();
                    aProductDiscount.CustomerMasterId = Convert.ToInt32(dataReader["CustomerMasterId"].ToString());
                    aProductDiscount.DiscountPercentage = Convert.ToDecimal(dataReader["DiscountPercentage"].ToString());
                    aProductDiscount.Status = dataReader["Status"].ToString();
                    aProductDiscount.ActiveDate = Convert.ToDateTime(dataReader["ActiveDate"].ToString());
                    aProductDiscount.InactiveDate = Convert.ToDateTime(dataReader["InactiveDate"].ToString());
                }
            }
            return aProductDiscount;
        }

        public bool UpdateProductDiscountInfo(ProductDiscount aProductDiscount)
        {
            string query = @"UPDATE tblProductDiscount SET ProductCode='" + aProductDiscount.ProductCode + "',CustomerMasterId='" + aProductDiscount.CustomerMasterId + "',DiscountPercentage='" + aProductDiscount.DiscountPercentage + "',ActiveDate='" + aProductDiscount.ActiveDate + "',InactiveDate='" + aProductDiscount.InactiveDate + "' WHERE DiscountId=" + aProductDiscount.DiscountId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
