
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class OrderInfoMaster
    {
        public int OrderId { get; set; }
        public int CustomerMasterId { get; set; }
        public string OrderCode { get; set; }
        public int ComUnitId { get; set; }
        public string ComUnitCode { get; set; }
        public string ComUnitName { get; set; }
        public string MIOCode { get; set; }
        public string Remarks { get; set; }
        public string MIOName { get; set; }
        public int ManufacId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public decimal GrossValue { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool IsInvoice { get; set; }
        public bool IsManual { get; set; }
        public string teritory { get; set; }
        public bool FCB { get; set; }

        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
