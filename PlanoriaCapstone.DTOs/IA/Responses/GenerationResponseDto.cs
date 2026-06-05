using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class GenerationResponseDto
    {
        public int GenerationId { get; set; }
        public int FileId { get; set; }
        public string ContentType { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        public int EstimatedTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
