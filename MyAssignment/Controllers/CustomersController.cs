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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyAssignment.Models.Infrastructure;

namespace MyAssignment.Controllers
{
    public class CustomersController : Controller
    {
        // private readonly CustomerDBContext _context;
        //private object custDataAccess;

        // CustDataAccessLayer customerDataAccess = new CustDataAccessLayer();
        // CustDataAccessLayer2 customerDataAccess;
        private readonly ICustDataAccessLayer customerDataAccess;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public CustomersController(ICustDataAccessLayer custDataAccess, IMediator mediator, IMapper mapper)
        {
            customerDataAccess = custDataAccess;
            _mediator = mediator;
            _mapper = mapper;
            //  _context = context;
        }

        // GET: Customers

        [Authorize]
        public IActionResult Index()
        {
            return View(customerDataAccess.Customers());
        }

        /*   // GET: Customers/Details/5
           public async Task<IActionResult> Details(string id)
           {
               if (id == null)
               {
                   return NotFound();
               }

               var customers = await _context.Customers
                   .FirstOrDefaultAsync(m => m.CustomerId == id);
               if (customers == null)
               {
                   return NotFound();
               }

               return View(customers);
           }*/

        // GET: Customers/Create
        /* public IActionResult Create()
         {
             return View();
         }

         // POST: Customers/Create
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("CustomerId,Name,Address,PaymentCategory,Phone,CreatedDate")] Customers customers)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(customers);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(customers);
         }*/


        /* // GET: Customers/Edit/5
         public async Task<IActionResult> Edit(string id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var customers = await _context.Customers.FindAsync(id);
             if (customers == null)
             {
                 return NotFound();
             }
             return View(customers);
         }

         // POST: Customers/Edit/5
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(string id, [Bind("CustomerId,Name,Address,PaymentCategory,Phone,CreatedDate")] Customers customers)
         {
             if (id != customers.CustomerId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(customers);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!CustomersExists(customers.CustomerId))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(customers);
         }*/


        /* // GET: Customers/Delete/5
         public async Task<IActionResult> Delete(string id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var customers = await _context.Customers
                 .FirstOrDefaultAsync(m => m.CustomerId == id);
             if (customers == null)
             {
                 return NotFound();
             }

             return View(customers);
         }

         // POST: Customers/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(string id)
         {
             var customers = await _context.Customers.FindAsync(id);
             _context.Customers.Remove(customers);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool CustomersExists(string id)
         {
             return _context.Customers.Any(e => e.CustomerId == id);
         }*/

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


        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public IActionResult DeleteConfirmed(string? id)
         {
             customerDataAccess.DeleteCustomers(id);
             return RedirectToAction("Index");
         }*/

    }
}



