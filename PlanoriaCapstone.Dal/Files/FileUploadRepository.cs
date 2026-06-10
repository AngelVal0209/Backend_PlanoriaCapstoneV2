using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class FileUploadRepository : IFileUploadRepository
{
    private readonly AppDbContext _context;

    public FileUploadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FileUpload?> GetByIdAsync(int id)
    {
        return await _context.FileUploads
            .Include(f => f.GeneratedContents)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<FileUpload>> GetByUserIdAsync(int userId)
    {
        return await _context.FileUploads
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.UploadedAt)
            .ToListAsync();
    }

    public async Task<FileUpload> CreateAsync(FileUpload fileUpload)
    {
        _context.FileUploads.Add(fileUpload);
        await _context.SaveChangesAsync();
        return fileUpload;
    }

    public async Task<FileUpload> UpdateAsync(FileUpload fileUpload)
    {
        _context.FileUploads.Update(fileUpload);
        await _context.SaveChangesAsync();
        return fileUpload;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var file = await _context.FileUploads.FindAsync(id);
        if (file == null) return false;
        _context.FileUploads.Remove(file);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<GeneratedContent?> GetGeneratedContentAsync(int fileUploadId)
    {
        return await _context.GeneratedContents
            .FirstOrDefaultAsync(g => g.FileUploadId == fileUploadId);
    }

    public async Task<GeneratedContent> CreateGeneratedContentAsync(GeneratedContent content)
    {
        _context.GeneratedContents.Add(content);
        await _context.SaveChangesAsync();
        return content;
    }

    // ✅ NUEVO MÉTODO
    public async Task<GeneratedContent> UpdateGeneratedContentAsync(GeneratedContent content)
    {
        _context.GeneratedContents.Update(content);
        await _context.SaveChangesAsync();
        return content;
    }
}