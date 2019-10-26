using Microsoft.EntityFrameworkCore.Migrations;

namespace JXB.Api.Migrations
{
    public partial class ModelMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DInterest");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Option1 = table.Column<string>(nullable: true),
                    Option2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DQuestion",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<string>(nullable: true),
                    Answer = table.Column<int>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    DUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DQuestion_DUser_DUserId",
                        column: x => x.DUserId,
                        principalTable: "DUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DQuestion_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DQuestion_DUserId",
                table: "DQuestion",
                column: "DUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DQuestion_QuestionId",
                table: "DQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_DQuestion_UserId",
                table: "DQuestion",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DQuestion");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DInterest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InterestId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
    }
}
