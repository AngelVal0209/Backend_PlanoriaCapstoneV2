using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Responses
{
    public class EmailLogResponseDto
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public DateTime SentAt { get; set; }
        public string ErrorMessage { get; set; }
    }
}
