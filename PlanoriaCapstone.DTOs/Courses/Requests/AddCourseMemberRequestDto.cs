using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Requests
{
    public class AddCourseMemberRequestDto
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}