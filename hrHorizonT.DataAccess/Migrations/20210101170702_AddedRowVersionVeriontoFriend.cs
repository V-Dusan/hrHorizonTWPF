using Microsoft.EntityFrameworkCore.Migrations;

namespace hrHorizonT.DataAccess.Migrations
{
    public partial class AddedRowVersionVeriontoFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    newName: "FriendMeeting",
            //    newSchema: "HR");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "HR",
                table: "Friend",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "HR",
                table: "Friend");

            //migrationBuilder.RenameTable(
            //    name: "FriendMeeting",
            //    schema: "HR",
            //    newName: "FriendMeeting");
        }
    }
}
