using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class BatchGenerationResponseDto
    {
        public int BatchId { get; set; }
        public int TotalFiles { get; set; }
        public int Successful { get; set; }
        public int Failed { get; set; }
        public List<GenerationResponseDto> Results { get; set; }
    }
}
