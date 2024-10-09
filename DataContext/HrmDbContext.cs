using AdminHRM.Entities;
using AdminHRM.Server.AppSettings;
using AdminHRM.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHRM.Server.DataContext
{
    public class HrmDbContext : DbContext
    {
        private readonly PostgreSetting _postgreSetting;
        public HrmDbContext(PostgreSetting postgreSetting)
        {
            _postgreSetting = postgreSetting;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgreSetting.ConnectionString ?? "");
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SubUnit> SubUnits { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubUnit>().ToTable("SubUnits").HasKey(x => x.Id);

            modelBuilder.Entity<Employee>().ToTable("Employees")
                .HasOne<SubUnit>(s => s.SubUnits)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.SubUnitId);

            modelBuilder.Entity<Employee>()
                .HasOne(s => s.SupperEmployee)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Employee>(s => s.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Leave>()
               .ToTable("Leaves")
               .HasOne<Employee>()
               .WithMany(e => e.Leaves)
               .HasForeignKey(l => l.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);


            // Cấu hình mối quan hệ 1-1 giữa Employee và IdentityUser
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId);
        }

        public override int SaveChanges()
        {
            var dateNow = DateTime.UtcNow;
            var errorList = new List<ValidationResult>();

            var entries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added ||
                            p.State == EntityState.Modified)
                .ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Count != 0)
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var dateNow = DateTime.UtcNow;
            var errorList = new List<ValidationResult>();

            var entries = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.CreateDate = itemBase.UpdateDate = dateNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity is BaseEntities itemBase)
                    {
                        itemBase.UpdateDate = dateNow;
                    }
                }

                Validator.TryValidateObject(entity, new ValidationContext(entity), errorList);
            }

            if (errorList.Count != 0)
            {
                throw new Exception(string.Join(", ", errorList.Select(p => p.ErrorMessage)).Trim());
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
