using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Users");

            HasKey(u => u.Id);
            Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class NormalUserMapper : EntityTypeConfiguration<NormalUser>
    {
        public NormalUserMapper()
        {
            Map<NormalUser>(u =>
            {
                u.ToTable("NormalUsers");
            });

            HasMany(u => u.Answers)
                .WithRequired(ca => ca.NormalUser)
                .HasForeignKey(ca => ca.NormalUserId)
                .WillCascadeOnDelete(false);
        }
    }

    public class MerchantManagerMapper : EntityTypeConfiguration<MerchantManager>
    {
        public MerchantManagerMapper()
        {
            Map<MerchantManager>(u =>
            {
                u.ToTable("MerchantManagers");
            });
        }
    }

    public class MerchantClientMapper : EntityTypeConfiguration<MerchantClient>
    {
        public MerchantClientMapper()
        {
            Map<MerchantClient>(u =>
            {
                u.ToTable("MerchantClients");
            });
        }
    }
}
