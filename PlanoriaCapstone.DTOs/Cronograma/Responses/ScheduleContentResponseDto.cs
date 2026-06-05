using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Responses
{
    public class ScheduleContentResponseDto
    {
        public Guid Id { get; set; }
        public string ContentType { get; set; }
        public Guid ContentId { get; set; }
        public string ContentName { get; set; }
        public int EstimatedMinutes { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
