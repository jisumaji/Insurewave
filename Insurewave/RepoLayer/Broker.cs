using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Broker : IBroker
    {
        InsurewaveContext db;
        public Broker()
        {
            db = new InsurewaveContext();
        }
        public void AddPolicy(PolicyDetail pd)
        {
            Policy p = new();
            p.AddPolicy(pd);
        }
        public void EditPolicy(PolicyDetail pd)
        {
            Policy p = new();
            p.EditPolicy(pd);
        }
        public UserDetail GetDetails(string userId)
        {
            //details from 2 tables
            UserDetail u=new();
            return u;
        }
        public List<PolicyDetail> OngoingPolicy(string userId)
        {
            throw new NotImplementedException();
        }
        public List<PolicyDetail> GetAllPolicies(string brokerId)
        {
            Policy p = new();
            List<PolicyDetail> detail = p.GetAllPoliciesBroker(brokerId);
            return detail;
        }
        public List<PolicyDetail> PendingReviews(string userId)
        {
            throw new NotImplementedException();
        }
        public void EditBrokerDetails(UserDetail u)
        {
        }
        public void RequestBroker(BuyerAsset ba)
        {

        }
    }
}
