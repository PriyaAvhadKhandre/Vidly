using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET   /api/Customers
        public IEnumerable<CustomerDto> GetCustomers() //public IEnumerable<Customer> GetCustomers() 
        {
            //return _context.Customers.ToList();
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        //GET   /api/Customers/1
        //public Customer GetCustomer(int id)
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //return customer;
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //POST  /api/customers
        [HttpPost]
        //public Customer CreateCustomer(Customer customer)             // MVC
        //public CustomerDto CreateCustomer(CustomerDto customerDto)    //WebAPI
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            //return customerDto;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT   /api/customers/1
        [HttpPut]
        //public void UpdateCustomer(int id,Customer customer)
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            //customerInDb.Name = customer.Name;
            //customerInDb.Birthdate = customer.Birthdate;
            //customerInDb.IsSubscribedToNewsletters = customer.IsSubscribedToNewsletters;
            //customerInDb.MembershipType = customer.MembershipType;

            Mapper.Map(customerDto,customerInDb);

            _context.SaveChanges();
            return Ok();
        }

        //DELETE    /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
