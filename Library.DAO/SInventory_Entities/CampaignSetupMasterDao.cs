using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class CampaignSetupMasterDao
    {
        public Int32 CampaignMasterId { get; set; }
        public Int32 CustomerInformationId { get; set; }
        public Decimal SlabAmount { get; set; }
        public Decimal DiscountPercentage { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public Boolean IsActive { get; set; }
        public Int32 InactiveBy { get; set; }
        public DateTime InactiveDate { get; set; }

    }
}
