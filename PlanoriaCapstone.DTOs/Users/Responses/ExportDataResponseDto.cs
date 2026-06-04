using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Responses
{
    public class ExportDataResponseDto
    {
        public string DownloadUrl { get; set; }
        public long FileSize { get; set; }
        public DateTime ExpiresAt { get; set; }
        public List<string> Formats { get; set; }
    }
}