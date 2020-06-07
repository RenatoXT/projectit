using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using projectit.DAO;
using projectit.Models;
using System.IO;

namespace projectit.Controllers
{
    public class LoginController : Controller
    {
        protected UsersDAO DAO { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            HttpContext.Session.SetString("Register", "true");
            return RedirectToAction("Create", "Users");
        }

        public IActionResult Login(string user, string password)
        {
            //TO-DO 
            //  Users table (Encrypt the password)
            UsersDAO DAO = new UsersDAO();
            if (DAO.Login(user, password))
            {
                HttpContext.Session.SetString("Login", "true");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Erro = "Login inválido";
                return View("Index");
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}