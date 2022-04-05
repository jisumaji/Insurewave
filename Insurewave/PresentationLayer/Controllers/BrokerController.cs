using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using RepoLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class BrokerController : Controller
    {
        InsurewaveContext _context;
        IBroker obj;
        public BrokerController(IBroker _obj)
        {
            _context = new InsurewaveContext();
            obj = _obj;
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
        public IActionResult GetAllPolicies()
        {
            string brokerId = (string)TempData["UserId"];
            List<PolicyDetail> bd = obj.GetAllPolicies(brokerId);
            TempData.Keep();
            return View(bd);
        }
        public IActionResult AddPolicy()
        {
            TempData.Keep();
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
            TempData.Keep();
            return View(policyDetail);
        }
        public async Task<IActionResult> EditPolicy(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var policyDetail = await _context.PolicyDetails.FindAsync(id);
            if (policyDetail == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            TempData.Keep();
            return View(policyDetail);
        }

        // POST: PolicyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPolicy(int id, [Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            TempData.Keep();
            if (id != policyDetail.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Policy p = new();
                    if (!p.PolicyDetailExists(policyDetail))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }
        public IActionResult CurrentRequests()
        {
            TempData.Keep();
            return View();
        }
    }
}
