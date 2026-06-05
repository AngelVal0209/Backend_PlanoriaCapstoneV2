using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Responses
{
    public class NotificationListResponseDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Priority { get; set; }
    }
}
