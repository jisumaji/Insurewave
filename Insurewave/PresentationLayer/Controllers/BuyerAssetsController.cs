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
    public class BuyerAssetsController : Controller
    {
        private readonly InsurewaveContext _context;

        public BuyerAssetsController(InsurewaveContext context)
        {
            _context = context;
        }

        // GET: BuyerAssets
        public async Task<IActionResult> Index()
        {
            var insurewaveContext = _context.BuyerAssets.Include(b => b.Country).Include(b => b.User);
            return View(await insurewaveContext.ToListAsync());
        }

        // GET: BuyerAssets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyerAsset = await _context.BuyerAssets
                .Include(b => b.Country)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (buyerAsset == null)
            {
                return NotFound();
            }

            return View(buyerAsset);
        }

        // GET: BuyerAssets/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.CurrencyConversions, "CountryId", "CountryName");
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "UserId");
            return View();
        }

        // POST: BuyerAssets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetId,UserId,CountryId,AssetName,PriceUsd,Type,Request")] BuyerAsset buyerAsset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyerAsset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.CurrencyConversions, "CountryId", "CountryName", buyerAsset.CountryId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "UserId", buyerAsset.UserId);
            return View(buyerAsset);
        }

        // GET: BuyerAssets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyerAsset = await _context.BuyerAssets.FindAsync(id);
            if (buyerAsset == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.CurrencyConversions, "CountryId", "CountryName", buyerAsset.CountryId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "UserId", buyerAsset.UserId);
            return View(buyerAsset);
        }

        // POST: BuyerAssets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,UserId,CountryId,AssetName,PriceUsd,Type,Request")] BuyerAsset buyerAsset)
        {
            if (id != buyerAsset.AssetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyerAsset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerAssetExists(buyerAsset.AssetId))
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
            ViewData["CountryId"] = new SelectList(_context.CurrencyConversions, "CountryId", "CountryName", buyerAsset.CountryId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "UserId", buyerAsset.UserId);
            return View(buyerAsset);
        }

        // GET: BuyerAssets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyerAsset = await _context.BuyerAssets
                .Include(b => b.Country)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (buyerAsset == null)
            {
                return NotFound();
            }

            return View(buyerAsset);
        }

        // POST: BuyerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyerAsset = await _context.BuyerAssets.FindAsync(id);
            _context.BuyerAssets.Remove(buyerAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyerAssetExists(int id)
        {
            return _context.BuyerAssets.Any(e => e.AssetId == id);
        }
    }
}
