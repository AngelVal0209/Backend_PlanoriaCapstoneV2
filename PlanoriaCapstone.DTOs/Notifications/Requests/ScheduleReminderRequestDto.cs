using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Requests
{
    public class ScheduleReminderRequestDto
    {
        public int ScheduleId { get; set; }
        public int RemindMinutesBefore { get; set; }
    }
}
