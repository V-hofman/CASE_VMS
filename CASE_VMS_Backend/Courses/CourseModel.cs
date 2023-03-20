namespace CASE_VMS_Backend.Courses
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public int DurationInDays { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
    }

    public class CourseDTO
    {
        public DateOnly StartDate { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
      
        public int NumberOfSignedIn { get; set; }
    }
}
