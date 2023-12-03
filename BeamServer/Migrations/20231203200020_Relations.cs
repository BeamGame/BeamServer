using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeamServer.Migrations
{
    /// <inheritdoc />
    public partial class Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Monsters_BeamonId",
                table: "Monsters",
                column: "BeamonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Beamons_BeamonId",
                table: "Monsters",
                column: "BeamonId",
                principalTable: "Beamons",
                principalColumn: "BeamonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Beamons_BeamonId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_BeamonId",
                table: "Monsters");
        }
    }
}
