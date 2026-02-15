using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.MasterSetup_DAO
{
    public class PatientPriorityDao
    {
        public int PatientPriorityId { get; set; }
        public int PatientStartPoint { get; set; }
        public int PatientEndPoint { get; set; }
        public int RxStartPoint { get; set; }
        public int RxEndPoint { get; set; }
        public String Patientstatus { get; set; }
        public String ColourCodeForNote { get; set; }
        public Int32 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
