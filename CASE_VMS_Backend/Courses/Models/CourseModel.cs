using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CASE_VMS_Backend.Courses.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
        }

        public CourseModel( int durationInDays, string courseTitle, string courseCode)
        {
            DurationInDays = durationInDays;
            CourseTitle = courseTitle;
            CourseCode = courseCode;
        }

        public int CourseModelID { get; set; }
        public int DurationInDays { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }

        public List<CourseInstance> CourseInstances { get; set; }
    }

    public class CourseResponseDTO
    {
        public CourseResponseDTO()
        {
        }

        public CourseResponseDTO(DateOnly startDate, int duration, string title)
        {
            StartDate = startDate;
            Duration = duration;
            Title = title;
        }

        public CourseResponseDTO(DateOnly startDate, int duration, string title, int id, List<AttendeeModel> attendees)
        {
            StartDate = startDate;
            Duration = duration;
            Title = title;
            Id = id;
            Attendees = attendees;
        }

        public DateOnly StartDate { get; set; }
        [NotMapped]
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public int NumberOfSignedIn { get; set; }

        public List<AttendeeModel>? Attendees { get; set; } = new();

        public List<int> Duplicates { get; set; }
    }

}
