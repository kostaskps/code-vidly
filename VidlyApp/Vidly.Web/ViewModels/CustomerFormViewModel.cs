﻿using System.Collections.Generic;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        

        public Customer Customer { get; set; }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> MembershipTypesList { get; set; }
    }
}
