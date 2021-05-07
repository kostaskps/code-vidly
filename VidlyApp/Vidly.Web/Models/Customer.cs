using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredCustomerName")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        // .NET Core 3 validation system changed. So make the properties nullable to perform NULL check
        [Required(ErrorMessage = "RequiredMembershipType")]
        public byte MembershipTypeId { get; set; }
        
        
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        public static Customer CreateFromViewModel(CustomerFormViewModel viewModel)
        {
            var newCustomer = new Customer
            {
                //Id = viewModel.Id.Value,
                Name = viewModel.Name,
                IsSubscribedToNewsletter = viewModel.IsSubscribedToNewsletter,
                MembershipTypeId = viewModel.MembershipTypeId.Value,
                Birthdate = viewModel.Birthdate.Value
            };
            return newCustomer;
        }
    }
}
