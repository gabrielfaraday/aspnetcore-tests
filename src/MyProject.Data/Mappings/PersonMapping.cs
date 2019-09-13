using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Core.People.Entities;

namespace MyProject.Data.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
               .HasColumnType("varchar(30)")
               .IsRequired();

            builder.Property(c => c.Email)
               .HasColumnType("varchar(40)")
               .IsRequired();

            builder.Property(c => c.DateOfBirth)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Person");
        }
    }
}