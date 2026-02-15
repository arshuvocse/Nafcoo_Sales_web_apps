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
   
    public class CompanywisebranchDal
    {
      //   ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        ClsCommonInternalDAL aCommonInternalDAL = new ClsCommonInternalDAL();

        public Int32 SaveDepositInfo(CompanyWiseDepositDao aDepositDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDepositDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@Amount", aDepositDao.Amount));
            aSqlParameterlist.Add(new SqlParameter("@AIT", aDepositDao.AIT));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDepositDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDepositDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@DepositDate", aDepositDao.DepositDate));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aDepositDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@IsExcelUpload", aDepositDao.IsExcelUpload));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDepositDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@DepositType", aDepositDao.DepositType));

            aSqlParameterlist.Add(new SqlParameter("@BankId", aDepositDao.BankId));
            aSqlParameterlist.Add(new SqlParameter("@CheckNumber", aDepositDao.CheckNumber ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CheckDate", aDepositDao.CheckDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BranchName", aDepositDao.BranchName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AccountName", aDepositDao.AccountName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AccountNameActual", aDepositDao.AccountNameActual ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@ReferenceName", aDepositDao.ReferenceName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DocumentLink", aDepositDao.DocumentLink ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FileName", aDepositDao.FileName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DocumentNote", aDepositDao.DocumentNote ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DocumentLinkPreview", aDepositDao.DocumentLinkPreview ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MonthName", aDepositDao.MonthName ?? (object)DBNull.Value));
            //aSqlParameterlist.Add(new SqlParameter("@ReferenceDate", aDepositDao.ReferenceDate ?? (object)DBNull.Value));

            return aCommonInternalDAL.SaveAction("sp_I_Diposit_New", aSqlParameterlist, "@DepositId");

            //string insertQuery = @"INSERT INTO tblCompanyWiseDeposit(CompanyId,BranchName,Amount,EntryBy,EntryDate,DepositDate,IsDelete,Remarks,DepositType,BankId,CheckNumber,CheckDate,AccountName) VALUES (@CompanyId,@BranchName,@Amount,@EntryBy,@EntryDate,@DepositDate,@IsDelete,@Remarks,@DepositType,@BankId,@CheckNumber,@CheckDate,@AccountName)";
            //return aCommonInternalDAL.SaveDataByInsertCommandWithIdentity(insertQuery, aSqlParameterlist, "SSIDB");
        }


        public bool UpdateDepositImage(CompanyWiseDepositDao aDepositDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DepositId", aDepositDao.DepositId));

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDepositDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@Amount", aDepositDao.Amount));
            aSqlParameterlist.Add(new SqlParameter("@AIT", aDepositDao.AIT)); 
            aSqlParameterlist.Add(new SqlParameter("@DepositDate", aDepositDao.DepositDate));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDepositDao.Remarks??(object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DepositType", aDepositDao.DepositType));

            aSqlParameterlist.Add(new SqlParameter("@BankId", aDepositDao.BankId));
            aSqlParameterlist.Add(new SqlParameter("@CheckNumber", aDepositDao.CheckNumber ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CheckDate", aDepositDao.CheckDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BranchName", aDepositDao.BranchName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AccountName", aDepositDao.AccountName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AccountNameActual", aDepositDao.AccountNameActual ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReferenceName", aDepositDao.ReferenceName ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aDepositDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@DocumentLink", aDepositDao.DocumentLink ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FileName", aDepositDao.FileName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DocumentNote", aDepositDao.DocumentNote ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DocumentLinkPreview", aDepositDao.DocumentLinkPreview ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MonthName", aDepositDao.MonthName ?? (object)DBNull.Value));

            return aCommonInternalDAL.UpdateAction("sp_UD_Diposit_New_T", aSqlParameterlist);


            //return aCommonInternalDAL.UpdateAction("sp_UD_Diposit_New", aSqlParameterlist);
            //string insertQuery = @"INSERT INTO tblCompanyWiseDeposit(CompanyId,BranchName,Amount,EntryBy,EntryDate,DepositDate,IsDelete,Remarks,DepositType,BankId,CheckNumber,CheckDate,AccountName) VALUES (@CompanyId,@BranchName,@Amount,@EntryBy,@EntryDate,@DepositDate,@IsDelete,@Remarks,@DepositType,@BankId,@CheckNumber,@CheckDate,@AccountName)";
            //return aCommonInternalDAL.SaveDataByInsertCommandWithIdentity(insertQuery, aSqlParameterlist, "SSIDB");
        }

        public DataTable LoadDepositList(string parameter)
        {
            //            string queryStr = @"SELECT AIT,DP.DepositId,CI.ComUnitName,BNK.BankName,DP.BranchName,DP.DepositDate,DP.Amount,DP.DepositType,DP.AccountName,DP.AccountNameActual,CheckNumber,CheckDate,Remarks, ReferenceName, ReferenceDate FROM tblCompanyWiseDeposit AS DP
            //INNER JOIN tblCompanyUnit  AS CI ON DP.CompanyId = CI.ComUnitId
            //LEFT JOIN tblBankInfo AS BNK ON DP.BankId = BNK.BankId WHERE DP.IsDelete = 0 " + parameter;

            string queryStr = @"SELECT AIT,DP.DepositId,CI.ComUnitName,BNK.BankName,DP.BranchName,DP.DepositDate,DP.Amount,DP.DepositType,DP.AccountName,DP.AccountNameActual,CheckNumber,CheckDate,Remarks, ReferenceName, ReferenceDate, ('http://103.244.247.93:83/'+DocumentLink) AS DocumentLink,DocumentNote,FileName,DocumentLinkPreview, CASE WHEN DocumentLink IS NULL THEN 'NO' ELSE 'YES' END IsImage,DP.EntryDate  FROM tblCompanyWiseDeposit AS DP
            INNER JOIN tblCompanyUnit  AS CI ON DP.CompanyId = CI.ComUnitId
            LEFT JOIN tblBankInfo AS BNK ON DP.BankId = BNK.BankId WHERE DP.IsDelete = 0 " + parameter;

            return aCommonInternalDAL.DataContainerDataTable(queryStr, "SSIDB");
        }
        public void LoadCompany(DropDownList ddl, string userId)
        {
            string queryStr = "select ComUnitId, ComUnitName  from tblCompanyUnit WHERE " +
                              " ComUnitId IN (SELECT CompanyUnitId FROM dbo.tblUserCompanyUnit WHERE UserId='" + userId.Trim() + "')";
            aCommonInternalDAL.LoadDropDownValue(ddl, "ComUnitName", "ComUnitId", queryStr, "SSIDB");
        }

        public void LoadBank(DropDownList ddl)
        {
            string query = @"SELECT BankId,BankName FROM tblBankInfo";
            aCommonInternalDAL.LoadDropDownValue(ddl, "BankName", "BankId", query, "SSIDB");
        }

        public void LoadBranch(DropDownList ddl, string bankId)
        {
            string query = @"SELECT BranchId,BranchName FROM tblBranchInfo WHERE BankId = " + bankId;
            aCommonInternalDAL.LoadDropDownValue(ddl, "BranchName", "BranchId", query, "SSIDB");
        }

        public DataTable GetDepositSlip(string parameter)
        {
            string queryStr = @"SELECT CONVERT(varchar,DepositDate,106)DepositDate,CONVERT(varchar,CheckDate,106)CheckDate,* FROM tblCompanyWiseDeposit WHERE DepositId =" + parameter;
            return aCommonInternalDAL.DataContainerDataTable(queryStr, "SSIDB");
        }
        
        public DataTable LoadBankById(string parameter)
        {
            string queryStr = @"SELECT * FROM tblBankInfo WHERE BankId =" + parameter;
            return aCommonInternalDAL.DataContainerDataTable(queryStr, "SSIDB");
        }



//        public Int32 SaveDepositInfo(CompanyWiseDepositDao aDepositDao)
//        {
//            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

//            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDepositDao.CompanyId));
//            aSqlParameterlist.Add(new SqlParameter("@Amount", aDepositDao.Amount));
//            aSqlParameterlist.Add(new SqlParameter("@AIT", aDepositDao.AIT));
//            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDepositDao.EntryBy));
//            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDepositDao.EntryDate));
//            aSqlParameterlist.Add(new SqlParameter("@DepositDate", aDepositDao.DepositDate));
//            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aDepositDao.IsDelete));
//            aSqlParameterlist.Add(new SqlParameter("@IsExcelUpload", aDepositDao.IsExcelUpload));
//            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDepositDao.Remarks));
//            aSqlParameterlist.Add(new SqlParameter("@DepositType", aDepositDao.DepositType));

//            aSqlParameterlist.Add(new SqlParameter("@BankId", aDepositDao.BankId ));
//            aSqlParameterlist.Add(new SqlParameter("@CheckNumber", aDepositDao.CheckNumber ?? (object)DBNull.Value));
//            aSqlParameterlist.Add(new SqlParameter("@CheckDate", aDepositDao.CheckDate ?? (object)DBNull.Value));
//            aSqlParameterlist.Add(new SqlParameter("@BranchName", aDepositDao.BranchName ?? (object)DBNull.Value));
//            aSqlParameterlist.Add(new SqlParameter("@AccountName", aDepositDao.AccountName ?? (object)DBNull.Value));
//            aSqlParameterlist.Add(new SqlParameter("@AccountNameActual", aDepositDao.AccountNameActual ?? (object)DBNull.Value));

//            aSqlParameterlist.Add(new SqlParameter("@ReferenceName", aDepositDao.ReferenceName ?? (object)DBNull.Value));
//            //aSqlParameterlist.Add(new SqlParameter("@ReferenceDate", aDepositDao.ReferenceDate ?? (object)DBNull.Value));


//            return aCommonInternalDAL.SaveAction("sp_I_Diposit", aSqlParameterlist, "@DepositId");
//            //string insertQuery = @"INSERT INTO tblCompanyWiseDeposit(CompanyId,BranchName,Amount,EntryBy,EntryDate,DepositDate,IsDelete,Remarks,DepositType,BankId,CheckNumber,CheckDate,AccountName) VALUES (@CompanyId,@BranchName,@Amount,@EntryBy,@EntryDate,@DepositDate,@IsDelete,@Remarks,@DepositType,@BankId,@CheckNumber,@CheckDate,@AccountName)";
//            //return aCommonInternalDAL.SaveDataByInsertCommandWithIdentity(insertQuery, aSqlParameterlist, "SSIDB");
//        }

//        public DataTable LoadDepositList(string parameter)
//        {
//            string queryStr = @"SELECT AIT,DP.DepositId,CI.ComUnitName,BNK.BankName,DP.BranchName,DP.DepositDate,DP.Amount,DP.DepositType,DP.AccountName,DP.AccountNameActual,CheckNumber,CheckDate,Remarks, ReferenceName, ReferenceDate FROM tblCompanyWiseDeposit AS DP
//INNER JOIN tblCompanyUnit  AS CI ON DP.CompanyId = CI.ComUnitId
//LEFT JOIN tblBankInfo AS BNK ON DP.BankId = BNK.BankId WHERE DP.IsDelete = 0 " + parameter;
//            return aCommonInternalDAL.DataContainerDataTable(queryStr, "SSIDB");
//        }

        public bool UpdateDepositInfo(CompanyWiseDepositDao aDepositDao)
        {
            //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@DepositId", aDepositDao.DepositId));
            //aSqlParameterlist.Add(new SqlParameter("@IsDelete", aDepositDao.IsDelete));
            //aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aDepositDao.DeleteBy));
            //aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aDepositDao.DeleteDate));

            string queryStr = @"UPDATE tblCompanyWiseDeposit SET IsDelete = '" + aDepositDao.IsDelete + "',DeleteBy = '" + aDepositDao.DeleteBy + "',DeleteDate = '" + aDepositDao.DeleteDate + "' WHERE DepositId = " + aDepositDao.DepositId ;
            return aCommonInternalDAL.UpdateDataByUpdateCommand(queryStr ,"SSIDB");
        }
    }
}
