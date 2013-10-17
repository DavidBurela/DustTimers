using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using CrestParser.Models;
using DustTimers.Web.Data.Configuration;

namespace DustTimers.Web.Data
{
    public class DustTimersDbContext : DbContext
    {

        public DustTimersDbContext()
            : base("DefaultConnection") { }
        
        // Tables
        public DbSet<District> Districts { get; set; }
        public DbSet<Infrastructure> Infrastructures { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<CrestParser.Models.System> Systems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set the database schema so that it works in a multi-tenant environment.
            modelBuilder.HasDefaultSchema("DustTimers");

            // Set column defaults & relationships
            modelBuilder.Configurations.Add(new ConstellationConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new InfrastructureConfiguration());
            modelBuilder.Configurations.Add(new OwnerConfiguration());
            modelBuilder.Configurations.Add(new PlanetConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new SystemConfiguration());
        }
    }

}