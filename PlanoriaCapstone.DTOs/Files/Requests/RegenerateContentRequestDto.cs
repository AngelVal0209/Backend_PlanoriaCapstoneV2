using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Files.Requests
{
    public class RegenerateContentRequestDto
    {
        public int GeneratedContentId { get; set; }
        public string AdjustComplexity { get; set; }
        public List<string> FocusOnTopics { get; set; }
    }
}