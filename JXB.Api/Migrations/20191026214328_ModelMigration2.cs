﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class ModelMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DInterest",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    InterestId = table.Column<string>(nullable: true),
                    DUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DInterest_DUser_DUserId",
                        column: x => x.DUserId,
                        principalTable: "DUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DInterest_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DInterest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DInterest_DUserId",
                table: "DInterest",
                column: "DUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DInterest_InterestId",
                table: "DInterest",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_DInterest_UserId",
                table: "DInterest",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DInterest");

            migrationBuilder.DropTable(
                name: "Interest");
        }
    }
}
