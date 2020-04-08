using Microsoft.EntityFrameworkCore;

namespace BirthdayGreetings.DataAccess
{
    public class BirthdayGreetingContext : DbContext
    {
        public BirthdayGreetingContext()
        {
        }

        public BirthdayGreetingContext(DbContextOptions<BirthdayGreetingContext> options)
           : base(options)
        {
        }

        // This is necessary when doing a database update on EF Core for a console app
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BirthdayGreetings;Integrated Security=True;");
        }

        public virtual DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd()
            );
        }
    }
}
