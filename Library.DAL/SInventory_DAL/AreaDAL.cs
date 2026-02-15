using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class AreaDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveAreaInfo(AreaInfo areaInfo)
        {
            string insertQuery = @"insert into tblArea (AreaId,AreaCode,AreaName) 
            values (" + areaInfo.AreaId + ",'" + areaInfo.AreaCode + "','"+areaInfo.AreaName+"')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool HasAreaName(AreaInfo areaInfo)
        {
            string query = "select * from tblArea where AreaCode = '" + areaInfo.AreaCode + "'";
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

        public DataTable LoadAreaView()
        {
            string query = @"SELECT * FROM dbo.tblArea
                            LEFT JOIN dbo.tblDistrict ON dbo.tblArea.DistrictId = dbo.tblDistrict.DistrictId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDistrict.ComUnitId = dbo.tblCompanyUnit.ComUnitId
                            LEFT JOIN dbo.tblRegion ON dbo.tblCompanyUnit.RegionId = dbo.tblRegion.RegionId
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DataTable LoadSummaryProductcodewiseGyash(DateTime f, DateTime t,string Dc)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@FromDate", f));
            aSqlParameterList.Add(new SqlParameter("@ToDate", t));
            aSqlParameterList.Add(new SqlParameter("@DCID", Dc));
            return aCommonInternalDal.GetDataTableAction("sp_GET_InvoiceWiseDetailsSalesReport", aSqlParameterList, "SSIDB");

           // return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        
        public AreaInfo AreaEditLoad(string areaId)
        {
            string query = @"SELECT * FROM dbo.tblArea
                            LEFT JOIN dbo.tblDistrict ON dbo.tblArea.DistrictId = dbo.tblDistrict.DistrictId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDistrict.ComUnitId = dbo.tblCompanyUnit.ComUnitId
                            LEFT JOIN dbo.tblRegion ON dbo.tblCompanyUnit.RegionId = dbo.tblRegion.RegionId
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId
                             where AreaId = '" + areaId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            AreaInfo areaInfo = new AreaInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    areaInfo.AreaId = Int32.Parse(dataReader["AreaId"].ToString());
                    areaInfo.AreaName = dataReader["AreaName"].ToString();
                    areaInfo.AreaCode = dataReader["AreaCode"].ToString();
                    //areaInfo.DistrictId = string.IsNullOrEmpty(dataReader["DistrictId"].ToString()) ? 0 : Convert.ToInt32(dataReader["DistrictId"].ToString());
                    //areaInfo.ComUnitId = string.IsNullOrEmpty(dataReader["ComUnitId"].ToString()) ? 0 : Convert.ToInt32(dataReader["ComUnitId"].ToString());
                    //areaInfo.RegionId = string.IsNullOrEmpty(dataReader["RegionId"].ToString()) ? 0 : Convert.ToInt32(dataReader["RegionId"].ToString());
                    //areaInfo.CompanyId = string.IsNullOrEmpty(dataReader["CompanyId"].ToString()) ? 0 : Convert.ToInt32(dataReader["CompanyId"].ToString());
                }
            }
            return areaInfo;
        }

        public bool UpdateAreaInfo(AreaInfo areaInfo)
        {
            string query = @"UPDATE tblArea SET AreaName='" + areaInfo.AreaName + "',AreaCode='" + areaInfo.AreaCode + "' WHERE AreaId=" + areaInfo.AreaId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public void LoadDistrictName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDistrict";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "DistrictName", "DistrictId", queryStr);
        }
    }
}
