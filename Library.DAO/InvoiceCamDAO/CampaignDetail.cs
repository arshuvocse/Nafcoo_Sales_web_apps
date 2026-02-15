using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.InvoiceCamDAO
{
   public class CampaignDetail
    {
        public int CampaignDetailId { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int BonusProductId { get; set; }
        public int BonusQuantity { get; set; }
        public string TypeName { get; set; }
        public string CodeName { get; set; }
        public string productCode { get; set; }
        public string campaignName { get; set; }


    }
}
