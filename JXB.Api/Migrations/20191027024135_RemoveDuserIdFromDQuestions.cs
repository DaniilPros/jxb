using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class RemoveDuserIdFromDQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DQuestions_DUsers_DUserId",
                table: "DQuestions");

            migrationBuilder.DropIndex(
                name: "IX_DQuestions_DUserId",
                table: "DQuestions");

            migrationBuilder.DropColumn(
                name: "DUserId",
                table: "DQuestions");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CheckInTime",
                table: "DUsers",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CheckInTime",
                table: "DUsers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DUserId",
                table: "DQuestions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DQuestions_DUserId",
                table: "DQuestions",
                column: "DUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestions_DUsers_DUserId",
                table: "DQuestions",
                column: "DUserId",
                principalTable: "DUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
