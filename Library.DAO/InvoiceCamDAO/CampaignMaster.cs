using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.InvoiceCamDAO
{
  public  class CampaignMaster
    {
        public int CampgainMasterId { get; set; }
        public int ProductLineID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignDesc { get; set; }
        public string CodeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public int CampainTypeId { get; set; }
        public int CustomerTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal MaxAmount { get; set; }
        public bool IsTradePolicy { get; set; }
        public int ProductQty { get; set; }
        public int BonusProductId { get; set; }

       public List<CampaignDetail> campDetail { get; set; }
    }
}
