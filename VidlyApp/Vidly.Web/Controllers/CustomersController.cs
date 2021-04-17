using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Web.Models;

namespace Vidly.Web.Controllers
{
    public class CustomersController : Controller
    {
        [Route("customers/index")]
        public IActionResult Index()
        {
            var customers = ProvideCustomers();
            return View(customers);
        }

        [Route("customers/details/{id?}")]
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                Id = 0;

            var customer = ProvideCustomers().SingleOrDefault(c => c.Id == Id);
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
    }
}
