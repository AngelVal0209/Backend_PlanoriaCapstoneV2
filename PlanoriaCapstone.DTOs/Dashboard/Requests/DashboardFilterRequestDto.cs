using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Requests
{
    public class DateRange
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }

    public class DashboardFilterRequestDto
    {
        public DateRange DateRange { get; set; }
        public List<Guid> CourseIds { get; set; }
        public bool IncludeArchived { get; set; }
    }
}
