using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Requests
{
    public class GenerateContentRequestDto
    {
        public Guid FileId { get; set; }
        public string ContentType { get; set; }
        public string Topic { get; set; }
        public Guid TargetCourseId { get; set; }
        public int NumberOfItems { get; set; } = 10;
        public string Difficulty { get; set; } = "medium";
        public string Language { get; set; } = "es";
    }
}
