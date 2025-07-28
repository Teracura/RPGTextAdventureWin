using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainLogic.AppDataManip.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameStateParameters",
                columns: table => new
                {
                    SaveSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    Saving = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDefeated = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScaleFactor = table.Column<decimal>(type: "TEXT", nullable: false),
                    GlobalEnemiesKilled = table.Column<int>(type: "INTEGER", nullable: false),
                    GlobalTimesDefeated = table.Column<int>(type: "INTEGER", nullable: false),
                    GlobalDungeonsCleared = table.Column<int>(type: "INTEGER", nullable: false),
                    KaitoKeyEffect = table.Column<bool>(type: "INTEGER", nullable: false),
                    NumberOfEnemiesPerDungeon = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfEnemiesDefeated = table.Column<int>(type: "INTEGER", nullable: false),
                    EnemyDefeated = table.Column<bool>(type: "INTEGER", nullable: false),
                    DungeonCleared = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStateParameters", x => x.SaveSlot);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    SaveSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Hp = table.Column<decimal>(type: "TEXT", nullable: false),
                    Dmg = table.Column<decimal>(type: "TEXT", nullable: false),
                    Mp = table.Column<decimal>(type: "TEXT", nullable: false),
                    DefencePercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxDefense = table.Column<decimal>(type: "TEXT", nullable: false),
                    Xp = table.Column<decimal>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnCounter = table.Column<int>(type: "INTEGER", nullable: false),
                    Money = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenseIncreasePerLevel = table.Column<decimal>(type: "TEXT", nullable: false),
                    DmgPerLevel = table.Column<decimal>(type: "TEXT", nullable: false),
                    HpPerLevel = table.Column<decimal>(type: "TEXT", nullable: false),
                    HeroType = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.SaveSlot);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStateParameters");

            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
