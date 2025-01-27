using Exam.DTOs;
using Exam.Models;

namespace Exam.Service.Interfaces
{
    public interface IExamService
    {
        // Lesson CRUD
        Task<List<LessonDto>> GetAllLessonsAsync();
        Task<LessonDto?> GetLessonByCodeAsync(string code);
        Task<Lesson> AddLessonAsync(Lesson lesson);
        Task UpdateLessonAsync(string code, Lesson updatedLesson);
        Task<bool> DeleteLessonAsync(string code);

        // Student CRUD
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(int id, Student updatedStudent);
        Task<bool> DeleteStudentAsync(int id);

        // Examination CRUD
        Task<List<ExaminationDto>> GetAllExamsAsync();
        Task<ExaminationDto?> GetExamByIdAsync(int id);
        Task<Examination> AddExamAsync(Examination exam);
        Task UpdateExamAsync(int id, Examination updatedExam);
        Task DeleteExamAsync(int id);
    }
}
