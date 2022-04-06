using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;

namespace PresentationLayer.Controllers
{
    public class PolicyDetailsController : Controller
    {
        private readonly InsurewaveContext _context;

        public PolicyDetailsController(InsurewaveContext context)
        {
            _context = context;
        }

        // GET: PolicyDetails
        public async Task<IActionResult> Index()
        {
            var insurewaveContext = _context.PolicyDetails.Include(p => p.Asset).Include(p => p.Broker).Include(p => p.Insurer);
            return View(await insurewaveContext.ToListAsync());
        }

        // GET: PolicyDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyDetail = await _context.PolicyDetails
                .Include(p => p.Asset)
                .Include(p => p.Broker)
                .Include(p => p.Insurer)
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyDetail == null)
            {
                return NotFound();
            }

            return View(policyDetail);
        }

        // GET: PolicyDetails/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName");
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId");
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId");
            return View();
        }

        // POST: PolicyDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", policyDetail.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", policyDetail.BrokerId);
            ViewData["InsurerId"] = new SelectList(_context.InsurerDetails, "InsurerId", "InsurerId", policyDetail.InsurerId);
            return View(policyDetail);
        }

        // GET: PolicyDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(policyDetail);
        }

        // POST: PolicyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyId,AssetId,InsurerId,BrokerId,Duration,Premium,LumpSum,StartDate,PremiumInterval,MaturityAmount,PolicyStatus,ReviewStatus,Feedback")] PolicyDetail policyDetail)
        {
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
                    if (!PolicyDetailExists(policyDetail.PolicyId))
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

        // GET: PolicyDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyDetail = await _context.PolicyDetails
                .Include(p => p.Asset)
                .Include(p => p.Broker)
                .Include(p => p.Insurer)
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyDetail == null)
            {
                return NotFound();
            }

            return View(policyDetail);
        }

        // POST: PolicyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyDetail = await _context.PolicyDetails.FindAsync(id);
            _context.PolicyDetails.Remove(policyDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyDetailExists(int id)
        {
            return _context.PolicyDetails.Any(e => e.PolicyId == id);
        }
    }
}
