namespace PlanoriaCapstone.Models;

public class UserCourseExamProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int TotalFlashcards { get; set; }
    public int FlashcardsStudied { get; set; }
    public int FlashcardsMastered { get; set; }
    public int TotalQuizzes { get; set; }
    public int QuizzesCompleted { get; set; }
    public int QuizzesPassed { get; set; }
    public decimal ExamReadinessScore { get; set; }
    public DateTime? LastCalculatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
}
