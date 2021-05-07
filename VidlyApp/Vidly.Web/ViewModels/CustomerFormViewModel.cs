using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class CustomerFormViewModel
    {
        public CustomerFormViewModel()
        {
            Id = 0;
        }

        public CustomerFormViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            MembershipTypeId = customer.MembershipTypeId;
            Birthdate = customer.Birthdate;
        }

        public int? Id { get; set; }

        [Required(ErrorMessage = "RequiredCustomerName")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // .NET Core 3 validation system changed. So make the properties nullable to perform NULL check
        [Required(ErrorMessage = "RequiredMembershipType")]
        public byte? MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        public IEnumerable<SelectListItem> MembershipTypesList { get; set; }

    }
}
