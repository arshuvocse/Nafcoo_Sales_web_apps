using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesSolution.Web.Models
{
    public class ExpenseTypeDetails
    {
        public int? ExpenseTypDetailsId { get; set; }

        public int? ExpenseTypeId { get; set; }

   

        public string FieldName { get; set; }

        public bool IsRequied { get; set; }

    }
}