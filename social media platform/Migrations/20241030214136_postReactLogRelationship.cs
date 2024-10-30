using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_platform.Migrations
{
    /// <inheritdoc />
    public partial class postReactLogRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_posts_PostID",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "comments",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_PostID",
                table: "comments",
                newName: "IX_comments_PostId");

            migrationBuilder.CreateIndex(
                name: "IX_reactLogs_PostId",
                table: "reactLogs",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reactLogs_posts_PostId",
                table: "reactLogs",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_reactLogs_posts_PostId",
                table: "reactLogs");

            migrationBuilder.DropIndex(
                name: "IX_reactLogs_PostId",
                table: "reactLogs");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "comments",
                newName: "PostID");

            migrationBuilder.RenameIndex(
                name: "IX_comments_PostId",
                table: "comments",
                newName: "IX_comments_PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_posts_PostID",
                table: "comments",
                column: "PostID",
                principalTable: "posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
