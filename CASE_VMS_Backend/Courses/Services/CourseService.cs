using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services.Interfaces;

namespace CASE_VMS_Backend.Courses.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseInstanceRepository _courseInstanceRepository;
        private readonly IAttendeeRepository _attendeeRepository;


        public CourseService(ICourseRepository courseRepository, ICourseInstanceRepository courseInstanceRepository, IAttendeeRepository attendeeRepository)
        {
            _courseRepository = courseRepository;
            _courseInstanceRepository = courseInstanceRepository;
            _attendeeRepository = attendeeRepository;
        }

        public async Task<List<CourseResponseDTO>> GetAllCourseInstances()
        {
            var result = await _courseInstanceRepository.GetAllAsync();
            List<CourseResponseDTO> courseResponses = new();

            foreach (var instance in result)
            {
                var response = new CourseResponseDTO(startDate: instance.StartTime, duration: instance.Course.DurationInDays, title: instance.Course.CourseTitle, id: instance.Id, attendees: instance.Attendees);
                courseResponses.Add(response);
            }
            return courseResponses;
        }

        public async Task<CourseModel> AddNewCourseJson(CourseModel course)
        {

            var courseToAdd = _courseRepository.GetAllAsync().Result.FirstOrDefault(c => c.CourseTitle == course.CourseTitle);
            if (courseToAdd == null)
            {
                await _courseRepository.AddAsync(course);
            }
            else
            {
                throw new DuplicateEntryException("Course already exists!");
            }
            return course;
        }

        public async Task<CourseInstance> AddNewCourseInstanceJson(CourseInstance courseInstance)
        {
            var courseInstanceToAdd = _courseInstanceRepository.GetAllAsync().Result.FirstOrDefault(ci => ci.CourseId == courseInstance.CourseId && ci.StartTime == courseInstance.StartTime);
            if (courseInstanceToAdd == null)
            {
                await _courseInstanceRepository.AddAsync(courseInstance);
            }
            else
            {
                throw new DuplicateEntryException("Course instance already exists!");
            }
            return courseInstance;
        }

    }
}
