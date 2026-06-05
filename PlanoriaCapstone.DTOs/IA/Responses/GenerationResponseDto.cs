using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class GenerationResponseDto
    {
        public Guid GenerationId { get; set; }
        public Guid FileId { get; set; }
        public string ContentType { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        public int EstimatedTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
