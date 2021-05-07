using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vidly.Web.DataAccess;
using Vidly.Web.Models;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyDBContext _dbContext;
        private readonly IStringLocalizer<CustomersController> _stringLocalizer;

        public CustomersController(VidlyDBContext dBContext, IStringLocalizer<CustomersController> localizer)
        {
            _dbContext = dBContext;
            _stringLocalizer = localizer;
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
            var membershipTypesInDB = await _dbContext.MembershipTypes.ToListAsync();

            var list = new List<SelectListItem>();

            foreach (var membershipType in membershipTypesInDB)
            {
                string localizedText = _stringLocalizer[membershipType.Name];
                list.Add(new SelectListItem(localizedText, membershipType.Id.ToString()));
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypesList = list
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CustomerFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypesInDB = await _dbContext.MembershipTypes.ToListAsync();

                var list = new List<SelectListItem>();

                foreach (var membershipType in membershipTypesInDB)
                {
                    string localizedText = _stringLocalizer[membershipType.Name];
                    list.Add(new SelectListItem(localizedText, membershipType.Id.ToString()));
                }

                var viewModel = new CustomerFormViewModel
                {
                    Customer = vm.Customer,
                    //SelectedMembershipType = vm.SelectedMembershipType,
                    MembershipTypesList = list
                };

                return View("CustomerForm", viewModel);
            }

            if (vm.Customer.Id == 0)
                _dbContext.Customers.Add(vm.Customer);
            else
            {
                var customerInDB = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == vm.Customer.Id);
                customerInDB.Name = vm.Customer.Name;
                customerInDB.Birthdate = vm.Customer.Birthdate;
                customerInDB.MembershipTypeId = vm.Customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = vm.Customer.IsSubscribedToNewsletter;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
