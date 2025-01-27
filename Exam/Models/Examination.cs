using Exam.Models;
using System.Text.Json.Serialization;

public class Examination
{
    public int Id { get; }
    public string LessonCode { get; set; } 
    public int StudentId { get; set; }    
    public DateTime ExamDate { get; set; }
    public int Score { get; set; }

    [JsonIgnore]
    public Lesson? Lesson { get; set; }

    [JsonIgnore] 
    public Student? Student { get; set; }
}