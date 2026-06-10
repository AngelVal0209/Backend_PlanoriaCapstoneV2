using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Quiz?> GetByIdAsync(int id)
    {
        return await _context.Quizzes
            .Include(q => q.Course)
            .Include(q => q.QuizQuestions!)        
                .ThenInclude(qq => qq.QuizOptions) 
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Quizzes
            .Where(q => q.CourseId == courseId)
            .Include(q => q.QuizQuestions!)
                .ThenInclude(qq => qq.QuizOptions)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes
            .Include(q => q.Course)
            .Include(q => q.QuizQuestions!)
                .ThenInclude(qq => qq.QuizOptions)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();
    }

    public async Task<Quiz> CreateAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<Quiz> UpdateAsync(Quiz quiz)
    {
        var existingQuiz = await _context.Quizzes
            .Include(q => q.QuizQuestions!)
                .ThenInclude(qq => qq.QuizOptions)
            .FirstOrDefaultAsync(q => q.Id == quiz.Id);

        if (existingQuiz != null)
        {
            // Solo actualizar propiedades simples del quiz
            existingQuiz.Title = quiz.Title;
            existingQuiz.Description = quiz.Description;
            existingQuiz.TotalQuestions = quiz.TotalQuestions;
            existingQuiz.PassingScore = quiz.PassingScore;
            existingQuiz.TimeLimitMinutes = quiz.TimeLimitMinutes;
            existingQuiz.ShuffleQuestions = quiz.ShuffleQuestions;
            existingQuiz.ShuffleOptions = quiz.ShuffleOptions;
            existingQuiz.AttemptsAllowed = quiz.AttemptsAllowed;
            existingQuiz.UpdatedAt = DateTime.UtcNow;

            // ✅ NO reemplazar QuizQuestions - solo actualizar si es necesario
            if (quiz.QuizQuestions != null && quiz.QuizQuestions.Any())
            {
                // Solo agregar preguntas NUEVAS (las que no existen)
                foreach (var newQuestion in quiz.QuizQuestions)
                {
                    if (existingQuiz.QuizQuestions == null)
                        existingQuiz.QuizQuestions = new List<QuizQuestion>();

                    var exists = existingQuiz.QuizQuestions.Any(q => q.Id == newQuestion.Id && newQuestion.Id > 0);
                    if (!exists)
                    {
                        existingQuiz.QuizQuestions.Add(newQuestion);
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
        return existingQuiz ?? quiz;
    }

    public async Task<QuizQuestion> AddQuestionAsync(QuizQuestion question)
    {
        _context.QuizQuestions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.QuizQuestions!)
                .ThenInclude(qq => qq.QuizOptions)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quiz == null) return false;

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
        return true;
    }
}