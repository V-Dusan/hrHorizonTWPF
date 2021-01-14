using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hrHorizonT.DataAccess.Migrations
{
    public partial class DrzavaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    newName: "FriendMeeting",
            //    newSchema: "HR");

            migrationBuilder.CreateTable(
                name: "Drzava",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sifra = table.Column<int>(type: "integer", nullable: false),
                    Oznaka = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Naziv = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drzava_Naziv",
                schema: "HR",
                table: "Drzava",
                column: "Naziv",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drzava_Oznaka",
                schema: "HR",
                table: "Drzava",
                column: "Oznaka",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drzava_Sifra",
                schema: "HR",
                table: "Drzava",
                column: "Sifra",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drzava",
                schema: "HR");

            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    schema: "HR",
            //    newName: "FriendMeeting");
        }
    }
}
