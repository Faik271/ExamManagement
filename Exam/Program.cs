using Exam.Data;
using Exam.Service.Classes;
using Exam.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<ExamDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers and services
builder.Services.AddControllers();
builder.Services.AddScoped<ExamService>();

// Add FluentValidation for automatic validation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<StudentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LessonValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ExaminationValidator>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Register middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add custom exception middleware
app.UseMiddleware<ExamManagementApp.Middlewares.ExceptionMiddleware>();

// Configure HTTP pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
