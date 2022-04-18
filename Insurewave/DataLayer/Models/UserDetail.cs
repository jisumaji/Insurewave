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
        [Required(ErrorMessage="PLease provide an email id")]
        [DataType(DataType.EmailAddress)]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please provide a valid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please select a role")]
        public string Role { get; set; }
        public int? LicenseId { get; set; }

        public virtual BrokerDetail BrokerDetail { get; set; }
        public virtual InsurerDetail InsurerDetail { get; set; }
        public virtual ICollection<BuyerAsset> BuyerAssets { get; set; }
    }
}
