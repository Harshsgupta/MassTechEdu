﻿// <auto-generated />
using System;
using MassTechEdu.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MassTechEdu.Migrations
{
    [DbContext(typeof(MasstechEduContext))]
    partial class MasstechEduContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MassTechEdu.Models.Assignment", b =>
                {
                    b.Property<int>("Assignmentid")
                        .HasColumnType("int")
                        .HasColumnName("assignmentid");

                    b.Property<string>("AssignmentDescription")
                        .HasColumnType("text")
                        .HasColumnName("assignment_description");

                    b.Property<string>("AssignmentName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("assignment_name");

                    b.Property<int?>("Courseid")
                        .HasColumnType("int")
                        .HasColumnName("courseid");

                    b.Property<DateTime?>("DateSubmitted")
                        .HasColumnType("datetime")
                        .HasColumnName("date_submitted");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime")
                        .HasColumnName("due_date");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_name");

                    b.Property<string>("FilePath")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_path");

                    b.Property<int?>("FileSize")
                        .HasColumnType("int")
                        .HasColumnName("file_size");

                    b.Property<int?>("Subcourseid")
                        .HasColumnType("int")
                        .HasColumnName("subcourseid");

                    b.Property<int?>("Userid")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("Assignmentid")
                        .HasName("PK__assignme__52C3145845A8D271");

                    b.HasIndex("Courseid");

                    b.HasIndex("Subcourseid");

                    b.HasIndex("Userid");

                    b.ToTable("assignment", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("CouurseId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SubCourseId")
                        .HasColumnType("int");

                    b.Property<string>("SubCourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suser")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("suser");

                    b.HasKey("CartId");

                    b.HasIndex(new[] { "SubCourseId" }, "IX_Cart_SubCourseId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.PaymentDetail", b =>
                {
                    b.Property<int>("PaymentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentDetailId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCourseId")
                        .HasColumnType("int");

                    b.Property<string>("SubCourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentDetailId");

                    b.HasIndex(new[] { "CourseId" }, "IX_PaymentDetails_CourseId");

                    b.HasIndex(new[] { "SubCourseId" }, "IX_PaymentDetails_SubCourseId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("MassTechEdu.Models.Quiz", b =>
                {
                    b.Property<int>("Quizid")
                        .HasColumnType("int")
                        .HasColumnName("quizid");

                    b.Property<int?>("Courseid")
                        .HasColumnType("int")
                        .HasColumnName("courseid");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("date_created");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime")
                        .HasColumnName("date_modified");

                    b.Property<string>("QuizDescription")
                        .HasColumnType("text")
                        .HasColumnName("quiz_description");

                    b.Property<int?>("QuizDuration")
                        .HasColumnType("int")
                        .HasColumnName("quiz_duration");

                    b.Property<string>("QuizName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("quiz_name");

                    b.Property<int?>("Subcourseid")
                        .HasColumnType("int")
                        .HasColumnName("subcourseid");

                    b.Property<int?>("Userid")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("Quizid")
                        .HasName("PK__quiz__CFF44815F9F2A784");

                    b.HasIndex("Courseid");

                    b.HasIndex("Subcourseid");

                    b.HasIndex("Userid");

                    b.ToTable("quiz", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.Review", b =>
                {
                    b.Property<int>("Reviewid")
                        .HasColumnType("int")
                        .HasColumnName("reviewid");

                    b.Property<int>("Courseid")
                        .HasColumnType("int")
                        .HasColumnName("courseid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("date_created");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<string>("ReviewText")
                        .HasColumnType("text")
                        .HasColumnName("review_text");

                    b.Property<int>("Userid")
                        .HasColumnType("int")
                        .HasColumnName("userid");

                    b.HasKey("Reviewid")
                        .HasName("PK__review__2ECE522C301F9A1F");

                    b.HasIndex("Courseid");

                    b.HasIndex("Userid");

                    b.ToTable("review", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.SubCourse", b =>
                {
                    b.Property<int>("SubCourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCourseId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("SubCourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCourseId");

                    b.HasIndex(new[] { "CourseId" }, "IX_SubCourse_CourseId");

                    b.ToTable("SubCourse", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MassTechEdu.Models.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCourseId")
                        .HasColumnType("int");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.HasIndex(new[] { "CourseId" }, "IX_Video_CourseId");

                    b.HasIndex(new[] { "SubCourseId" }, "IX_Video_SubCourseId");

                    b.ToTable("Video", (string)null);
                });

            modelBuilder.Entity("MassTechEdu.Models.Assignment", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("Courseid")
                        .HasConstraintName("FK__assignmen__cours__656C112C");

                    b.HasOne("MassTechEdu.Models.SubCourse", "Subcourse")
                        .WithMany("Assignments")
                        .HasForeignKey("Subcourseid")
                        .HasConstraintName("FK__assignmen__subco__66603565");

                    b.HasOne("MassTechEdu.Models.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK__assignmen__useri__6754599E");

                    b.Navigation("Course");

                    b.Navigation("Subcourse");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MassTechEdu.Models.Cart", b =>
                {
                    b.HasOne("MassTechEdu.Models.SubCourse", "SubCourse")
                        .WithMany("Carts")
                        .HasForeignKey("SubCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubCourse");
                });

            modelBuilder.Entity("MassTechEdu.Models.PaymentDetail", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("CourseId")
                        .IsRequired();

                    b.HasOne("MassTechEdu.Models.SubCourse", "SubCourse")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("SubCourseId")
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("SubCourse");
                });

            modelBuilder.Entity("MassTechEdu.Models.Quiz", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("Quizzes")
                        .HasForeignKey("Courseid")
                        .HasConstraintName("FK__quiz__courseid__60A75C0F");

                    b.HasOne("MassTechEdu.Models.SubCourse", "Subcourse")
                        .WithMany("Quizzes")
                        .HasForeignKey("Subcourseid")
                        .HasConstraintName("FK__quiz__subcoursei__619B8048");

                    b.HasOne("MassTechEdu.Models.User", "User")
                        .WithMany("Quizzes")
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK__quiz__userid__628FA481");

                    b.Navigation("Course");

                    b.Navigation("Subcourse");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MassTechEdu.Models.Review", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("Reviews")
                        .HasForeignKey("Courseid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__review__courseid__5CD6CB2B");

                    b.HasOne("MassTechEdu.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__review__userid__5DCAEF64");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MassTechEdu.Models.SubCourse", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("SubCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("MassTechEdu.Models.Video", b =>
                {
                    b.HasOne("MassTechEdu.Models.Course", "Course")
                        .WithMany("Videos")
                        .HasForeignKey("CourseId")
                        .IsRequired();

                    b.HasOne("MassTechEdu.Models.SubCourse", "SubCourse")
                        .WithMany("Videos")
                        .HasForeignKey("SubCourseId")
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("SubCourse");
                });

            modelBuilder.Entity("MassTechEdu.Models.Course", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("PaymentDetails");

                    b.Navigation("Quizzes");

                    b.Navigation("Reviews");

                    b.Navigation("SubCourses");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("MassTechEdu.Models.SubCourse", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Carts");

                    b.Navigation("PaymentDetails");

                    b.Navigation("Quizzes");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("MassTechEdu.Models.User", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Quizzes");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
