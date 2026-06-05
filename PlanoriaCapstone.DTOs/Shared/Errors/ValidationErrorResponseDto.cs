using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Shared.Errors
{
    public class ValidationErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
