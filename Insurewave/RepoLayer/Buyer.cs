using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Buyer:IBuyer
    {
        InsurewaveContext db;
        public Buyer()
        {
            db = new InsurewaveContext();
        }
        public void AddAsset(BuyerAsset buyerasset)
        {
            db.BuyerAssets.Add(buyerasset);
            db.SaveChanges();
        }
        public List<BuyerAsset> GetAllAssets(int id)
        {
            List<BuyerAsset> asset = db.BuyerAssets.Where(a => a.AssetId == id).ToList();
            //List<BuyerAsset> asset = db.BuyerAssets.Where(t => t.AssetId==id);
            return asset;

        }
       
    }
}
