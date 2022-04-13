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
        public List<PolicyDetail> GetAllPoliciesInsurer(string insurerId);
        public List<PolicyDetail> GetRequestList(string insurerId);
        public bool PolicyDetailExists(PolicyDetail policyId);
        public List<PolicyDetail> GetAllPoliciesAsset(int assetid);
        public PolicyDetail GetPolicyByPolId(int polid);

    }
}
