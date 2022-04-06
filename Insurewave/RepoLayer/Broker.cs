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
        public void GetDetails(string userId)
        {
            //call get details of user details
        }
        public void EditBrokerDetails(UserDetail u)
        {
            //call user detail edit page
        }
        public List<PolicyDetail> GetAllPolicies(string brokerId)
        {
            Policy p = new();
            List<PolicyDetail> detail = p.GetAllPoliciesBroker(brokerId);
            return detail;
        }
        public void AddPolicy(PolicyDetail pd)
        {
            Policy p = new();
            p.AddPolicy(pd);
        }
        public void EditPolicy(int assetId)
        {
            Policy p = new();
            PolicyDetail pd = p.GetPolicyById(assetId);
            p.EditPolicy(pd);
        }    
        public List<BrokerRequest> GetRequests(string brokerId)
        {
            Request obj = new();
            return obj.GetRequestList(brokerId);
        }
        public List<BrokerDetail> GetAllBrokers()
        {
            List<BrokerDetail> bd = db.BrokerDetails.ToList();
            return bd;
        }
    }
}
