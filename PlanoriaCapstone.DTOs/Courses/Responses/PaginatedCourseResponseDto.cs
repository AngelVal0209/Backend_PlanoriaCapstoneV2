using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Courses.Responses
{
    public class PaginatedCourseResponseDto
    {
        public List<CourseListResponseDto> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int LastPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}