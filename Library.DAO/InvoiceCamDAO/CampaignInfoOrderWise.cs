using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO.InvoiceCamDAO
{
    public class CampaignInfoOrderWise
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public decimal Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
        public bool IsApplied { get; set; }
    }
}
