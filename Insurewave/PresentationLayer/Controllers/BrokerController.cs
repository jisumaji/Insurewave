using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
namespace PresentationLayer.Controllers
{
    public class BrokerController : Controller
    {
        public IActionResult Index()
        {
            
            TempData.Keep();
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult OngoingPolicies()
        {
            return View();
        }
        public IActionResult PendingReviews()
        {
            return View();
        }
        public IActionResult AddPolicy()
        {
            TempData.Keep();
            return RedirectToAction("AddPolicy", "Policy");
        }
        public IActionResult EditPolicy()
        {
            return View();
        }
        public IActionResult EditDetails()
        {
            return View();
        }
        public IActionResult CurrentRequests()
        {
            return View();
        }
    }
}
