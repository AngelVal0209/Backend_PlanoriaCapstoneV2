using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class HeatmapDay
    {
        public DateTime Date { get; set; }
        public int Intensity { get; set; }
    }

    public class HeatmapDataResponseDto
    {
        public int Year { get; set; }
        public List<HeatmapDay> Days { get; set; }
        public int TotalActivity { get; set; }
    }
}
