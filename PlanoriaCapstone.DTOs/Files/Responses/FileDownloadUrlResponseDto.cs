using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Files.Responses
{
    public class FileDownloadUrlResponseDto
    {
        public string DownloadUrl { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Filename { get; set; }
        public long FileSize { get; set; }
    }
}