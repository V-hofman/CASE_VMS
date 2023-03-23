using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;

namespace CASE_VMS_Backend.Courses.Repository
{
    public class CourseInstanceRepository : ICourseInstanceRepository
    {
        private readonly CourseContext _context;

        public CourseInstanceRepository(CourseContext context)
        {
            this._context = context;
        }

        public Task<CourseInstance> AddAsync(CourseInstance newCourse)
        {
            newCourse.Course = _context.Courses.FirstOrDefault(c => c.CourseCode == newCourse.Course.CourseCode);
            
            if(newCourse.Course == null)
            {
                throw new Exception("Course cant be found!");
            }

            if (_context.CourseInstances.FirstOrDefault(ci => ci.Course == newCourse.Course && ci.StartTime == newCourse.StartTime) != null)
            {
                throw new DuplicateEntryException("Instance already exists");
            }
            _context.CourseInstances.Add(newCourse);
            _context.SaveChanges();
            return Task.FromResult(newCourse);
        }

        public Task<CourseInstance> UpdateAsync(CourseInstance newCourse)
        {
            newCourse.Course = _context.Courses.FirstOrDefault(c => c.CourseCode == newCourse.Course.CourseCode);
            
            if(newCourse.Course == null)
            {
                throw new Exception("Course cant be found!");
            }
            _context.CourseInstances.Update(newCourse);
            _context.SaveChanges();
            return Task.FromResult(newCourse);  
        }

        public async Task<List<CourseInstance>> GetAllAsync()
        {
            return await this._context.CourseInstances.Include(ci => ci.Course).Include(ci => ci.Attendees).ToListAsync();
        }

        public async Task<CourseInstance> GetByIdAsync(int Id)
        {
            return await this._context.CourseInstances.Include(ci => ci.Course).Include(ci => ci.Attendees).FirstOrDefaultAsync(c => c.Id == Id);
        }

    }
}
