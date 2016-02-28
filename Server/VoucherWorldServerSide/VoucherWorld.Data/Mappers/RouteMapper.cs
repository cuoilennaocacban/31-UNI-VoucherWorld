using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class RouteMapper : EntityTypeConfiguration<Route>
    {
        public RouteMapper()
        {
            ToTable("Routes");

            HasKey(r => r.Id);
            Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(r => r.OpenEndedQuestion)
                .WithRequired(oq => oq.Route);

            HasOptional(r => r.Gift)
                .WithRequired(g => g.Route);
        }
    }
}
