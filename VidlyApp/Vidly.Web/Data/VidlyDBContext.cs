using Microsoft.EntityFrameworkCore;
using Vidly.Web.Models;

namespace Vidly.Web.Data
{
    public class VidlyDBContext : DbContext
    {
        public VidlyDBContext(DbContextOptions<VidlyDBContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
