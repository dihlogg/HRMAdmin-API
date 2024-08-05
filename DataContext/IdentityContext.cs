using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminHRM.Server.DataContext
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
        }
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString(), NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString(), NormalizedName = "USER" },
                new IdentityRole() { Name = "Human Resources", ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString(), NormalizedName = "HUMAN RESOURCES" }
                );
        }
    }

}
