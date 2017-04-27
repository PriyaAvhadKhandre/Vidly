using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Vidly.Models;
using Vidly.Controllers;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController() //Initialize ApplicationDBcontext
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();

            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            
            return View("CustomerForm", viewModel);
        }

        [HttpPost] //So that it is called only during HttpPost and not Get action //public ActionResult Create(NewCustomerViewModel viewModel) //MVC fraework maps object to the requested data.        
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid) //Adding Validations
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletters = customer.IsSubscribedToNewsletters;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        public ActionResult Index()
        {
            //var customer = CustomersList();
            //var customer = _context.Customers.Include(c => c.MembershipType).ToList(); // (Displays DiscountRate) If ToList() is used the query will be executed immediately 

            var customer = _context.Customers.Include(c => c.MembershipType).ToList(); // Section 3 : Exercise 1 : Membership Type column display instead of DiscountRate

            return View(customer);
        }

        public ActionResult Details(int id)
        {
            //var customer = CustomersList().Where(c => c.Id == id).FirstOrDefault();
            var customer = _context.Customers.Where(c => c.Id == id).Include(c => c.MembershipType).FirstOrDefault();


            if (customer == null)
                return HttpNotFound();
            else
                return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };

            return View("CustomerForm",viewModel);
        }
        
        //public List<Customer> CustomersList()
        //{
        //    //var customer = new List<Customer>();
        //    var customer = new List<Customer> { new Customer() { Id = 1, Name = "John Smith" }
        //                                      , new Customer() { Id = 2, Name = "Mary Willaims" } };

        //    return customer;
        //}      
    }
}