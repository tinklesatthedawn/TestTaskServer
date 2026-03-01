using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DAL.Configuration;
using Domain.Entities;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<InterfaceEntity> Interfaces { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<RegisterEntity> Registers { get; set; }
        public DbSet<RegisterValueEntity> RegisterValues { get; set; }
        public DbSet<LogMessageEntity> LogMessages { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())         
                .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<InterfaceEntity>(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration<DeviceEntity>(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration<RegisterEntity>(new RegisterConfiguration());
            modelBuilder.ApplyConfiguration<RegisterValueEntity>(new RegisterValueConfiguration());
            modelBuilder.ApplyConfiguration<LogMessageEntity>(new LogMessageConfiguration());
        }
    }
}
