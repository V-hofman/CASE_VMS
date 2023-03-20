namespace CASE_VMS_Backend.Courses.Models.AttendeeModels
{
    public class AttendeeModel
    {
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

        public CorporateInfoModel? CorporateInfo { get; set; }
        public PrivateInfoModel? PrivateInfo { get; set; }
    }
}
