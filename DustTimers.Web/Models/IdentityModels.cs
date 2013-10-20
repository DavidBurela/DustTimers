using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DustTimers.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set the database schema so that it works in a multi-tenant environment.
            //DISABLED - this currently isn't supported with automatic migrations
            //modelBuilder.HasDefaultSchema("DustTimers");

            base.OnModelCreating(modelBuilder);
        }
    }
}