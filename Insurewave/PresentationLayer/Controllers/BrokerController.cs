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
        public IActionResult Index(UserDetail u)
        {
            return View();
        }
        public IActionResult OngoingPolicy()
        {
            return View();
        }
        public IActionResult PendingReviews()
        {
            return View();
        }
    }
}
