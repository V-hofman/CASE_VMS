using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CASE_VMS_Backend.Courses.Endpoints
{
    [ApiController]
    [Route("api/courses")]
    public class FileUpload : ControllerBase
    {
        private ICourseRepository _courseRepository;
        private FileToCourseParser _fileToCourseParser;

        public FileUpload(ICourseRepository courseRepository, FileToCourseParser fileToCourseParser)
        {
            _courseRepository = courseRepository;
            _fileToCourseParser = fileToCourseParser;
        }

        [HttpGet]
        public async Task<ActionResult<CourseResponseDTO>> GetCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult<CourseResponseDTO>> PostCourse(IFormFile file)
        {

            var AmountOfDupes = _fileToCourseParser.ParseFile(file);
            return Created(file.FileName + " Succesfully added! and " + AmountOfDupes[0] + " Course dupes were found and re-used! And " + AmountOfDupes[1] + " Instance dupes were found and skipped!", null);

        }
    }
}
