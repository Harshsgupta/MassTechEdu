using System;
using System.Collections.Generic;
using MassTechEdu.Models;
using Microsoft.EntityFrameworkCore;

namespace MassTechEdu.Data;

public partial class MasstechEduContext : DbContext
{
    public MasstechEduContext()
    {
    }

    public MasstechEduContext(DbContextOptions<MasstechEduContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SubCourse> SubCourses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MasstechEdu;Integrated Security=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignmentid).HasName("PK__assignme__52C3145845A8D271");

            entity.ToTable("assignment");

            entity.Property(e => e.Assignmentid)
                .ValueGeneratedNever()
                .HasColumnName("assignmentid");
            entity.Property(e => e.AssignmentDescription)
                .HasColumnType("text")
                .HasColumnName("assignment_description");
            entity.Property(e => e.AssignmentName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("assignment_name");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.DateSubmitted)
                .HasColumnType("datetime")
                .HasColumnName("date_submitted");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.Subcourseid).HasColumnName("subcourseid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Courseid)
                .HasConstraintName("FK__assignmen__cours__656C112C");

            entity.HasOne(d => d.Subcourse).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Subcourseid)
                .HasConstraintName("FK__assignmen__subco__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__assignmen__useri__6754599E");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.HasIndex(e => e.SubCourseId, "IX_Cart_SubCourseId");

            entity.Property(e => e.Suser).HasColumnName("suser");

            entity.HasOne(d => d.SubCourse).WithMany(p => p.Carts).HasForeignKey(d => d.SubCourseId);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_PaymentDetails_CourseId");

            entity.HasIndex(e => e.SubCourseId, "IX_PaymentDetails_SubCourseId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SubCourse).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.SubCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Quizid).HasName("PK__quiz__CFF44815F9F2A784");

            entity.ToTable("quiz");

            entity.Property(e => e.Quizid)
                .ValueGeneratedNever()
                .HasColumnName("quizid");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("date_modified");
            entity.Property(e => e.QuizDescription)
                .HasColumnType("text")
                .HasColumnName("quiz_description");
            entity.Property(e => e.QuizDuration).HasColumnName("quiz_duration");
            entity.Property(e => e.QuizName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("quiz_name");
            entity.Property(e => e.Subcourseid).HasColumnName("subcourseid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Course).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.Courseid)
                .HasConstraintName("FK__quiz__courseid__60A75C0F");

            entity.HasOne(d => d.Subcourse).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.Subcourseid)
                .HasConstraintName("FK__quiz__subcoursei__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__quiz__userid__628FA481");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Reviewid).HasName("PK__review__2ECE522C301F9A1F");

            entity.ToTable("review");

            entity.Property(e => e.Reviewid)
                .ValueGeneratedNever()
                .HasColumnName("reviewid");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewText)
                .HasColumnType("text")
                .HasColumnName("review_text");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Course).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Courseid)
                .HasConstraintName("FK__review__courseid__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__review__userid__5DCAEF64");
        });

        modelBuilder.Entity<SubCourse>(entity =>
        {
            entity.ToTable("SubCourse");

            entity.HasIndex(e => e.CourseId, "IX_SubCourse_CourseId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.SubCourses).HasForeignKey(d => d.CourseId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.ToTable("Video");

            entity.HasIndex(e => e.CourseId, "IX_Video_CourseId");

            entity.HasIndex(e => e.SubCourseId, "IX_Video_SubCourseId");

            entity.HasOne(d => d.Course).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SubCourse).WithMany(p => p.Videos)
                .HasForeignKey(d => d.SubCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
