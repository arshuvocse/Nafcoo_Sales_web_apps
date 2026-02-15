using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.MasterSetup_DAO
{
    public class DeliveryDayDao
    {
        public int DeliveryDayId { get; set; }
        public String DeliveryDay { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
