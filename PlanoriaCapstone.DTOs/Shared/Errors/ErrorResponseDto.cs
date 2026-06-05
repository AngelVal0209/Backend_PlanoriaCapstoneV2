using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Shared.Errors
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public DateTime Timestamp { get; set; }
        public string Path { get; set; }
        public string RequestId { get; set; }
    }
}
