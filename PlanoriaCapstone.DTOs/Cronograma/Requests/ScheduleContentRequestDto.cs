using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Requests
{
    public class ScheduleContentRequestDto
    {
        public string ContentType { get; set; }
        public Guid ContentId { get; set; }
        public int EstimatedMinutes { get; set; }
    }
}
