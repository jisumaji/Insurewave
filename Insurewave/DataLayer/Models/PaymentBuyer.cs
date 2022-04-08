using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class PaymentBuyer
    {
        public int PolicyId { get; set; }
        public string PaidStatus { get; set; }

        public virtual PolicyDetail Policy { get; set; }
    }
}
