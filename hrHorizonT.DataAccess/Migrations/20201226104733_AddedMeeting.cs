using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hrHorizonT.DataAccess.Migrations
{
    public partial class AddedMeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meeting",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendMeeting",
                schema: "HR",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "integer", nullable: false),
                    MeetingsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendMeeting", x => new { x.FriendsId, x.MeetingsId });
                    table.ForeignKey(
                        name: "FK_FriendMeeting_Friend_FriendsId",
                        column: x => x.FriendsId,
                        principalSchema: "HR",
                        principalTable: "Friend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendMeeting_Meeting_MeetingsId",
                        column: x => x.MeetingsId,
                        principalSchema: "HR",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendMeeting_MeetingsId",
                schema: "HR",
                table: "FriendMeeting",
                column: "MeetingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendMeeting",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Meeting",
                schema: "HR");
        }
    }
}
