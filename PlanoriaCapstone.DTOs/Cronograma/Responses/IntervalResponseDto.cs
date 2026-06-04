using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Responses
{
    public class IntervalResponseDto
    {
        public Guid Id { get; set; }
        public string IntervalType { get; set; }
        public int DurationMinutes { get; set; }
        public int OrderPosition { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}
