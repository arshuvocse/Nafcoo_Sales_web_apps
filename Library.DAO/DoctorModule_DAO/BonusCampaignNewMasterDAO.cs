using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesSolution.Web.Models
{
    public class BonusCampaignNewMasterDAO
    {
        public int CampgainMasterId { get; set; }

        public string CampaignCode { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? CompanyId { get; set; }

        public string CampaignName { get; set; }

        public string CampaignDesc { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? Todate { get; set; }

        public int? CampainTypeId { get; set; }
        public int? ProductLineID { get; set; }
        public int? BonusProductId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ProductQty { get; set; }

        public decimal? MaxAmount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTradePolicy { get; set; }

        public int? CustomerTypeId { get; set; }

        public List<BonusCampaignNewDetailDAO> BonusCampaignNewDetailDAOs { get; set; }
    }

    public class CampaignCustomerDetailDAO
    {
        public int CampaignCustomerDetailId { get; set; }
        public int CampgainMasterId { get; set; }

        public int? CustomerMasterId { get; set; }
         

    }


    public class CustomerPropUpdateMasterDAO
    {
        public int CustPropMasterId { get; set; }

        public int? TypeId { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string ConvertType { get; set; }

        public bool? IsTransfer { get; set; }
    }
}