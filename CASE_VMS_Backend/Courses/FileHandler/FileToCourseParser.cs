using System.Text;

namespace CASE_VMS_Backend.Courses.FileHandler
{
    public class FileToCourseParser
    {

        public List<string> Index(IFormFile file) => ReadAsList(file);

        public CourseDTO ParseFile(IFormFile file)
        {
            var course = new CourseDTO();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while(reader.Peek() >= 0)
                {
                    switch (reader.ReadLine())
                    {
                        case string a when a.Contains("Titel:"):
                            course.Title = a;
                            break;
                        case string b when b.Contains("Cursuscode:"):
                            course.Title = b;
                            break;
                        case string c when c.Contains("Duur:"):
                            course.Title = c;
                            break;
                        case string d when d.Contains("StartDatum:"):
                            course.Title = d;
                            break;
                    }

                }
            }


            return course;
        }

        public List<string> ReadAsList(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString().Split(Environment.NewLine).ToList();
        }
    }
}
