using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using System.Text.Json.Serialization;

namespace CASE_VMS_Backend.Courses.Models
{
    public class CourseInstance
    {
        public CourseInstance()
        {
        }

        public CourseInstance(DateOnly startTime)
        {
            StartTime = startTime;
        }

        public CourseInstance(DateOnly startTime, CourseModel course)
        {
            StartTime = startTime;
            Course = course;
        }

        public int Id { get; set; }
        public DateOnly StartTime { get; set; }


        public CourseModel Course { get; set; }
        public int CourseId { get; set; }

        public virtual List<AttendeeModel> Attendees { get; set; } = new();
        
    }
}
