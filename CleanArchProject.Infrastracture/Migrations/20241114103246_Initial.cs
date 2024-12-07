using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchProject.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depatment",
                columns: table => new
                {
                    DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depatment", x => x.DID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Period = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudID);
                    table.ForeignKey(
                        name: "FK_Student_Depatment_DID",
                        column: x => x.DID,
                        principalTable: "Depatment",
                        principalColumn: "DID");
                });

            migrationBuilder.CreateTable(
                name: "DepartmetSubject",
                columns: table => new
                {
                    DeptSubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DID = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmetSubject", x => x.DeptSubID);
                    table.ForeignKey(
                        name: "FK_DepartmetSubject_Depatment_DID",
                        column: x => x.DID,
                        principalTable: "Depatment",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmetSubject_Subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudSubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudID = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => x.StudSubID);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Student_StudID",
                        column: x => x.StudID,
                        principalTable: "Student",
                        principalColumn: "StudID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmetSubject_DID",
                table: "DepartmetSubject",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmetSubject_SubID",
                table: "DepartmetSubject",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DID",
                table: "Student",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_StudID",
                table: "StudentSubject",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubID",
                table: "StudentSubject",
                column: "SubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmetSubject");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Depatment");
        }
    }
}
