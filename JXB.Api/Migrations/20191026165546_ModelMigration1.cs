using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class ModelMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MinUsersCount = table.Column<int>(nullable: false),
                    MaxUsersCount = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DActivity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ActivityId = table.Column<string>(nullable: true),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DActivity_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Responsibility",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ActivityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsibility_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ResponsibilityId = table.Column<string>(nullable: true),
                    DActivityId = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DUser_DActivity_DActivityId",
                        column: x => x.DActivityId,
                        principalTable: "DActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DUser_Responsibility_ResponsibilityId",
                        column: x => x.ResponsibilityId,
                        principalTable: "Responsibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DActivity_ActivityId",
                table: "DActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DUser_DActivityId",
                table: "DUser",
                column: "DActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DUser_ResponsibilityId",
                table: "DUser",
                column: "ResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DUser_UserId",
                table: "DUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_ActivityId",
                table: "Responsibility",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DUser");

            migrationBuilder.DropTable(
                name: "DActivity");

            migrationBuilder.DropTable(
                name: "Responsibility");

            migrationBuilder.DropTable(
                name: "Activity");
        }
    }
}
