using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Files.Requests
{
    public class DeleteFileRequestDto
    {
        public Guid FileId { get; set; }
        public bool Permanent { get; set; }
    }
}