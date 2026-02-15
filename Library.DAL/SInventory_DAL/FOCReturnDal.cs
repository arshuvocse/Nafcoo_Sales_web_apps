using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;

namespace Library.DAL.SInventory_DAL
{
    public class FOCReturnDal
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();


        public void LoadFOC(DropDownList dropDownList)
        {
            string query = @"SELECT M.DcStockOutMasterId,DcStockOutCode + ' - ( Customer ID: ' + CustomerCode + ' ) ' DcStockOutCode FROM tblDeStockOutMaster AS M

                            LEFT JOIN (SELECT D.DcStockOutMasterId,SUM(D.StackOutQty) - SUM(ISNULL(RD.ReturnQuantity,0)) ReturnAbleStock FROM tblDeStockOutDetails AS D
                            LEFT JOIN (SELECT D.DcStockOutDetailsId,SUM(ReturnQuantity) ReturnQuantity FROM tblDeStockOutDetails AS D
                            LEFT JOIN Table_FOC_ReturnDetails AS RD ON D.DcStockOutDetailsId = RD.DcStockOutDetailsId
                            LEFT JOIN Table_FOC_ReturnMaster AS RM ON RD.FOCReturnMasterId = RM.FOCReturnMasterId
                            WHERE RM.ApprovalStatus IN ('Approved','Posted') GROUP BY D.DcStockOutDetailsId) AS RD ON D.DcStockOutDetailsId = RD.DcStockOutDetailsId
                            GROUP BY D.DcStockOutMasterId) AS FD ON M.DcStockOutMasterId = FD.DcStockOutMasterId 
                            
                            WHERE M.Status = 'Approved' AND ReturnAbleStock > 0 ORDER BY M.DcStockOutMasterId DESC ";

            aCommonInternalDal.LoadDropDownValue(dropDownList, "DcStockOutCode", "DcStockOutMasterId", query, "SSIDB");
        }


        public DataTable LoadFOCById(string dcStockOutMasterId)
        {
            string query = @"SELECT M.DcStockOutMasterId,D.DcStockOutDetailsId,ProductCode,ProductName,BatchNo,ExpDate,ReceiveDate,D.StackOutQty,(D.StackOutQty - ISNULL(RD.ReturnQuantity,0)) ReturnAbleStock  FROM tblDeStockOutMaster AS M
                             LEFT JOIN tblDeStockOutDetails AS D ON M.DcStockOutMasterId = D.DcStockOutMasterId
                             LEFT JOIN (SELECT D.DcStockOutDetailsId,SUM(ReturnQuantity) ReturnQuantity FROM tblDeStockOutDetails AS D
                             LEFT JOIN Table_FOC_ReturnDetails AS RD ON D.DcStockOutDetailsId = RD.DcStockOutDetailsId
                             LEFT JOIN Table_FOC_ReturnMaster AS RM ON RD.FOCReturnMasterId = RM.FOCReturnMasterId
                             WHERE RM.ApprovalStatus IN ('Approved','Posted') GROUP BY D.DcStockOutDetailsId)  AS RD ON D.DcStockOutDetailsId = RD.DcStockOutDetailsId
                             WHERE M.DcStockOutMasterId = " + dcStockOutMasterId;

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }



        public DataTable GetFOCReturnList(string param)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@Param", param));
                DataTable dt = accessManager.GetDataTable("sp_FOC_ReturnList", aSqlParameterlist);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public Int32 SaveFOCReturnMaster(int userId, DateTime returnDate, int focId)
        {
            int masterid = 0;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@ReturrnBy", userId));
                aSqlParameterlist.Add(new SqlParameter("@ReturnDate", returnDate));
                aSqlParameterlist.Add(new SqlParameter("@FOCId", focId));

                masterid = accessManager.SaveDataReturnPrimaryKey("sp_FOC_MasterInsert", aSqlParameterlist);

                return masterid;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public Int32 SaveFOCReturnDetails(int masterid, int dcStockOutDetailsId, decimal returnQty, string remarks)
        {
            int detailsId = 0;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@FOCReturnMasterId", masterid));
                aSqlParameterlist.Add(new SqlParameter("@DCStockOutDetailsId", dcStockOutDetailsId));
                aSqlParameterlist.Add(new SqlParameter("@ReturnQuantity", returnQty));
                aSqlParameterlist.Add(new SqlParameter("@ReturnRemarks", remarks));

                detailsId = accessManager.SaveDataReturnPrimaryKey("sp_FOC_DetailsInsert", aSqlParameterlist);

                return detailsId;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool ApproveReturn(int masterid)
        {
            bool status = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);

                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@FOCReturnMasterId", masterid));
                status = accessManager.UpdateData("sp_FOC_DetailsReturnApproval", aSqlParameterlist);

                return status;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        
    }
}
