using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IInsurer
    {
        public List<PolicyDetail> GetAllPolicies(string insurerId);
        public void GetDetails(string userId);
        public void EditInsurerDetails(UserDetail u);
        public List<PolicyDetail> GetRequests(string insurerId);
        public void AddFeedback(int policyId, string feedback);
    }
}
