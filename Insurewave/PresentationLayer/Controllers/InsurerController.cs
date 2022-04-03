using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PendingReviews()
        {
            return View();
        }
        public IActionResult OngoingPolicy()
        {
            return View();
        }
    }
}
