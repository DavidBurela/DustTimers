using System.Data.Entity.Migrations;
using DustTimers.Web.Data;
using DustTimers.Web.Models;

namespace DustTimers.Web.Migrations
{
    internal sealed class ApplicationDbConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ApplicationDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DustTimers.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
