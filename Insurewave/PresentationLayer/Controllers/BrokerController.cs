using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using RepoLayer;

namespace PresentationLayer.Controllers
{
    public class BrokerController : Controller
    {
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
            TempData.Keep();
            return RedirectToAction("AddPolicy", "Policy");
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
