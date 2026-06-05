using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Auth.Requests
{
    public class VerifyEmailRequestDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}