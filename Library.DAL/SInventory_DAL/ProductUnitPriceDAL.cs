using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class ProductUnitPriceDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveProductUnitPrice(ProductUnitPrice aProductUnitPrice)
        {
            string insertQuery = @"insert into tblUnitPrice (UnitPriceId,ProductId,ProductCode,ProductName,PackSize,CostPrice,UnitPrice,VATPercentage,VATAmountPerUnit,IsActive,ActiveDate,MRPPrice) 
            values (" + aProductUnitPrice.UnitPriceId + "," + aProductUnitPrice.ProductId + ",'" + aProductUnitPrice.ProductCode + "','" + aProductUnitPrice.ProductName + "','" + aProductUnitPrice.PackSize + "','" + aProductUnitPrice.CostPrice + "','" + aProductUnitPrice.UnitPrice + "','" + aProductUnitPrice.VATPercentage + "','" + aProductUnitPrice.VATAmountPerUnit + "','" + aProductUnitPrice.IsActive + "','" + aProductUnitPrice.ActiveDate + "','" + aProductUnitPrice.MRPPrice + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool SaveProductUnitPriceUpdate(ProductUnitPrice aProductUnitPrice)
        {
            string insertQuery = @"insert into tblUnitPriceUpdate (UnitPriceUpdateId,UnitPriceId,ProductId,ProductCode,ProductName,PackSize,CostPrice,UnitPrice,VATPercentage,VATAmountPerUnit,ActiveDate) 
            values ('" + aProductUnitPrice.UnitPriceUpdateId + "','" + aProductUnitPrice.UnitPriceId + "'," + aProductUnitPrice.ProductId + ",'" + aProductUnitPrice.ProductCode + "','" + aProductUnitPrice.ProductName + "','" + aProductUnitPrice.PackSize + "','" + aProductUnitPrice.CostPrice + "','" + aProductUnitPrice.UnitPrice + "','" + aProductUnitPrice.VATPercentage + "','" + aProductUnitPrice.VATAmountPerUnit + "','" + aProductUnitPrice.ActiveDate + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasProductName(ProductUnitPrice aCategory)
        {
            string query = "select * from tblUnitPrice where ProductId = '" + aCategory.ProductId + "'";
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
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public DataTable LoadUnitPriceView()
        {
            string query = @"SELECT (UnitPrice+VATAmountPerUnit)TPVat ,* from tblUnitPrice 
                LEFT JOIN dbo.tblProduct ON dbo.tblUnitPrice.ProductId = dbo.tblProduct.ProductId
                LEFT JOIN dbo.tblProductSQ ON dbo.tblProduct.ProductBrandId = dbo.tblProductSQ.ProductBrandId
                LEFT JOIN dbo.tblPackSize ON dbo.tblProduct.PackSizeId=dbo.tblPackSize.PackSizeId
                LEFT JOIN dbo.tblProType ON dbo.tblProType.ProTypeId=dbo.tblProduct.ProTypeId
                LEFT JOIN dbo.tblProCategory ON dbo.tblProduct.CategoryId = dbo.tblProCategory.CategoryId
                LEFT JOIN dbo.tblManufacturer ON dbo.tblProduct.ManufacId=dbo.tblManufacturer.ManufacId
                LEFT JOIN dbo.tblStockUOM ON dbo.tblProduct.StockUOMId=dbo.tblStockUOM.StockUOMId
                LEFT JOIN dbo.tblProductCase ON dbo.tblProductCase.CaseId=dbo.tblProduct.CaseId where tblUnitPrice.IsActive='1'";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public DataTable GetProductPriceReportInfo()
        {
            string query = @"SELECT [ProductCode],[ProductName],[PackSize],[CostPrice],[UnitPrice],[VATPercentage],[VATAmountPerUnit],[IsActive],[ActiveDate],[InActiveDate] from tblUnitPrice WITH(NOLOCK) ORDER BY IsActive DESC";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }



        /// /////////////////////////////////////////////////////////////////////////
        
        public ProductUnitPrice ProductUnitPriceEditLoad(string UnitPriceId)
        {
            string query = "select * from tblUnitPrice where UnitPriceId = '" + UnitPriceId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ProductUnitPrice aCategory = new ProductUnitPrice();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCategory.UnitPriceId = Int32.Parse(dataReader["UnitPriceId"].ToString());
                    aCategory.ProductId = Int32.Parse(dataReader["ProductId"].ToString());
                    aCategory.ProductCode = dataReader["ProductCode"].ToString();
                    aCategory.ProductName = dataReader["ProductName"].ToString();
                    aCategory.VATAmountPerUnit = Convert.ToDecimal(dataReader["VATAmountPerUnit"].ToString());
                    aCategory.VATPercentage = Convert.ToDecimal(dataReader["VATPercentage"].ToString());
                    aCategory.PackSize = dataReader["PackSize"].ToString();
                    aCategory.CostPrice = Convert.ToDecimal(dataReader["CostPrice"].ToString());
                    aCategory.UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"].ToString());
                    aCategory.MRPPrice = Convert.ToDecimal(dataReader["MRPPrice"].ToString());
                    aCategory.ActiveDate = Convert.ToDateTime(dataReader["ActiveDate"].ToString());
                    aCategory.IsActive = Convert.ToBoolean(dataReader["IsActive"].ToString());
                }
            }
            return aCategory;
        }
        public ProductUnitPrice ProductUnitPriceEditLoadProduct(string productId)
        {
            string query = "select * from tblUnitPrice where ProductId = '" + productId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ProductUnitPrice aCategory = new ProductUnitPrice();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCategory.UnitPriceId = Int32.Parse(dataReader["UnitPriceId"].ToString());
                    aCategory.ProductId = Int32.Parse(dataReader["ProductId"].ToString());
                    aCategory.ProductCode = dataReader["ProductCode"].ToString();
                    aCategory.ProductName = dataReader["ProductName"].ToString();
                    aCategory.VATAmountPerUnit = Convert.ToDecimal(dataReader["VATAmountPerUnit"].ToString());
                    aCategory.VATPercentage = Convert.ToDecimal(dataReader["VATPercentage"].ToString());
                    aCategory.PackSize = dataReader["PackSize"].ToString();
                    aCategory.CostPrice = Convert.ToDecimal(dataReader["CostPrice"].ToString());
                    aCategory.UnitPrice = Convert.ToDecimal(dataReader["UnitPrice"].ToString());
                }
            }
            return aCategory;
        }
        
        public bool UpdateCustCategoryInfo(ProductUnitPrice aCategory)
        {
            string query = @"UPDATE tblUnitPrice SET ProductName='" + aCategory.ProductName + "',CostPrice='" + aCategory.CostPrice + "',UnitPrice='" + aCategory.UnitPrice + "',PackSize='" + aCategory.PackSize + "',ProductCode='" + aCategory.ProductCode + "',VATPercentage='" + aCategory.VATPercentage + "',VATAmountPerUnit='"+aCategory.VATAmountPerUnit+"' WHERE UnitPriceId=" + aCategory.UnitPriceId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool UpdateActive(DateTime inactivedate,string unitpriceId)
        {
            string query = @"UPDATE tblUnitPrice SET IsActive='False',InActiveDate='" + inactivedate + "' WHERE UnitPriceId=" + unitpriceId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        private DataAccessManager accessManager = new DataAccessManager();

        public bool UpdateActivePrice(DateTime inactivedate,string unitpriceId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Inactivedate", inactivedate));
                aSqlParameters.Add(new SqlParameter("@UnitpriceId", unitpriceId));
                aSqlParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["LoginName"].ToString()));
                aSqlParameters.Add(new SqlParameter("@UpdateDate", DateTime.Now));
                bool status = accessManager.UpdateData("sp_Save_InactiveUnitPrice", aSqlParameters);

                return status;
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
        public bool DeleteProduct(string productId)
        {
            string query = @"DELETE FROM dbo.tblUnitPrice WHERE ProductId='"+productId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public DataTable LoadProduct(string productId)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT * FROM tblProduct where ProductId='" + productId.Trim() + "' ";
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }

        public DataTable TotalUnitPriceReport()
        {
            string query = @"select * from tblUnitPrice";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

    }
}
