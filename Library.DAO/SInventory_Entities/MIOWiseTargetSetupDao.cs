using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class MIOWiseTargetSetupDao
    {
        public Int32 MioTargetId { get; set; }
        public string AreaCode { get; set; }
        public string TerritoryName { get; set; }
        public string MioName { get; set; }
        public string TargetCategory { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
