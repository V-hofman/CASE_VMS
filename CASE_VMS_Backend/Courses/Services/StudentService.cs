using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.Courses.Repository;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services.Interfaces;

namespace CASE_VMS_Backend.Courses.Services
{
    public class StudentService : IStudentService
    {
        private readonly ICourseInstanceRepository _courseInstanceRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public StudentService(
            ICourseInstanceRepository courseInstanceRepository,
            IAttendeeRepository attendeeRepository
            )
        {
            _courseInstanceRepository = courseInstanceRepository;
            _attendeeRepository = attendeeRepository;
        }

        public AttendeeModelDTO AddNewStudentToCourse(AttendeeModelDTO studentCourse)
        {
            if (string.IsNullOrWhiteSpace(studentCourse.FirstName) || string.IsNullOrWhiteSpace(studentCourse.Surname))
            {
                throw new ArgumentException("Student name or surname is empty!");
            }
            var student = _attendeeRepository.GetAllAsync().Result.FirstOrDefault(s => s.Name == studentCourse.FirstName && s.Surname == studentCourse.Surname);
            if (student == null)
            {
                student = new AttendeeModel(studentCourse.FirstName, studentCourse.Surname);

            }
            var course = _courseInstanceRepository.GetByIdAsync(studentCourse.courseId);
            if (course.Result == null)
            {
                throw new CourseNotFoundException("Course instance doesnt exist!");
            }

            if (course.Result.Attendees.Any(c => c.Name == student.Name && c.Surname == student.Surname))
            {
                throw new AlreadySignedInException("Student is already in this class!");
            }

            student.Courses.Add(course.Result);
            course.Result.Attendees.Add(student);

            _courseInstanceRepository.UpdateAsync(course.Result);
            return studentCourse;
        }

        public AttendeeModel[] GrabStudentsFromCourseInstanceId(int Id)
        {
            var courseInstance = _courseInstanceRepository.GetByIdAsync(Id);
            if (courseInstance.Result == null)
            {
                throw new CourseNotFoundException("Course instance doesnt exist!");
            }
            return courseInstance.Result.Attendees.ToArray();
        }

    }
}
