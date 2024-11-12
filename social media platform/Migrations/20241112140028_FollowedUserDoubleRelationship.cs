using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_platform.Migrations
{
    /// <inheritdoc />
    public partial class FollowedUserDoubleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FollowedUserId",
                table: "followedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_followedUsers_UserId",
                table: "followedUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_followedUsers_users_FollowedUserId",
                table: "followedUsers",
                column: "FollowedUserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_followedUsers_users_UserId",
                table: "followedUsers",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_followedUsers_users_FollowedUserId",
                table: "followedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_followedUsers_users_UserId",
                table: "followedUsers");

            migrationBuilder.DropIndex(
                name: "IX_followedUsers_UserId",
                table: "followedUsers");

            migrationBuilder.AlterColumn<long>(
                name: "FollowedUserId",
                table: "followedUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
