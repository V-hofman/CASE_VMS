namespace CASE_VMS_Backend.Courses.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseDTO>> GetAllAsync();
        Task<CourseDTO> AddAsync(CourseDTO newCourse);
    }
}