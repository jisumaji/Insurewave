using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class BrokerInsurer
    {
        public int Biid { get; set; }
        public int BrokerId { get; set; }
        public int InsurerId { get; set; }
        public int AssetId { get; set; }
        public string BrokerStatus { get; set; }
        public int PolicyId { get; set; }

        public virtual BuyerAsset Asset { get; set; }
        public virtual BrokerDetail Broker { get; set; }
        public virtual InsurerDetail Insurer { get; set; }
        public virtual PolicyDetail Policy { get; set; }
    }
}
