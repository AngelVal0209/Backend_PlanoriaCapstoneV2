namespace PlanoriaCapstone.Models;

public class UserCourse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public string Role { get; set; } = "owner";
    public DateTime JoinedAt { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
}
