using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.System.Responses
{
    public class HealthCheckResponseDto
    {
        public string Status { get; set; }
        public string Version { get; set; }
        public string Uptime { get; set; }
        public string Database { get; set; }
        public string Cache { get; set; }
        public string Queue { get; set; }
        public List<string> Services { get; set; }
    }
}
