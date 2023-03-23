using CASE_VMS_Backend.Courses.Models.AttendeeModels;

namespace CASE_VMS_Backend.Courses.Services
{
    public interface IStudentService
    {
        AttendeeModelDTO AddNewStudentToCourse(AttendeeModelDTO studentCourse);
        AttendeeModel[] GrabStudentsFromCourseInstanceId(int Id);
    }
}