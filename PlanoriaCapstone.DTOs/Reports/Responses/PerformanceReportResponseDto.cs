using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Reports.Responses
{
    public class PerformanceReportResponseDto
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public int FlashcardsMastered { get; set; }
        public int QuizzesPassed { get; set; }
        public decimal AverageQuizScore { get; set; }
        public List<string> WeakTopics { get; set; }
        public List<string> StrongTopics { get; set; }
        public List<string> ImprovementAreas { get; set; }
    }
}
