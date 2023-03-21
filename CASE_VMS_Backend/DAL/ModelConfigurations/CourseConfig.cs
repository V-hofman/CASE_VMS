using CASE_VMS_Backend.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CASE_VMS_Backend.DAL.ModelConfigurations
{
    public class CourseConfig : IEntityTypeConfiguration<CourseModel>
    {
        
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder
                .ToTable("Course")
                .HasMany(c => c.CourseInstances)
                .WithOne(ci => ci.Course);

            builder.Property(c => c.CourseTitle)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("Title");

            builder.Property(c => c.CourseCode)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(c => c.CourseModelID)
                .IsRequired()
                .HasColumnName("CourseID");
        }
    }
}
