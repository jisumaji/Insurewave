using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class BuyerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DisplayAssets()
        {
            return View();
        }
        public IActionResult ViewPolicy()
        {
            return View();
        }
        public IActionResult RequestToBroker()
        {
            return View();
        }
        public IActionResult AddAssets()
        {
            return View();
        }
    }
}
