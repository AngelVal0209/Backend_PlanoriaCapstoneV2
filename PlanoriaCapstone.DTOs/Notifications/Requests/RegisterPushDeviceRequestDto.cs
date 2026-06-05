using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Requests
{
    public class RegisterPushDeviceRequestDto
    {
        public string DeviceToken { get; set; }
        public string Platform { get; set; }
        public string DeviceName { get; set; }
    }
}
