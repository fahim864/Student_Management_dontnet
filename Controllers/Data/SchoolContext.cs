using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Controllers.Models;

namespace StudentManagementAPI.Controllers.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Section>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Sections)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Section)
                .WithMany()
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.Restrict); 


            base.OnModelCreating(modelBuilder);
        }

    }
}
