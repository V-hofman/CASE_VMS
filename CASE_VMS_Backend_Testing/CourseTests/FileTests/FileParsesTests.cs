using CASE_VMS_Backend.Courses.Exceptions;
using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing.CourseTests.FileTests
{
    public class FileParsesTests
    {

        [Fact]
        [Trait("FileParser", ".CorrectFile")]
        public void ParseFile_FileToCourseParser_ShouldParseTxtFile()
        {

            //Setup Mock file to send to parser
            var fileMock = new Mock<IFormFile>();
            var courseRepoMock = new Mock<ICourseRepository>();
            var courseInstanceRepoMock = new Mock<ICourseInstanceRepository>();
            
            var content = "Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 08/10/2018";
            var fileName = "Foo.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var expected = new List<int>() { 0, 0 };
            
            
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;


            courseRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseModel>()))
                .Returns(Task.FromResult(new CourseModel(3, "Title", "TEST")));
            
            courseInstanceRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseInstance>()))
                .Returns(Task.FromResult(new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(3, "Title", "TEST"))));

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            var parser = new FileToCourseParser(courseRepoMock.Object, courseInstanceRepoMock.Object);

            var result = parser.ParseFile(fileMock.Object);

            Assert.Equal(expected, result);


        }

        [Fact]
        [Trait("FileParser", ".CorrectFile")]
        public void ParseFile_FileToCourseParser_ShouldNotParsePdfFile()
        {

            //Setup Mock file to send to parser
            var fileMock = new Mock<IFormFile>();
            var courseRepoMock = new Mock<ICourseRepository>();
            var courseInstanceRepoMock = new Mock<ICourseInstanceRepository>();

            var content = "Titel: C# Programmeren\nCursuscode: CNETIN\nDuur: 5 dagen\nStartdatum: 08/10/2018";
            var fileName = "Foo.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var expected = new List<int>() { 0, 0 };


            writer.Write(content);
            writer.Flush();
            stream.Position = 0;


            courseRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseModel>()))
                .Returns(Task.FromResult(new CourseModel(3, "Title", "TEST")));

            courseInstanceRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseInstance>()))
                .Returns(Task.FromResult(new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(3, "Title", "TEST"))));

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            var parser = new FileToCourseParser(courseRepoMock.Object, courseInstanceRepoMock.Object);

            Action act = () => parser.ParseFile(fileMock.Object);
            Exception actual = Assert.Throws<Exception>(act);

            //Assert
            Assert.Equal("File is not a .txt file", actual.Message);


        }

        [Fact]
        [Trait("FileParser", ".CorrectFile")]
        public void ParseFile_FileToCourseParser_ShouldNotParseEmptyTxtFile()
        {

            //Setup Mock file to send to parser
            var fileMock = new Mock<IFormFile>();
            var courseRepoMock = new Mock<ICourseRepository>();
            var courseInstanceRepoMock = new Mock<ICourseInstanceRepository>();

            var content = "";
            var fileName = "Foo.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var expected = new List<int>() { 0, 0 };


            writer.Write(content);
            writer.Flush();
            stream.Position = 0;


            courseRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseModel>()))
                .Returns(Task.FromResult(new CourseModel(3, "Title", "TEST")));

            courseInstanceRepoMock.Setup(x => x.AddAsync(It.IsAny<CourseInstance>()))
                .Returns(Task.FromResult(new CourseInstance(DateOnly.FromDateTime(DateTime.Now), new CourseModel(3, "Title", "TEST"))));

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            var parser = new FileToCourseParser(courseRepoMock.Object, courseInstanceRepoMock.Object);

            Action act = () => parser.ParseFile(fileMock.Object);
            Exception actual = Assert.Throws<Exception>(act);
            
            //Assert
            Assert.Equal("File is empty", actual.Message);


        }
    }
}
