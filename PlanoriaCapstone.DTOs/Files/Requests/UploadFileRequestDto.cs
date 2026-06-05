using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace PlanoriaCapstone.DTOs.Files.Requests
{
    public class UploadFileRequestDto
    {
        public IFormFile File { get; set; }
        public string FileType { get; set; }
        public int CourseId { get; set; }
    }
}