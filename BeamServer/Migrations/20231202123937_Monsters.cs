using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeamServer.Migrations
{
    /// <inheritdoc />
    public partial class Monsters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestStarter",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Beamons",
                columns: table => new
                {
                    BeamonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    SpecialAttack = table.Column<int>(type: "integer", nullable: false),
                    SpecialDefense = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    BeamonType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beamons", x => x.BeamonId);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    MonsterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BeamonId = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Exp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.MonsterId);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.MoveId);
                });

            migrationBuilder.CreateTable(
                name: "BeamonMoves",
                columns: table => new
                {
                    BeamonMoveId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonsterId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeamonMoves", x => x.BeamonMoveId);
                    table.ForeignKey(
                        name: "FK_BeamonMoves_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "MonsterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeamonMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "MoveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beamons",
                columns: new[] { "BeamonId", "Attack", "BeamonType", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[,]
                {
                    { 1, 40, 136, 40, 45, "Fordin", 65, 65, 45 },
                    { 2, 48, 4, 65, 44, "Kroki", 50, 64, 43 },
                    { 3, 52, 2, 43, 40, "Devidin", 60, 50, 60 },
                    { 4, 45, 640, 50, 60, "Aerodin", 80, 80, 70 },
                    { 5, 60, 128, 44, 35, "Weastoat", 40, 54, 55 }
                });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "MoveId", "Name" },
                values: new object[,]
                {
                    { 1, "Cut" },
                    { 2, "Ember" },
                    { 3, "Growl" },
                    { 4, "PoisonPowder" },
                    { 5, "QuickAttack" },
                    { 6, "SandAttack" },
                    { 7, "Scratch" },
                    { 8, "Sing" },
                    { 9, "SuperSonic" },
                    { 10, "Surf" },
                    { 11, "Tackle" },
                    { 12, "ThunderWave" },
                    { 13, "Vine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeamonMoves_MonsterId_MoveId",
                table: "BeamonMoves",
                columns: new[] { "MonsterId", "MoveId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BeamonMoves_MoveId",
                table: "BeamonMoves",
                column: "MoveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeamonMoves");

            migrationBuilder.DropTable(
                name: "Beamons");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropColumn(
                name: "RequestStarter",
                table: "AspNetUsers");
        }
    }
}
