using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class DatasetDto
    {
        public string Label { get; set; }
        public List<decimal> Data { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public bool Fill { get; set; }
    }

    public class ChartDataResponseDto
    {
        public List<string> Labels { get; set; }
        public List<DatasetDto> Datasets { get; set; }
    }
}
