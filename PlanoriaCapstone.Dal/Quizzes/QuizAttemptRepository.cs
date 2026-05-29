using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class QuizAttemptRepository : IQuizAttemptRepository
{
    private readonly AppDbContext _context;

    public QuizAttemptRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<QuizAttempt?> GetByIdAsync(int id)
    {
        return await _context.QuizAttempts
            .Include(a => a.QuizAnswers!)
                .ThenInclude(a => a.Question)
            .Include(a => a.Quiz)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<QuizAttempt>> GetByUserAsync(int userId)
    {
        return await _context.QuizAttempts
            .Where(a => a.UserId == userId)
            .Include(a => a.Quiz)
            .OrderByDescending(a => a.StartedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId)
    {
        return await _context.QuizAttempts
            .Where(a => a.QuizId == quizId)
            .OrderByDescending(a => a.StartedAt)
            .ToListAsync();
    }

    public async Task<QuizAttempt> CreateAsync(QuizAttempt attempt)
    {
        _context.QuizAttempts.Add(attempt);
        await _context.SaveChangesAsync();
        return attempt;
    }

    public async Task<QuizAttempt> UpdateAsync(QuizAttempt attempt)
    {
        _context.QuizAttempts.Update(attempt);
        await _context.SaveChangesAsync();
        return attempt;
    }

    public async Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer)
    {
        _context.QuizAnswers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<IEnumerable<QuizAnswer>> GetAnswersByAttemptAsync(int attemptId)
    {
        return await _context.QuizAnswers
            .Where(a => a.AttemptId == attemptId)
            .Include(a => a.Question)
            .Include(a => a.SelectedOption)
            .ToListAsync();
    }
}
