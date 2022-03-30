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
    public class UserLoginController : Controller
    {
        IUser obj;
        public UserLoginController(IUser _obj)
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
            if(a)
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
            List<SelectListItem> roles = new()
            {
                new SelectListItem { Value = "Buyer", Text = "Buyer" },
                new SelectListItem { Value = "Broker", Text = "Broker" },
                new SelectListItem { Value = "Insurer", Text = "Insurer" },
            };
            List<SelectListItem> gender = new()
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" },
                new SelectListItem { Value = "Others", Text = "Others" },
            };
            ViewBag.roles = roles;
            ViewBag.gender = gender;
            obj.AddUser(u);
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
