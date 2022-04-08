using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IBroker
    {
        public void GetDetails(string userId);
        public void EditBrokerDetails(UserDetail u);
        public List<PolicyDetail> GetAllPolicies(string brokerId);
        public void EditPolicy(int assetId);
        public void AddPolicy(PolicyDetail p);
        public List<BrokerRequest> GetRequests(string brokerId);
        public List<BrokerDetail> GetAllBrokers();
        public void ChangeReviewStatus(int assetId, string brokerId);

    }
}
