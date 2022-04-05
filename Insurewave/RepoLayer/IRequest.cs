using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IRequest
    {
        public List<BrokerRequest> GetRequestList(string brokerId);
        public void AddRequest(BrokerRequest br);
    }
}
