using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Library.DAL.InternalCls;
using Library.DAO.SInventory_Entities;

namespace Library.DAL.SInventory_DAL
{
    public class IngridentsDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveIngridents(Ingridents aIngridents)
        {
            string insertQuery = @"insert into tblIngridents (IngridentsId,IngridentsName,IngridentsType) 
            values (" + aIngridents.IngridentsId + ",'" + aIngridents.IngridentsName + "','" + aIngridents.IngridentsType + "')";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, "SSIDB");
        }

        public bool HasIngridentsName(Ingridents aIngridents)
        {
            string query = "select * from tblIngridents where IngridentsName = '" + aIngridents.IngridentsName + "'";
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

        public DataTable LoadIngridents()
        {
            string query = @"SELECT * from tblIngridents ";

            return aCommonInternalDal.DataContainerDataTable(query, "SSIDB");
        }

        public Ingridents IngridentsEditLoad(string ID)
        {
            string query = "select * from tblIngridents where IngridentsId = '" + ID + "'";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "SSIDB");
            Ingridents aIngridents = new Ingridents();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aIngridents.IngridentsId = Int32.Parse(dataReader["IngridentsId"].ToString());
                    aIngridents.IngridentsName = dataReader["IngridentsName"].ToString();
                    aIngridents.IngridentsType = dataReader["IngridentsType"].ToString();
                }
            }
            return aIngridents;
        }

        public bool UpdateIngridentsInfo(Ingridents aIngridents)
        {

            string query = @"UPDATE tblIngridents SET IngridentsName='" + aIngridents.IngridentsName + "',IngridentsType='" + aIngridents.IngridentsType + "' WHERE IngridentsId=" + aIngridents.IngridentsId + "";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "SSIDB");
        }
    }
}
