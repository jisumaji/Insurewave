using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using RepoLayer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Controllers
{
    public class BrokerController : Controller
    {
        InsurewaveContext _context;

        public BrokerController()
        {
            _context = new InsurewaveContext();
        }
        public IActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        public IActionResult GetDetails()
        {
            //redirect to details of user
            TempData.Keep();
            return RedirectToAction("Details","User");
        }
        public IActionResult EditDetails()
        {
            //redirect to details of user
            TempData.Keep();
            return RedirectToAction("Edit", "User");
        }
        public IActionResult GetAllPolicies()
        {
            TempData.Keep();
            string brokerId = (string)TempData["UserId"];
            Broker b = new();
            List<PolicyDetail> bd = b.GetAllPolicies(brokerId);
            return View(bd);
        }
        public IActionResult AddPolicy()
        {
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName");
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId");
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId");
            return View();
        }
        /*[HttpPost]
        public IActionResult AddPolicy(PolicyDetail pd)
        {
            TempData.Keep();
            Policy p = new Policy();
            pd.BrokerId = TempData["UserId"] as string;
            pd.ReviewStatus = "no";
            p.AddPolicy(pd);
            return RedirectToAction("GetAllPolicies");
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPolicy([Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep();
                policyDetail.BrokerId = TempData["UserId"] as string;
                policyDetail.ReviewStatus = "no";

                _context.Add(policyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }
        public IActionResult EditPolicy()
        {
            return View();
        }
        
        public IActionResult CurrentRequests()
        {
            return View();
        }
    }
}
