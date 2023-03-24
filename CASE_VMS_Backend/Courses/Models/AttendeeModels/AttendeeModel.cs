using System.Text.Json.Serialization;

namespace CASE_VMS_Backend.Courses.Models.AttendeeModels
{
    public class AttendeeModel
    {
        public AttendeeModel()
        {
        }

        public AttendeeModel(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public AttendeeModel(string name, string surname, CorporateInfoModel? corporateInfo)
        {
            Name = name;
            Surname = surname;
            CorporateInfo = corporateInfo;
        }

        public AttendeeModel(string name, string surname, PrivateInfoModel? privateInfo)
        {
            Name = name;
            Surname = surname;
            PrivateInfo = privateInfo;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [JsonIgnore]
        public CorporateInfoModel? CorporateInfo { get; set; }
        [JsonIgnore]
        public PrivateInfoModel? PrivateInfo { get; set; }
        [JsonIgnore]
        public virtual List<CourseInstance> Courses { get; set; } = new();
    }

    public class AttendeeModelDTO
    {
        public AttendeeModelDTO()
        {
        }

        public AttendeeModelDTO(string firstName, string surname, int courseId)
        {
            FirstName = firstName;
            Surname = surname;
            this.courseId = courseId;
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int courseId { get; set; }
    }

}
