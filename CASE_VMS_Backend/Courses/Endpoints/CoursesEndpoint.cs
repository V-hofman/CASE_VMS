using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CASE_VMS_Backend.Courses.Endpoints
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesEndpoint : ControllerBase
    {
        private readonly ICourseInstanceRepository _courseInstanceRepository;
        private readonly FileToCourseParser _fileToCourseParser;
        private readonly ICourseService _courseService;

        public CoursesEndpoint( FileToCourseParser fileToCourseParser, ICourseInstanceRepository courseInstanceRepository, ICourseService courseService)
        { 
            _fileToCourseParser = fileToCourseParser;
            _courseInstanceRepository = courseInstanceRepository;
            _courseService = courseService;
        }

        /// <summary>
        /// Endpoint to get all the courses
        /// </summary>
        /// <returns>Http action code with a courseResponseDTO</returns>
        [HttpGet]
        public async Task<ActionResult<CourseResponseDTO>> GetCourses()
        {
            var result = await _courseService.GetAllCourseInstances();
            return Ok(result);
        }


        /// <summary>
        /// Endpoint to post a new course
        /// </summary>
        /// <param name="file">.txt file to be uploaded</param>
        /// <returns>Accepted ActionResult, with a long string with duplicates and how many were added</returns>
        [HttpPost]
        public async Task<ActionResult<CourseResponseDTO>> PostCourse(IFormFile file)
        {

            var AmountOfDupes = _fileToCourseParser.ParseFile(file);
            return Accepted(file.FileName + " Succesfully added! and " + AmountOfDupes[0] + " Course dupes were found and re-used! And " + AmountOfDupes[1] + " Instance dupes were found and skipped!" + (_fileToCourseParser.Courses.Count - AmountOfDupes[0]) + "Courses Added!" + (_fileToCourseParser.CourseInstances.Count - AmountOfDupes[1]) + "Instances added!", null);

        }

        /// <summary>
        /// Endpoint to post a new course
        /// </summary>
        /// <param name="wrappers">Array of WrapperModel</param>
        /// <returns>Accepted or badrequest if it already exists</returns>
        [HttpPost]
        [Route("json")]
        public async Task<ActionResult<WrapperModel[]>> PostCourseJson([FromBody] WrapperModel[] wrappers)
        {
            try
            {
                foreach (var wrapper in wrappers)
                {
                    var courseCode = wrapper.CourseCode.Split(":")[1].Trim();
                    var courseTitle = wrapper.Title.Split(":")[1].Trim();

                    var Course = new CourseModel(durationInDays: wrapper.Duration, courseTitle: courseTitle, courseCode: courseCode);
                    var CourseInstance = new CourseInstance(startTime: DateOnly.FromDateTime(wrapper.StartDate), course: Course);

                    await _courseService.AddNewCourseJson(Course);
                    await _courseService.AddNewCourseInstanceJson(CourseInstance);
                }
            }
            catch
            {
                return BadRequest("Instance already exist!");
            }


            return Accepted("Succesfully added the course",wrappers);
        }

    }
}
