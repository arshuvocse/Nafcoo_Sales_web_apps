using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class CompanyInfoDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveDataForCompanyInfo(CompanyInformation aCompanyInfo)
        {
            string insertQuery = @"insert into tblCompanyInfo (CompanyId,CompanyCode,CompanyName,Address,ContactNo,FaxNo,Remarks) 
            values (" + aCompanyInfo.CompanyId + ",'" + aCompanyInfo.CompanyCode + "','" + aCompanyInfo.CompanyName + "','" + aCompanyInfo.Address + "'," +
                                 "'" + aCompanyInfo.ContactNo + "','" + aCompanyInfo.FaxNo + "','" + aCompanyInfo.Remarks + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasCompanyName(CompanyInformation aCompanyInfo)
        {
            string query = "select * from tblCompanyInfo where CompanyName = '" + aCompanyInfo.CompanyName + "'";
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


        public DataTable LoadCompanyInfo()
        {
            string query = @"SELECT tblCompanyInfo.CompanyId, tblCompanyInfo.CompanyName,tblCompanyInfo.Address,tblCompanyInfo.ContactNo,tblCompanyInfo.FaxNo,tblCompanyInfo.Remarks FROM tblCompanyInfo ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public CompanyInformation CompanyInfoEditLoad(string companyInfoId)
        {
            string query = "select * from tblCompanyInfo where CompanyId = '" + companyInfoId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            CompanyInformation aCompanyInfo = new CompanyInformation();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aCompanyInfo.CompanyId = Int32.Parse(dataReader["CompanyId"].ToString());
                    aCompanyInfo.CompanyCode = dataReader["CompanyCode"].ToString();
                    aCompanyInfo.CompanyName = dataReader["CompanyName"].ToString();
                    aCompanyInfo.ContactNo = dataReader["ContactNo"].ToString();
                    aCompanyInfo.Address = dataReader["Address"].ToString();
                    aCompanyInfo.FaxNo = dataReader["FaxNo"].ToString();
                    aCompanyInfo.Remarks = dataReader["Remarks"].ToString();

                }
            }
            return aCompanyInfo;
        }

        public bool UpdateCompanyInfo(CompanyInformation aCompanyInfo)
        {
            string query = @"UPDATE tblCompanyInfo SET CompanyName='" + aCompanyInfo.CompanyName + "',Address='" + aCompanyInfo.Address + "',ContactNo='" + aCompanyInfo.ContactNo + "',FaxNo='" + aCompanyInfo.FaxNo + "',Remarks='" + aCompanyInfo.Remarks + "' WHERE CompanyId=" + aCompanyInfo.CompanyId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
