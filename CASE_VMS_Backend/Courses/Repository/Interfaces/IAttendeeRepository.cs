using CASE_VMS_Backend.Courses.Models.AttendeeModels;

namespace CASE_VMS_Backend.Courses.Repository.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<AttendeeModel> AddAsync(AttendeeModel newAttendee);
        Task<List<AttendeeModel>> GetAllAsync();
    }
}