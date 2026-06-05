using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.IA.Requests
{
    public class BatchGenerateRequestDto
    {
        public List<Guid> Files { get; set; }
        public string ContentType { get; set; }
        public Guid TargetCourseId { get; set; }
    }
}
