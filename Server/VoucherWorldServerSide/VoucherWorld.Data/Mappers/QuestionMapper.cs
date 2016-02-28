using System.Data.Entity.ModelConfiguration;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Mappers
{
    public class ScalingQuestionMapper : EntityTypeConfiguration<ScalingQuestion>
    {
        public ScalingQuestionMapper()
        {
            ToTable("ScalingQuestions");
        }
    }

    public class OpenEndedQuestionMapper : EntityTypeConfiguration<OpenEndedQuestion>
    {
        public OpenEndedQuestionMapper()
        {
            ToTable("OpenEndedQuestions");
        }
    }
}
