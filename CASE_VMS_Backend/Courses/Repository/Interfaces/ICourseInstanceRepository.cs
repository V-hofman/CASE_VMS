using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Repository.Interfaces
{
    public interface ICourseInstanceRepository
    {
        Task<CourseInstance> AddAsync(CourseInstance newCourse);
        Task<List<CourseInstance>> GetAllAsync();
        Task<CourseInstance> GetByIdAsync(int Id);

        Task<CourseInstance> UpdateAsync(CourseInstance newCourse);
    }
}