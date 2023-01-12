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
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Camper> Campers { get; set; }
        public DbSet<Councelor> Councelors { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Visit> CamperNextOfKins { get; set; }
    }
}
