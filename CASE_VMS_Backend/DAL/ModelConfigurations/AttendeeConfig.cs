using CASE_VMS_Backend.Courses.Models;
using CASE_VMS_Backend.Courses.Models.AttendeeModels;
using CASE_VMS_Backend.DAL.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CASE_VMS_Backend.DAL.ModelConfigurations
{
    public class AttendeeConfig : IEntityTypeConfiguration<AttendeeModel>
    {

        public void Configure(EntityTypeBuilder<AttendeeModel> builder)
        {
            builder
                .ToTable("Attendees");

            builder
                .Property(a => a.Name)
                .IsRequired();

            builder
                .Property(a => a.Surname)
                .IsRequired();

            builder.HasMany<CourseInstance>(a => a.Courses)
                .WithMany(c => c.Attendees);

        }
    }
}
