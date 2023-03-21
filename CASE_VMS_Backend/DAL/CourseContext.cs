using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.DAL.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CASE_VMS_Backend.DAL
{
    public class CourseContext : DbContext
    {
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<CourseInstance> CourseInstances { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<CourseModel>(new CourseConfig());
            modelBuilder.ApplyConfiguration<CourseInstance>(new CourseInstanceConfig());
        }
    }
}
