using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class BrokerDetail
    {
        public BrokerDetail()
        {
            PolicyDetails = new HashSet<PolicyDetail>();
        }

        public string BrokerId { get; set; }
        public int LicenseId { get; set; }
        public int? CustomerCount { get; set; }
        public double? Commission { get; set; }

        public virtual UserDetail Broker { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
