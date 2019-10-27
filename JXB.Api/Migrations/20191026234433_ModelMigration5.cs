using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class ModelMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DActivity_Activity_ActivityId",
                table: "DActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestion_DUser_DUserId",
                table: "DQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestion_Question_QuestionId",
                table: "DQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestion_Users_UserId",
                table: "DQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_DUser_DActivity_DActivityId",
                table: "DUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DUser_Responsibility_ResponsibilityId",
                table: "DUser");

            migrationBuilder.DropForeignKey(
                name: "FK_DUser_Users_UserId",
                table: "DUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibility_Activity_ActivityId",
                table: "Responsibility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsibility",
                table: "Responsibility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DUser",
                table: "DUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DQuestion",
                table: "DQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DActivity",
                table: "DActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.RenameTable(
                name: "Responsibility",
                newName: "Responsibilities");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "DUser",
                newName: "DUsers");

            migrationBuilder.RenameTable(
                name: "DQuestion",
                newName: "DQuestions");

            migrationBuilder.RenameTable(
                name: "DActivity",
                newName: "DActivities");

            migrationBuilder.RenameTable(
                name: "Activity",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibility_ActivityId",
                table: "Responsibilities",
                newName: "IX_Responsibilities_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_DUser_UserId",
                table: "DUsers",
                newName: "IX_DUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DUser_ResponsibilityId",
                table: "DUsers",
                newName: "IX_DUsers_ResponsibilityId");

            migrationBuilder.RenameIndex(
                name: "IX_DUser_DActivityId",
                table: "DUsers",
                newName: "IX_DUsers_DActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestion_UserId",
                table: "DQuestions",
                newName: "IX_DQuestions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestion_QuestionId",
                table: "DQuestions",
                newName: "IX_DQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestion_DUserId",
                table: "DQuestions",
                newName: "IX_DQuestions_DUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DActivity_ActivityId",
                table: "DActivities",
                newName: "IX_DActivities_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsibilities",
                table: "Responsibilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DUsers",
                table: "DUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DQuestions",
                table: "DQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DActivities",
                table: "DActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DActivities_Activities_ActivityId",
                table: "DActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestions_DUsers_DUserId",
                table: "DQuestions",
                column: "DUserId",
                principalTable: "DUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestions_Questions_QuestionId",
                table: "DQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestions_Users_UserId",
                table: "DQuestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUsers_DActivities_DActivityId",
                table: "DUsers",
                column: "DActivityId",
                principalTable: "DActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUsers_Responsibilities_ResponsibilityId",
                table: "DUsers",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUsers_Users_UserId",
                table: "DUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibilities_Activities_ActivityId",
                table: "Responsibilities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DActivities_Activities_ActivityId",
                table: "DActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestions_DUsers_DUserId",
                table: "DQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestions_Questions_QuestionId",
                table: "DQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DQuestions_Users_UserId",
                table: "DQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_DUsers_DActivities_DActivityId",
                table: "DUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DUsers_Responsibilities_ResponsibilityId",
                table: "DUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DUsers_Users_UserId",
                table: "DUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibilities_Activities_ActivityId",
                table: "Responsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responsibilities",
                table: "Responsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DUsers",
                table: "DUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DQuestions",
                table: "DQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DActivities",
                table: "DActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Responsibilities",
                newName: "Responsibility");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "DUsers",
                newName: "DUser");

            migrationBuilder.RenameTable(
                name: "DQuestions",
                newName: "DQuestion");

            migrationBuilder.RenameTable(
                name: "DActivities",
                newName: "DActivity");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activity");

            migrationBuilder.RenameIndex(
                name: "IX_Responsibilities_ActivityId",
                table: "Responsibility",
                newName: "IX_Responsibility_ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_DUsers_UserId",
                table: "DUser",
                newName: "IX_DUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DUsers_ResponsibilityId",
                table: "DUser",
                newName: "IX_DUser_ResponsibilityId");

            migrationBuilder.RenameIndex(
                name: "IX_DUsers_DActivityId",
                table: "DUser",
                newName: "IX_DUser_DActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestions_UserId",
                table: "DQuestion",
                newName: "IX_DQuestion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestions_QuestionId",
                table: "DQuestion",
                newName: "IX_DQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_DQuestions_DUserId",
                table: "DQuestion",
                newName: "IX_DQuestion_DUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DActivities_ActivityId",
                table: "DActivity",
                newName: "IX_DActivity_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responsibility",
                table: "Responsibility",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DUser",
                table: "DUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DQuestion",
                table: "DQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DActivity",
                table: "DActivity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DActivity_Activity_ActivityId",
                table: "DActivity",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestion_DUser_DUserId",
                table: "DQuestion",
                column: "DUserId",
                principalTable: "DUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestion_Question_QuestionId",
                table: "DQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DQuestion_Users_UserId",
                table: "DQuestion",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUser_DActivity_DActivityId",
                table: "DUser",
                column: "DActivityId",
                principalTable: "DActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUser_Responsibility_ResponsibilityId",
                table: "DUser",
                column: "ResponsibilityId",
                principalTable: "Responsibility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DUser_Users_UserId",
                table: "DUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibility_Activity_ActivityId",
                table: "Responsibility",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
