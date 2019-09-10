using System;
using FluentValidation;
using FluentValidation.Results;

namespace MyProject.Core.People.Entities
{
    public class Person : AbstractValidator<Person>
    {
        public Person(string name, string email, DateTime dateOfBirth)
        {
            Name = name;
            Email = email;
            DateOfBirth = dateOfBirth;
            ValidationResult = new ValidationResult();
        }

        protected Person() { }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            ValidateName();
            ValidateEmail();
            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        private void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name can't be empty.")
                .Length(2, 150).WithMessage("Name length must have between 2 and 150.");
        }

        private void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage("E-mail is not in valid format.")
                .NotEmpty().WithMessage("E-mail can't be empty.")
                .MaximumLength(256).WithMessage("E-mail length must be maximum 256.");
        }

        private void ValidateDateOfBirth()
        {
            RuleFor(c => c.DateOfBirth)
                .GreaterThanOrEqualTo(DateTime.Today.AddYears(-18)).WithMessage("Person must be at least 18.");
        }
    }
}