using PlanoriaCapstone.DTOs.Courses.Requests;
using PlanoriaCapstone.DTOs.Courses.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface ICourseService
{
    Task<CourseResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<CourseListResponseDto>> GetByUserIdAsync(int userId);
    Task<CourseResponseDto> CreateAsync(int userId, CreateCourseRequestDto request);
    Task<CourseResponseDto?> UpdateAsync(int id, UpdateCourseRequestDto request);
    Task<bool> DeleteAsync(int id);
    Task ArchiveAsync(int id);
    Task RestoreAsync(int id);
    Task SetExamDateAsync(int id, SetExamDateRequestDto request);
    Task<CourseExamResponseDto?> GetExamDateAsync(int id);
    Task RemoveExamDateAsync(int id);
    Task<IEnumerable<CourseMemberResponseDto>> GetMembersAsync(int courseId);
    Task AddMemberAsync(int courseId, int userId, AddCourseMemberRequestDto request);
    Task RemoveMemberAsync(int courseId, int userId);
    Task ChangeMemberRoleAsync(int courseId, int targetUserId, UpdateMemberRoleRequestDto request);
    Task<CourseStatsResponseDto> GetStatsAsync(int courseId, int userId);
    Task<IEnumerable<CourseListResponseDto>> SearchAsync(int userId, CourseSearchRequestDto request);
}
