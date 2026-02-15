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
    public class RegionDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveRegionInfo(RegionInfo aRegionInfo)
        {
            string insertQuery = @"insert into tblRegion (RegionId,RegionCode,RegionName) 
            values (" + aRegionInfo.RegionId + ",'" + aRegionInfo.RegionCode + "','" + aRegionInfo.RegionName + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasRegionName(RegionInfo aRegionInfo)
        {
            string query = "select * from tblRegion where RegionCode = '" + aRegionInfo.RegionCode + "'";
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

        public void LoadCompanyName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblCompanyInfo";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "CompanyName", "CompanyId", queryStr);
        }


        public DataTable LoadRegionInfo()
        {
            string query = @"SELECT * from tblRegion
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public RegionInfo RegionInfoEditLoad(string RegionId)
        {
            string query = "select * from tblRegion where RegionId = '" + RegionId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            RegionInfo aRegionInfo = new RegionInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aRegionInfo.RegionId = Int32.Parse(dataReader["RegionId"].ToString());
                    aRegionInfo.RegionCode = dataReader["RegionCode"].ToString();
                    aRegionInfo.RegionName = dataReader["RegionName"].ToString();
                    //aRegionInfo.CompanyId = Convert.ToInt32(dataReader["CompanyId"].ToString());
                }

            }
            return aRegionInfo;
        }

        public bool UpdateRegionInfo(RegionInfo aRegionInfo)
        {

            string query = @"UPDATE tblRegion SET RegionName='" + aRegionInfo.RegionName + "',RegionCode='" + aRegionInfo.RegionCode + "' WHERE RegionId=" + aRegionInfo.RegionId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
