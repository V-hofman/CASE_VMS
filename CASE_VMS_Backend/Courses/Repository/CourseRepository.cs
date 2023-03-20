namespace CASE_VMS_Backend.Courses.Repository
{
    public class CourseRepository : ICourseRepository
    {
        List<CourseDTO> Courses = new List<CourseDTO>
        {
         new CourseDTO{  Duration = 5, Title = "Introduction to C#", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5 },
         new CourseDTO{  Duration = 5, Title = "Introduction to Java", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5  },
         new CourseDTO{  Duration = 5, Title = "Introduction to Python", StartDate = DateOnly.FromDateTime(DateTime.Now), NumberOfSignedIn =  5 }
        };

        public Task<CourseDTO> AddAsync(CourseDTO newCourse)
        {
            this.Courses.Add(newCourse);
            return Task.FromResult(newCourse);
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            return Courses;
        }
    }
}
