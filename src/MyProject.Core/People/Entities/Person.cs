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

        public Person(int id, string name, string email, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            Email = email;
            DateOfBirth = dateOfBirth;
            ValidationResult = new ValidationResult();
        }

        protected Person() { }

        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public virtual bool IsValid()
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
                .MaximumLength(30).WithMessage("Name length must be maximum 30.");
        }

        private void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail can't be empty.")
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage("E-mail is not in valid format.")
                .MaximumLength(20).WithMessage("E-mail length must be maximum 20.");
        }
    }
}