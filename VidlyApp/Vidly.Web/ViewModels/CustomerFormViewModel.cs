using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        

        public Customer Customer { get; set; }

        public IEnumerable<SelectListItem> MembershipTypesList { get; set; }
    }
}
