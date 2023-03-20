using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CASE_VMS_Backend.Courses.Endpoints
{
    [ApiController]
    [Route("api/courses")]
    public class FileUpload : ControllerBase
    {
        private ICourseRepository _courseRepository;

        public FileUpload(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CourseDTO>> GetCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDTO>> PostCourse(IFormFile file)
        {

            var parser = new FileToCourseParser();
            parser.ParseFile(file);

            return Ok();
        }
    }
}
