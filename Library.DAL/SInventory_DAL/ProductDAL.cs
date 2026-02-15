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
    public class ProductDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveDataForProduct(Product aProduct)
        {
            string insertQuery = @"insert into tblProduct (ProductId,ProductCode,ProductName,Description,PackSize,CategoryId,ManufacId,StockUOMId,ProTypeId,ProductBrandId,CaseId,PackSizeId,TherapueticGroupId,GenericGroupId,ProductGroupId,ProductImage,ProductLineID,IsActive) 
            values (" + aProduct.ProductId + ",'" + aProduct.ProductCode + "','" + aProduct.ProductName + "','" + aProduct.Description + "'," +
                                 "'" + aProduct.PackSize + "'," + aProduct.CategoryId + ",'" + aProduct.ManufacId + "','" + aProduct.StockUOMId + "','" + aProduct.ProTypeId + "','" + aProduct.ProductBrandId + "','" + aProduct.CaseId + "','" + aProduct.PackSizeId + "','" + aProduct.TherapueticGroupId + "','" + aProduct.GenericGroupId + "','" + aProduct.ProductGroupId + "','" + aProduct.ProductImage + "','" + aProduct.ProductLineID + "','" + aProduct.IsActive + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasProductName(Product aProduct)
        {
            string query = "select * from tblProduct where ProductCode = '" + aProduct.ProductCode + "'";
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


        public DataTable LoadProduct()
        {
            string query = @"SELECT gg.GenericGroupName, cat.CategoryName, pro.PackSize, pg.GroupName, um.StockUOMName, * FROM tblProduct pro with (nolock)
LEFT JOIN dbo.tblProCategory  cat  with (nolock) ON pro.CategoryId = cat.CategoryId
 LEFT JOIN dbo.tblPackSize ps with (nolock) ON pro.PackSizeId=ps.PackSizeId
 LEFT JOIN dbo.tblProductCase  pCase  with (nolock) ON pCase.ProductCode=pro.ProductCode

 LEFT JOIN dbo.tblGenericGroup gg with (nolock) ON pro.GenericGroupId=gg.GenericGroupId
 LEFT JOIN dbo.tblProductGroup pg with (nolock) ON pro.ProductGroupId=pg.GroupId
 LEFT JOIN dbo.tblStockUOM um with (nolock) ON pro.StockUOMId=um.StockUOMId   order by pro.ProductName asc
 ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public Product ProductEditLoad(string ProductId)
        {
            string query = "select  STUFF( (SELECT CONCAT(',', brn.ComUnitId , '') FROM dbo.tblProductDCDetails brn(NOLOCK)  WHERE brn.ProductId=tblProduct.ProductId ORDER BY brn.ComUnitId FOR XML PATH ('') ),1,1,'') AS ProductDCID,* from tblProduct where ProductId = '" + ProductId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            Product aProduct = new Product();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aProduct.ProductId = Int32.Parse(dataReader["ProductId"].ToString());
                    aProduct.ProductCode = dataReader["ProductCode"].ToString();
                    aProduct.ProductName = dataReader["ProductName"].ToString();
                    aProduct.PackSize = dataReader["PackSize"].ToString();
                    aProduct.Description = dataReader["Description"].ToString();
                    aProduct.ProductDCID = dataReader["ProductDCID"].ToString();

                    if (dataReader["CategoryId"] != DBNull.Value)
                    {
                        aProduct.CategoryId = Convert.ToInt32(dataReader["CategoryId"].ToString());

                    }
                    if (dataReader["ManufacId"] != DBNull.Value)
                    {
                        aProduct.ManufacId = Convert.ToInt32(dataReader["ManufacId"].ToString());
                    }

                    if (dataReader["StockUOMId"] != DBNull.Value)
                    {
                        aProduct.StockUOMId = Convert.ToInt32(dataReader["StockUOMId"].ToString());
                    }

                    if (dataReader["ProTypeId"] != DBNull.Value)
                    {
                        aProduct.ProTypeId = Convert.ToInt32(dataReader["ProTypeId"].ToString());
                    }

                    if (dataReader["ProductBrandId"] != DBNull.Value)
                    {
                        aProduct.ProductBrandId = Convert.ToInt32(dataReader["ProductBrandId"].ToString());
                    }

                    if (dataReader["CaseId"] != DBNull.Value)
                    {
                        aProduct.CaseId = Convert.ToInt32(dataReader["CaseId"].ToString());
                    }

                    if (dataReader["PackSizeId"] != DBNull.Value)
                    {
                        aProduct.PackSizeId = Convert.ToInt32(dataReader["PackSizeId"].ToString());
                    }

                    if (dataReader["GenericGroupId"] != DBNull.Value)
                    {
                        aProduct.GenericGroupId = Convert.ToInt32(dataReader["GenericGroupId"].ToString());
                    }

                    if (dataReader["TherapueticGroupId"] != DBNull.Value)
                    {
                        aProduct.TherapueticGroupId = Convert.ToInt32(dataReader["TherapueticGroupId"].ToString());
                    }

                    if (dataReader["ProductGroupId"] != DBNull.Value)
                    {
                        aProduct.ProductGroupId = Convert.ToInt32(dataReader["ProductGroupId"].ToString());
                    }
                    if (dataReader["ProductLineID"] != DBNull.Value)
                    {
                        aProduct.ProductLineID = Convert.ToInt32(dataReader["ProductLineID"].ToString());
                    }
                    aProduct.ProductImage = dataReader["ProductImage"].ToString();



                }
            }
            return aProduct;
        }

        public bool UpdateProduct(Product aProduct)
        {
            string query = @"UPDATE tblProduct SET  IsActive='" + aProduct.IsActive + "',ProductName='" + aProduct.ProductName + "',ProductCode='" + aProduct.ProductCode + "',Description='" + aProduct.Description + "',PackSize='" + aProduct.PackSize + "',CategoryId=" + aProduct.CategoryId + ",ManufacId='" + aProduct.ManufacId + "',StockUOMId='" + aProduct.StockUOMId + "',ProTypeId='" + aProduct.ProTypeId + "',ProductBrandId='" + aProduct.ProductBrandId + "',ProductGroupId='" + aProduct.ProductGroupId + "',GenericGroupId='" + aProduct.GenericGroupId + "',TherapueticGroupId='" + aProduct.TherapueticGroupId + "',ProductImage='" + aProduct.ProductImage    + "',ProductLineID='" + aProduct.ProductLineID + "' WHERE ProductId=" + aProduct.ProductId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }


        public void LoadCategoryName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblProCategory";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "CategoryName", "CategoryId", queryStr);
        }
        public void LoadManufac(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblManufacturer";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }
        public void LoadPackSize(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblPackSize";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "PackSizeName", "PackSizeId", queryStr);
        }
        public void LoadStockUOM(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblStockUOM";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "StockUOMName", "StockUOMId", queryStr);
        }
        public void LoadType(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblProType";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ProTypeName", "ProTypeId", queryStr);
        }
        public void LoadIngrident(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblIngridents";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "IngridentsName", "IngridentsId", queryStr);
        }
        public void LoadProductSQ(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblProductSQ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ProductSQName", "ProductBrandId", queryStr);
        }

        public void LoadTherapeuticGroup(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblTherapeuticGroup with (nolock) where IsActive=1";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "TherapeuticGroupName", "TherapeuticGroupId", queryStr);
        }

        public void LoadGenericGroup(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblGenericGroup with (nolock) where IsActive=1";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "GenericGroupName", "GenericGroupId", queryStr);
        }

        public void LoadProductType_new(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblProductGroup with (nolock)";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "GroupName", "GroupId", queryStr);
        }
        public void LoadShippingCartonSize(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblProductCase";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "PcsPerCase", "CaseId", queryStr);
        }
        public DataTable ProductPriceDetailWithCase(string productCode)
        {
            string query = @"SELECT UP.*,PC.CaseQty,PC.PcsPerCase FROM dbo.tblUnitPrice UP "+
                            " LEFT JOIN dbo.tblProduct P ON UP.ProductCode = P.ProductCode  " +
                             " LEFT JOIN dbo.tblProductCase PC ON p.CaseId = PC.CaseId   " +
                            " WHERE UP.ProductCode='" + productCode.Trim() + "' ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
    }
}
