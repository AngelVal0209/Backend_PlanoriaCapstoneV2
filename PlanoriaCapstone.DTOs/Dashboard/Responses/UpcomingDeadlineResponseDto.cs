using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class UpcomingDeadlineResponseDto
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string CourseName { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysRemaining { get; set; }
        public string Urgency { get; set; }
    }
}
