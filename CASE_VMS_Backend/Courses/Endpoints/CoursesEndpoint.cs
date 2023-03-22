using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CASE_VMS_Backend.Courses.Endpoints
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesEndpoint : ControllerBase
    {
        private ICourseRepository _courseRepository;
        private ICourseInstanceRepository _courseInstanceRepository;
        private FileToCourseParser _fileToCourseParser;

        public CoursesEndpoint(ICourseRepository courseRepository, FileToCourseParser fileToCourseParser, ICourseInstanceRepository courseInstanceRepository)
        {
            _courseRepository = courseRepository;
            _fileToCourseParser = fileToCourseParser;
            _courseInstanceRepository = courseInstanceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CourseResponseDTO>> GetCourses()
        {
            var courseInstances = await _courseInstanceRepository.GetAllAsync();
            List<CourseInstance> instances = new();
            List<CourseResponseDTO> courseResponses = new();

            foreach (var instance in courseInstances) 
            {
                if(instance.CourseId == null)
                {
                    Console.WriteLine("Found detached course, skipping!");
                    continue;
                }else
                {
                    var response = new CourseResponseDTO(startDate: instance.StartTime, duration: instance.Course.DurationInDays, title: instance.Course.CourseTitle);
                    courseResponses.Add(response);
                }
            }
            return Ok(courseResponses);
        }

        [HttpPost]
        public async Task<ActionResult<CourseResponseDTO>> PostCourse(IFormFile file)
        {

            var AmountOfDupes = _fileToCourseParser.ParseFile(file);
            return Created(file.FileName + " Succesfully added! and " + AmountOfDupes[0] + " Course dupes were found and re-used! And " + AmountOfDupes[1] + " Instance dupes were found and skipped!", null);

        }
    }
}
