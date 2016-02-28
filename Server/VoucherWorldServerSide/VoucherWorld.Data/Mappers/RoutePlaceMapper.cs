using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class RoutePlaceMapper : EntityTypeConfiguration<RoutePlace>
    {
        public RoutePlaceMapper()
        {
            ToTable("RoutePlaces");

            HasKey(rp => new {rp.RouteId, rp.PlaceId});

            HasRequired(rp => rp.Route)
                .WithMany(r => r.RoutePlaces)
                .HasForeignKey(rp => rp.RouteId)
                .WillCascadeOnDelete(false);

            HasRequired(rp => rp.Place)
                .WithMany(r => r.RoutePlaces)
                .HasForeignKey(rp => rp.PlaceId)
                .WillCascadeOnDelete(false);
        }
    }
}
