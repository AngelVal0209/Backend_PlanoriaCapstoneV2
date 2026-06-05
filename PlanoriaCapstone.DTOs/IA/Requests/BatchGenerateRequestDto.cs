using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Requests
{
    public class BatchGenerateRequestDto
    {
        public List<int> Files { get; set; }
        public string ContentType { get; set; }
        public int TargetCourseId { get; set; }
    }
}
