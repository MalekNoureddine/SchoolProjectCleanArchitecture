using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AdjustResetPasswordtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswords",
                table: "ResetPasswords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResetPasswords");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasswords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswords",
                table: "ResetPasswords",
                column: "Token")
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResetPasswords",
                table: "ResetPasswords");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ResetPasswords",
                type: "NVARCHAR(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ResetPasswords",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResetPasswords",
                table: "ResetPasswords",
                column: "Id");
        }
    }
}
