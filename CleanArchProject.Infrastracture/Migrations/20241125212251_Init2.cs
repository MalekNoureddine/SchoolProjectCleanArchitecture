using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubject_Depatment_DID",
                table: "DepartmetSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Depatment_DID",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubject_StudID",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubject",
                table: "DepartmetSubject");

            migrationBuilder.DropIndex(
                name: "IX_DepartmetSubject_DID",
                table: "DepartmetSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Depatment",
                table: "Depatment");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentSubject");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmetSubject");

            migrationBuilder.RenameTable(
                name: "Depatment",
                newName: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "NVARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "Subjects",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Student",
                type: "VARCHAR(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Student",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Student",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Student",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "Departments",
                type: "NVARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DName",
                table: "Departments",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject",
                columns: new[] { "StudID", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubject",
                table: "DepartmetSubject",
                columns: new[] { "DID", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubject_Departments_DID",
                table: "DepartmetSubject",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Departments_DID",
                table: "Student",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubject_Departments_DID",
                table: "DepartmetSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Departments_DID",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubject",
                table: "DepartmetSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Depatment");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentSubject",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Student",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Student",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Student",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Student",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmetSubject",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "Depatment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DName",
                table: "Depatment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubject",
                table: "DepartmetSubject",
                column: "DeptSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Depatment",
                table: "Depatment",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_StudID",
                table: "StudentSubject",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmetSubject_DID",
                table: "DepartmetSubject",
                column: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubject_Depatment_DID",
                table: "DepartmetSubject",
                column: "DID",
                principalTable: "Depatment",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Depatment_DID",
                table: "Student",
                column: "DID",
                principalTable: "Depatment",
                principalColumn: "DID");
        }
    }
}
