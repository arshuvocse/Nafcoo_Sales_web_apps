using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.Target_DAO
{
    public class TargetSchemaDetailDAO
    {
        public int SchemaDetailId { get; set; }
        public int? SchemaMasterId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Percentage { get; set; }
    }
}
