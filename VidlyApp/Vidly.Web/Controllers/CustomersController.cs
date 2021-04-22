using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Web.DataAccess;
using Vidly.Web.Models;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyDBContext _dbContext;

        public CustomersController(VidlyDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IActionResult Index()
        {
            var customers = _dbContext.Customers.Include(c => c.MembershipType);
            return View(customers);
        }

        [Route("customers/details/{id?}")]
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return NotFound();

            var customer = _dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        public async Task<IActionResult> New()
        {
            var membershipTypes = await _dbContext.MembershipTypes.ToListAsync();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        // GET Customers/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (!Id.HasValue)
                return NotFound();

            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == Id);
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _dbContext.MembershipTypes.ToListAsync()
            };

            return View("CustomerForm", viewModel);
        }
        
        /* POST Customers/Save
         * To protect from overposting attacks, enable the specific properties you want to bind to, for
         * more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost]
        public async Task<IActionResult> Save(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if(customer.Id == 0)
                    _dbContext.Customers.Add(customer);
                else
                {
                    var customerInDB = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == customer.Id);
                    customerInDB.Name = customer.Name;
                    customerInDB.Birthdate = customer.Birthdate;
                    customerInDB.MembershipTypeId = customer.MembershipTypeId;
                    customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                }
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
