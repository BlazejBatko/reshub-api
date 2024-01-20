using Microsoft.EntityFrameworkCore;
using ResHub.Models;

namespace ResHub.Services
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ResidenceModel> Residences { get; set; }

    }
}
