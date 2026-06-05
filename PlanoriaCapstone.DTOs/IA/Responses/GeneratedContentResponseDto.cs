using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class GeneratedContentResponseDto
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public string FileOriginalName { get; set; }
        public string ContentType { get; set; }
        public Guid GeneratedEntityId { get; set; }
        public string EntityName { get; set; }
        public string TopicSpecified { get; set; }
        public string GenerationConfig { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
