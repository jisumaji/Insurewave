using System;
using System.Collections.Generic;

#nullable disable

namespace PresentationLayer.Models
{
    public partial class BrokerRequest
    {
        public int RequestId { get; set; }
        public int? AssetId { get; set; }
        public string BrokerId { get; set; }
        public string ReviewStatus { get; set; }

        public virtual BuyerAsset Asset { get; set; }
        public virtual BrokerDetail Broker { get; set; }
    }
}
