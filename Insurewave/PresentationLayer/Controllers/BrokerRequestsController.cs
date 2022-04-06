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
    public class BrokerRequestsController : Controller
    {
        private readonly InsurewaveContext _context;

        public BrokerRequestsController(InsurewaveContext context)
        {
            _context = context;
        }

        // GET: BrokerRequests
        public async Task<IActionResult> Index()
        {
            var insurewaveContext = _context.BrokerRequests.Include(b => b.Asset).Include(b => b.Broker);
            return View(await insurewaveContext.ToListAsync());
        }

        // GET: BrokerRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerRequest = await _context.BrokerRequests
                .Include(b => b.Asset)
                .Include(b => b.Broker)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (brokerRequest == null)
            {
                return NotFound();
            }

            return View(brokerRequest);
        }

        // GET: BrokerRequests/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName");
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId");
            return View();
        }

        // POST: BrokerRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,AssetId,BrokerId,ReviewStatus")] BrokerRequest brokerRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brokerRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", brokerRequest.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", brokerRequest.BrokerId);
            return View(brokerRequest);
        }

        // GET: BrokerRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerRequest = await _context.BrokerRequests.FindAsync(id);
            if (brokerRequest == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", brokerRequest.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", brokerRequest.BrokerId);
            return View(brokerRequest);
        }

        // POST: BrokerRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,AssetId,BrokerId,ReviewStatus")] BrokerRequest brokerRequest)
        {
            if (id != brokerRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brokerRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokerRequestExists(brokerRequest.RequestId))
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
            ViewData["AssetId"] = new SelectList(_context.BuyerAssets, "AssetId", "AssetName", brokerRequest.AssetId);
            ViewData["BrokerId"] = new SelectList(_context.BrokerDetails, "BrokerId", "BrokerId", brokerRequest.BrokerId);
            return View(brokerRequest);
        }

        // GET: BrokerRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerRequest = await _context.BrokerRequests
                .Include(b => b.Asset)
                .Include(b => b.Broker)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (brokerRequest == null)
            {
                return NotFound();
            }

            return View(brokerRequest);
        }

        // POST: BrokerRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brokerRequest = await _context.BrokerRequests.FindAsync(id);
            _context.BrokerRequests.Remove(brokerRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrokerRequestExists(int id)
        {
            return _context.BrokerRequests.Any(e => e.RequestId == id);
        }
    }
}
