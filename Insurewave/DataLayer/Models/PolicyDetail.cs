using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class PolicyDetail
    {
        public int PolicyId { get; set; }
        public int AssetId { get; set; }
        public string InsurerId { get; set; }
        public string BrokerId { get; set; }
        public int Duration { get; set; }
        public decimal Premium { get; set; }
        public decimal LumpSum { get; set; }
        public DateTime StartDate { get; set; }
        public int PremiumInterval { get; set; }
        public decimal MaturityAmount { get; set; }
        public string PolicyStatus { get; set; }
        public string ReviewStatus { get; set; }
        public string Feedback { get; set; }

        public virtual BuyerAsset Asset { get; set; }
        public virtual BrokerDetail Broker { get; set; }
        public virtual InsurerDetail Insurer { get; set; }
        public virtual PaymentBuyer PaymentBuyer { get; set; }
    }
}
