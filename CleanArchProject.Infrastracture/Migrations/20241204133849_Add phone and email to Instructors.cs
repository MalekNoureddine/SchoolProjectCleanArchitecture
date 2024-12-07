using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddphoneandemailtoInstructors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Instructors_InsId",
                table: "Subj_Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructor_Subjects_SubID",
                table: "Subj_Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor");

            migrationBuilder.RenameTable(
                name: "Subj_Instructor",
                newName: "Subj_Instructors");

            migrationBuilder.RenameIndex(
                name: "IX_Subj_Instructor_SubID",
                table: "Subj_Instructors",
                newName: "IX_Subj_Instructors_SubID");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subj_Instructors",
                table: "Subj_Instructors",
                columns: new[] { "InsId", "SubID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructors_Instructors_InsId",
                table: "Subj_Instructors",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subj_Instructors_Subjects_SubID",
                table: "Subj_Instructors",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructors_Instructors_InsId",
                table: "Subj_Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Subj_Instructors_Subjects_SubID",
                table: "Subj_Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subj_Instructors",
                table: "Subj_Instructors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Instructors");

            migrationBuilder.RenameTable(
                name: "Subj_Instructors",
                newName: "Subj_Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_Subj_Instructors_SubID",
                table: "Subj_Instructor",
                newName: "IX_Subj_Instructor_SubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subj_Instructor",
                table: "Subj_Instructor",
                columns: new[] { "InsId", "SubID" });

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
    }
}
