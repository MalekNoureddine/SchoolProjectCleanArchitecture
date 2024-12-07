using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Departments_DID",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Student_StudID",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Student_DID",
                table: "Students",
                newName: "IX_Students_DID");

            migrationBuilder.AddColumn<int>(
                name: "InsId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudID");

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENameAr = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: false),
                    EName = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dept_Instructor",
                columns: table => new
                {
                    DepartmentDID = table.Column<int>(type: "int", nullable: false),
                    InstructorInsId = table.Column<int>(type: "int", nullable: false),
                    InsId = table.Column<int>(type: "int", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dept_Instructor", x => new { x.DepartmentDID, x.InstructorInsId });
                    table.ForeignKey(
                        name: "FK_Dept_Instructor_Departments_DepartmentDID",
                        column: x => x.DepartmentDID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dept_Instructor_Instructors_InstructorInsId",
                        column: x => x.InstructorInsId,
                        principalTable: "Instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subj_Instructor",
                columns: table => new
                {
                    InstructorInsId = table.Column<int>(type: "int", nullable: false),
                    SubjectSubID = table.Column<int>(type: "int", nullable: false),
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subj_Instructor", x => new { x.InstructorInsId, x.SubjectSubID });
                    table.ForeignKey(
                        name: "FK_Subj_Instructor_Instructors_InstructorInsId",
                        column: x => x.InstructorInsId,
                        principalTable: "Instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subj_Instructor_Subjects_SubjectSubID",
                        column: x => x.SubjectSubID,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsId",
                table: "Departments",
                column: "InsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dept_Instructor_InstructorInsId",
                table: "Dept_Instructor",
                column: "InstructorInsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Subj_Instructor_SubjectSubID",
                table: "Subj_Instructor",
                column: "SubjectSubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsId",
                table: "Departments",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_StudID",
                table: "StudentSubject",
                column: "StudID",
                principalTable: "Students",
                principalColumn: "StudID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_StudID",
                table: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Dept_Instructor");

            migrationBuilder.DropTable(
                name: "Subj_Instructor");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InsId",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DID",
                table: "Student",
                newName: "IX_Student_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "StudID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Departments_DID",
                table: "Student",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Student_StudID",
                table: "StudentSubject",
                column: "StudID",
                principalTable: "Student",
                principalColumn: "StudID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
