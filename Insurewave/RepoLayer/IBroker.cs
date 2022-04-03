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
        public UserDetail GetDetails(string userId);
        public List<PolicyDetail> OngoingPolicy(string userId);
        public List<PolicyDetail> PendingReviews(string userId);
        public void EditPolicy(int policyId);
    }
}
