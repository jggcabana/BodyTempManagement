using BodyTemp.Entities.Models;
using BodyTemp.Repositories.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Repositories.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<BodyTemperature> BodyTemperatures { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetDecimalProps(builder);

            builder.Entity<Employee>()
                .ToTable("Employees", builder => builder.Property("Id").HasColumnName("EmployeeId"))
                .HasData(Seeders.Seeders.Employees());

            builder.Entity<Employee>()
                .HasIndex(e => e.EmployeeNumber)
                .IsUnique(true);

            builder.Entity<BodyTemperature>()
                .ToTable("BodyTemperatures", builder => builder.Property("Id").HasColumnName("BodyTemperatureId"));
        }

        private void SetDecimalProps(ModelBuilder modelBuilder)
        {
            var decimalProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(4);
                property.SetScale(1);
            }
        }
    }
}
