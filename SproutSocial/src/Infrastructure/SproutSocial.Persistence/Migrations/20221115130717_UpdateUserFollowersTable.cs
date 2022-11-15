using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SproutSocial.Persistence.Migrations
{
    public partial class UpdateUserFollowersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserFollows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserFollows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserFollows");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "UserFollows");
        }
    }
}
