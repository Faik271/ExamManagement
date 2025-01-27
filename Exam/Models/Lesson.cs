using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Lesson
{
    public string LessonCode { get; set; } 
    public string LessonName { get; set; }
    public int GradeLevel { get; set; }
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }

    [JsonIgnore] 
    public ICollection<Examination> Examinations { get; set; } = new List<Examination>();
}