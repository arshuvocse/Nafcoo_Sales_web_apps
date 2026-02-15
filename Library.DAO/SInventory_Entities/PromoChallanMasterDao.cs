using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.SInventory_Entities
{
    public class PromoChallanMasterDao
    {

        public int PromoChallanId { get; set; }

        public string PromoChallanCode { get; set; }

        public int? ChallanBy { get; set; }

        public DateTime? ChallanDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsForwarded { get; set; }

        public int? ForwardBy { get; set; }

        public DateTime? ForwardDate { get; set; }

        public bool? ApprovalStatus { get; set; }

        public int? ApprovedBy { get; set; }
        public int ComUnitId { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
