﻿using CASE_VMS_Backend.Courses.Models;

namespace CASE_VMS_Backend.Courses.Repository
{
    public interface ICourseRepository
    {
        Task<List<CourseModel>> GetAllAsync();
        Task<CourseModel> AddAsync(CourseModel newCourse);
    }
}