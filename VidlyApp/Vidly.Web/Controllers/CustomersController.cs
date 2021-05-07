using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vidly.Web.Contracts;
using Vidly.Web.DataAccess;
using Vidly.Web.Models;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Controllers
{
    public class CustomersController : BaseVidlyController
    {
        private VidlyDBContext _dbContext;
        private readonly IStringLocalizer<CustomersController> _stringLocalizer;

        public CustomersController(VidlyDBContext dbContext, IProvideUnitOfWork unitOfWork, IStringLocalizer<CustomersController> localizer) : base(unitOfWork)
        {
            _dbContext = dbContext;
            _stringLocalizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            var customersInDB = _dbContext.Customers;
            var membershipTypesInDB = await UnitOfWork.MembershipTypes.GetAllAsync();

            foreach (var customer in customersInDB)
            {
                foreach (var membershipType in membershipTypesInDB)
                {
                    if (membershipType.Id != customer.MembershipTypeId)
                        continue;
                    customer.MembershipType.Name = _stringLocalizer[membershipType.Name];
                }
            }

            return View(customersInDB);
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
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypesList = await GetLocalizedMembershipTypesDropDownAsync()
            };

            return View("CustomerForm", viewModel);
        }

        // GET Customers/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (!Id.HasValue)
                return NotFound();

            var customerInDB = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == Id);
            if (customerInDB == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel(customerInDB)
            {
                MembershipTypesList = await GetLocalizedMembershipTypesDropDownAsync()
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
                var viewModel = new CustomerFormViewModel
                {
                    MembershipTypesList = await GetLocalizedMembershipTypesDropDownAsync()
                };

                return View("CustomerForm", viewModel);
            }

            if (vm.Id == 0)
            {
                var newCustomer = Customer.CreateFromViewModel(vm);
                _dbContext.Customers.Add(newCustomer);
            }
            else
            {
                var customerInDB = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == vm.Id);
                customerInDB.Name = vm.Name;
                customerInDB.Birthdate = vm.Birthdate.Value;
                customerInDB.MembershipTypeId = vm.MembershipTypeId.Value;
                customerInDB.IsSubscribedToNewsletter = vm.IsSubscribedToNewsletter;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetLocalizedMembershipTypesDropDownAsync()
        {
            var membershipTypesInDB = await UnitOfWork.MembershipTypes.GetAllAsync();

            var localizedItemList = new List<SelectListItem>(membershipTypesInDB.Count);

            var enumerator = membershipTypesInDB.GetEnumerator();
            while (enumerator.MoveNext())
            {
                localizedItemList.Add(new SelectListItem(_stringLocalizer[enumerator.Current.Name], enumerator.Current.Id.ToString()));
            }
            return localizedItemList;
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
