using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Responses
{
    public class UserActivityResponseDto
    {
        public DateTime? LastLogin { get; set; }
        public int TotalStudyTime { get; set; }
        public int TotalCardsReviewed { get; set; }
        public int TotalQuizzesCompleted { get; set; }
        public int StreakDays { get; set; }
    }
}