using CASE_VMS_Backend.Courses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing.CourseTests.EndpointTests
{
    public class CourseEndpointTests
    {
        private readonly HttpClient _client;
        public CourseEndpointTests(HttpClient client)
        {
            _client = client;
        }

        [Fact]
        public async Task GetAllCourseInstances()
        {
            var response = await _client.GetAsync("/api/courses");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CourseResponseDTO>>(stringResponse);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(2, result.Count);
        }

    }
}
