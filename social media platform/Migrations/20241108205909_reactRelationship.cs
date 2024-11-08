using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_media_platform.Migrations
{
    /// <inheritdoc />
    public partial class reactRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "availableReacts");

            migrationBuilder.AlterColumn<byte>(
                name: "ReactId",
                table: "reactLogs",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reactLogs_Reacts_ReactId",
                table: "reactLogs");

            migrationBuilder.DropTable(
                name: "Reacts");

            migrationBuilder.DropIndex(
                name: "IX_reactLogs_ReactId",
                table: "reactLogs");

            migrationBuilder.AlterColumn<long>(
                name: "ReactId",
                table: "reactLogs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateTable(
                name: "availableReacts",
                columns: table => new
                {
                    AvailableReactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReactName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availableReacts", x => x.AvailableReactID);
                });
        }
    }
}
