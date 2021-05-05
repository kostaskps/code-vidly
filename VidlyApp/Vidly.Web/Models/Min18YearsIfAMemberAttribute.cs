using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

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

            var customer = validationContext.ObjectInstance as Customer;

            if (!customer.MembershipTypeId.HasValue || customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (!customer.Birthdate.HasValue)
                return new ValidationResult(stringLocalizer["BirthDateRequired"]);

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult(stringLocalizer["CustomerAtLeast18Years"]);
        }

    }
}
