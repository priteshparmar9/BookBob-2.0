using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookbob.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _CustomerRepo;
        public CustomerController(ICustomerRepository CustomerRepo)
        {
            _CustomerRepo = CustomerRepo;
        }
        public IActionResult Index()
        {
            var model = _CustomerRepo.GetAllCustomers();
            return View(model);
        }

        public ViewResult Details(int id)
        {
            Customer Customer = _CustomerRepo.GetCustomer(id);
            if (Customer == null)
            {
                Response.StatusCode = 404;
                return View("CustomerNotFound", id);
            }
            return View(Customer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer Customer)
        {
            if (ModelState.IsValid)
            {
                Customer newCustomer = _CustomerRepo.Add(Customer);
                return RedirectToAction("details", new { id = newCustomer.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Customer Customer = _CustomerRepo.GetCustomer(id);
            return View(Customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the Customer being edited from the database
                Customer Customer = _CustomerRepo.GetCustomer(model.Id);
                // Update the Customer object with the data in the model object
                Customer.Name = model.Name;
                Customer.Email = model.Email;
                Customer.Password = model.Password;
                // Call update method on the repository service passing it the
                // Customer object to update the data in the database table
                Customer updatedCustomer = _CustomerRepo.Update(Customer);

                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Customer Customer = _CustomerRepo.GetCustomer(id);
            if (Customer == null)
            {
                return NotFound();
            }
            return View(Customer);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var Customer = _CustomerRepo.GetCustomer(id);
            _CustomerRepo.Delete(Customer.Id);

            return RedirectToAction("index");
        }
    }
}