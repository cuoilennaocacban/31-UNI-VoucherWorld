namespace VoucherWorld.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VoucherWorldContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

#if DEBUG
        protected override void Seed(VoucherWorld.Data.VoucherWorldContext context)
        {
            new VoucherWorldDataSeeder(context).Seed();
        }
#endif
    }
}
