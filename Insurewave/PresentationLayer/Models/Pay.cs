using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class Pay
    {
        public string AssetName { get; set; }
        public decimal LumpSum { get; set; }
        public decimal Premium { get; set; }
        public string PaidStatus { get; set; }
    }
}
