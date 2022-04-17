using DataLayer.Models;
using Microsoft.AspNetCore.Http;
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
                //HttpContext.Session.SetInt32("LoggedIn", 1);
                HttpContext.Session.SetString("FirstName", u.FirstName);
                HttpContext.Session.SetString("UserId", u.UserId);
                if (u.Role.Equals("broker"))
                {
                    //HttpContext.Session.SetString("Role", u.Role);
                    return RedirectToAction("Index", "Broker");
                }
                else if (u.Role.Equals("insurer"))
                    return RedirectToAction("Index", "Insurer");
                else
                    return RedirectToAction("Index", "Buyer");
            }
            else
            {
                //TempData["msg"] = "UserId or Password is wrong.!";
                return RedirectToAction("Error");
            }
        }
        public IActionResult Register()
        {
            /*List<string> allUsers = obj.GetAllUserIds();
            ViewBag.allUsers = allUsers.ToArray();*/
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
            if (obj.GetAllUserIds().Contains(u.UserId))
                return RedirectToAction("Unavailable");

            obj.AddUser(u);
            TempData["UserId"] = u.UserId;
            if (u.Role.Equals("insurer"))
            {
            
                InsurerDetail insert = new InsurerDetail
                {
                    InsurerId = (string)TempData["UserId"]
                };
                obj.AddInsurerDetails(insert);
            }
            else if(u.Role.Equals("broker"))
            {
                BrokerDetail insert = new BrokerDetail
                {
                    BrokerId = (string)TempData["UserId"]
                };
                obj.AddBrokerDetails(insert);
            }
            return RedirectToAction("Index");
            //return View();
        }
        public IActionResult Unavailable()
        {

            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string UserId, string pwd)
        {
            //TempData["Id"] = UserId;
            //TempData["pwd"] = pwd;
            UserDetail ud = obj.GetUserById(UserId);
            if (ud != null)
                obj.ChangePassword(UserId, pwd);
            else
                return RedirectToAction("Error");
            return RedirectToAction("PasswordChanged");
        }
        public IActionResult PasswordChanged()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Clear();
            return RedirectToAction("index", "home");
        }
        public IActionResult Error()
        {
            return View();
        }
    }

}
