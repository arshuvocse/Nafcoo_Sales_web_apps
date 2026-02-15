using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.MasterSetup_DAO
{
    public class TotalInfoDAO
    {
        public string TotalOrder { get; set; }
        public string totalInvoice { get; set; }
        public string totalDelivery { get; set; }
        public string totalPayment { get; set; }
        public string TotalRejection { get; set; }
        public string TotalOrderPer { get; set; }
        public string TotalInvoicerPer { get; set; }
        public string TotalDcr { get; set; }
        public string TotalRX { get; set; }
        public string rxMTD { get; set; }
        public string TotalDcrType { get; set; }
        public string TotalRXType { get; set; }
        public string TotalAttandence { get; set; }
        public string totalCustomerCoverage { get; set; }
        public string TotalLeave { get; set; }

        public string NetTpToday { get; set; }
        public string NetTpMTD { get; set; }
        public string NetAmountToday { get; set; }
        public string NetAmountMTD { get; set; }
        public string InvNetTpToday { get; set; }
        public string InvNetTpMTD { get; set; }
        public string InvNetAmountToday { get; set; }
        public string InvNetAmountMTD { get; set; }
        public string DelNetTpToday { get; set; }
        public string DelNetTpMTD { get; set; }
        public string DelNetAmountToday { get; set; }
        public string DelNetAmountMTD { get; set; }


    }
}
