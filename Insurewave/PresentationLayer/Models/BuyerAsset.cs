using System;
using System.Collections.Generic;

#nullable disable

namespace PresentationLayer.Models
{
    public partial class BuyerAsset
    {
        public BuyerAsset()
        {
            BrokerRequests = new HashSet<BrokerRequest>();
            PolicyDetails = new HashSet<PolicyDetail>();
        }

        public int AssetId { get; set; }
        public string UserId { get; set; }
        public int? CountryId { get; set; }
        public string AssetName { get; set; }
        public decimal PriceUsd { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }

        public virtual CurrencyConversion Country { get; set; }
        public virtual UserDetail User { get; set; }
        public virtual ICollection<BrokerRequest> BrokerRequests { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
