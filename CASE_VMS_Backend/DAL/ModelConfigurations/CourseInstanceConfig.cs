using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.DAL.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace CASE_VMS_Backend.DAL.ModelConfigurations
{
    public class CourseInstanceConfig : IEntityTypeConfiguration<CourseInstance>
    {
        public void Configure(EntityTypeBuilder<CourseInstance> builder)
        {
            builder
                .ToTable("CourseInstances");

            builder
                .Property(p => p.StartTime)
                .IsRequired()
                .HasConversion<DateTime_DateOnly>();
        }
    }
}
