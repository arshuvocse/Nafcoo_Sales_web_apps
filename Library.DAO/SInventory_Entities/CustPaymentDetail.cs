using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class CustPaymentDetail
    {
        public int CustPayDetailId { get; set; }
        public int InvoiceId { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int CustPayId { get; set; }
        public Boolean IsAdjust { get; set; }
    }
}
