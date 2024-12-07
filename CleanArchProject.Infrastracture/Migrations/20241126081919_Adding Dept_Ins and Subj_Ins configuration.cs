using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingDept_InsandSubj_Insconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dept_Instructor_Departments_DepartmentDID",
                table: "Dept_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Dept_Instructor_Instructors_InstructorInsId",
                table: "Dept_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Instructors_InstructorInsId",
                table: "Subj_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Subjects_SubjectSubID",
                table: "Subj_Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Subj_Instructor_SubjectSubID",
                table: "Subj_Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dept_Instructor",
                table: "Dept_Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Dept_Instructor_InstructorInsId",
                table: "Dept_Instructor");

            migrationBuilder.DropColumn(
                name: "InstructorInsId",
                table: "Subj_Instructor");

            migrationBuilder.DropColumn(
                name: "SubjectSubID",
                table: "Subj_Instructor");

            migrationBuilder.DropColumn(
                name: "DepartmentDID",
                table: "Dept_Instructor");

            migrationBuilder.DropColumn(
                name: "InstructorInsId",
                table: "Dept_Instructor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor",
                columns: new[] { "InsId", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dept_Instructor",
                table: "Dept_Instructor",
                columns: new[] { "InsId", "DID" });

            migrationBuilder.CreateIndex(
                name: "IX_Subj_Instructor_SubID",
                table: "Subj_Instructor",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_Dept_Instructor_DID",
                table: "Dept_Instructor",
                column: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dept_Instructor_Departments_DID",
                table: "Dept_Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dept_Instructor_Instructors_InsId",
                table: "Dept_Instructor",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructor_Instructors_InsId",
                table: "Subj_Instructor",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructor_Subjects_SubID",
                table: "Subj_Instructor",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dept_Instructor_Departments_DID",
                table: "Dept_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Dept_Instructor_Instructors_InsId",
                table: "Dept_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Instructors_InsId",
                table: "Subj_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Subjects_SubID",
                table: "Subj_Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Subj_Instructor_SubID",
                table: "Subj_Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dept_Instructor",
                table: "Dept_Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Dept_Instructor_DID",
                table: "Dept_Instructor");

            migrationBuilder.AddColumn<int>(
                name: "InstructorInsId",
                table: "Subj_Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectSubID",
                table: "Subj_Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentDID",
                table: "Dept_Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorInsId",
                table: "Dept_Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor",
                columns: new[] { "InstructorInsId", "SubjectSubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dept_Instructor",
                table: "Dept_Instructor",
                columns: new[] { "DepartmentDID", "InstructorInsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Subj_Instructor_SubjectSubID",
                table: "Subj_Instructor",
                column: "SubjectSubID");

            migrationBuilder.CreateIndex(
                name: "IX_Dept_Instructor_InstructorInsId",
                table: "Dept_Instructor",
                column: "InstructorInsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dept_Instructor_Departments_DepartmentDID",
                table: "Dept_Instructor",
                column: "DepartmentDID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dept_Instructor_Instructors_InstructorInsId",
                table: "Dept_Instructor",
                column: "InstructorInsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructor_Instructors_InstructorInsId",
                table: "Subj_Instructor",
                column: "InstructorInsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructor_Subjects_SubjectSubID",
                table: "Subj_Instructor",
                column: "SubjectSubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
