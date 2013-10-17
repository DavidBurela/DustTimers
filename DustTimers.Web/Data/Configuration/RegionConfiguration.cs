using CrestParser.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DustTimers.Web.Data.Configuration
{
    public class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}