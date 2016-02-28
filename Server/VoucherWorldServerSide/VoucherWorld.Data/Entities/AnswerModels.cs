using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace VoucherWorld.Data.Entities
{
    public class Answer : Entity
    {
        public int Id { get; set; }
        
        public int NormalUserId { get; set; }
        public NormalUser NormalUser { get; set; }
        
        public Answer() {}
    }

    public class ScalingAnswer : Answer
    {
        public int Content { get; set; }

        public int ScalingQuestionId { get; set; }
        public ScalingQuestion ScalingQuestion { get; set; }
    }

    public class OpenEndedAnswer : Answer
    {
        public string Content { get; set; }

        public int OpenEndedQuestionId { get; set; }
        public OpenEndedQuestion OpenEndedQuestion { get; set; }
    }
}
