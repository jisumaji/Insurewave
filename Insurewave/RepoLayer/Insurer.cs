using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Insurer:IInsurer
    {
        InsurewaveContext db;
        public Insurer()
        {
            db = new InsurewaveContext();
        }
        public List<PolicyDetail> GetAllPolicies(string insurerId)
        {
            Policy p = new();
            return p.GetAllPoliciesInsurer(insurerId);
        }
        public void GetDetails(string userId)
        {
            //call get details of user details
        }
        public void EditInsurerDetails(UserDetail u)
        {
            //call user detail edit page
        }
        public List<PolicyDetail> GetRequests(string insurerId)
        {
            Policy obj = new();
            return obj.GetRequestList(insurerId);
        }
        public void AddFeedback(int policyId,string feedback)
        {
            Policy p = new();
            PolicyDetail pd = p.GetPolicyById(policyId);
            pd.PolicyStatus = "rejected";
            pd.Feedback = feedback;
            db.SaveChanges();
        }
    }
}
