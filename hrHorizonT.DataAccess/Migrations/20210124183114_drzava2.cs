using Microsoft.EntityFrameworkCore.Migrations;

namespace hrHorizonT.DataAccess.Migrations
{
    public partial class drzava2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hr");

            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    newName: "FriendMeeting",
            //    newSchema: "HR");

            migrationBuilder.CreateSequence(
                name: "drzava_id_seq",
                schema: "hr");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "HR",
                table: "Drzava",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "drzava2",
                schema: "hr",
                columns: table => new
                {
                    drzava_id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('hr.drzava_id_seq'::regclass)"),
                    Sifra = table.Column<int>(type: "integer", nullable: false),
                    Oznaka = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Naziv = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drzava2", x => x.drzava_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "drzava2",
                schema: "hr");

            migrationBuilder.DropSequence(
                name: "drzava_id_seq",
                schema: "hr");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "HR",
                table: "Drzava");

            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    schema: "HR",
            //    newName: "FriendMeeting");
        }
    }
}
