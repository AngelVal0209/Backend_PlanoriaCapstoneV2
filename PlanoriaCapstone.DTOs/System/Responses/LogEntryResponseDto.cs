using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.System.Responses
{
    public class LogEntryResponseDto
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Context { get; set; }
        public Guid? UserId { get; set; }
        public string IpAddress { get; set; }
    }
}
