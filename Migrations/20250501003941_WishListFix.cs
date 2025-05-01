using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirthdayApp.Migrations
{
    /// <inheritdoc />
    public partial class WishListFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_friendships_AspNetUsers_RequesterId",
                table: "friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_wishLists_AspNetUsers_OwnerId",
                table: "wishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wishLists",
                table: "wishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_friendships",
                table: "friendships");

            migrationBuilder.RenameTable(
                name: "wishLists",
                newName: "Wishlists");

            migrationBuilder.RenameTable(
                name: "friendships",
                newName: "Friendships");

            migrationBuilder.RenameIndex(
                name: "IX_wishLists_OwnerId",
                table: "Wishlists",
                newName: "IX_Wishlists_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_friendships_RequesterId",
                table: "Friendships",
                newName: "IX_Friendships_RequesterId");

            migrationBuilder.RenameIndex(
                name: "IX_friendships_ReceiverId",
                table: "Friendships",
                newName: "IX_Friendships_ReceiverId");

            migrationBuilder.AlterColumn<string>(
                name: "BookedByUser",
                table: "Wishlists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_RequesterId",
                table: "Friendships",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_OwnerId",
                table: "Wishlists",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_RequesterId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_OwnerId",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                newName: "wishLists");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "friendships");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_OwnerId",
                table: "wishLists",
                newName: "IX_wishLists_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_RequesterId",
                table: "friendships",
                newName: "IX_friendships_RequesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_ReceiverId",
                table: "friendships",
                newName: "IX_friendships_ReceiverId");

            migrationBuilder.AlterColumn<string>(
                name: "BookedByUser",
                table: "wishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_wishLists",
                table: "wishLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_friendships",
                table: "friendships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_friendships_AspNetUsers_ReceiverId",
                table: "friendships",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_friendships_AspNetUsers_RequesterId",
                table: "friendships",
                column: "RequesterId",
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
    }
}
