using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;

namespace CASE_VMS_Backend.Courses.Repository
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly CourseContext _context;

        public AttendeeRepository(CourseContext context)
        {
            this._context = context;
        }

        public Task<AttendeeModel> AddAsync(AttendeeModel newAttendee)
        {
            _context.Attendees.Add(newAttendee);
            _context.SaveChanges();
            return Task.FromResult(newAttendee);
        }

        public async Task<List<AttendeeModel>> GetAllAsync()
        {
            return await this._context.Attendees.ToListAsync();
        }
    }
}
