using CrestParser.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DustTimers.Web.Data.Configuration
{
    public class InfrastructureConfiguration : EntityTypeConfiguration<Infrastructure>
    {
        public InfrastructureConfiguration()
        {
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}