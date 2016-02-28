using System.Data.Entity;
using Repository.Pattern.Ef6;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Mappers;
using VoucherWorld.Data.Migrations;

namespace VoucherWorld.Data
{
    public class VoucherWorldContext : DataContext
    {
        public VoucherWorldContext() :
            base("name=LocalConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoucherWorldContext, Configuration>());
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<ScalingAnswer> ScalingAnswers { get; set; }
        public DbSet<OpenEndedAnswer> OpenEndedAnswers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<ScalingQuestion> ScalingQuestions { get; set; }
        public DbSet<OpenEndedQuestion> OpenEndedQuestions { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RoutePlace> RoutePlaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NormalUser> NormalUsers { get; set; }
        public DbSet<MerchantManager> ManagerUsers { get; set; }
        public DbSet<MerchantClient> MerchantClients { get; set; } 
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnswerMapper());
            modelBuilder.Configurations.Add(new ScalingAnswerMapper());
            modelBuilder.Configurations.Add(new OpenEndedAnswerMapper());
            modelBuilder.Configurations.Add(new EnrollmentMapper());
            modelBuilder.Configurations.Add(new GiftMapper());
            modelBuilder.Configurations.Add(new MerchantMapper());
            modelBuilder.Configurations.Add(new PlaceMapper());
            modelBuilder.Configurations.Add(new ScalingQuestionMapper());
            modelBuilder.Configurations.Add(new OpenEndedQuestionMapper());
            modelBuilder.Configurations.Add(new RouteMapper());
            modelBuilder.Configurations.Add(new RoutePlaceMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new NormalUserMapper());
            modelBuilder.Configurations.Add(new MerchantManagerMapper());
            modelBuilder.Configurations.Add(new MerchantClientMapper());

            base.OnModelCreating(modelBuilder);
        }

    }
}
