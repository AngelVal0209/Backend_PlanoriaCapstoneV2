using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Users.Requests
{
    public class UpdateNotificationSettingsRequestDto
    {
        public bool? StudyReminders { get; set; }
        public bool? ExamAlerts { get; set; }
        public bool? AchievementAlerts { get; set; }
        public string ReminderTime { get; set; }
        public int? ReminderDaysBeforeExam { get; set; }
    }
}