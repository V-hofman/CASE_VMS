using CASE_VMS_Backend.Courses.FileHandler;
using CASE_VMS_Backend.Courses.Repository;
using CASE_VMS_Backend.Courses.Repository.Interfaces;
using CASE_VMS_Backend.Courses.Services;
using CASE_VMS_Backend.Courses.Services.Interfaces;
using CASE_VMS_Backend.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularFrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSingleton<CourseContext>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICourseInstanceRepository, CourseInstanceRepository>();
builder.Services.AddTransient<IAttendeeRepository, AttendeeRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ICourseService, CourseService>();


builder.Services.AddTransient<FileToCourseParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AngularFrontEnd");
app.MapControllers();

app.Run();