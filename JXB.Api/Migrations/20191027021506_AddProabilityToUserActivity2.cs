using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class AddProabilityToUserActivity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivity_Activities_ActivityId",
                table: "UserActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivity_Users_UserId",
                table: "UserActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivity",
                table: "UserActivity");

            migrationBuilder.RenameTable(
                name: "UserActivity",
                newName: "UserActivities");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivity_UserId",
                table: "UserActivities",
                newName: "IX_UserActivities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivity_ActivityId",
                table: "UserActivities",
                newName: "IX_UserActivities_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivities",
                table: "UserActivities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Activities_ActivityId",
                table: "UserActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Users_UserId",
                table: "UserActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Activities_ActivityId",
                table: "UserActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Users_UserId",
                table: "UserActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserActivities",
                table: "UserActivities");

            migrationBuilder.RenameTable(
                name: "UserActivities",
                newName: "UserActivity");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivity",
                newName: "IX_UserActivity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivity",
                newName: "IX_UserActivity_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserActivity",
                table: "UserActivity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivity_Activities_ActivityId",
                table: "UserActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivity_Users_UserId",
                table: "UserActivity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
