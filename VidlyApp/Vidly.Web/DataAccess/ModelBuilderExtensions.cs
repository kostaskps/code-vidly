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
                new MembershipType { Id = 1, SignupFee = 0, DurationInMonths = 0, DiscountRate = 0, Name = "Pay as You Go" },
                new MembershipType { Id = 2, SignupFee = 30, DurationInMonths = 1, DiscountRate = 10, Name = "Monthly" },
                new MembershipType { Id = 3, SignupFee = 90, DurationInMonths = 3, DiscountRate = 15, Name = "Quarterly" },
                new MembershipType { Id = 4, SignupFee = 300, DurationInMonths = 12, DiscountRate = 2, Name = "Yearly" }
            );
        }

        public static void SeedGenresTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Thriller" },
                new Genre { Id = 3, Name = "Family" },
                new Genre { Id = 4, Name = "Romance" },
                new Genre { Id = 5, Name = "Comedy" }
            ); ;
        }
    }
}
