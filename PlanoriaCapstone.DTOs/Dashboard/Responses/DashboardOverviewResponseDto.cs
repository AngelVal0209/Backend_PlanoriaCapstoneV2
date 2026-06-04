using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Dashboard.Responses
{
    public class DashboardOverviewResponseDto
    {
        public int TotalStudyTimeToday { get; set; }
        public int TotalStudyTimeWeek { get; set; }
        public int TotalStudyTimeMonth { get; set; }
        public int CardsReviewedToday { get; set; }
        public int QuizzesCompletedToday { get; set; }
        public int StreakDays { get; set; }
        public int UpcomingExamsCount { get; set; }
        public int PendingReviewsCount { get; set; }
    }
}
