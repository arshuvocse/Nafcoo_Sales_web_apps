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
    public class AccountSettingsDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public UserInformation UserInformationEditLoad(string userId)
        {
            string query = "select * from tblUser where UserId = '" + userId + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            UserInformation aUserInformation = new UserInformation();


            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aUserInformation.UserId = Int32.Parse(dataReader["UserId"].ToString());
                    aUserInformation.UserName = dataReader["UserName"].ToString();
                    aUserInformation.UserType = dataReader["UserType"].ToString();
                    aUserInformation.LoginName = dataReader["LoginName"].ToString();
                    aUserInformation.Password = dataReader["Password"].ToString();
                    aUserInformation.UserStatus = dataReader["UserStatus"].ToString();
                    aUserInformation.Email = dataReader["Email"].ToString();
                    aUserInformation.ContactNo = dataReader["ContactNo"].ToString();
                }

            }
            return aUserInformation;
        }


        public bool UpdateUserInfo(UserInformation aUserInformation)
        {
            string query = @"UPDATE tblUser SET UserName='" + aUserInformation.UserName + "',UserType='" + aUserInformation.UserType + "',LoginName='" + aUserInformation.LoginName + "',Password='" + aUserInformation.Password + "',UserStatus='" + aUserInformation.UserStatus + "',Email='" + aUserInformation.Email + "',ContactNo='" + aUserInformation.ContactNo + "' WHERE UserId=" + aUserInformation.UserId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }

        public void GetUserOnDropDownList(DropDownList ddl)
        {
            string query = "SELECT UserId,UserCode + ':' + UserName AS UserName FROM tblUser";
            aCommonInternalDal.LoadDropDownValue(ddl, "UserName", "UserId", query, "SSIDB");

        }
    }
}
