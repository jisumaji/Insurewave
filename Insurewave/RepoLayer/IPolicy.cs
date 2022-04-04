using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IPolicy
    {
        public PolicyDetail GetPolicyById(int id);
        public List<PolicyDetail> GetAllPoliciesBroker(string brokerId);
        public void AddPolicy(PolicyDetail p);
        public void EditPolicy(PolicyDetail p);
    }
}
