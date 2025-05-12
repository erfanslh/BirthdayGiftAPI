using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirthdayApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_wishLists_AspNetUsers_OwnerId",
                table: "wishLists");

            migrationBuilder.AddForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_wishLists_AspNetUsers_OwnerId",
                table: "wishLists",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_wishLists_AspNetUsers_OwnerId",
                table: "wishLists");

            migrationBuilder.AddForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishLists_AspNetUsers_OwnerId",
                table: "wishLists",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
