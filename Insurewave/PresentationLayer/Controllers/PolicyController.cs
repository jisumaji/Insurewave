using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPolicy()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddPolicy(PolicyDetail p)
        {
            Policy obj = new();
            PolicyDetail policyDetail = new()
            {
                AssetId =(int)TempData["AssetId"],
                InsurerId = p.InsurerId,
                BrokerId = (string)TempData["BrokerId"],
                Duration = p.Duration,
                Premium = p.Premium,
                LumpSum = p.LumpSum,
                StartDate = p.StartDate,
                PremiumInterval = p.PremiumInterval,
                MaturityAmount = p.MaturityAmount,
                PolicyStatus = "pending",
                ReviewStatus = "no"
            };
            obj.AddPolicy(policyDetail);
            TempData.Keep();
            return View();
        }
        public IActionResult EditPolicy()
        {
            int assetId = (int)TempData["AssetId"];
            Policy obj = new();
            PolicyDetail p = obj.GetPolicyById(assetId);
            return View(p);
        }
        [HttpPost]
        public IActionResult EditPolicy(PolicyDetail p)
        {
            Policy obj = new();
            obj.EditPolicy(p);
            string from = (string)TempData["from"];
            if(from=="broker")
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");

        }
    }
}
