using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class AnswerMapper : EntityTypeConfiguration<Answer>
    {
        public AnswerMapper()
        {
            ToTable("Answers");

            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.NormalUser)
                .WithMany(nu => nu.Answers)
                .HasForeignKey(c => c.NormalUserId);
        }
    }

    public class ScalingAnswerMapper : EntityTypeConfiguration<ScalingAnswer>
    {
        public ScalingAnswerMapper()
        {
            Map<ScalingAnswer>(sa =>
            {
                sa.ToTable("ScalingAnswers");
            });

            HasRequired(sa => sa.ScalingQuestion)
                .WithMany(sq => sq.ScalingAnswers)
                .HasForeignKey(sa => sa.ScalingQuestionId);
        }
    }

    public class OpenEndedAnswerMapper : EntityTypeConfiguration<OpenEndedAnswer>
    {
        public OpenEndedAnswerMapper()
        {
            Map<OpenEndedAnswer>(oa =>
            {
                oa.ToTable("OpenEndedAnswers");
            });

            HasRequired(oa => oa.OpenEndedQuestion)
                .WithMany(oq => oq.OpenEndedAnswers)
                .HasForeignKey(oa => oa.OpenEndedQuestionId);
        }
    }
}
