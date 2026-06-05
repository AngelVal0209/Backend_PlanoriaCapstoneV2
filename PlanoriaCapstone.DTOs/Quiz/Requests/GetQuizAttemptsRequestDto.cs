using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class GetQuizAttemptsRequestDto
    {
        public Guid QuizId { get; set; }
        public int? Limit { get; set; }
        public string SortBy { get; set; }
    }
}
