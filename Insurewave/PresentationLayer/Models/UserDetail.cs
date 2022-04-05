using System;
using System.Collections.Generic;

#nullable disable

namespace PresentationLayer.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            BuyerAssets = new HashSet<BuyerAsset>();
        }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public int? LicenseId { get; set; }

        public virtual BrokerDetail BrokerDetail { get; set; }
        public virtual InsurerDetail InsurerDetail { get; set; }
        public virtual ICollection<BuyerAsset> BuyerAssets { get; set; }
    }
}
