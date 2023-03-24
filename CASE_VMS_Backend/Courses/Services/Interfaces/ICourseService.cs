using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseResponseDTO>> GetAllCourseInstances();
        Task<CourseInstance> AddNewCourseInstanceJson(CourseInstance courseInstance);
        Task<CourseModel> AddNewCourseJson(CourseModel course);
    }
}