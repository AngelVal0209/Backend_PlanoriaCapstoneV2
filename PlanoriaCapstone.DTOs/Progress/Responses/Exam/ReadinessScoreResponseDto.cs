using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Exam
{
    public class ReadinessFactors
    {
        public decimal FlashcardsMastery { get; set; }
        public decimal QuizzesPerformance { get; set; }
        public decimal StudyConsistency { get; set; }
        public decimal TimeUntilExam { get; set; }
    }

    public class ReadinessScoreResponseDto
    {
        public decimal CurrentScore { get; set; }
        public decimal PreviousScore { get; set; }
        public decimal ChangePercentage { get; set; }
        public ReadinessFactors Factors { get; set; }
    }
}
