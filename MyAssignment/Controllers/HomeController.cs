using System;
using MediatR;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAssignment.Models;
using Microsoft.AspNetCore.Http;


namespace MyAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;


        public HomeController(IMediator mediator)
        {
           
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InvalidSession()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginRequestModel loginRequestModel)
        {

            var result = _mediator.Send(loginRequestModel);
            if (result.Result.Success)
            {
                HttpContext.Session.SetString("username",loginRequestModel.Username);
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                ViewData["Error"] = "Invalid Details";
                return View();
            }


        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterRequestModel registerRequestModel)
        {

            var result = _mediator.Send(registerRequestModel);
            if (result.Result.Success)
            {
                ViewData["EmailID"] = registerRequestModel.Username;
                return View("Login");
            }
            else
            {
                ViewData["Error"] = "Registraion Failed User Already Exists";
                return View(registerRequestModel);
            }
           

        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordRequestModel changePasswordRequestModel)
        {
            var result = _mediator.Send(changePasswordRequestModel);
            if (result.Result.Success)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
