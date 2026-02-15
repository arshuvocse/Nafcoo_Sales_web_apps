using System;
using System.Data;
using Library.DAL.InternalCls;

namespace Library.DAL.PanalCls
{
    public class PanalClsDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal;

        public DataTable Login(string loginName, string password)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @" select tblDis.CompanyUnitId DICCompanyUnitId, rt.RoleType RoleTypeName,  case when  emp.EmpInfoId is null then u.UserName else emp.EmpName end EmpName , dgs.DesigName, * from dbo.tblUser U with (nolock)
 left join tblUserCompanyUnit UC  with (nolock) on U.UserId=UC.UserId
 left join tblEmpGeneralInfo emp  with (nolock) on U.EmpInfoId=emp.EmpInfoId
 left join tblDesignation dgs  with (nolock) on dgs.DesignationId=emp.DesignationId
  LEFT   JOIN dbo.tbl_UserRoleInfo  AS urR  with (nolock)  ON urR.UserRoleID = U.UserRoleID 
  LEFT   JOIN dbo.tblRoleType AS rt  with (nolock)  ON rt.RoleTypeId = urR.RoleTypeId

    LEFT JOIN (select * from (Select  UserId, CompanyUnitId ,
ROW_NUMBER() OVER (
      PARTITION BY UserId
      ORDER BY UserComId asc

   ) row_num
FROM tblUserCompanyUnit with (nolock)  )as tblt where row_num=1) AS tblDis on tblDis.UserId=U.UserId
where LoginName='" + loginName + "' and Password='" + password + "' and UserStatus='active'";
            return _aCommonInternalDal.DataContainerDataTable(queryString);
        }


        public DataTable LoginByEmpId(int EmpiD)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @" select tblDis.CompanyUnitId DICCompanyUnitId, rt.RoleType RoleTypeName,  case when  emp.EmpInfoId is null then u.UserName else emp.EmpName end EmpName , dgs.DesigName, * from dbo.tblUser U with (nolock)
 left join tblUserCompanyUnit UC  with (nolock) on U.UserId=UC.UserId
 left join tblEmpGeneralInfo emp  with (nolock) on U.EmpInfoId=emp.EmpInfoId
 left join tblDesignation dgs  with (nolock) on dgs.DesignationId=emp.DesignationId
  LEFT   JOIN dbo.tbl_UserRoleInfo  AS urR  with (nolock)  ON urR.UserRoleID = U.UserRoleID 
  LEFT   JOIN dbo.tblRoleType AS rt  with (nolock)  ON rt.RoleTypeId = urR.RoleTypeId

    LEFT JOIN (select * from (Select  UserId, CompanyUnitId ,
ROW_NUMBER() OVER (
      PARTITION BY UserId
      ORDER BY UserComId asc

   ) row_num
FROM tblUserCompanyUnit with (nolock)  )as tblt where row_num=1) AS tblDis on tblDis.UserId=U.UserId
where emp.EmpInfoId='" + EmpiD + "'   and UserStatus='active'";
            return _aCommonInternalDal.DataContainerDataTable(queryString);
        }

        public DataTable MainMenu()
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = "select * from tblMainMenu where ParantId is null or ParantId=''";
            aTableMainMenu= _aCommonInternalDal.DataContainerDataTable(queryString);
            return aTableMainMenu;
        }
        public bool LoginLog(string userId,string LoginName,DateTime loginTime)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "INSERT INTO dbo.tblLoginLog ( UserId, LoginName, LoginTime ) VALUES  ( '"+userId+"', '"+LoginName+"', '"+loginTime+"')";
            _aCommonInternalDal.SaveDataByInsertCommand(queryString);
            return true;
        }
        public DataTable MainMenu(int userId)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = @"select tblMainMenu.* from tblMainMenu "+
                                    " INNER JOIN dbo.tblMenuDistribution ON dbo.tblMainMenu.SL = dbo.tblMenuDistribution.MenuSL " +
                                    " WHERE UserId='" + userId + "' AND (ParantId is null or ParantId='')";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aTableMainMenu;
        }
        public DataTable SubItem(string Id)
        {
            DataTable aDataTableSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "'";
            aDataTableSubItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataTableSubItem;
        }

        public DataTable GetExpiryInfo()
        {
            DataTable aDataTableSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();

            string queryString = @"SELECT * FROM Table_LicenseInfos WHERE IsActive = 1";

            aDataTableSubItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataTableSubItem;
        }

        public DataTable SubItem(string Id, int userId)
        {
            DataTable aDataTableSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();

            string queryString = @"select tblMainMenu.* from tblMainMenu " +
                                   " INNER JOIN dbo.tblMenuDistribution ON dbo.tblMainMenu.SL = dbo.tblMenuDistribution.MenuSL " +
                                   " WHERE UserId='" + userId + "' AND ParantId='" + Id + "'";

            
            aDataTableSubItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataTableSubItem;
        }
        public DataTable SubSubItem(string Id)
        {
            DataTable aDataSubSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "'";
            aDataSubSubItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataSubSubItem;
        }
        public DataTable SubSubItem(string Id, int userId)
        {
            DataTable aDataSubSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @"select tblMainMenu.* from tblMainMenu " +
                                   " INNER JOIN dbo.tblMenuDistribution ON dbo.tblMainMenu.SL = dbo.tblMenuDistribution.MenuSL " +
                                   " WHERE UserId='" + userId + "' AND ParantId='" + Id + "'";
            aDataSubSubItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataSubSubItem;
        }
        public DataTable SubSubChildItem(string Id)
        {
            DataTable aDataSubSubChildItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "'";
            aDataSubSubChildItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataSubSubChildItem;
        }
        public DataTable SubSubChildItem(string Id, int userId)
        {
            DataTable aDataSubSubChildItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @"select tblMainMenu.* from tblMainMenu " +
                                    " INNER JOIN dbo.tblMenuDistribution ON dbo.tblMainMenu.SL = dbo.tblMenuDistribution.MenuSL " +
                                    " WHERE UserId='" + userId + "' AND ParantId='" + Id + "'";
            aDataSubSubChildItem = _aCommonInternalDal.DataContainerDataTable(queryString);
            return aDataSubSubChildItem;
        }

    }
}
