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
    public class ZoneInfoDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveZoneInfo(ZoneInfo aZoneInfo)
        {
            string insertQuery = @"insert into tblZone (ZoneId,ZoneCode,ZoneName,ComUnitId,ComUnitName) 
            values (" + aZoneInfo.ZoneId + ",'" + aZoneInfo.ZoneCode + "','" + aZoneInfo.ZoneName + "'," + aZoneInfo.ComUnitId + ",'"+aZoneInfo.ComUnitName+"')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasZoneName(ZoneInfo aZoneInfo)
        {
            string query = "select * from tblZone where ZoneName = '" + aZoneInfo.ZoneName + "'";
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

        public void LoadCompanyUnit(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblCompanyUnit";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ComUnitName", "ComUnitId", queryStr);
        }


        public DataTable LoadZoneInfo()
        {
            string query = @"SELECT * from tblZone ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public ZoneInfo ZoneEditLoad(string zoneId)
        {
            string query = "select * from tblZone where ZoneId = '" + zoneId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            ZoneInfo aZoneInfo = new ZoneInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aZoneInfo.ZoneId = Int32.Parse(dataReader["ZoneId"].ToString());
                    aZoneInfo.ZoneCode = dataReader["ZoneCode"].ToString();
                    aZoneInfo.ZoneName = dataReader["ZoneName"].ToString();
                    aZoneInfo.ComUnitId = Convert.ToInt32(dataReader["ComUnitId"].ToString());
                }

            }
            return aZoneInfo;
        }

        public bool UpdateZoneInfo(ZoneInfo aZoneInfo)
        {

            string query = @"UPDATE tblZone SET ZoneName='" + aZoneInfo.ZoneName + "',ComUnitName='" + aZoneInfo.ComUnitName + "',ComUnitId='" + aZoneInfo.ComUnitId + "' WHERE ZoneId=" + aZoneInfo.ZoneId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
   
}
