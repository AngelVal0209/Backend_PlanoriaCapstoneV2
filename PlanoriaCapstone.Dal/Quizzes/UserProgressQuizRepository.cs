using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class UserProgressQuizRepository : IUserProgressQuizRepository
{
    private readonly AppDbContext _context;

    public UserProgressQuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProgressQuiz?> GetByUserAndQuizAsync(int userId, int quizId)
    {
        return await _context.UserProgressQuizzes
            .FirstOrDefaultAsync(p => p.UserId == userId && p.QuizId == quizId);
    }

    public async Task<IEnumerable<UserProgressQuiz>> GetByUserAsync(int userId)
    {
        return await _context.UserProgressQuizzes
            .Where(p => p.UserId == userId)
            .Include(p => p.Quiz)
            .ToListAsync();
    }

    public async Task<UserProgressQuiz> CreateOrUpdateAsync(UserProgressQuiz progress)
    {
        var existing = await _context.UserProgressQuizzes
            .FirstOrDefaultAsync(p => p.UserId == progress.UserId && p.QuizId == progress.QuizId);

        if (existing != null)
        {
            existing.TotalAttempts = progress.TotalAttempts;
            existing.BestScore = progress.BestScore;
            existing.AverageScore = progress.AverageScore;
            existing.LastAttemptAt = progress.LastAttemptAt;
            existing.PassedCount = progress.PassedCount;
            _context.UserProgressQuizzes.Update(existing);
        }
        else
        {
            _context.UserProgressQuizzes.Add(progress);
        }

        await _context.SaveChangesAsync();
        return existing ?? progress;
    }
}
