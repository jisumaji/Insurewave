using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            BrokerDetails = new HashSet<BrokerDetail>();
            BuyerAssets = new HashSet<BuyerAsset>();
            InsurerDetails = new HashSet<InsurerDetail>();
        }

        public int UserId { get; set; }
        public string MailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<BrokerDetail> BrokerDetails { get; set; }
        public virtual ICollection<BuyerAsset> BuyerAssets { get; set; }
        public virtual ICollection<InsurerDetail> InsurerDetails { get; set; }
    }
}
