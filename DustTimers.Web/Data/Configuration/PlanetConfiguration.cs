using CrestParser.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DustTimers.Web.Data.Configuration
{
    public class PlanetConfiguration : EntityTypeConfiguration<Planet>
    {
        public PlanetConfiguration()
        {
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}