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
    public class MarketInfoDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveMarketInfo(MarketInfo aMarketInfo)
        {
            string insertQuery = @"insert into tblMarket (MarketId,MarketCode,MarketName) 
            values (" + aMarketInfo.MarketId + ",'" + aMarketInfo.MarketCode + "','" + aMarketInfo.MarketName + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasMarketName(MarketInfo aMarketInfo)
        {
            string query = "select * from tblMarket where MarketCode = '" + aMarketInfo.MarketCode + "'";
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

        public DataTable LoadMarketCiew()
        {
            string query = @"SELECT  * FROM dbo.tblMarket
          ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public MarketInfo MarketInfoEditLoad(string MarketId)
        {
            string query = @"SELECT  * FROM dbo.tblMarket
                 where MarketId = '" + MarketId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            MarketInfo aMarketInfo = new MarketInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aMarketInfo.MarketId = Int32.Parse(dataReader["MarketId"].ToString());
                    aMarketInfo.MarketCode = dataReader["MarketCode"].ToString();
                    aMarketInfo.MarketName = dataReader["MarketName"].ToString();
                    //aMarketInfo.AreaId = Convert.ToInt32(dataReader["AreaId"].ToString());
                    //aMarketInfo.MiaId = Convert.ToInt32(dataReader["MiaId"].ToString());
                    //aMarketInfo.DistrictId = Convert.ToInt32(dataReader["DistrictId"].ToString());
                    //aMarketInfo.ComUnitId = Convert.ToInt32(dataReader["ComUnitId"].ToString());
                    //aMarketInfo.RegionId = Convert.ToInt32(dataReader["RegionId"].ToString());
                    //aMarketInfo.CompanyId = Convert.ToInt32(dataReader["CompanyId"].ToString());
                    
                }
            }
            return aMarketInfo;
        }

        public bool UpdateCustCategoryInfo(MarketInfo aMarketInfo)
        {
            string query = @"UPDATE tblMarket SET MarketName='" + aMarketInfo.MarketName + "',MarketCode='" + aMarketInfo.MarketCode + "'  WHERE MarketId=" + aMarketInfo.MarketId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public void LoadAreaName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblArea ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "AreaName", "AreaId", queryStr);
        }
        public void LoadMiaName(DropDownList ddl,string areaId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblMIAInfo where AreaId='"+areaId+"' ";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "MiaName", "MiaId", queryStr);
        }
    }
}
