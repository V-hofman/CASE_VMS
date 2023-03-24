using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services;
using CASE_VMS_Backend.DAL;
using Moq;

namespace CASE_VMS_Backend_Testing.CourseTests.ServiceTests
{
    public class StudentServiceTests
    {
        Mock<IAttendeeRepository> mockAttendeeRepository;
        Mock<ICourseInstanceRepository> mockCourseInstanceRepository;
        StudentService studentService { get; set; }
        public StudentServiceTests()
        {
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockAttendeeRepository = new Mock<IAttendeeRepository>();
            mockCourseInstanceRepository = new Mock<ICourseInstanceRepository>();
            studentService = new StudentService(mockCourseInstanceRepository.Object, mockAttendeeRepository.Object);
        }

        [Fact]
        public void AddNewStudentToCourse_ShouldSignIn()
        {   
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockAttendeeRepository.Setup( repo => repo.GetAllAsync()).ReturnsAsync(new List<AttendeeModel>() {new AttendeeModel("Test", "Test2") });
            mockCourseInstanceRepository.Setup( repo => repo.GetByIdAsync(1)).ReturnsAsync(new CourseInstance());
            var testUpload = new AttendeeModelDTO("Test", "Test2", 1);
            // Act
            var result = studentService.AddNewStudentToCourse(testUpload);
            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AddNewStudentToCourse_ShouldNotUploadAlreadySignedIn()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockAttendeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<AttendeeModel>() { new AttendeeModel("Test", "Test2") });
            mockCourseInstanceRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new CourseInstance());
            var testUpload = new AttendeeModelDTO("Test", "Test2", 1);
            // Act
            var result = studentService.AddNewStudentToCourse(testUpload);
            // Assert
            Assert.Throws<AlreadySignedInException>(() => studentService.AddNewStudentToCourse(testUpload));
        }
        [Fact]
        public void AddNewStudentToCourse_IfCourseDoesntExist_ShouldThrowError()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockAttendeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<AttendeeModel>() { new AttendeeModel("Test", "Test2") });
            var testUpload = new AttendeeModelDTO("Test", "Test2", 1);
            // Act
            // Assert
            Assert.Throws<CourseNotFoundException>(() => studentService.AddNewStudentToCourse(testUpload));
        }
        [Fact]
        public void AddNewStudentToCourse_IfStudentNameIsEmpty_ShouldThrowError()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockAttendeeRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<AttendeeModel>() { new AttendeeModel("Test", "Test2") });
            var testUpload = new AttendeeModelDTO("", "  ", 1);
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => studentService.AddNewStudentToCourse(testUpload));
        }
        [Fact]
        public void GrabStudentFromCourseInstanceId_IfInstanceDoesntExist_ShouldThrowError()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            // Act
            // Assert
            Assert.Throws<CourseNotFoundException>(() => studentService.GrabStudentsFromCourseInstanceId(1));
        }

        [Fact]
        public void GrabStudentFromCourseInstanceId_IfInstanceDoesExist_ShouldReturnStudents()
        {
            // Arrange
            using CourseContext context = new CourseContext(InMemoryDb.SetInMemoryDb());
            mockCourseInstanceRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new CourseInstance());
            // Act
            // Assert
            Assert.NotNull(studentService.GrabStudentsFromCourseInstanceId(1));
        }
    }
}
