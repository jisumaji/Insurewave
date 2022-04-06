using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
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
        IRequest obj2;
        IBroker obj3;
        InsurewaveContext db = new InsurewaveContext();
        public BuyerController(IBuyer _obj,IRequest _obj2,IBroker _obj3)
        {
            obj = _obj;
            obj2 = _obj2;
            obj3 = _obj3;
        }
        public IActionResult Index()
        {
            //userdetail  =  u;
            //ViewBag.name  =  userdetail.FirstName;
            TempData.Keep();
            return View();
        }
        public IActionResult DisplayAssets()
        {
            string id1 = (string)TempData["UserId"];
            TempData.Keep();
            List<BuyerAsset> result = obj.GetAllAssets(id1);
            return View(result);
        }
        public IActionResult AddAssets()
        {
            ViewData["CountryId"] = new SelectList(db.CurrencyConversions, "CountryId", "CountryName");
            return View();
        }
        [HttpPost]
        public IActionResult AddAssets(BuyerAsset b)
        {
            //List<CurrencyConversion> cntry = obj.GetAllCountry();
            string id2 = (string)TempData["UserId"];
            BuyerAsset assetinsert = new BuyerAsset
            {
                UserId = id2,
                CountryId=b.CountryId,
                AssetName=b.AssetName,
                PriceUsd=b.PriceUsd,
                Type=b.Type
            };
            obj.AddAsset(assetinsert);
            return RedirectToAction("Index");
        }
        
        public IActionResult DeleteOneAsset(int assetid)
        {
            BuyerAsset p = obj.GetAssetById(assetid);
            return View(p);
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(int assetid)
        {
            obj.DeleteAsset(assetid);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int assetid)
        { 
            BuyerAsset p = obj.GetAssetById(assetid);
            TempData["Request"] = p.Request;
            return View(p);
        }
        [HttpPost]
        public IActionResult Edit(BuyerAsset b)
        {
            string userid = (string)TempData["UserId"];
            string request = (string)TempData["Request"];
            TempData["AssetId"]=b.AssetId;
            TempData.Keep();
            BuyerAsset asset = new BuyerAsset
            {
                AssetId = b.AssetId,
                UserId = userid,
                CountryId = b.CountryId,
                AssetName = b.AssetName,
                PriceUsd = b.PriceUsd,
                Type = b.Type,
                Request = request
            };
            obj.EditAsset(asset);
            return RedirectToAction("Index");
        }

        public IActionResult ViewPolicy()
        {
            return View();
        }
        public IActionResult RequestToBroker(int assetid)
        {
            TempData["assetid"] = assetid;
            List<BrokerDetail> result = obj3.GetAllBrokers();
            return View(result);

            //return View();
        }
        public IActionResult AddRequest1(string brokerid)
        {
            TempData["BrokerId"] = brokerid;
            BrokerRequest br = new BrokerRequest();
            br.AssetId = (int)TempData["AssetId"];
            br.BrokerId = brokerid;
            br.ReviewStatus = "no";
            obj2.AddRequest(br);
            return RedirectToAction("Index");
           // return View(br);
        }
        /*
        [HttpPost]
        public IActionResult AddRequest1(BrokerRequest brq)
        {
            // List<CurrencyConversion> cntry = obj.GetAllCountry();
           //TempData["UserId"]=brokerid;
            BrokerRequest assetinsert = new BrokerRequest
            {
                
                AssetId = (int)TempData["AssetId"],
                BrokerId=brokerid,
                ReviewStatus="no"
               
                AssetId= 100028,
                BrokerId= "moitri@gmail.com",
                ReviewStatus="no"
                
            };
            obj2.AddRequest(assetinsert);
            return RedirectToAction("Index");
        }
       */
    }
}
