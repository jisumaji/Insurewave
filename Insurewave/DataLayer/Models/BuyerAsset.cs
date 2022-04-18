using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DataLayer.Models
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
        [Required(ErrorMessage ="Country cannot be null")]
        public int? CountryId { get; set; }
        [Required(ErrorMessage ="Please enter an asset name")]
        public string AssetName { get; set; }
        [Required(ErrorMessage ="Please enter a price for your asset")]
        public decimal PriceUsd { get; set; }
        [Required(ErrorMessage ="Please enter a type")]
        public string Type { get; set; }
        public string Request { get; set; }

        public virtual CurrencyConversion Country { get; set; }
        public virtual UserDetail User { get; set; }
        public virtual ICollection<BrokerRequest> BrokerRequests { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
    }
}
