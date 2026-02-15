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
    public class AppSetupDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveAppSetup(AppSetupDAO appSetupDao)
        {
            string insertQuery = @"insert into tblAppSetup (SL,UserId,Email,EntryBy,EntryDate) 
            values ('" + appSetupDao.SL + "','" + appSetupDao.UserId + "','" + appSetupDao.Email + "','" + appSetupDao.EntryBy + "','" + appSetupDao.EntryDate + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }
        public DataTable LoadAppSetup(string parameter)
        {
            string query = @"SELECT * FROM tblAppSetup
            LEFT JOIN dbo.tblMainMenu ON dbo.tblMainMenu.SL=tblAppSetup.SL
            LEFT JOIN dbo.tblUser ON dbo.tblUser.UserId=tblAppSetup.SL "+parameter;
            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }
        public bool UpdateAreaInfo(AppSetupDAO appSetupDao)
        {
            string query = @"UPDATE tblAppSetup SET SL='" + appSetupDao.SL + "',UserId='" + appSetupDao.UserId + "',Email='" + appSetupDao.Email + "',EntryBy='" + appSetupDao.EntryBy + "',EntryDate='" + appSetupDao.EntryDate + "' WHERE AppSetupId=" + appSetupDao.AppSetupId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public bool DeleteAreaInfo(string id)
        {
            string query = @"DELETE FROM tblAppSetup WHERE SL='"+id+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
        public void LoadMenuName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblMainMenu where IsApprovalPage='1'";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "ManuName", "SL", queryStr);
        }
        public void LoadUser(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "SELECT * FROM dbo.tblUser WHERE UserStatus='Active'";
            aInternalDal.LoadDropDownValueWithoutDataBase(ddl, "UserName", "UserId", queryStr);
        }
    }
}
