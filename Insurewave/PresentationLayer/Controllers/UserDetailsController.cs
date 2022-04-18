using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace PresentationLayer.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly InsurewaveContext _context;

        public UserDetailsController(InsurewaveContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Details()
        {
            string id = HttpContext.Session.GetString("UserId");
            if (id == null)
            {
                return NotFound();
            }

            var userDetail = await _context.UserDetails
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Password,FirstName,LastName,Gender,Role,LicenseId")] UserDetail userDetail)
        {
            if (id != userDetail.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailExists(userDetail.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                HttpContext.Session.SetString("FirstName",userDetail.FirstName);
                return RedirectToAction(nameof(Details));
            }
            return View(userDetail);
        }
        private bool UserDetailExists(string id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }
}
