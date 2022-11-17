using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebAPINorthwind.Models;

namespace WebAPINorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NorthwindContext context;

        public CustomerController (NorthwindContext context)
        {
            this.context = context;
        }

        //GET /api/Customer/
        [HttpGet]
        public IEnumerable Get()
        {
            IEnumerable<Customer> customers = (from c in context.Customers select c).ToList();
            return customers;

        }

        //GET /api/Customer/id
        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            Customer customer = ( from c in context.Customers where c.CustomerId.ToLower() == id.ToLower() select c ).SingleOrDefault();
            return customer;
        }

        //GET /api/Customer/compName/contName
        [HttpGet("{compName}/{contName}")]
        public dynamic Get (string compName, string contName)
        {
            dynamic customer = (from c in context.Customers where c.CompanyName.ToLower() == compName.ToLower() && c.ContactName.ToLower() == contName.ToLower() select c).SingleOrDefault();
            return customer;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put (string id, [FromBody] Customer customer)
        {
            if (id.ToLower() != customer.CustomerId.ToLower())
            {
                return BadRequest();
            }

            //EF para modificar en la DB
            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(string id)
        {
            //EF
            var customer = context.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            //EF
            context.Customers.Remove(customer);
            context.SaveChanges();

            return customer;

        }
    }
}
