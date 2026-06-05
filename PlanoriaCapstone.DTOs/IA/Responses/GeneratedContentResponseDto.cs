using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Responses
{
    public class GeneratedContentResponseDto
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string FileOriginalName { get; set; }
        public string ContentType { get; set; }
        public int GeneratedEntityId { get; set; }
        public string EntityName { get; set; }
        public string TopicSpecified { get; set; }
        public string GenerationConfig { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
