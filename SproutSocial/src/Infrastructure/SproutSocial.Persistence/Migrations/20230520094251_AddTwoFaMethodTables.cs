using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SproutSocial.Persistence.Migrations
{
    public partial class AddTwoFaMethodTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorAuthMethod",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "TwoFaMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TwoFactorAuthMethod = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFaMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTwoFaMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TwoFaMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTwoFaMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTwoFaMethods_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTwoFaMethods_TwoFaMethods_TwoFaMethodId",
                        column: x => x.TwoFaMethodId,
                        principalTable: "TwoFaMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTwoFaMethods_TwoFaMethodId",
                table: "UserTwoFaMethods",
                column: "TwoFaMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTwoFaMethods_UserId",
                table: "UserTwoFaMethods",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTwoFaMethods");

            migrationBuilder.DropTable(
                name: "TwoFaMethods");

            migrationBuilder.AddColumn<int>(
                name: "TwoFactorAuthMethod",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
