using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Requests
{
    public class UpdateProfileRequestDto
    {
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public string Timezone { get; set; }
    }
}