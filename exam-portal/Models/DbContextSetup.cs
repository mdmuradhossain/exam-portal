using Microsoft.EntityFrameworkCore;

namespace exam_portal.Models
{
    public class DbContextSetup : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Question>  Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Replace "your_connection_string_here" with your actual SQL Server connection string
                string connectionString = "Server=localhost;Database=exam_portal;Encrypt=False;Trusted_Connection=True;TrustServerCertificate=True;";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Exam)
                .WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false); // Temporarily disable foreign key constraint creation

            modelBuilder.Entity<Exam>()
            .HasMany(a => a.Questions) // Author has many Books, specifies the 'many' side of the relationship
            .WithOne(b => b.Exam) // Book is associated with one Author, specifies the 'one' side of the relationship
            .HasForeignKey(b => b.ExamId);
        }

    }
}
