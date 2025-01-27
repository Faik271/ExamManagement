using Exam.Models;
using Exam.Service.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamController : ControllerBase
{
    private readonly ExamService _examService;

    public ExamController(ExamService examService)
    {
        _examService = examService;
    }


    // CRUD for Lessons

    [HttpGet("lessons")]
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
    {
        var lessons = await _examService.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpGet("lessons/{code}")]
    public async Task<ActionResult<Lesson>> GetLessonByCode(string code)
    {
        var lesson = await _examService.GetLessonByCodeAsync(code);
        return Ok(lesson);
    }

    [HttpPost("lessons")]
    public async Task<ActionResult<Lesson>> AddLesson(Lesson lesson)
    {
        var createdLesson = await _examService.AddLessonAsync(lesson);
        return CreatedAtAction(nameof(GetLessonByCode), new { code = createdLesson.LessonCode }, createdLesson);
    }

    [HttpPut("lessons/{code}")]
    public async Task<IActionResult> UpdateLesson(string code, Lesson lesson)
    {
        await _examService.UpdateLessonAsync(code, lesson);
        return NoContent();
    }

    [HttpDelete("lessons/{code}")]
    public async Task<IActionResult> DeleteLesson(string code)
    {
        await _examService.DeleteLessonAsync(code);
        return NoContent();
    }

    // CRUD for Students

    [HttpGet("students")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _examService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("students/{id}")]
    public async Task<ActionResult<Student>> GetStudentById(int id)
    {
        var student = await _examService.GetStudentByIdAsync(id);
        return Ok(student);
    }

    [HttpPost("students")]
    public async Task<ActionResult<Student>> AddStudent(Student student)
    {
        var createdStudent = await _examService.AddStudentAsync(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.StudentNumber }, createdStudent);
    }

    [HttpPut("students/{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        await _examService.UpdateStudentAsync(id, student);
        return NoContent();
    }

    [HttpDelete("students/{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _examService.DeleteStudentAsync(id);
        return NoContent();
    }


    // CRUD for Examination
    [HttpGet("examinations")]
    public async Task<ActionResult<IEnumerable<Examination>>> GetExaminations()
    {
        var exams = await _examService.GetAllExamsAsync();
        return Ok(exams);
    }

    [HttpGet("examinations/{id}")]
    public async Task<ActionResult<Examination>> GetExaminationById(int id)
    {
        var exam = await _examService.GetExamByIdAsync(id);
        return Ok(exam);
    }

    [HttpPost("examinations")]
    public async Task<ActionResult<Examination>> AddExamination(Examination exam)
    {
        var createdExam = await _examService.AddExamAsync(exam);
        return CreatedAtAction(nameof(GetExaminationById), new { id = createdExam.Id }, createdExam);
    }

    [HttpPut("examinations/{id}")]
    public async Task<IActionResult> UpdateExamination(int id, Examination exam)
    {
        await _examService.UpdateExamAsync(id, exam);
        return NoContent();
    }

    [HttpDelete("examinations/{id}")]
    public async Task<IActionResult> DeleteExamination(int id)
    {
        await _examService.DeleteExamAsync(id);
        return NoContent();
    }
}
