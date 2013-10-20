using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DustTimers.LegacyApi.Models;

namespace DustTimers.Web.Data.Configuration
{
    public class CorporationConfiguration : EntityTypeConfiguration<Corporation>
    {
        public CorporationConfiguration()
        {
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}