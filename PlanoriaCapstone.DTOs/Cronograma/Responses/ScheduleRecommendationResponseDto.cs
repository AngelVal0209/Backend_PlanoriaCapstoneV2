using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Responses
{
    public class ScheduleRecommendationResponseDto
    {
        public string RecommendedStartTime { get; set; }
        public int RecommendedDuration { get; set; }
        public List<string> SuggestedContent { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
    }
}
