using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Files.Requests;

namespace Backend_PlanoriaCapstone.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public class FileUploadRequest
        {
            public int CourseId { get; set; }
            public required IFormFile File { get; set; }
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();

            using var stream = request.File.OpenReadStream();
            var result = await _fileService.UploadAsync(userId.Value, request.CourseId, stream, request.File.FileName, request.File.ContentType, request.File.Length);
            return Ok(result);
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetUploadStatus(int id)
        {
            var result = await _fileService.GetUploadStatusAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUploadHistory()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();

            var history = await _fileService.GetUploadHistoryAsync(userId.Value);
            return Ok(history);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpload(int id)
        {
            var deleted = await _fileService.DeleteUploadAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/process")]
        public async Task<IActionResult> ProcessFile(int id, [FromBody] ProcessFileRequestDto request)
        {
            var result = await _fileService.ProcessFileAsync(id, request.TargetCourseId, request.ContentFormat);
            return Ok(result);
        }

        [HttpGet("{id}/processing-status")]
        public async Task<IActionResult> GetProcessingStatus(int id)
        {
            var result = await _fileService.GetProcessingStatusAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("{id}/reprocess")]
        public async Task<IActionResult> Reprocess(int id)
        {
            var result = await _fileService.ReprocessAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var (stream, contentType, fileName) = await _fileService.DownloadAsync(id);
                return File(stream, contentType, fileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/stream")]
        public async Task<IActionResult> StreamFile(int id)
        {
            try
            {
                var stream = await _fileService.StreamFileAsync(id);
                return File(stream, "application/octet-stream");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
