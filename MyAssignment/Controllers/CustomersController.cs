using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAssignment.Models;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyAssignment.Models.Infrastructure;

namespace MyAssignment.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustDataAccessLayer customerDataAccess;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public CustomersController(ICustDataAccessLayer custDataAccess, IMediator mediator, IMapper mapper)
        {
            customerDataAccess = custDataAccess;
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: Customers

        [Authorize]
        public IActionResult Index()
        {
            return View(customerDataAccess.Customers());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(AddCustomerRequestModel cust)
        {
            if (ModelState.IsValid)
            {
                _mediator.Send(cust);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cust);
            }
        }

        [Authorize]
        public IActionResult Edit(string? id)
        {
            var cust = _mediator.Send(new GetCustomerDetailsRequestModel { CustomerId = id });
            return View(_mapper.Map<EditCustomerRequestModel>(cust.Result));
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCustomerRequestModel customers)
        {
            _mediator.Send(customers);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Details(string? id) 
        { if (id == null)
            { 
                return NotFound(); 
            } 
            return View(_mediator.Send(new GetCustomerDetailsRequestModel { CustomerId = id }).Result); 
        }

        [Authorize]
        public IActionResult Delete(string? id)
        {
            var cust = _mediator.Send(new GetCustomerDetailsRequestModel { CustomerId = id });
            return View(_mapper.Map<Customers>(cust.Result));
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string? id)
        {
            _mediator.Send(new DeleteCustomerRequestModel { CustomerId = id });
            return RedirectToAction("index");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Home");
        }
    }
}



