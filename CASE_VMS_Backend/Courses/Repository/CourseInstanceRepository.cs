using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;

namespace CASE_VMS_Backend.Courses.Repository
{
    public class CourseInstanceRepository
    {
        private readonly CourseContext _context;
        
        public CourseInstanceRepository(CourseContext context)
        {
            this._context = context;
        }

        public Task<CourseInstance> AddAsync(CourseInstance newCourse)
        {
            _context.CourseInstances.Add(newCourse);
            _context.SaveChanges();
            return Task.FromResult(newCourse);
        }

        public async Task<List<CourseInstance>> GetAllAsync()
        {
            return await this._context.CourseInstances.ToListAsync();
        }

    }
}
