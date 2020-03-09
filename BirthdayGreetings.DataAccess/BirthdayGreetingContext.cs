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
