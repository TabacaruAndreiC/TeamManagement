using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagement.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamOwners_Users_UserId",
                table: "TeamOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamOwners_TeamOwnerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamOwnerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_TeamOwners_UserId",
                table: "TeamOwners");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "TeamOwners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TeamOwners");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamOwnerId",
                table: "Teams",
                column: "TeamOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOwners_UserId",
                table: "TeamOwners",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamOwners_Users_UserId",
                table: "TeamOwners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamOwners_TeamOwnerId",
                table: "Teams",
                column: "TeamOwnerId",
                principalTable: "TeamOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
