namespace CASE_VMS_Backend.Courses.Models
{
    public class CourseInstance
    {
        public int Id { get; set; }
        public DateOnly StartTime { get; set; }


        
        public CourseModel Course { get; set; }
        public int CourseId { get; set; }
        
    }
}
