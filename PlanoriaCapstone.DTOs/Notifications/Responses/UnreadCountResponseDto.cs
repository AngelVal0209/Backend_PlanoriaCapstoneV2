using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Notifications.Responses
{
    public class UnreadCountResponseDto
    {
        public int Count { get; set; }
        public bool HasMore { get; set; }
    }
}
