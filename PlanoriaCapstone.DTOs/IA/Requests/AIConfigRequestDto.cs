using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Requests
{
    public class AIConfigRequestDto
    {
        public string Provider { get; set; }
        public string ApiKey { get; set; }
        public string Model { get; set; }
        public int MaxTokens { get; set; } = 2000;
        public decimal Temperature { get; set; } = 0.7m;
    }
}
