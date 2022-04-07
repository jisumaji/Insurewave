using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            BuyerAssets = new HashSet<BuyerAsset>();
        }

        [DataType(DataType.EmailAddress)]
        public string UserId { get; set; }
        [DataType(DataType.Password)]
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
