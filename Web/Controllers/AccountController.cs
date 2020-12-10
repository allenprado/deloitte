using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.InterfaceOpenApp;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {

        AccountViewModel account = new AccountViewModel();
        List<User> listUsers = new List<User>();
        List<UserViewModel> listUserVM = new List<UserViewModel>();

        private readonly IUserApp _user;
        private readonly IHttpContextAccessor _accessor;
        public AccountController(IUserApp user, IHttpContextAccessor accessor)
        {
            _user = user;
            _accessor = accessor;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountViewModel account)
        {
            User userDB = new User();
            userDB = _user.GetByUserName(account.UserName);

            if(account.UserName == "test" && account.Password == "pwd123")
            {
                _accessor.HttpContext.Session.SetString("UserNameSession", account.UserName);
                _accessor.HttpContext.Session.SetInt32("IsLoggedSession", 1);
                return RedirectToAction("Index", "ToDoList");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            _accessor.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ListUser()
        {

            foreach (var item in await _user.List())
            {
                listUserVM.Add(new UserViewModel
                {
                   IdUser = item.IdUser,
                   UserName = item.UserName,
                   FirstName = item.FirstName,
                   LastName = item.LastName
                });
            }

            return View(listUserVM);
        }

    }
}
