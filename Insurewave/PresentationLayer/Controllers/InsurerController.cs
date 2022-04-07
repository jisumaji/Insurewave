using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class InsurerController : Controller
    {
        //pending reviews
        //ongoing policy
        //home==Index
        IInsurer obj;
        InsurewaveContext _context;
        public InsurerController(IInsurer _obj)
        {
            obj = _obj;
            _context=new InsurewaveContext();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetDetails()//edit insifde rtj
        {
            return RedirectToAction("Details", "UserDetails");
        }
        public async Task<IActionResult> GetAllPolicies()
        {
            string insurerId = HttpContext.Session.GetString("UserId");
            /*List<PolicyDetail> bd = obj.GetAllPolicies(insurerId);
            return View(bd);*/
            var insurewaveContext = _context.PolicyDetails.Include(p => p.Asset).Include(p => p.Broker).Include(p => p.Insurer).Where(a => a.InsurerId == insurerId);
            return View(await insurewaveContext.ToListAsync());
        }
        public async Task<IActionResult> CurrentRequests()
        {
            string insurerId = HttpContext.Session.GetString("UserId");
            var insurewaveContext = _context.PolicyDetails.Include(b => b.Asset).Include(b => b.Broker).Where(a => a.InsurerId == insurerId && a.ReviewStatus == "no");
            return View(await insurewaveContext.ToListAsync());
        }
        public IActionResult RejectPolicy(int policyId)
        {
            List<PolicyDetail> br = obj.GetRequests(HttpContext.Session.GetString("UserId"));
            ViewData["AssetId"] = new SelectList(br, "AssetId", "AssetId");
            //ViewBag.AssetId = policyId;
            ViewBag.InsurerId = HttpContext.Session.GetString("UserId");
            TempData["PolicyId"] = policyId;

            ViewData["BrokerId"] = new SelectList(_context.PolicyDetails, "BrokerId", "BrokerId");
            //ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId");
            TempData.Keep();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectPolicy([Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            if (ModelState.IsValid)
            {
                policyDetail.AssetId = (int)TempData["AssetId"];
                policyDetail.BrokerId = HttpContext.Session.GetString("UserId");
                policyDetail.ReviewStatus = "yes";
                policyDetail.PolicyStatus = "accepted";
                /*Broker r = new();
                r.ChangeReviewStatus((int)policyDetail.AssetId, policyDetail.BrokerId);*/

                _context.Add(policyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }







        public IActionResult AcceptPolicy(int assetId)
        {
            Request r = new();
            List<BrokerRequest> br = r.GetRequestList(HttpContext.Session.GetString("UserId"));
            //ViewData["AssetId"] = new SelectList(br, "AssetId", "AssetId");
            ViewBag.assetId = assetId;
            ViewBag.brokerId = HttpContext.Session.GetString("UserId");
            TempData["AssetId"] = assetId;

            //ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId");
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId");
            TempData.Keep();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptPolicy([Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            if (ModelState.IsValid)
            {
                policyDetail.AssetId = (int)TempData["AssetId"];
                policyDetail.BrokerId = HttpContext.Session.GetString("UserId");
                policyDetail.ReviewStatus = "yes";
                policyDetail.PolicyStatus = "rejected";
                Broker r = new();
                r.ChangeReviewStatus((int)policyDetail.AssetId, policyDetail.BrokerId);

                _context.Add(policyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }
    }
}