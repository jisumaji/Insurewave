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
    public class BrokerDetailsController : Controller
    {
        private readonly InsurewaveContext _context;

        public BrokerDetailsController(InsurewaveContext context)
        {
            _context = context;
        }

        // GET: BrokerDetails
        public async Task<IActionResult> Index()
        {
            var insurewaveContext = _context.BrokerDetails.Include(b => b.Broker);
            return View(await insurewaveContext.ToListAsync());
        }

        // GET: BrokerDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerDetail = await _context.BrokerDetails
                .Include(b => b.Broker)
                .FirstOrDefaultAsync(m => m.BrokerId == id);
            if (brokerDetail == null)
            {
                return NotFound();
            }

            return View(brokerDetail);
        }

        // GET: BrokerDetails/Create
        public IActionResult Create()
        {
            ViewData["BrokerId"] = new SelectList(_context.UserDetails, "UserId", "UserId");
            return View();
        }

        // POST: BrokerDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrokerId,CustomerCount,Commission")] BrokerDetail brokerDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brokerDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrokerId"] = new SelectList(_context.UserDetails, "UserId", "UserId", brokerDetail.BrokerId);
            return View(brokerDetail);
        }

        // GET: BrokerDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerDetail = await _context.BrokerDetails.FindAsync(id);
            if (brokerDetail == null)
            {
                return NotFound();
            }
            ViewData["BrokerId"] = new SelectList(_context.UserDetails, "UserId", "UserId", brokerDetail.BrokerId);
            return View(brokerDetail);
        }

        // POST: BrokerDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BrokerId,CustomerCount,Commission")] BrokerDetail brokerDetail)
        {
            if (id != brokerDetail.BrokerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brokerDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokerDetailExists(brokerDetail.BrokerId))
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
            ViewData["BrokerId"] = new SelectList(_context.UserDetails, "UserId", "UserId", brokerDetail.BrokerId);
            return View(brokerDetail);
        }

        // GET: BrokerDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokerDetail = await _context.BrokerDetails
                .Include(b => b.Broker)
                .FirstOrDefaultAsync(m => m.BrokerId == id);
            if (brokerDetail == null)
            {
                return NotFound();
            }

            return View(brokerDetail);
        }

        // POST: BrokerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var brokerDetail = await _context.BrokerDetails.FindAsync(id);
            _context.BrokerDetails.Remove(brokerDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrokerDetailExists(string id)
        {
            return _context.BrokerDetails.Any(e => e.BrokerId == id);
        }
    }
}
