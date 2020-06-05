using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication6
{
    public class Context : DbContext
    {
        public Context([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new StudentCourseConfig());
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }

    }

    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasData(new List<Student>
            {
                new Student
                {
                     Email ="email1@test.com",
                      FirstName ="A",
                       LastName = "A",
                        Id = 1
                },
                new Student
                {
                    Email ="email2@test.com",
                      FirstName ="B",
                       LastName = "B",
                        Id = 2
                }
            });
        }
    }

    public class StudentCourse
    {
        public virtual int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public virtual int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }

    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("StudentCourse");

            builder.HasOne(a => a.Student).WithMany(a => a.StudentsCourses).HasForeignKey(a => a.StudentId);
            builder.HasOne(a => a.Course).WithMany(a => a.StudentsCourses).HasForeignKey(a => a.CourseId);

            builder
                .HasKey(a => new
                {
                    a.StudentId,
                    a.CourseId
                });


        }
    }

    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }
    }

    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");

            builder.HasData(new List<Course>
            {
                new Course
                {
                    Id = 1,
                     CourseName = "C#"
                },
                 new Course
                 {
                      Id = 2,
                       CourseName = "C++"
                 },
                  new Course
                  {
                      Id = 3,
                       CourseName = "F#"
                  }
            });
        }
    }
}
