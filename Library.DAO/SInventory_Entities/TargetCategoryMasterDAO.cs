using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class TargetCategoryMasterDAO
    {
        public int TargetId { get; set; }
        public string TargetCategory { get; set; }
        public decimal TotalTargetByTp { get; set; }
        public decimal TotalTargetByTpVat { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }

    }
}
