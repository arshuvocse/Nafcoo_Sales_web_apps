using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesSolution.Web.Models
{
    public class CustomerType
    {

        public int CustomerTypeId { get; set; }

        public string CustomerTypee { get; set; }

        public string CustTypeCode { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsCampaign { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsTradeDiscount { get; set; }
        public bool? IsFixedDiscount { get; set; }

        public int? InactiveBy { get; set; }

        public DateTime? InactiveDate { get; set; }
    }


    public class AttendanceLog
    {

        public int? ApprovalId { get; set; }
        public DateTime? Date { get; set; }
        public int? FromEmpId { get; set; }
        public int? ToEmpId { get; set; }
        public int? TableId { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public int? Step { get; set; }
        public int? GroupId { get; set; }
        public int? RegionId { get; set; }
        public int? AreaId { get; set; }
        public int? TerritoryId { get; set; }
        public int? ToGroupId { get; set; }
        public int? ToRegionId { get; set; }
        public int? ToAreaId { get; set; }
        public int? ToTerritoryId { get; set; }
        public string EntryByS { get; set; }
        public DateTime? EntryDateS { get; set; }
        public TimeSpan? EntryTimeS { get; set; }
        public String ApproveByS { get; set; }
        public DateTime? ApproveDateS { get; set; }
        public TimeSpan? ApproveTimeS { get; set; }
        public String EntryByApp { get; set; }
        public DateTime? EntryDateApp { get; set; }
        public TimeSpan? EntryTimeApp { get; set; }
        public String ApproveByApp { get; set; }
        public DateTime? ApproveDateApp { get; set; }
        public TimeSpan? ApproveTimeApp { get; set; }
        public int? MenuId { get; set; }

    }
}