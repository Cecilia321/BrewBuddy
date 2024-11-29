using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewBuddy.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffieMachine",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoffieMa__44EE5B38A8A30860", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CD5065536", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntervalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    AssignmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    FinishedDateAndTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tasks__7C6949B1D001C0CB", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_CoffieMachine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "CoffieMachine",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineInfo",
                columns: table => new
                {
                    InfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MachineI__4DEC9D7A52EF3225", x => x.InfoId);
                    table.ForeignKey(
                        name: "FK__MachineIn__Machi__2D27B809",
                        column: x => x.MachineId,
                        principalTable: "CoffieMachine",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__MachineIn__TaskI__2F10007B",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__MachineIn__UserI__2E1BDC42",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_MachineId",
                table: "Assignments",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_AssignmentId",
                table: "MachineInfo",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_MachineId",
                table: "MachineInfo",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_UserId",
                table: "MachineInfo",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineInfo");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CoffieMachine");
        }
    }
}
