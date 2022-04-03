using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IBuyer
    {
        public void AddAsset(BuyerAsset buyerasset);
        public List<BuyerAsset> GetAllAssets(int id);
    }
}
