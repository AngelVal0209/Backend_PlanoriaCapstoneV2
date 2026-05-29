namespace PlanoriaCapstone.Models;

public class FileUpload
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string OriginalFilename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public User? User { get; set; }
    public ICollection<GeneratedContent>? GeneratedContents { get; set; }
}
