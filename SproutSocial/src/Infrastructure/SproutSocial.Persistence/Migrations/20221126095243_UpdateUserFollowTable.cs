using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SproutSocial.Persistence.Migrations
{
    public partial class UpdateUserFollowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowerUserId",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowerUserId",
                table: "UserFollows",
                newName: "FollowedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowerUserId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowedUserId",
                table: "UserFollows",
                column: "FollowedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowedUserId",
                table: "UserFollows",
                newName: "FollowerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowedUserId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowerUserId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserFollows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserFollows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_FollowerUserId",
                table: "UserFollows",
                column: "FollowerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
