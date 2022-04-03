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
        InsurewaveContext _ic;
        public Broker()
        {
            _ic = new InsurewaveContext();
        }
        public void EditPolicy(int policyId)
        {
            throw new NotImplementedException();
        }

        public UserDetail GetDetails(string userId)
        {
            //details from 2 tables
            UserDetail u=new UserDetail();
            return u;
        }

        public List<PolicyDetail> OngoingPolicy(string userId)
        {
            throw new NotImplementedException();
        }

        public List<PolicyDetail> PendingReviews(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
