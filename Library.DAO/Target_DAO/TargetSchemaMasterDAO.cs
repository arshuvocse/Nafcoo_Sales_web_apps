using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.Target_DAO
{
    public class TargetSchemaMasterDAO
    {
        public int SchemaMasterId { get; set; }
        public string SchemaName { get; set; }
        public decimal? SchemaAmount { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
