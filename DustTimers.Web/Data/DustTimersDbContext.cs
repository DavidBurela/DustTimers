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
            modelBuilder.HasDefaultSchema("DustTimers");
        }
    }

}