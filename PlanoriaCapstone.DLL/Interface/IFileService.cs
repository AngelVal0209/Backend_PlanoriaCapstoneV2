using PlanoriaCapstone.DTOs.Files.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IFileService
{
    Task<FileUploadResponseDto> UploadAsync(int userId, int courseId, Stream fileStream, string fileName, string contentType, long fileSize);
    Task<FileUploadResponseDto?> GetUploadStatusAsync(int fileId);
    Task<IEnumerable<FileUploadResponseDto>> GetUploadHistoryAsync(int userId);
    Task<bool> DeleteUploadAsync(int fileId);
    Task<FileProcessingStatusResponseDto> ProcessFileAsync(int fileId, int targetCourseId, string contentType);
    Task<FileProcessingStatusResponseDto?> GetProcessingStatusAsync(int fileId);
    Task<FileProcessingStatusResponseDto> ReprocessAsync(int fileId);
    Task<(Stream Stream, string ContentType, string FileName)> DownloadAsync(int fileId);
    Task<string?> GetFileUrlAsync(int fileId);
    Task<Stream> StreamFileAsync(int fileId);
}
