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
        InsurewaveContext db = new InsurewaveContext();
        UserDetail userdetail;


        public BuyerController(IBuyer _obj)
        {
            obj = _obj;
        }

        public IActionResult Index(UserDetail u)
        {
            userdetail  =  u;
            ViewBag.name  =  userdetail.FirstName; 
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
            List<CurrencyConversion> cntry = obj.GetAllCountry();
            BuyerAsset assetinsert = new BuyerAsset
            {
                UserId = (string)TempData["id"],
                //UserId=b.UserId,
                CountryId=b.CountryId,
                AssetName=b.AssetName,
                PriceUsd=b.PriceUsd,
                Type=b.Type
            };
            obj.AddAsset(assetinsert);

            /*public IActionResult Create()
            {
                ViewData["CountryId"] = new SelectList(_context.CurrencyConversions, "CountryId", "CountryName");
                ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "UserId");
                return View();
            }*/
            List<int> ids = obj.GetAllCountryIds();
            List<string> names = obj.GetAllCountryNames();
            List<Country> countries = new List<Country>();
            for(int i=0;i<countries.Count;i++)
            {
                countries.Add(new Country {Id= ids.ElementAt(i), Name = names.ElementAt(i) });
            }
            ViewBag.countries = new SelectList(countries, "CountryId", "CountryName");
            return View();
        }
       

    }
}
