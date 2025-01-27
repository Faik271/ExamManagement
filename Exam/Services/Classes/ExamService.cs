using System.ComponentModel.DataAnnotations;
using Exam.Data;
using Exam.DTOs;
using Exam.Models;
using Exam.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Service.Classes
{
    public class ExamService:IExamService
    {
        private readonly ExamDbContext _context;

        public ExamService(ExamDbContext context)
        {
            _context = context;
        }

        // CRUD for Lesson Table

        public async Task<List<LessonDto>> GetAllLessonsAsync()
        {
            return await _context.Lessons
                .AsNoTracking() 
                .Select(lesson => MapToLessonDto(lesson)) 
                .ToListAsync();
        }

        
        public async Task<LessonDto?> GetLessonByCodeAsync(string code)
        {
            var lesson = await _context.Lessons.AsNoTracking().FirstOrDefaultAsync(l => l.LessonCode == code);
            return lesson == null ? null : MapToLessonDto(lesson); 
        }

        
        private static LessonDto MapToLessonDto(Lesson lesson)
        {
            return new LessonDto
            {
                LessonCode = lesson.LessonCode,
                LessonName = lesson.LessonName,
                GradeLevel = lesson.GradeLevel,
                TeacherFirstName = lesson.TeacherFirstName,
                TeacherLastName = lesson.TeacherLastName
            };
        }

        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            // Check if a lesson with the same LessonCode already exists
            var existingLesson = await _context.Lessons.FindAsync(lesson.LessonCode);
            if (existingLesson != null)
            {
                throw new ValidationException($"A lesson with the code '{lesson.LessonCode}' already exists.");
            }
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task UpdateLessonAsync(String code, Lesson updatedLesson)
        {
            var existingLesson = await _context.Lessons.FindAsync(code);
            if (existingLesson == null)
            {
                throw new ValidationException($"Lesson with code {code} not found.");
            }
            existingLesson.LessonName = updatedLesson.LessonName;
            existingLesson.GradeLevel = updatedLesson.GradeLevel;
            existingLesson.TeacherFirstName = updatedLesson.TeacherFirstName;
            existingLesson.TeacherLastName = updatedLesson.TeacherLastName;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLessonAsync(string code)
        {
            var lesson = await _context.Lessons.FindAsync(code);
            if (lesson == null)
            {
                throw new ValidationException($"Lesson with code {code} not found.");
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }

        // CRUD for Student Table

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            return await _context.Students
                .AsNoTracking() 
                .Select(student => MapToStudentDto(student)) 
                .ToListAsync();
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students
                .AsNoTracking() 
                .FirstOrDefaultAsync(s => s.StudentNumber == id);

            return student == null ? null : MapToStudentDto(student); 
        }

        private static StudentDto MapToStudentDto(Student student)
        {
            return new StudentDto
            {
                StudentNumber = student.StudentNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                GradeLevel = student.GradeLevel
            };
        }


        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task UpdateStudentAsync(int id, Student updatedStudent)
        {
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                throw new ValidationException($"Student with ID {id} not found.");
            }
            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.GradeLevel = updatedStudent.GradeLevel;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        // CRUD for Exam Table
        public async Task<List<ExaminationDto>> GetAllExamsAsync()
        {
            return await _context.Examinations
                .Include(e => e.Lesson)
                .Include(e => e.Student)
                .AsNoTracking() 
                .Select(e => MapToExaminationDto(e))
                .ToListAsync();
        }

        public async Task<ExaminationDto?> GetExamByIdAsync(int id)
        {
            var exam = await _context.Examinations
                .Include(e => e.Lesson)
                .Include(e => e.Student)
                .AsNoTracking() 
                .FirstOrDefaultAsync(e => e.Id == id);

            return exam == null ? null : MapToExaminationDto(exam); 
        }

        private static ExaminationDto MapToExaminationDto(Examination exam)
        {
            return new ExaminationDto
            {
                Id = exam.Id,
                LessonCode = exam.LessonCode,
                LessonName = exam.Lesson.LessonName, 
                StudentId = exam.StudentId,
                StudentFirstName = exam.Student.FirstName, 
                StudentLastName = exam.Student.LastName,
                ExamDate = exam.ExamDate,
                Score = exam.Score
            };
        }


        public async Task<Examination> AddExamAsync(Examination exam)
        {
            // Validate LessonCode
            if (!await _context.Lessons.AnyAsync(l => l.LessonCode == exam.LessonCode))
            {
                throw new ValidationException($"Lesson with code {exam.LessonCode} does not exist.");
            }

            // Validate StudentId
            if (!await _context.Students.AnyAsync(s => s.StudentNumber == exam.StudentId))
            {
                throw new ValidationException($"Student with ID {exam.StudentId} does not exist.");
            }

            _context.Examinations.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task UpdateExamAsync(int id, Examination updatedExam)
        {
            var existingExam = await _context.Examinations.FindAsync(id);
            if (existingExam == null)
            {
                throw new ValidationException($"Exam with ID {updatedExam.Id} not found.");
            }

            existingExam.LessonCode = updatedExam.LessonCode;
            existingExam.StudentId = updatedExam.StudentId;
            existingExam.ExamDate = updatedExam.ExamDate;
            existingExam.Score = updatedExam.Score;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _context.Examinations.FindAsync(id);
            if (exam == null)
            {
                throw new ValidationException($"Exam with ID {id} not found.");
            }

            _context.Examinations.Remove(exam);
            await _context.SaveChangesAsync();
        }
    }
}
