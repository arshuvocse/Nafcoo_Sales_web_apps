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
    public class DistrictDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveDistrictInfo(DistrictInfo aDistrictInfo)
        {
            string insertQuery = @"insert into tblDistrict (DistrictId,DistrictCode,DistrictName) 
            values (" + aDistrictInfo.DistrictId + ",'" + aDistrictInfo.DistrictCode + "','" + aDistrictInfo.DistrictName + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasDistrictName(DistrictInfo aDistrict)
        {
            string query = "select * from tblDistrict where DistrictCode = '" + aDistrict.DistrictCode + "'";
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

        public DataTable LoadDistrictView()
        {
            string query = @"SELECT * from tblDistrict
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDistrict.ComUnitId = dbo.tblCompanyUnit.ComUnitId
                            LEFT JOIN dbo.tblRegion ON dbo.tblCompanyUnit.RegionId = dbo.tblRegion.RegionId
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public DistrictInfo DistrictInfoEditLoad(string DistrictId)
        {
            string query = @"SELECT * from tblDistrict
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDistrict.ComUnitId = dbo.tblCompanyUnit.ComUnitId
                            LEFT JOIN dbo.tblRegion ON dbo.tblCompanyUnit.RegionId = dbo.tblRegion.RegionId
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId where DistrictId = '" + DistrictId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            DistrictInfo aDistrict = new DistrictInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aDistrict.DistrictId = Int32.Parse(dataReader["DistrictId"].ToString());
                    //aDistrict.ComUnitId = Int32.Parse(dataReader["ComUnitId"].ToString());
                    aDistrict.DistrictCode = dataReader["DistrictCode"].ToString();
                    aDistrict.DistrictName = dataReader["DistrictName"].ToString();
                    //aDistrict.RegionId = Convert.ToInt32(dataReader["RegionId"].ToString());
                    //aDistrict.CompanyId = Convert.ToInt32(dataReader["CompanyId"].ToString());
                    
                }

            }
            return aDistrict;
        }

        public bool UpdateDistrictInfo(DistrictInfo aDistrict)
        {

            string query = @"UPDATE tblDistrict SET DistrictName='" + aDistrict.DistrictName + "',DistrictCode='" + aDistrict.DistrictCode + "' WHERE DistrictId=" + aDistrict.DistrictId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public void LoadCompanyUnit(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblCompanyUnit";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }

        public void LoadZoneByComUnit(DropDownList ddl,string comUnitId)
        {
            ClsCommonInternalDAL aInternalDal=new ClsCommonInternalDAL();
            string queryStr = "select ZoneId,ZoneName from tblZone where ComUnitId ='"+comUnitId+"'";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ZoneName", "ZoneId", queryStr);
        }
    }
}
