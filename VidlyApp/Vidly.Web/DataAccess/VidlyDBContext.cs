using Microsoft.EntityFrameworkCore;
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
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
