using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class ActivityItemResponseDto
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string CourseName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Metadata { get; set; }
    }
}
