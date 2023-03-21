using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository;
using System.Text;
using System.Text.RegularExpressions;

namespace CASE_VMS_Backend.Courses.FileHandler
{
    public class FileToCourseParser
    {
        public FileToCourseParser(ICourseRepository courseRepo)
        {
            this.courseRepo = courseRepo;
        }

        private ICourseRepository courseRepo { get; set; }

        public List<WrapperModel> CourseWrappers { get; set; }
        public List<CourseModel> Courses { get; set; } = new();
        public List<CourseResponseDTO> CourseResponseDTOs { get; set; } = new();
            
        public void ParseFile(IFormFile file)
        {
            CourseWrappers = new List<WrapperModel>();
            var courseWrapper = new WrapperModel();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while(reader.Peek() >= 0)
                {
                    switch (reader.ReadLine())
                    {
                        case string a when a.Contains("Titel:"):
                            courseWrapper.Title = a.Split(":")[1];
                            break;
                        case string b when b.Contains("Cursuscode:"):
                            courseWrapper.CourseCode = b.Split(":")[1];
                            break;
                        case string c when c.Contains("Duur:"):
                            courseWrapper.Duration = int.Parse(Regex.Match(c.Split(":")[1], @"\d+").Value );
                            break;
                        case string d when d.Contains("Startdatum:"):
                            courseWrapper.StartDate = DateOnly.ParseExact(d.Split(":")[1].Trim(), "d/MM/yyyy");
                            break;
                        case string e when string.IsNullOrWhiteSpace(e):
                            CourseWrappers.Add(courseWrapper);
                            courseWrapper = new WrapperModel();
                            break;
                    }

                }
            }

            ParseWrappers();
            return;
        }

        public void ParseWrappers()
        {
            foreach (var wrapper in CourseWrappers)
            {
                var Course = new CourseModel(durationInDays: wrapper.Duration, courseTitle: wrapper.Title, courseCode: wrapper.CourseCode);
                var CourseResponse = new CourseResponseDTO(duration: wrapper.Duration, title: wrapper.Title, startDate: wrapper.StartDate);

                Courses.Add(Course);
                CourseResponseDTOs.Add(CourseResponse);
                Console.WriteLine(Course);
            }
            PushToDataBase();
        }

        public void PushToDataBase()
        {
            foreach (var course in Courses)
            {

                courseRepo.AddAsync(course);
            }
        }

    }
}
