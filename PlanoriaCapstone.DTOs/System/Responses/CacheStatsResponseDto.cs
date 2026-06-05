using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.System.Responses
{
    public class CacheStatsResponseDto
    {
        public int Hits { get; set; }
        public int Misses { get; set; }
        public decimal HitRate { get; set; }
        public long Size { get; set; }
        public DateTime? LastClearedAt { get; set; }
    }
}
