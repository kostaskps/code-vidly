using System;
using System.ComponentModel.DataAnnotations;

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
        public byte? MembershipTypeId { get; set; }
        
        [Min18YearsIfAMember]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
    }
}
