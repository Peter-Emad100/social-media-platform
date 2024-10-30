using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_platform.Migrations
{
    /// <inheritdoc />
    public partial class postCommentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_comments_PostID",
                table: "comments",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_posts_PostID",
                table: "comments",
                column: "PostID",
                principalTable: "posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_posts_PostID",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_PostID",
                table: "comments");
        }
    }
}
