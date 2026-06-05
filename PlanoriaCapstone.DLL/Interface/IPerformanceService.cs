using PlanoriaCapstone.DTOs.Progress.Requests;

namespace PlanoriaCapstone.Bll.Interface;

public interface IPerformanceService
{
    Task<object> GetGlobalStatsAsync(int userId);
    Task<object> GetRankingAsync(int userId);
    Task<object> GetAchievementsAsync(int userId);
    Task<object> GetWeeklyTrendAsync(int userId);
    Task<object> GetMonthlyTrendAsync(int userId);
    Task<object> GetYearlyReportAsync(int userId, int year);
    Task SetGoalsAsync(int userId, SetGoalRequestDto request);
    Task<object> GetGoalsAsync(int userId);
    Task UpdateGoalProgressAsync(int userId, UpdateGoalProgressRequestDto request);
    Task<object> CheckAchievementAsync(int userId);
}
