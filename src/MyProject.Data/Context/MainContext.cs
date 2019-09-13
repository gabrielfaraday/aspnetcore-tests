using Microsoft.EntityFrameworkCore;
using MyProject.Core.People.Entities;
using MyProject.Data.Mappings;

namespace MyProject.Data.Context
{
    public class MainContext : DbContext
    {
        public DbSet<Person> Contatos { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}