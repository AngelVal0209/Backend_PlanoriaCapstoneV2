using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Exam
{
    public class ReadinessPoint
    {
        public DateTime Date { get; set; }
        public decimal Score { get; set; }
    }

    public class ReadinessHistoryResponseDto
    {
        public List<ReadinessPoint> History { get; set; }
        public string Trend { get; set; }
        public decimal PredictedScoreOnExamDate { get; set; }
        public decimal ConfidenceInterval { get; set; }
    }
}
