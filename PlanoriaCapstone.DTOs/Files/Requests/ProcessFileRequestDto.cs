using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Files.Requests
{
    public class ProcessFileRequestDto
    {
        public Guid FileId { get; set; }
        public string ContentFormat { get; set; }
        public string Topic { get; set; }
        public Guid TargetCourseId { get; set; }
        public string Difficulty { get; set; }
    }
}