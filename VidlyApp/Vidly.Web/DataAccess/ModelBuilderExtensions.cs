using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vidly.Web.Models;

// Seeding EF Core data: https://www.learnentityframeworkcore.com/migrations/seeding

namespace Vidly.Web.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void SeedMembershipTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, SignupFee = 0, DurationInMonths = 0, DiscountRate = 0 },
                new MembershipType { Id = 2, SignupFee = 30, DurationInMonths = 1, DiscountRate = 10 },
                new MembershipType { Id = 3, SignupFee = 90, DurationInMonths = 3, DiscountRate = 15 },
                new MembershipType { Id = 4, SignupFee = 300, DurationInMonths = 12, DiscountRate = 2 }
            );
        }
    }
}
