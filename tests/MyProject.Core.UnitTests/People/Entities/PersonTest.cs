using System;
using System.Linq;
using MyProject.Core.People.Entities;
using Xunit;

namespace MyProject.Core.UnitTests.People.Entities
{
    public class PersonTest
    {
        const string VALID_NAME = "Nome";
        const string VALID_EMAIL = "email@prov.com";
        const string VALID_DATE_OF_BIRTH = "1980-09-01"; 

        [Fact]
        public void Name_ShouldNotBeEmpty()
        {
            var person = new Person("", VALID_EMAIL, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "Name can't be empty."));
        }

        [Fact]
        public void Name_ShouldNotBeNull()
        {
            var person = new Person(null, VALID_EMAIL, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "Name can't be empty."));
        }

        [Fact]
        public void Name_ShouldNotHaveMoreThan30characters()
        {
            var person = new Person("abcasdfresjshf s abcasdfresjshf s", VALID_EMAIL, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "Name length must be maximum 30."));
        }

        [Fact]
        public void Name_ShouldHaveMaximum30characters()
        {
            var person = new Person("abcasdfresjshf s abcasdfresjsh", VALID_EMAIL, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.True(result);
            Assert.Empty(person.ValidationResult.Errors);
        }

        [Fact]
        public void Email_ShouldNotBeEmpty()
        {
            var person = new Person(VALID_NAME, "", Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "E-mail can't be empty."));
        }

        [Fact]
        public void Email_ShouldNotBeNull()
        {
            var person = new Person(VALID_NAME, null, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "E-mail can't be empty."));
        }

        [Theory]
        [InlineData("abc@def.com")]
        [InlineData("abc@def.com.br")]
        [InlineData("abc.def@ghi.gov.br")]
        [InlineData("abc-def@ghi.org.br")]
        public void Email_ShouldHaveValidFormat(string email)
        {
            var person = new Person(VALID_NAME, email, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.True(result);
            Assert.Empty(person.ValidationResult.Errors);
        }

        [Theory]
        [InlineData("abc@def.")]
        [InlineData("abc@")]
        [InlineData("abc.defghi.gov.br")]
        [InlineData("@ghi.org.br")]
        public void Email_ShouldNotHaveInvalidFormat(string email)
        {
            var person = new Person(VALID_NAME, email, Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "E-mail is not in valid format."));
        }

        [Fact]
        public void Email_ShouldNotHaveMoreThan20characters()
        {
            var person = new Person(VALID_NAME, "abcdefghi@abcdefg.com", Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.False(result);
            Assert.NotNull(person.ValidationResult.Errors);
            Assert.NotEmpty(person.ValidationResult.Errors);
            Assert.NotNull(person.ValidationResult.Errors.FirstOrDefault(p => p.ErrorMessage == "E-mail length must be maximum 20."));
        }

        [Fact]
        public void Email_ShouldHaveMaximum20characters()
        {
            var person = new Person(VALID_NAME, "bcdefghi@abcdefg.com", Convert.ToDateTime(VALID_DATE_OF_BIRTH));

            var result = person.IsValid();

            Assert.True(result);
            Assert.Empty(person.ValidationResult.Errors);
        }
    }
}