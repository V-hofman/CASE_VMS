using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing.CourseTests.RepositoryTests
{
    public class CourseInstanceRepositoryTests
    {

        private List<CourseInstance> TestCourses = new List<CourseInstance>()
        {
            new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(3, "Title", "TEST")),
            new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(4, "Title2", "TESTA")),
            new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(5, "Title3", "TESTB")),
            new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(4, "Title4", "TESTC")),
        };


        [Fact]
        [Trait("CourseInstanceRepo", "Get")]
        public void Get_CourseRepository_SuccesCall()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseInstanceRepository>();
            mockCourseRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(TestCourses));

            var expected = TestCourses;

            //Act
            var result = mockCourseRepository.Object.GetAllAsync();

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Fact]
        [Trait("CourseInstanceRepo", "Post")]
        public void Add_CourseRepository_SuccesCall()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseInstanceRepository>();
            var expected = new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(4, "Title8", "TESTD"));
            mockCourseRepository.Setup(x => x.AddAsync(expected)).Returns(Task.FromResult(expected));


            //Act
            var result = mockCourseRepository.Object.AddAsync(expected);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Fact]
        [Trait("CourseInstanceRepo", "Post")]
        public void Add_CourseRepository_FailCall_DuplicateException()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseInstanceRepository>();
            var duplicate = new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(4, "Title4", "TESTC"));
            mockCourseRepository.Setup(course => course.AddAsync(It.IsAny<CourseInstance>())).Throws(new DuplicateEntryException("Test Error"));

            //Act
            Action act = () => mockCourseRepository.Object.AddAsync(duplicate);
            DuplicateEntryException actual = Assert.Throws<DuplicateEntryException>(act);
            //Assert
            Assert.Equal("Test Error", actual.Message);
        }


    }
}
