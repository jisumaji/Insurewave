using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Policy:IPolicy
    {
        InsurewaveContext db;
        public Policy()
        {
            db = new InsurewaveContext();
        }
        public PolicyDetail GetPolicyById(int assetId)
        {
            PolicyDetail p = db.PolicyDetails.Where(a => a.AssetId == assetId).FirstOrDefault();
            return p;
        }
        public List<PolicyDetail> GetAllPoliciesBroker(string brokerId)
        {
            List<PolicyDetail> p = db.PolicyDetails.Where(a=>a.BrokerId==brokerId).ToList();
            return p;
        }
        public List<PolicyDetail> GetAllPoliciesInsurer(string insurerId)
        {
            List<PolicyDetail> p = db.PolicyDetails.Where(a => a.InsurerId == insurerId).ToList();
            return p;
        }

       /*for one asset only*/
        public List<PolicyDetail> GetAllPoliciesAsset(int assetid)
        {
            List<PolicyDetail> p = db.PolicyDetails.Where(a => a.AssetId == assetid ).Where(a => a.PolicyStatus=="pending").ToList();
            return p;
        }
        
        public void AddPolicy(PolicyDetail p)
        {
            db.PolicyDetails.Add(p);
            db.SaveChanges();
        }
        public void EditPolicy(PolicyDetail p)
        {
            PolicyDetail edit = db.PolicyDetails.Where(a => a.AssetId == p.AssetId).FirstOrDefault();
            edit.Duration = p.Duration;
            edit.Premium = p.Premium;
            edit.LumpSum = p.LumpSum;
            edit.StartDate = p.StartDate;
            edit.PremiumInterval = p.PremiumInterval;
            edit.MaturityAmount = p.MaturityAmount;
            edit.PolicyStatus = p.PolicyStatus;
            edit.ReviewStatus = p.ReviewStatus;
            edit.Feedback = p.Feedback;
            db.SaveChanges();
        }
        public List<PolicyDetail> GetRequestList(string insurerId)
        {
            List<PolicyDetail> pd = db.PolicyDetails.Where(a => a.InsurerId == insurerId && a.ReviewStatus == "no").ToList();
            return pd;
        }

        public bool PolicyDetailExists(PolicyDetail p)
        {
            bool result = db.PolicyDetails.Contains(p);
            return result;
        }
        public PolicyDetail GetPolicyByPolId(int polid)
        {
            PolicyDetail p = db.PolicyDetails.Where(a => a.PolicyId == polid).FirstOrDefault();
            return p;
        }
    }
}
