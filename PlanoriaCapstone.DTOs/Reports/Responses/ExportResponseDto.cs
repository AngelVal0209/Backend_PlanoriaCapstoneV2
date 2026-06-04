using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Reports.Responses
{
    public class ExportResponseDto
    {
        public string DownloadUrl { get; set; }
        public long FileSize { get; set; }
        public string Format { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string FileName { get; set; }
    }
}
