using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerAsset = DataLayer.Models.BuyerAsset;
using CurrencyConversion = DataLayer.Models.CurrencyConversion;

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
            return View();
        }
        [HttpPost]
        public IActionResult AddAssets(BuyerAsset b)
        {
            List<CurrencyConversion> cntry = obj.GetAllCountry();
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
        public IActionResult RequestToBroker()
        {
            return View();
        }
    }
}
