using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Library.DAL.InternalCls;
using Library.DAL.SInventory_DAL;
using Library.DAO.SInventory_Entities;

namespace Library.BLL.SInventory_BLL
{
    public class ProductTargetBLL
    {
        ProductTargetDAL aProductTargetDAL = new ProductTargetDAL();
        public DataTable LoadProductTarget()
        {
            return aProductTargetDAL.LoadProductTarget();
        }
    }
}
