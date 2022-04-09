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
        private bool PolicyDetailExists(int id)
        {
            return _context.PolicyDetails.Any(e => e.PolicyId == id);
        }
        public async Task<IActionResult> Reject(string method, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("method", method);
            var policyDetail = await _context.PolicyDetails.FindAsync(id);
            if (policyDetail == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }

        // POST: PolicyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, [Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            if (id != policyDetail.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string method = HttpContext.Session.GetString("method");
                try
                {
                    policyDetail.ReviewStatus = "yes";
                    policyDetail.PolicyStatus = method;
                    if (method == "accepted")
                    {
                        PaymentBuyer pb = new PaymentBuyer();
                        pb.PolicyId = policyDetail.PolicyId;
                        _context.PaymentBuyers.Add(pb);
                    }
                    _context.Update(policyDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyDetailExists(policyDetail.PolicyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CurrentRequests));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }
    }
}