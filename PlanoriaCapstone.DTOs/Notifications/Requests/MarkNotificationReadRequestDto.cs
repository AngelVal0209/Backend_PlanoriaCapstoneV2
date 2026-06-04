using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Requests
{
    public class MarkNotificationReadRequestDto
    {
        public Guid NotificationId { get; set; }
        public bool Read { get; set; }
    }
}
