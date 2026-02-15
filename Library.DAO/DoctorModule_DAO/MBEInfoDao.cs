using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.DoctorModule_DAO
{
  public class MBEInfoDao
    {
        public int? EmployeeId { get; set; }

        public int? SubTerritoryId { get; set; }

        public bool? IsActive { get; set; }

        public int MBEInfoId { get; set; }

        public int? CompanyId { get; set; }

        public string Vacant { get; set; }

        public DateTime? ActiveDate { get; set; }

        public DateTime? ActiveInActiveDate { get; set; }

        public string InActiveBy { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }


        // for view 
        public int GroupId { get; set; }
        public int RegionId { get; set; }
        public int AreaId { get; set; }
        public int TerritoryId { get; set; }

        public string ActiveDateStr { get; set; }
    }
}
