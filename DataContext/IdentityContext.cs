using AdminHRM.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminHRM.Server.DataContext
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<Employee>().HasNoKey();
            // Employee with Supervisor relationship (self-referencing 1-N)
            //modelBuilder.Entity<Employee>()
            //    .HasOne(s => s.SupperEmployee)
            //    .WithMany(g => g.Employees)
            //    .HasForeignKey(s => s.EmployeeId)
            //    .OnDelete(DeleteBehavior.Restrict);
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
