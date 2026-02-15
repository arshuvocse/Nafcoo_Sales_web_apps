using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.DoctorModule_DAO
{
    public class DoctorDegreeDao
    {
        public int DegreeId { get; set; }
        public int? DoctorTypeId { get; set; }

        public string DegreeName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Activedate { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

    public class TourPlanDto
    {
        public string TourPlanDate { get; set; }
        public string Session { get; set; }   // "Morning" / "Evening"
        public int TourTypeId { get; set; }
        public int SubTerritoryId { get; set; }
        public string Market { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalStatusOthers { get; set; }
    }
    public class TourPlanInfoMasterDAO
    {
        public string Type { get; set; }
        public string remarks { get; set; }
        public List<TourPlanInfoNewDAO> aTourPlanInfo { get; set; }
    }

    public class TourPlanInfoNewDAO
    {
        public DateTime? TourPlanDate { get; set; }
        public int? SerialNo { get; set; }
        public int? EmpId { get; set; }

        // Morning
        public int? MorTourTypeId { get; set; }
        public int? MorTerritoryId { get; set; }
        public string MorMarketId { get; set; }

        // Evening
        public int? EveTourTypeId { get; set; }
        public int? EveTerritoryId { get; set; }
        public string EveMarketId { get; set; }
    }
}
