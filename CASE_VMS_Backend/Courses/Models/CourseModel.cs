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

        public int CourseID { get; set; }
        public int DurationInDays { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
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

        public DateOnly StartDate { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public int NumberOfSignedIn { get; set; }
    }

}
