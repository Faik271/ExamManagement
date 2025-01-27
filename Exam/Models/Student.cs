using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Student
{
    public int StudentNumber { get; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int GradeLevel { get; set; }

    [JsonIgnore] 
    public ICollection<Examination> Examinations { get; set; } = new List<Examination>();
}