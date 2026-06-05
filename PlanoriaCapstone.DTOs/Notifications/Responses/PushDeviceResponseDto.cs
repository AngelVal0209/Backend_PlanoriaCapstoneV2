using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Responses
{
    public class PushDeviceResponseDto
    {
        public Guid Id { get; set; }
        public string Platform { get; set; }
        public string DeviceName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUsedAt { get; set; }
    }
}
