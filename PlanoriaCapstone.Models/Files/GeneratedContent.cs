namespace PlanoriaCapstone.Models;

public class GeneratedContent
{
    public int Id { get; set; }
    public int FileUploadId { get; set; }
    public int CourseId { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public int GeneratedEntityId { get; set; }
    public string? TopicSpecified { get; set; }
    public string? GenerationConfig { get; set; }
    public DateTime CreatedAt { get; set; }

    public FileUpload? FileUpload { get; set; }
    public Course? Course { get; set; }
}
