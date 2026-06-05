using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Exam
{
    public class PriorityContent
    {
        public string Type { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class WeaknessesResponseDto
    {
        public List<string> WeakTopics { get; set; }
        public List<string> RecommendedActions { get; set; }
        public List<PriorityContent> PriorityContent { get; set; }
        public int EstimatedTimeToImprove { get; set; }
    }
}
