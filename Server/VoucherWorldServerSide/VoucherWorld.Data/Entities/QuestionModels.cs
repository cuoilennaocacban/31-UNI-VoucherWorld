using System.Collections.Generic;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace VoucherWorld.Data.Entities
{
    public class ScalingQuestion : Entity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public Place Place { get; set; }

        public ICollection<ScalingAnswer> ScalingAnswers { get; set; } 
        
        public ScalingQuestion()
        {
            ScalingAnswers = new List<ScalingAnswer>();
        }
    }

    public class OpenEndedQuestion : Entity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public Route Route { get; set; }

        public ICollection<OpenEndedAnswer> OpenEndedAnswers { get; set; }

        public OpenEndedQuestion()
        {
            OpenEndedAnswers = new List<OpenEndedAnswer>();
        }
    }

    public class QuestionModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}