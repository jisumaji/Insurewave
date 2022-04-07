using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
namespace RepoLayer
{
    public class Request:IRequest
    {
        InsurewaveContext db;
        public Request()
        {
            db = new InsurewaveContext();
        }
        public List<BrokerRequest> GetRequestList(string brokerId)
        {
            List<BrokerRequest> br = db.BrokerRequests.Where(a => a.BrokerId == brokerId && a.ReviewStatus=="no").ToList();
            return br;
        }
        public void AddRequest(BrokerRequest br)
        {
            br.ReviewStatus = "no";
            db.Add(br);
            db.SaveChanges();
        }
        public void ChangeStatus(int assetId, string brokerId)
        {
            //int requestId=db.BrokerRequests.Select(a=>a.AssetId==assetId )
            BrokerRequest br = db.BrokerRequests.Where(a => a.BrokerId == brokerId && a.AssetId == assetId).FirstOrDefault();
            br.ReviewStatus = "yes";
            db.SaveChanges();
        }
    }
}
