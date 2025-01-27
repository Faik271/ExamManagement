using Exam.Models;
using FluentValidation;

namespace Exam.Validators
{
    public class ExaminationValidator : AbstractValidator<Examination>
    {
        public ExaminationValidator()
        {
            RuleFor(exam => exam.LessonCode)
                .NotEmpty()
                .WithMessage("Lesson code is required.")
                .MaximumLength(3)
                .WithMessage("Lesson code must not exceed 3 characters.");

            RuleFor(exam => exam.StudentId)
                .GreaterThan(0)
                .WithMessage("Student ID must be greater than 0.");

            RuleFor(exam => exam.ExamDate)
                .NotEmpty()
                .WithMessage("Exam date is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Exam date cannot be in the future.");

            RuleFor(exam => exam.Score)
                .InclusiveBetween(0, 100)
                .WithMessage("Score must be between 0 and 100.");
        }
    }
}
