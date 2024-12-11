using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewBuddy.Migrations
{
    /// <inheritdoc />
    public partial class Assignment : Migration
    {
        public object MachineId { get; internal set; }
        public object UserId { get; internal set; }
        public object FinishedDateAndTime { get; internal set; }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_CoffieMachine_MachineId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_MachineId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "DailyDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "IntervalType",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "FinishedDateAndTime",
                table: "Assignments",
                newName: "DateAndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAndTime",
                table: "Assignments",
                newName: "FinishedDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyDate",
                table: "Assignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntervalType",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_MachineId",
                table: "Assignments",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_CoffieMachine_MachineId",
                table: "Assignments",
                column: "MachineId",
                principalTable: "CoffieMachine",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
