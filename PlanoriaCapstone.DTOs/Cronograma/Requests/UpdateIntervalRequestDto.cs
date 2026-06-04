using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Cronograma.Requests
{
    public class UpdateIntervalRequestDto
    {
        public string IntervalType { get; set; }
        public int? DurationMinutes { get; set; }
        public int? OrderPosition { get; set; }
    }
}
