using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_platform.Migrations
{
    /// <inheritdoc />
    public partial class ReactImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reactLogs_Reacts_ReactId",
                table: "reactLogs");

            migrationBuilder.DropTable(
                name: "Reacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reactLogs",
                table: "reactLogs");

            migrationBuilder.DropIndex(
                name: "IX_reactLogs_PostId",
                table: "reactLogs");

            migrationBuilder.DropIndex(
                name: "IX_reactLogs_ReactId",
                table: "reactLogs");

            migrationBuilder.DropColumn(
                name: "ReactLogId",
                table: "reactLogs");

            migrationBuilder.DropColumn(
                name: "ReactId",
                table: "reactLogs");

            migrationBuilder.AddColumn<int>(
                name: "React",
                table: "reactLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_reactLogs",
                table: "reactLogs",
                columns: new[] { "PostId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reactLogs",
                table: "reactLogs");

            migrationBuilder.DropColumn(
                name: "React",
                table: "reactLogs");

            migrationBuilder.AddColumn<long>(
                name: "ReactLogId",
                table: "reactLogs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte>(
                name: "ReactId",
                table: "reactLogs",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_reactLogs",
                table: "reactLogs",
                column: "ReactLogId");

            migrationBuilder.CreateTable(
                name: "Reacts",
                columns: table => new
                {
                    ReactId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReactName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacts", x => x.ReactId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reactLogs_PostId",
                table: "reactLogs",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_reactLogs_ReactId",
                table: "reactLogs",
                column: "ReactId");

            migrationBuilder.AddForeignKey(
                name: "FK_reactLogs_Reacts_ReactId",
                table: "reactLogs",
                column: "ReactId",
                principalTable: "Reacts",
                principalColumn: "ReactId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
