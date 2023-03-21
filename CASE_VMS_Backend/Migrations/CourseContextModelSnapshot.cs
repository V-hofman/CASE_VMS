﻿// <auto-generated />
using System;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CASE_VMS_Backend.Migrations
{
    [DbContext(typeof(CourseContext))]
    partial class CourseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.CourseInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseInstances", (string)null);
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.CourseModel", b =>
                {
                    b.Property<int>("CourseModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseModelID"));

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Title");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.HasKey("CourseModelID");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.CourseInstance", b =>
                {
                    b.HasOne("CASE_VMS_Backend.Courses.Models.CourseModel", "Course")
                        .WithMany("CourseInstances")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.CourseModel", b =>
                {
                    b.Navigation("CourseInstances");
                });
#pragma warning restore 612, 618
        }
    }
}
