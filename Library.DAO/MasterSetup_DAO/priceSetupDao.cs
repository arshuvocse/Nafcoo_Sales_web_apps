using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.MasterSetup_DAO
{
    public class priceSetupDao
    {
        public int PriceGroupId { get; set; }
        public string PriceGroupName { get; set; }
        public string CheckPriceGroupName { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
