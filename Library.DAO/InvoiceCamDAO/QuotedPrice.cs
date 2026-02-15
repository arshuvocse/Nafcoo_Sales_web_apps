using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
   public class QuotedPrice
    {
        public string Description { get; set; }
        public string QuotedPriceDetailId { get; set; }
        public string Policy { get; set; }
        public string CustomerMasterId { get; set; }
        public string ActiveFromDate { get; set; }
        public string ActiveToDate { get; set; }
        public string ProductId { get; set; }
        public string UnitPrice { get; set; }
        public string Vat { get; set; }
    }
}
