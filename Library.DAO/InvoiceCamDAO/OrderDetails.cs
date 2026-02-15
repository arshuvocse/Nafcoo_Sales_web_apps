using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.InvoiceCamDAO
{
    public class OrderDetails
    {
        public int OrderDetailID { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitVatAmount { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string ProductName { get; set; }


        public bool IsGiftProduct { get; set; }
        public bool IsCampaignProduct { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public string CampaingName { get; set; }
        public string CampaignType { get; set; }

        public int? CampaignMasterId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }

        public bool IsCamp { get; set; }






    }
}
