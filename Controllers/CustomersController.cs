using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Services;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
       // private readonly ApplicationDbContext _context;
        private readonly ICustomersServices _customersServices;

        public CustomersController(ApplicationDbContext contex, ICustomersServices customersServices)
        {
           // _context = contex;
            _customersServices = customersServices;
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = _customersServices.GetCustomerDetails(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _customersServices.GetMemberships();

            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };


            return View("CustomerForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customersServices.GetCustomers(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _customersServices.GetMemberships()
        };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _customersServices.GetMemberships()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) 
            {
                _customersServices.InsertCustomer(customer);
            }
            else
            {
                _customersServices.EditCustomer(customer);
            }

           

            return RedirectToAction("Index", "Customers");
        }
    }
}