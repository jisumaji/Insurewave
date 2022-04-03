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
            UserDetail u  =  obj.GetUserById(userdetails.UserId);
            if (a)
            {
                if(u.Role.Equals("broker"))
                    return RedirectToAction("Index","Broker",u);
                else if (u.Role.Equals("insurer"))
                    return RedirectToAction("Index", "Insurer",u);
                else
                    return RedirectToAction("Index");
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
                new SelectListItem { Value = "female", Text = "Female" },
                new SelectListItem { Value = "others", Text = "Others" },
            };
            ViewBag.roles = roles;
            ViewBag.gender = gender;
            obj.AddUser(u);
            TempData["id"] = u.UserId;
            if (u.Role.Equals("insurer"))
            {
            
                InsurerDetail insert = new InsurerDetail
                {
                    InsurerId = (string)TempData["id"]
                };
                obj.AddInsurerDetails(insert);
            }
            else if(u.Role.Equals("broker"))
            {
                BrokerDetail insert = new BrokerDetail
                {
                    BrokerId = (string)TempData["id"]
                };
                obj.AddBrokerDetails(insert);
            }
            return RedirectToAction("Index");
            //return View();
        }
        
    }

}
