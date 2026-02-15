using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.DataManager;
using Library.DAL.InternalCls;
using Library.DAL.MAIN_FUNCTION;

namespace Library.DAL.SInventory_DAL
{
    public class CommonStructureDal
    {

        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        private DataAccessManager accessManager = new DataAccessManager();

        private DB_Manager aDbManager = new DB_Manager();

        public void LoadTargetCategory(DropDownList ddl)
        {
            string query = @"SELECT TargetId, TargetCategory FROM tbl_Target_CategoryMaster";
            aCommonInternalDal.LoadDropDownValue(ddl, "TargetCategory", "TargetId", query, "SSIDB");

        }

        public DataTable LoadSalesLine()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetSalesLine", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadGroup()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetGroup", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadBrand()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetBrandMeansProductSQ", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public DataTable LoadClusterMeansRegion()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetCluster", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        

        public DataTable LoadRegionMeansAreaByClusterId(int clusterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                
                aSqlParameters.Add(new SqlParameter("@ClusterId", clusterId));

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetRegion", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadAreaMeansTerritoryByRegionId(int clusterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@RegionId", clusterId));

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetArea", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadTerritoryMeansSubTerritoryByAreaId(int clusterId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                aSqlParameters.Add(new SqlParameter("@AreaId", clusterId));

                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetTerritory", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadClusterHead()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();

                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetClusterHeadInfo", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadRsmByClusterHead(Int32 clusterheadId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@RsmId", clusterheadId));
                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetRsmInfo", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadAsmByRsm(Int32 rsmId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@AsmId", rsmId));
                DataTable dt = accessManager.GetDataTable("sp_FieldForce_GetMioInfo", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadRegionMeansAreaByClusterIdListBox(string parameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", parameter));
                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetRegionListBox", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadAreaMeansTerritoryByRegionIdListBox(string parameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", parameter));
                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetAreaListBox", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable LoadTerritoryMeansSubTerritoryByAreaIdListBox(string parameter)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SalesDB);


                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Pram", parameter));
                DataTable dt = accessManager.GetDataTable("sp_Hierarchy_GetTerritoryListBox", aSqlParameters);
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
    }
}
