using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Portal.Models.Manager
{
    public class MvcOpenEndedAnswerModel
    {
        public string Question { get; set; }
        public IEnumerable<OpenEndedAnswer> Answers { get; set; }

        public MvcOpenEndedAnswerModel(IEnumerable<OpenEndedAnswer> answers)
        {
            Answers = answers;
            if (answers.Count() != 0)
            {
                Question = answers.ToList()[0].OpenEndedQuestion.Content;
            }
        }
    }

    public class MvcScalingAnswerModel
    {
        public string Question { get; set; }
        public IEnumerable<ScalingAnswer> Answers { get; set; }
        public IEnumerable<int> AnswerValues { get; set; } 
        public IEnumerable<int> AnswerCount { get; set; } 
        public List<float> AnswerPercentage { get; set; }

        public MvcScalingAnswerModel(IEnumerable<ScalingAnswer> answers)
        {
            Answers = answers;
            if (!Answers.Any())
            {
                AnswerValues = new List<int>();
                AnswerCount = new List<int>();
                AnswerPercentage = new List<float>();
            }
            else
            {
                Question = Answers.ToList()[0].ScalingQuestion.Content;

                AnswerValues = Answers
                    .GroupBy(a => a.Content)
                    .Distinct()
                    .Select(x => x.Key)
                    .ToList();

                AnswerCount = Answers
                    .GroupBy(a => a.Content)
                    .Select(a => a.Count())
                    .ToList();

                AnswerPercentage = new List<float>();
                foreach (var val in AnswerCount)
                {
                    double per = 100 * (double)val / AnswerCount.Sum();
                    per = Math.Round(per, 2);
                    //var per = (float) (Math.Round((double) (100*val/AnswerCount.Sum()), 2));
                    AnswerPercentage.Add((float)per);
                }
            }
        }
    }
}