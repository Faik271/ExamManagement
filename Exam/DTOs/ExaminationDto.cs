namespace Exam.DTOs
{
    public class ExaminationDto
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public string LessonName { get; set; } // From related `Lesson`
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; } // From related `Student`
        public string StudentLastName { get; set; } // From related `Student`
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }

}
