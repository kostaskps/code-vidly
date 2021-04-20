using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Web.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignupFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
}
