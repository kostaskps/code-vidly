using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Web.DataAccess;
using Vidly.Web.Models;

namespace Vidly.Web.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyDBContext _dbContext;

        public CustomersController(VidlyDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [Route("customers/index")]
        public IActionResult Index()
        {
            var customers = _dbContext.Customers.Include(c => c.MembershipType);
            return View(customers);
        }

        [Route("customers/details/{id?}")]
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                Id = 0;

            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        private IEnumerable<Customer> ProvideCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith"},
                new Customer { Id = 2, Name = "Mary Williams"}
            };
            return customers;
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
