using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Auth.Responses
{
    public class VerificationSentResponseDto
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public DateTime ResentAt { get; set; }
    }
}