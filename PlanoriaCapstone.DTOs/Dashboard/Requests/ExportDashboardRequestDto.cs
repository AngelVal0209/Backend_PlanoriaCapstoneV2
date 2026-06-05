using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Requests
{
    public class ExportDashboardRequestDto
    {
        public string Format { get; set; }
        public bool IncludeCharts { get; set; }
        public bool IncludeRawData { get; set; }
        public DateRange DateRange { get; set; }
        public List<Guid> CourseIds { get; set; }
    }
}
