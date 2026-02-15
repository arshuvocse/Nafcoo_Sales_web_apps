using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class WorkTypeDao
    {
        public int WorkTypeId { get; set; }  
       public String WorkType { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
