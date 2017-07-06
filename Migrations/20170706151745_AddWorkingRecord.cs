using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SfAttendance.Migrations
{
    public partial class AddWorkingRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClockIn = table.Column<DateTime>(nullable: true),
                    ClockOut = table.Column<DateTime>(nullable: true),
                    OverTime = table.Column<TimeSpan>(nullable: true),
                    RecordeDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    WorkingTime = table.Column<TimeSpan>(nullable: true),
                    WorkingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecords_UserId",
                table: "WorkRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkRecords");
        }
    }
}
