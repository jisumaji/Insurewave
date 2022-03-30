using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class PolicyDetail
    {
        public PolicyDetail()
        {
            BrokerInsurers = new HashSet<BrokerInsurer>();
        }

        public int PolicyId { get; set; }
        public int AssetId { get; set; }
        public int InsurerId { get; set; }
        public int Duration { get; set; }
        public decimal Premium { get; set; }
        public decimal LumpSum { get; set; }
        public DateTime StartDate { get; set; }
        public int PremiumInterval { get; set; }
        public string PolicyStatus { get; set; }
        public decimal MaturityAmount { get; set; }
        public string Feedback { get; set; }

        public virtual BuyerAsset Asset { get; set; }
        public virtual InsurerDetail Insurer { get; set; }
        public virtual ICollection<BrokerInsurer> BrokerInsurers { get; set; }
    }
}
