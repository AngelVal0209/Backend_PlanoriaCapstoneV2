using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz.Requests
{
    public class QuestionOrderItem
    {
        public Guid Id { get; set; }
        public int OrderPosition { get; set; }
    }

    public class ReorderQuestionsRequestDto
    {
        public List<QuestionOrderItem> QuestionOrder { get; set; }
    }
}
