using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Repository
{
    public class CourseRepository : ICourseRepository
    {
        List<CourseResponseDTO> Courses = new List<CourseResponseDTO>
        {
         new CourseResponseDTO{  Duration = 5, Title = "Introduction to C#", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5 },
         new CourseResponseDTO{  Duration = 5, Title = "Introduction to Java", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5  },
         new CourseResponseDTO{  Duration = 5, Title = "Introduction to Python", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5 }
        };

        public Task<CourseResponseDTO> AddAsync(CourseResponseDTO newCourse)
        {
            this.Courses.Add(newCourse);
            return Task.FromResult(newCourse);
        }

        public async Task<IEnumerable<CourseResponseDTO>> GetAllAsync()
        {
            return Courses;
        }
    }
}
