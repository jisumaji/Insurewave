using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class InsurerDetail
    {
        public InsurerDetail()
        {
            BrokerInsurers = new HashSet<BrokerInsurer>();
            PolicyDetails = new HashSet<PolicyDetail>();
        }

        public int InsurerId { get; set; }
        public int UserId { get; set; }
        public int LicenseId { get; set; }
        public int? NoOfProducts { get; set; }

        public virtual UserDetail User { get; set; }
        public virtual ICollection<BrokerInsurer> BrokerInsurers { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
