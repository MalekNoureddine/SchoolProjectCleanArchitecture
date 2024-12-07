using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updateSubjectPerioddataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Period",
                table: "Subjects",
                type: "INT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "Subjects",
                type: "Date",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
