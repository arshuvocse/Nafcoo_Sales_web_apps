using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class MIOWiseTargetSetupMasterDao
    {
        public Int32 MioTargetMasterId { get; set; }
        public String Month { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

    }
}
