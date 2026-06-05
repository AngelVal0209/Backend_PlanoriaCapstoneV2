using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class DistributionDataResponseDto
    {
        public List<string> Labels { get; set; }
        public List<decimal> Values { get; set; }
        public decimal Total { get; set; }
    }
}
