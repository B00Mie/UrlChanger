using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Abstract;
using UrlChanger.Concrete;
using UrlChanger.Models;

namespace UrlChanger.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IJWTUserService _userService;
        private DatabaseRepo repo;
        public UsersController(IJWTUserService userService, ApplicationContext context)
        {
            _userService = userService;
            repo = new DatabaseRepo(context);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Authenticate()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(User user)
        {
            var token = _userService.Authenticate(user);
            
            if (token == null)
            {
                return RedirectToAction("Authenticate");
            }
            //Request.Headers["Authorization"] = $"Bearer {token}";
            Response.Cookies.Append("Authorization", $"Bearer {token}");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Authenticate");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (repo.Users.GetRecords().Where(x => x.Login == user.Login).FirstOrDefault() != null)
                return RedirectToAction("Register"); //need to add model errors
            repo.Users.CreateRecord(user);
            repo.Save();

            return RedirectToAction("Index", "Home");
        }


    }
}
