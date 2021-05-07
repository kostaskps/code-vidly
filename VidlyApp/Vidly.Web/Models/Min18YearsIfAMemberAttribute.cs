using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Models
{
    public class Min18YearsIfAMemberAttribute : ValidationAttribute
    {
        public Min18YearsIfAMemberAttribute() : base()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // https://stackoverflow.com/questions/57776877/asp-net-core-custom-validation-error-message-not-localized
            var stringLocalizer = validationContext.GetService(typeof(IStringLocalizer<Customer>)) as IStringLocalizer<Customer>;

            var viewModel = validationContext.ObjectInstance as CustomerFormViewModel;

            if (!viewModel.MembershipTypeId.HasValue || viewModel.MembershipTypeId == MembershipType.Unknown || viewModel.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (!viewModel.Birthdate.HasValue)
                return new ValidationResult(stringLocalizer["BirthDateRequired"]);

            var age = DateTime.Today.Year - viewModel.Birthdate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult(stringLocalizer["CustomerAtLeast18Years"]);
        }

    }
}
