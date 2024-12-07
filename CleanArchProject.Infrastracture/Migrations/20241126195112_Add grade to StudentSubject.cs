using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddgradetoStudentSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Degree",
                table: "StudentSubject",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "StudentSubject");
        }
    }
}
