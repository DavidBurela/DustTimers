using System.Data.Entity.Migrations;
using DustTimers.Web.Data;

namespace DustTimers.Web.Migrations
{
    internal sealed class DustTimersDbConfiguration : DbMigrationsConfiguration<DustTimersDbContext>
    {
        public DustTimersDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DustTimers.Web.Data.DustTimersDbContext";
        }

        protected override void Seed(DustTimersDbContext context)
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
