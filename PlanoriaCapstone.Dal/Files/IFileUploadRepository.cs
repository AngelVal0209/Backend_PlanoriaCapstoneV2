using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IFileUploadRepository
{
    Task<FileUpload?> GetByIdAsync(int id);
    Task<IEnumerable<FileUpload>> GetByUserIdAsync(int userId);
    Task<FileUpload> CreateAsync(FileUpload fileUpload);
    Task<FileUpload> UpdateAsync(FileUpload fileUpload);
    Task<bool> DeleteAsync(int id);
    Task<GeneratedContent?> GetGeneratedContentAsync(int fileUploadId);
    Task<GeneratedContent> CreateGeneratedContentAsync(GeneratedContent content);
}
