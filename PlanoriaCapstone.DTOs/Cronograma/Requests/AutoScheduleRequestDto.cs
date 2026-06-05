using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Requests
{
    public class AutoScheduleRequestDto
    {
        public int CourseId { get; set; }
        public decimal StudyHoursPerDay { get; set; }
        public string PreferredStartTime { get; set; }
        public string PreferredEndTime { get; set; }
        public List<int> DaysOfWeek { get; set; }
        public bool PrioritizeExam { get; set; }
    }
}
