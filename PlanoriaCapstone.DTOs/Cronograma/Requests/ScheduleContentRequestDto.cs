using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Requests
{
    public class ScheduleContentRequestDto
    {
        public int ScheduleId { get; set; }
        public string ContentType { get; set; }
        public int ContentId { get; set; }
        public int EstimatedMinutes { get; set; }
    }
}
