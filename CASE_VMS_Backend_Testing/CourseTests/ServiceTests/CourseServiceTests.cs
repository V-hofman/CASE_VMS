using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services;
using CASE_VMS_Backend.DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing.CourseTests.ServiceTests
{
    public class CourseServiceTests
    {

        Mock<ICourseRepository> mockCourseRepository;
        Mock<ICourseInstanceRepository> mockCourseInstanceRepository;
        Mock<IAttendeeRepository> mockAttendeeRepository;
        CourseService courseService { get; set; }

        public CourseServiceTests()
        {

            mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseInstanceRepository = new Mock<ICourseInstanceRepository>();
            mockAttendeeRepository = new Mock<IAttendeeRepository>();
            courseService = new CourseService(mockCourseRepository.Object, mockCourseInstanceRepository.Object, mockAttendeeRepository.Object);
        }

        [Fact]
        public void GetAllCourseInstances()
        {
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            // Act
            var result = courseService.GetAllCourseInstances();
            // Assert
            Assert.NotNull(result);
            // Arrange

        }

        [Fact]
        public void AddNewCourseJson()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            var testUpload = new CourseModel(5, "Test", "TESTING");

            // Act
            var result = courseService.AddNewCourseJson(testUpload);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AddNewCourseInstanceJson()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());

            var testUpload = new CourseInstance(DateOnly.FromDateTime(DateTime.Now), course: new CourseModel(5, "Test", "TESTING"));
            // Act
            var result = courseService.AddNewCourseInstanceJson(testUpload);
            // Assert
            Assert.NotNull(result);
        }
    }
}
