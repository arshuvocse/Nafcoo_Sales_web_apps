using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.ViewModels;

namespace Library.DAO.InvoiceCamDAO
{
    public class OrderMaster
    {
        public int OrderId { get; set; }
        public int EmpId { get; set; }
        public int ComunitId { get; set; }
        public string CustomerCode { get; set; }
        public string SubmittedDate { get; set; }
        public string CollectionDate { get; set; }
        public string DeliveryDate { get; set; }
        public string Remarks { get; set; }
        public string OrderType { get; set; }
        public double TpPercentage { get; set; }


        public List<OrderDetails> OrderDetails { get; set; }
        public List<CampaignMaster> CampaignMasters { get; set; }
        


    }
}
