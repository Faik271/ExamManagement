using Exam.Models;
using FluentValidation;

namespace Exam.Validators
{
    public class LessonValidator : AbstractValidator<Lesson>
    {
        public LessonValidator()
        {
            RuleFor(lesson => lesson.LessonCode)
                .NotEmpty()
                .WithMessage("Lesson code is required.")
                .MaximumLength(3)
                .WithMessage("Lesson code must not exceed 3 characters.");


            RuleFor(lesson => lesson.LessonName)
                .NotEmpty()
                .WithMessage("Lesson name is required.")
                .MaximumLength(30)
                .WithMessage("Lesson name must not exceed 30 characters.");

            RuleFor(lesson => lesson.GradeLevel)
                .InclusiveBetween(1, 12)
                .WithMessage("Grade level must be between 1 and 12.");

            RuleFor(lesson => lesson.TeacherFirstName)
                .NotEmpty()
                .WithMessage("Teacher first name is required.")
                .MaximumLength(20)
                .WithMessage("Teacher first name must not exceed 20 characters.");

            RuleFor(lesson => lesson.TeacherLastName)
                .NotEmpty()
                .WithMessage("Teacher last name is required.")
                .MaximumLength(20)
                .WithMessage("Teacher last name must not exceed 20 characters.");
        }
    }
}
