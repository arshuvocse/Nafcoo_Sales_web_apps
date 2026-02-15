using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAO.Panal_Entities;

namespace Library.DAL.Panal_DAL
{
  public class PanalDAL
    {
      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public bool MenuSaveDal(ObjPanal aObjPanal)
      {
          string query = @"INSERT INTO dbo.tblMainMenu ( SL ,ManuName ,URL ,ParantId) VALUES  ( " + aObjPanal.SL + " ,'" + aObjPanal.ManuName + "','" + aObjPanal.URL + "' ,  '" + aObjPanal.ParantId + "'  )";
          return aCommonInternalDal.SaveDataByInsertCommand(query, "SSIDB");
      }


      public bool MenuPermissionRemove(int sl, int userId)
      {
          string parantId = Convert.ToString(sl);
          string query = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE SL =" + sl + " ) AND UserId="+userId+"";
          string query1 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "' ) AND UserId=" + userId + "";
          string query2 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "') ) AND UserId=" + userId + "";
          string query3 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "')) ) AND UserId=" + userId + "";

          bool ok = aCommonInternalDal.DeleteDataByDeleteCommand(query);
          bool ok1 = aCommonInternalDal.DeleteDataByDeleteCommand(query1);
          bool ok2 = aCommonInternalDal.DeleteDataByDeleteCommand(query2);
          bool ok3 = aCommonInternalDal.DeleteDataByDeleteCommand(query3);

          return true;
      }



      public void MainMenuDropDown(DropDownList aDropDownList)
      {
          string query = "select * from tblMainMenu where ParantId is null or ParantId=''";
          aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManuName", "SL", query,"SSIDB");
      }
      public void MenuDropDown(DropDownList aDropDownList,string id)
      {
          string query = "select * from tblMainMenu where  ParantId='"+id+"'";
          aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManuName", "SL", query, "SSIDB");

      }

      public bool CheckMenuSl(int MenuSl, int userId)
      {
          string query = @"select * from tblMenuDistribution where MenuSL=" + MenuSl + " and UserId=" + userId + "";
          DataTable aTable = new DataTable();
          aTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
          if (aTable.Rows.Count==0)
          {
              return false;
          }

          return true;
      }

      public string GetParantId(int sL)
      {
          string parantId = string.Empty;
          string query = "select * from tblMainMenu where  SL=" + sL + "";
          DataTable aTable = new DataTable();
          aTable = aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
          parantId = aTable.Rows[0]["ParantId"].ToString();

          return parantId;
      }

      public bool SaveMainMenu(int MenuSl, int userId)
      {
          ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
          int SL = aClsPrimaryKeyFind.PrimaryKeyMax("SL", "tblMenuDistribution", "SSIDB");
          string query = @"INSERT INTO tblMenuDistribution ( SL ,UserId ,MenuSL ,Status) VALUES  ( " + SL + " ," + userId + "," + MenuSl + " ,  'True'  )";
          return aCommonInternalDal.SaveDataByInsertCommand(query, "SSIDB");
      }



      public void UserDdl(DropDownList userDropDownList)
      {
          string query = "select * from tblUser";
          aCommonInternalDal.LoadDropDownValue(userDropDownList, "UserName", "UserId", query, "SSIDB");
      }

      public void MainMenu(DropDownList userDropDownList, string userId)
      {
          string query = "select * from tblMainMenu INNER JOIN  tblMenuDistribution ON tblMainMenu.SL=tblMenuDistribution.MenuSL  where UserId='" + userId + "' and ( ParantId IS NULL OR ParantId='')";
          aCommonInternalDal.LoadDropDownValue(userDropDownList, "ManuName", "SL", query, "SSIDB");
      }
      public DataTable MainMenuLoad(string userId)
      {
          string query = @"   SELECT  tbltemp.* FROM  (SELECT MM.*,ISNULL(MD.Status,'False') AS Status  FROM dbo.tblMainMenu MM "+
                            " LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL "+
                            " WHERE MD.UserId = '" + userId + "'  " +
                            " union  "+
                            " SELECT MM.*, Status=0  FROM dbo.tblMainMenu MM   "+
                            " WHERE MM.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')  )   " +
                            " AS tbltemp WHERE tbltemp.ParantId IS NULL OR tbltemp.ParantId =''   " +
                            " ORDER BY tbltemp.SL";


          return aCommonInternalDal.DataContainerDataTable(query);
      }

      public DataTable OtherMenuLoad(string userId,string parantId)
      {
          //string query = @"   SELECT  tbltemp.* FROM  (SELECT MM.*,ISNULL(MD.Status,'False') AS Status  FROM dbo.tblMainMenu MM " +
          //                  " LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL " +
          //                  " WHERE MD.UserId = '" + userId + "'  " +
          //                  " union  " +
          //                  " SELECT MM.*, Status=0  FROM dbo.tblMainMenu MM   " +
          //                  " WHERE MM.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')  )   " +
          //                  " AS tbltemp WHERE  tbltemp.ParantId ='" + parantId + "'   " +
          //                  " ORDER BY tbltemp.SL";


          string Query = @" SELECT tblSelected.* FROM  (SELECT tblMenuTemp.*,tbl1.[Status]  FROM  (SELECT * FROM dbo.tblMainMenu WHERE  " +
                              "  ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "') " +

                              "    UNION " +

                              "   SELECT * FROM dbo.tblMainMenu WHERE " +
                              "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                              "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')) " +

                               "   UNION  " +

                              "   SELECT * FROM dbo.tblMainMenu WHERE " +
                              "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                              "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                              "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                              "   AS tblMenuTemp   " +

                              "   INNER JOIN (SELECT MM.*,ISNULL(MD.Status,'False') AS [Status]  FROM dbo.tblMainMenu MM   " +
                              "   LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL   " +
                              "   WHERE (MM.ParantId IS NOT NULL OR MM.ParantId <>'') and MD.UserId = '" + userId + "') AS tbl1 ON tblMenuTemp.SL = tbl1.SL)   " +
                              "   AS tblSelected  " +


                             "    UNION   " +

                              "   SELECT tblMenuTemp.* ,Status=0 FROM  (SELECT * FROM dbo.tblMainMenu WHERE   " +
                              "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')  " +

                              "    UNION  " +

                              "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                              "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                              "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))  " +

                              "    UNION  " +

                              "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                              "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                             "    ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                             "    ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                            "     AS tblMenuTemp WHERE tblMenuTemp.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')";
                                

          return aCommonInternalDal.DataContainerDataTable(Query);
      }


    }
}
