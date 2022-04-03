using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class License
    {
        public class ViewModel
        {
            public List<UserDetail> Userdetail { get; set; }
            public List<InsurerDetail> Insurerdetail { get; set; }
            public List<BrokerDetail> Brokerdetail { get; set; }
        }
    }
}
