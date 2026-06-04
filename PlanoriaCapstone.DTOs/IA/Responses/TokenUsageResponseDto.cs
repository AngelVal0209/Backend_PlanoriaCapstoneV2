using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class TokenUsageResponseDto
    {
        public int TotalTokensUsed { get; set; }
        public decimal EstimatedCost { get; set; }
        public int RequestsCount { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
