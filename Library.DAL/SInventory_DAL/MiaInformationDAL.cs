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
    public class MiaInformationDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveDataForMiaInfo(MiaInformation aMiaInformation)
        {
            string insertQuery = @"insert into tblMIAInfo (MiaId,AreaId,MiaCode,MiaName) 
            values (" + aMiaInformation.MiaId + ",'" + aMiaInformation.AreaId + "','" + aMiaInformation.MiaCode + "','" + aMiaInformation.MiaName + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public bool HasMiaName(MiaInformation aMiaInformation)
        {
            string query = "select * from tblMIAInfo where MiaCode = '" + aMiaInformation.MiaCode + "'";
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

        public bool MioUpdate(string ter, string mio, string name)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ter", ter));
            aSqlParameterlist.Add(new SqlParameter("@mio", mio));
            aSqlParameterlist.Add(new SqlParameter("@name", name));

            return aCommonInternalDal.DeleteAction("sp_MioUpdateInCustomerInfo", aSqlParameterlist);

        }

        public DataTable LoadMiaInformationView()
        {
            string query = @"SELECT *  FROM tblMIAInfo
                            LEFT JOIN dbo.tblArea ON dbo.tblMIAInfo.AreaId = dbo.tblArea.AreaId
                            LEFT JOIN dbo.tblDistrict ON dbo.tblArea.DistrictId = dbo.tblDistrict.DistrictId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblDistrict.ComUnitId = dbo.tblCompanyUnit.ComUnitId
                            LEFT JOIN dbo.tblRegion ON dbo.tblCompanyUnit.RegionId = dbo.tblRegion.RegionId
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblRegion.CompanyId = dbo.tblCompanyInfo.CompanyId
                            LEFT JOIN dbo.tblManufacturer ON dbo.tblMIAInfo.ManufacId=dbo.tblManufacturer.ManufacId
                             ";
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        
        public MiaInformation MiaInformationEditLoad(string MiaId)
        {
            string query = @"SELECT *  FROM tblMIAInfo
                       
                         where MiaId = '" + MiaId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            MiaInformation aMiaInformation = new MiaInformation();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aMiaInformation.MiaId = Int32.Parse(dataReader["MiaId"].ToString());
                    aMiaInformation.MiaName = dataReader["MiaName"].ToString();
                    aMiaInformation.MiaCode = dataReader["MiaCode"].ToString();
                    //aMiaInformation.AreaId = Convert.ToInt32(dataReader["AreaId"].ToString());
                    //aMiaInformation.DistrictId = Convert.ToInt32(dataReader["DistrictId"].ToString());
                    //aMiaInformation.ComUnitId = Convert.ToInt32(dataReader["ComUnitId"].ToString());
                    //aMiaInformation.RegionId = Convert.ToInt32(dataReader["RegionId"].ToString());
                    //aMiaInformation.CompanyId = Convert.ToInt32(dataReader["CompanyId"].ToString());
                    
                }
            }
            return aMiaInformation;
        }
        
        public bool UpdateaMiaInformation(MiaInformation aMiaInformation)
        {
            string query = @"UPDATE tblMIAInfo SET MiaName='" + aMiaInformation.MiaName + "',MiaCode=" + aMiaInformation.MiaCode + " WHERE MiaId=" + aMiaInformation.MiaId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        
        public void LoadRegionname(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblRegion";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "RegionName", "RegionId", queryStr);
        }
        public void LoadManfac(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblManufacturer";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManufacName", "ManufacId", queryStr);
        }
        public DataTable LoadEmpInfo(string EmpInfoId)
        {
            DataTable aDataTableEmpInfo = new DataTable();
            string query = @"SELECT * FROM tblEmpGeneralInfo where EmpMasterCode='" + EmpInfoId.Trim() + "' ";
            aDataTableEmpInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableEmpInfo;
        }
        public DataTable LoadMiaInfoId(string MiaInfoId)
        {
            DataTable aDataTableMiaInfo = new DataTable();
            string query = @"SELECT * FROM tblMIAInfo where MiaCode='" + MiaInfoId.Trim() + "' ";
            aDataTableMiaInfo = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
            return aDataTableMiaInfo;
        }
        
    }
}
