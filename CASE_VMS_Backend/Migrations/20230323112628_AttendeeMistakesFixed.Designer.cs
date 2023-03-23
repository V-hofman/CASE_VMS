﻿// <auto-generated />
using System;
using CASE_VMS_Backend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CASE_VMS_Backend.Migrations
{
    [DbContext(typeof(CourseContext))]
    [Migration("20230323112628_AttendeeMistakesFixed")]
    partial class AttendeeMistakesFixed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttendeeModelCourseInstance", b =>
                {
                    b.Property<int>("AttendeesId")
                        .HasColumnType("int");

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.HasKey("AttendeesId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("AttendeeModelCourseInstance");
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.AttendeeModels.AttendeeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CorporateInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrivateInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CorporateInfoId");

                    b.HasIndex("PrivateInfoId");

                    b.ToTable("Attendees", (string)null);
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.AttendeeModels.CorporateInfoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CorporateInfoModel");
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.AttendeeModels.PrivateInfoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrivateInfoModel");
                });

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

            modelBuilder.Entity("AttendeeModelCourseInstance", b =>
                {
                    b.HasOne("CASE_VMS_Backend.Courses.Models.AttendeeModels.AttendeeModel", null)
                        .WithMany()
                        .HasForeignKey("AttendeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CASE_VMS_Backend.Courses.Models.CourseInstance", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CASE_VMS_Backend.Courses.Models.AttendeeModels.AttendeeModel", b =>
                {
                    b.HasOne("CASE_VMS_Backend.Courses.Models.AttendeeModels.CorporateInfoModel", "CorporateInfo")
                        .WithMany()
                        .HasForeignKey("CorporateInfoId");

                    b.HasOne("CASE_VMS_Backend.Courses.Models.AttendeeModels.PrivateInfoModel", "PrivateInfo")
                        .WithMany()
                        .HasForeignKey("PrivateInfoId");

                    b.Navigation("CorporateInfo");

                    b.Navigation("PrivateInfo");
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
