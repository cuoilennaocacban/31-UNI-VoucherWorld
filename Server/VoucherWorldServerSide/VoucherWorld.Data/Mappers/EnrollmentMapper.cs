using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class EnrollmentMapper : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentMapper()
        {
            ToTable("Enrollments");

            HasKey(e => new {e.NormalUserId, e.RouteId});

            HasRequired(e => e.NormalUser)
                .WithMany(nu => nu.Enrollments)
                .HasForeignKey(e => e.NormalUserId);

            HasRequired(e => e.Route)
                .WithMany(r => r.Enrollments)
                .HasForeignKey(e => e.RouteId);
        }
    }
}
