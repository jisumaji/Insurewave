using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class BuyerController : Controller
    {
        IBuyer obj;
        public BuyerController(IBuyer _obj)
        {
            obj = _obj;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult DisplayAssets()
        {
            List<BuyerAsset> result = obj.GetAllAssets(1);
            return View(result);
        }

        public IActionResult ViewPolicy()
        {
            return View();
        }
        public IActionResult RequestToBroker()
        {
            return View();
        }
        public IActionResult AddAssets()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAssets(BuyerAsset b)
        {
            obj.AddAsset(b);
            return View();
        }
    }
}
