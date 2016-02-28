using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class PlaceMapper : EntityTypeConfiguration<Place>
    {
        public PlaceMapper()
        {
            ToTable("Places");

            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(p => p.ScalingQuestion)
                .WithRequired(sq => sq.Place);
        }
    }
}
