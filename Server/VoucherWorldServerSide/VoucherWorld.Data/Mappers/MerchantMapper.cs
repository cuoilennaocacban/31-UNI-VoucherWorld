using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class MerchantMapper : EntityTypeConfiguration<Merchant>
    {
        public MerchantMapper()
        {
            ToTable("Merchants");

            HasRequired(m => m.MerchantManager)
                .WithOptional(mm => mm.Merchant);

            HasMany(m => m.MerchantClients)
                .WithRequired(mc => mc.Merchant)
                .HasForeignKey(mc => mc.MerchantId)
                .WillCascadeOnDelete(false);

            HasMany(m => m.Places)
                .WithRequired(p => p.Merchant)
                .WillCascadeOnDelete();

            HasMany(m => m.Routes)
                .WithRequired(r => r.Merchant)
                .WillCascadeOnDelete();
        }
    }
}
