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
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<DashboardCard> DashboardCards { get; set; }
        public virtual DbSet<RequestReason> RequestReasons { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<RequestInformUser> RequestInformUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubUnit>()
                .ToTable("SubUnits")
                .HasKey(x => x.Id);

            modelBuilder.Entity<RequestInformUser>()
                .ToTable("RequestInformUsers")
                .HasKey(x => x.Id);

            modelBuilder.Entity<RequestType>()
               .ToTable("RequestTypes")
               .HasKey(x => x.Id);

            modelBuilder.Entity<RequestReason>()
               .ToTable("RequestReasons")
               .HasKey(x => x.Id);

            modelBuilder.Entity<RequestStatus>()
               .ToTable("RequestStatuses")
               .HasKey(x => x.Id);

            modelBuilder.Entity<DashboardCard>()
                .HasKey(c => c.CardId);

            // Employee with SubUnit relationship (1-N)
            modelBuilder.Entity<Employee>()
                .ToTable("Employees")
                .HasOne<SubUnit>(s => s.SubUnits)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.SubUnitId);

            // Employee with Supervisor relationship (self-referencing 1-N)
            modelBuilder.Entity<Employee>()
                .HasOne(s => s.SupperEmployee)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // IdentityUser with Employee relationship (1-1)
            modelBuilder.Entity<Employee>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Employee>(s => s.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Card with LeaveRequest relationship (1-N)
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Card)
                .WithMany(c => c.LeaveRequests)
                .HasForeignKey(l => l.CardId)
                .HasPrincipalKey(c => c.CardId) // Liên kết với CardId thay vì Id
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.ApprovedUser)
                .WithMany(e => e.ApprovedRequests)
                .HasForeignKey(l => l.ApprovedId)
                .IsRequired(false)  // Cho phép giá trị null
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Suppervisor)
                .WithMany(e => e.SuppervisedRequests)
                .HasForeignKey(l => l.SuppervisorId)
                .IsRequired(false)  // Cho phép giá trị null
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestInformUser>()
                .HasOne(r => r.Employees)
                .WithMany(e => e.InformRequests) // Một nhân viên có nhiều RequestInformUser
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestInformUser>()
                .HasOne(r => r.LeaveRequest)
                .WithMany(l => l.InformUsers) // Một LeaveRequest có nhiều RequestInformUser
                .HasForeignKey(r => r.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
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
