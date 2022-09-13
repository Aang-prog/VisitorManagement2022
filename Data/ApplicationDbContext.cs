using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitorManagement2022.Models;

namespace VisitorManagement2022.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<VisitorManagement2022.Models.StaffNames>? StaffNames { get; set; }
        public DbSet<VisitorManagement2022.Models.Visitors>? Visitors { get; set; }
    }
}