using Microsoft.EntityFrameworkCore;
using Vidly.Web.Extensions;
using Vidly.Web.Models;

namespace Vidly.Web.DataAccess
{
    public class VidlyDBContext : DbContext
    {
        public VidlyDBContext(DbContextOptions<VidlyDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedMembershipTypes();
            modelBuilder.SeedGenresTypes();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
