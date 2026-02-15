using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class TargetCategoryDetailsDAO
    {
        public int TargetDetailsId { get; set; }
        public int TargetId { get; set; }
        public string ProductCode { get; set; }
        public decimal TargetQty { get; set; }
        public decimal TpPerPack { get; set; }
        public decimal VatPerPack { get; set; }
        public decimal TargetValueByTp { get; set; }
        public decimal TargetValueByTpVat { get; set; }
    }
}
