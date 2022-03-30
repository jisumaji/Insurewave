using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class BrokerDetail
    {
        public BrokerDetail()
        {
            BrokerInsurers = new HashSet<BrokerInsurer>();
        }

        public int BrokerId { get; set; }
        public int UserId { get; set; }
        public int LicenseId { get; set; }
        public int? CustomerCount { get; set; }
        public double? Commission { get; set; }

        public virtual UserDetail User { get; set; }
        public virtual ICollection<BrokerInsurer> BrokerInsurers { get; set; }
    }
}
