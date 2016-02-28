using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class GiftMapper : EntityTypeConfiguration<Gift>
    {
        public GiftMapper()
        {
            ToTable("Gifts");
        }
    }
}
