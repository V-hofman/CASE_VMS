using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.FileHandler
{
    public interface IFileToCourseParser
    {
        List<CourseInstance> CourseInstances { get; set; }
        List<CourseResponseDTO> CourseResponseDTOs { get; set; }
        List<CourseModel> Courses { get; set; }
        List<WrapperModel> CourseWrappers { get; set; }

        List<int> ParseFile(IFormFile file);
        List<int> ParseWrappers();
        List<int> PushToDataBase();
    }
}