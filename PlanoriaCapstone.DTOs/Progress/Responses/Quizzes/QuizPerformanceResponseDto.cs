using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Quizzes
{
    public class TopicAccuracy
    {
        public string Topic { get; set; }
        public decimal Accuracy { get; set; }
    }

    public class ScorePoint
    {
        public int Attempt { get; set; }
        public decimal Score { get; set; }
    }

    public class QuizPerformanceResponseDto
    {
        public List<TopicAccuracy> WeakTopics { get; set; }
        public List<TopicAccuracy> StrongTopics { get; set; }
        public int AverageResponseTime { get; set; }
        public List<ScorePoint> ScoreTrend { get; set; }
    }
}
