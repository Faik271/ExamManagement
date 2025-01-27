using Exam.Models;
using FluentValidation;

namespace Exam.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {

            RuleFor(student => student.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(30)
                .WithMessage("First name must not exceed 30 characters.");

            RuleFor(student => student.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(30)
                .WithMessage("Last name must not exceed 30 characters.");

            RuleFor(student => student.GradeLevel)
                .InclusiveBetween(1, 12)
                .WithMessage("Grade level must be between 1 and 12.");
        }
    }
}
