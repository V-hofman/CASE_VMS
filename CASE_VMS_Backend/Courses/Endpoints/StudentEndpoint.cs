using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services;
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

        [HttpGet]
        public async Task<ActionResult<AttendeeModel[]>> GetStudents()
        {
            var students = _attendeeRepository.GetAllAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<AttendeeModel>> PostStudent([FromBody] AttendeeModelDTO student)
        {
            this._studentService.AddNewStudentToCourse(student);
            return Ok(student);
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<AttendeeModel[]>> GetStudentsByCourseId(int Id)
        {
            return Ok(this._studentService.GrabStudentsFromCourseInstanceId(Id));
        }
    }
}
