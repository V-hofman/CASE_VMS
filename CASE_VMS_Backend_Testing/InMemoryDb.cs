using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASE_VMS_Backend_Testing
{
    public static class InMemoryDb
    {
        public static DbContextOptions<CourseContext> SetInMemoryDb()
        {
            return new DbContextOptionsBuilder<CourseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
            
    }
}
