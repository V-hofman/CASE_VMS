using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseResponseDTO>> GetAllAsync();
        Task<CourseResponseDTO> AddAsync(CourseResponseDTO newCourse);
    }
}