using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        IUser obj;
        public LoginController(IUser _obj)
        {
            obj = _obj;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserDetail userdetails)
        {
            bool a = obj.LoginUser(userdetails);
            if (a)
            {
                return RedirectToAction("Page1");
            }

            else
            {
                TempData["msg"] = "UserId or Password is wrong.!";
                //return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Page1()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserDetail u)
        {
            //ViewBag.insurerDetail  =  new InsurerDetail();
            //ViewBag.brokerDetail  =  new BrokerDetail();
            List<SelectListItem> roles = new()
            {
                new SelectListItem { Value = "buyer", Text = "Buyer" },
                new SelectListItem { Value = "broker", Text = "Broker" },
                new SelectListItem { Value = "insurer", Text = "Insurer" },
            };
            List<SelectListItem> gender = new()
            {
                new SelectListItem { Value = "male", Text = "Male" },
                new SelectListItem { Value = "memale", Text = "Female" },
                new SelectListItem { Value = "others", Text = "Others" },
            };
            ViewBag.roles = roles;
            ViewBag.gender = gender;
            obj.AddUser(u);
            if (u.Role.Equals("insurer"))
            {
                TempData["id"] = u.UserId;
                return RedirectToAction("Insurer");
                //return RedirectToAction("Insurer", new {mailId = u.UserId });
            }
            /*else if (u.Role.Equals("broker"))
                return RedirectToAction("Broker");*/
            return RedirectToAction("Index");
        }
        public IActionResult Insurer(string mailId)
        {
            //ViewBag.insurerId = mailId;
            return View();
        }
        [HttpPost]
        public IActionResult Insurer(InsurerDetail i)
        {
            InsurerDetail ins = new InsurerDetail
            {
                InsurerId = (string)TempData["id"],
                LicenseId =i.LicenseId,
            };
            obj.AddInsurerDetails(ins);
            return RedirectToAction("Index");
        }
        /*public IActionResult Create(UserDetail userdetail)
        {
            db.UserDetails.Add(userdetail);
            db.SaveChanges();
            return
        }*/
    }

}
