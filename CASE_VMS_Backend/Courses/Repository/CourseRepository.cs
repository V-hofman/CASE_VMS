using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;

namespace CASE_VMS_Backend.Courses.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseContext _context;
        public CourseRepository(CourseContext context)
        {
            this._context = context;
        }

        public Task<CourseModel> AddAsync(CourseModel newCourse)
        {
            if (_context.Courses.FirstOrDefault(c => c.CourseCode == newCourse.CourseCode) != null)
            {
                throw new DuplicateEntryException("Course with Code " + newCourse.CourseCode + " already exists");
            }
            _context.Courses.Add(newCourse);
            _context.SaveChanges();
            return Task.FromResult(newCourse);
        }

        public async Task<List<CourseModel>> GetAllAsync()
        {
            return await this._context.Courses.Include(c => c.CourseInstances).ToListAsync();
        }
    }
}
