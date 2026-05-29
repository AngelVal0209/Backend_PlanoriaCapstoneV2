using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class UserCourseExamProgressRepository : IUserCourseExamProgressRepository
{
    private readonly AppDbContext _context;

    public UserCourseExamProgressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserCourseExamProgress?> GetByUserAndCourseAsync(int userId, int courseId)
    {
        return await _context.UserCourseExamProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);
    }

    public async Task<UserCourseExamProgress> CreateOrUpdateAsync(UserCourseExamProgress progress)
    {
        var existing = await _context.UserCourseExamProgresses
            .FirstOrDefaultAsync(p => p.UserId == progress.UserId && p.CourseId == progress.CourseId);

        if (existing != null)
        {
            existing.TotalFlashcards = progress.TotalFlashcards;
            existing.FlashcardsStudied = progress.FlashcardsStudied;
            existing.FlashcardsMastered = progress.FlashcardsMastered;
            existing.TotalQuizzes = progress.TotalQuizzes;
            existing.QuizzesCompleted = progress.QuizzesCompleted;
            existing.QuizzesPassed = progress.QuizzesPassed;
            existing.ExamReadinessScore = progress.ExamReadinessScore;
            existing.LastCalculatedAt = progress.LastCalculatedAt;
            _context.UserCourseExamProgresses.Update(existing);
        }
        else
        {
            _context.UserCourseExamProgresses.Add(progress);
        }

        await _context.SaveChangesAsync();
        return existing ?? progress;
    }

    public async Task<IEnumerable<ExamReadinessScore>> GetReadinessHistoryAsync(int userId, int courseId)
    {
        return await _context.ExamReadinessScores
            .Where(s => s.UserId == userId && s.CourseId == courseId)
            .OrderByDescending(s => s.CalculatedAt)
            .ToListAsync();
    }

    public async Task AddReadinessScoreAsync(ExamReadinessScore score)
    {
        _context.ExamReadinessScores.Add(score);
        await _context.SaveChangesAsync();
    }
}
