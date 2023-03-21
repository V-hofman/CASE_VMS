using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Repository
{
    public interface ICourseInstanceRepository
    {
        Task<CourseInstance> AddAsync(CourseInstance newCourse);
        Task<List<CourseInstance>> GetAllAsync();
    }
}