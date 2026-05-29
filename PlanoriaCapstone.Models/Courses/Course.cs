namespace PlanoriaCapstone.Models;

public class Course
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? ExamDate { get; set; }
    public TimeSpan? ExamTime { get; set; }
    public string ColorHex { get; set; } = "#3498db";
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<UserCourse>? UserCourses { get; set; }
    public ICollection<FileUpload>? FileUploads { get; set; }
    public ICollection<FlashcardDeck>? FlashcardDecks { get; set; }
    public ICollection<Quiz>? Quizzes { get; set; }
    public ICollection<UserCourseExamProgress>? UserCourseExamProgresses { get; set; }
    public ICollection<ExamReadinessScore>? ExamReadinessScores { get; set; }
    public ICollection<GeneratedContent>? GeneratedContents { get; set; }
}
