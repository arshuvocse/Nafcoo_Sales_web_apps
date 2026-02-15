using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.DoctorModule_DAO
{
   public class InvoiceTypeDao
    {
       public int InvoiceTypeId { get; set; }
       public string TypeName { get; set; }
       public bool IsActive { get; set; }
       public int EntryBy { get; set; }
       public DateTime EntryDate { get; set; }
       public int? UpdateBy { get; set; }
       public DateTime UpdateDate { get; set; }
       public DateTime Activedate { get; set; }
    }
}
