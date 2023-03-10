using CampSleepaway.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Data
{
    public class CampSleepawayContext : DbContext
    {
        // Configures an appSettings.json file and specifies that the project is using SQL Server as dbms.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        // Creates temportal tables that adds history functionality.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Camper>()
                .ToTable(nameof(Camper), b => b.IsTemporal());

            modelBuilder
                .Entity<NextOfKin>()
                .ToTable(nameof(NextOfKin), b => b.IsTemporal());

            modelBuilder
                .Entity<Councelor>()
                .ToTable(nameof(Councelor), b => b.IsTemporal());

            modelBuilder
                .Entity<Cabin>()
                .ToTable(nameof(Cabin), b => b.IsTemporal());
        }

        public DbSet<Camper> Campers { get; set; }
        public DbSet<Councelor> Councelors { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Visit> Visits { get; set; }

        // CamperNextOfKins is a join table.
        public DbSet<CamperNextOfKin> CamperNextOfKins { get; set; }
    }
}
