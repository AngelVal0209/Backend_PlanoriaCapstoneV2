using PlanoriaCapstone.DTOs.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class CourseMemberResponseDto
    {
        public int Id { get; set; }
        public UserResponseDto User { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}