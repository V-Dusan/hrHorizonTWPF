using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hrHorizonT.DataAccess.Migrations
{
    public partial class ProgrammingLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteLanguageId",
                schema: "HR",
                table: "Friend",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguage",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FavoriteLanguageId",
                schema: "HR",
                table: "Friend",
                column: "FavoriteLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_ProgrammingLanguage_FavoriteLanguageId",
                schema: "HR",
                table: "Friend",
                column: "FavoriteLanguageId",
                principalSchema: "HR",
                principalTable: "ProgrammingLanguage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_ProgrammingLanguage_FavoriteLanguageId",
                schema: "HR",
                table: "Friend");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguage",
                schema: "HR");

            migrationBuilder.DropIndex(
                name: "IX_Friend_FavoriteLanguageId",
                schema: "HR",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "FavoriteLanguageId",
                schema: "HR",
                table: "Friend");
        }
    }
}
