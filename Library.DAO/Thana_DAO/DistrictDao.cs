using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.Thana_DAO
{
    public class DistrictDao
    {
        public string DistrictName { get; set; }

        public int? DivisionId { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName_BN { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public string url { get; set; }

        public int EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
