using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Quizzes
{
    public class PeriodInfo
    {
        public DateTime Date { get; set; }
        public decimal Score { get; set; }
    }

    public class QuizComparisonResponseDto
    {
        public PeriodInfo Period1 { get; set; }
        public PeriodInfo Period2 { get; set; }
        public decimal Improvement { get; set; }
        public string BestQuiz { get; set; }
        public string WorstQuiz { get; set; }
    }
}
