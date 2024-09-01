using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTechEdu.Migrations
{
    /// <inheritdoc />
    public partial class masstech : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "SubCourse",
                columns: table => new
                {
                    SubCourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCourse", x => x.SubCourseId);
                    table.ForeignKey(
                        name: "FK_SubCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    reviewid = table.Column<int>(type: "int", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    review_text = table.Column<string>(type: "text", nullable: true),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__review__2ECE522C301F9A1F", x => x.reviewid);
                    table.ForeignKey(
                        name: "FK__review__courseid__5CD6CB2B",
                        column: x => x.courseid,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__review__userid__5DCAEF64",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    assignmentid = table.Column<int>(type: "int", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: true),
                    subcourseid = table.Column<int>(type: "int", nullable: true),
                    userid = table.Column<int>(type: "int", nullable: true),
                    assignment_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    assignment_description = table.Column<string>(type: "text", nullable: true),
                    due_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    file_path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    file_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    file_size = table.Column<int>(type: "int", nullable: true),
                    date_submitted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__assignme__52C3145845A8D271", x => x.assignmentid);
                    table.ForeignKey(
                        name: "FK__assignmen__cours__656C112C",
                        column: x => x.courseid,
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK__assignmen__subco__66603565",
                        column: x => x.subcourseid,
                        principalTable: "SubCourse",
                        principalColumn: "SubCourseId");
                    table.ForeignKey(
                        name: "FK__assignmen__useri__6754599E",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCourseId = table.Column<int>(type: "int", nullable: false),
                    SubCourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouurseId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    suser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_SubCourse_SubCourseId",
                        column: x => x.SubCourseId,
                        principalTable: "SubCourse",
                        principalColumn: "SubCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCourseId = table.Column<int>(type: "int", nullable: false),
                    SubCourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.PaymentDetailId);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_PaymentDetails_SubCourse_SubCourseId",
                        column: x => x.SubCourseId,
                        principalTable: "SubCourse",
                        principalColumn: "SubCourseId");
                });

            migrationBuilder.CreateTable(
                name: "quiz",
                columns: table => new
                {
                    quizid = table.Column<int>(type: "int", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: true),
                    subcourseid = table.Column<int>(type: "int", nullable: true),
                    userid = table.Column<int>(type: "int", nullable: true),
                    quiz_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    quiz_description = table.Column<string>(type: "text", nullable: true),
                    quiz_duration = table.Column<int>(type: "int", nullable: true),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__quiz__CFF44815F9F2A784", x => x.quizid);
                    table.ForeignKey(
                        name: "FK__quiz__courseid__60A75C0F",
                        column: x => x.courseid,
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK__quiz__subcoursei__619B8048",
                        column: x => x.subcourseid,
                        principalTable: "SubCourse",
                        principalColumn: "SubCourseId");
                    table.ForeignKey(
                        name: "FK__quiz__userid__628FA481",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SubCourseId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Video_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Video_SubCourse_SubCourseId",
                        column: x => x.SubCourseId,
                        principalTable: "SubCourse",
                        principalColumn: "SubCourseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignment_courseid",
                table: "assignment",
                column: "courseid");

            migrationBuilder.CreateIndex(
                name: "IX_assignment_subcourseid",
                table: "assignment",
                column: "subcourseid");

            migrationBuilder.CreateIndex(
                name: "IX_assignment_userid",
                table: "assignment",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SubCourseId",
                table: "Cart",
                column: "SubCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_CourseId",
                table: "PaymentDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_SubCourseId",
                table: "PaymentDetails",
                column: "SubCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_courseid",
                table: "quiz",
                column: "courseid");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_subcourseid",
                table: "quiz",
                column: "subcourseid");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_userid",
                table: "quiz",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_review_courseid",
                table: "review",
                column: "courseid");

            migrationBuilder.CreateIndex(
                name: "IX_review_userid",
                table: "review",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_SubCourse_CourseId",
                table: "SubCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_CourseId",
                table: "Video",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_SubCourseId",
                table: "Video",
                column: "SubCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "quiz");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubCourse");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
