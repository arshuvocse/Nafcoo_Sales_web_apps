using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.Thana_DAO
{
    public class DivisionDao
    {

        public int DivisionId { get; set; }

        public string DivisionCode { get; set; }

        public string DivisionName { get; set; }

        public string DivisionName_BN { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
