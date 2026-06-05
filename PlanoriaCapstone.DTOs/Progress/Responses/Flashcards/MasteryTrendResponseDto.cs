using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progress.Responses.Flashcards
{
    public class MasteryTrendResponseDto
    {
        public List<DateTime> Dates { get; set; }
        public List<decimal> MasteryScores { get; set; }
        public List<int> NewCards { get; set; }
        public List<int> LearnedCards { get; set; }
        public List<int> MasteredCards { get; set; }
        public List<int> ReviewDueCards { get; set; }
    }
}
