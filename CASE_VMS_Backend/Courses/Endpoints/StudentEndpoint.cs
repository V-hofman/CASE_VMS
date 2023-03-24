using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CASE_VMS_Backend.Courses.Endpoints
{
    [ApiController]
    [Route("api/students")]
    public class StudentEndpoint : ControllerBase
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IStudentService _studentService;

        public StudentEndpoint(IAttendeeRepository attendeeRepository, IStudentService studentService)
        {
            this._attendeeRepository = attendeeRepository;
            this._studentService = studentService;
        }

        /// <summary>
        /// Endpoint to get all the students
        /// </summary>
        /// <returns>OK with all the students</returns>
        [HttpGet]
        public async Task<ActionResult<AttendeeModel[]>> GetStudents()
        {
            var students = _attendeeRepository.GetAllAsync();
            return Ok(students);
        }
        /// <summary>
        /// Adds a student to a course
        /// </summary>
        /// <param name="student">AttedeeModelDTO holding a student and a course number</param>
        /// <returns>Ok with the model</returns>
        [HttpPost]
        public async Task<ActionResult<AttendeeModel>> PostStudent([FromBody] AttendeeModelDTO student)
        {
            this._studentService.AddNewStudentToCourse(student);
            return Ok(student);
        }

        /// <summary>
        /// Grabs all the students from a course
        /// </summary>
        /// <param name="Id">the id of the course you wish to grab the students from</param>
        /// <returns>All the students in an array</returns>
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<AttendeeModel[]>> GetStudentsByCourseId(int Id)
        {
            return Ok(this._studentService.GrabStudentsFromCourseInstanceId(Id));
        }
    }
}
