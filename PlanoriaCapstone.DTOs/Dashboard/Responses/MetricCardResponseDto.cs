using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class MetricCardResponseDto
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public decimal Change { get; set; }
        public string ChangeType { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
