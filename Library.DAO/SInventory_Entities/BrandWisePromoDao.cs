using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class BrandWisePromoDao
    {
        public int PromoWiseBrandSetupId { get; set; }
        public int BrandId { get; set; }
        public bool IsActive { get; set; }
        public int PromoProductId { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
