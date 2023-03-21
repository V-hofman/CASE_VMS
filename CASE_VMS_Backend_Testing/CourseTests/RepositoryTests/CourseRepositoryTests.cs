using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing.CourseTests.RepositoryTests
{
    public class CourseRepositoryTests
    {

        private List<CourseModel> TestCourses = new List<CourseModel>()
        {
            new CourseModel(3, "Title", "TEST"),
            new CourseModel(4, "Title2", "TESTA"),
            new CourseModel(5, "Title3", "TESTB"),
            new CourseModel(4, "Title4", "TESTC"),
        };


        
        [Fact]
        [Trait("CourseRepo", "Get")]
        public void Get_CourseRepository_SuccesCall()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(TestCourses));

            var expected = TestCourses;
            
            //Act
            var result = mockCourseRepository.Object.GetAllAsync();

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Fact]
        [Trait("CourseRepo", "Post")]
        public void Add_CourseRepository_SuccesCall()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseRepository>();
            var expected = new CourseModel(5, "Title5", "TESTD");
            mockCourseRepository.Setup(x => x.AddAsync(expected)).Returns(Task.FromResult(expected));

            
            //Act
            var result = mockCourseRepository.Object.AddAsync(expected);

            //Assert
            Assert.Equal(expected, result.Result);
        }

        [Fact]
        [Trait("CourseRepo", "Post")]
        public void Add_CourseRepository_FailCall_DuplicateException()
        {
            //Arrange
            var mockCourseRepository = new Mock<ICourseRepository>();
            var duplicate = new CourseModel(4, "Title4", "TESTC");
            mockCourseRepository.Setup(course => course.AddAsync(It.IsAny<CourseModel>())).Throws(new DuplicateEntryException("Test Error"));

            //Act
            Action act = () => mockCourseRepository.Object.AddAsync(duplicate);
            DuplicateEntryException actual = Assert.Throws<DuplicateEntryException>(act);
            //Assert
            Assert.Equal("Test Error", actual.Message);
        }
        
        
    }
}

