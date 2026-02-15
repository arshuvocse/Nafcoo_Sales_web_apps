using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class TopSheetMasterDao
    {

        public int TopSheetGenReportId { get; set; }
        public string TopSheetGenCode { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public Int32 DAId { get; set; }
    }
}
