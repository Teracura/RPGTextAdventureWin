using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainLogic.AppDataManip.Migrations
{
    /// <inheritdoc />
    public partial class AddedEquipments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EquippedAccessory",
                table: "GameStateParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EquippedArmor",
                table: "GameStateParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EquippedWeapon",
                table: "GameStateParameters",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OwnedItems",
                columns: table => new
                {
                    SaveSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_SmallHealingPotion = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_BigHealingPotion = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_BlessingOfLife = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_SmallManaPotion = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_BigManaPotion = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_BlessingOfTheMagician = table.Column<int>(type: "INTEGER", nullable: false),
                    Common_KaitoKey = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_WoodenSword = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_IronSword = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_SwordOfMarcus = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_LeatherArmor = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_ChainMailArmor = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_PlateArmor = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_ArmorOfMarcus = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_WoodenShield = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_IronShield = table.Column<int>(type: "INTEGER", nullable: false),
                    Warrior_ShieldOfMarcus = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_QuickBow = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_LongBow = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_Crossbow = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_BowOfTheElves = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_TraineeArmor = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_HunterArmor = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_ArmorOfTheElves = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_SneakingBoots = table.Column<int>(type: "INTEGER", nullable: false),
                    Archer_BootsOfTheElves = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_WoodenStaff = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_WizardStaff = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_StaffOfTheRoyalWizard = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_StaffOfTheArchmage = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_ManaAmulet = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_SpellBook = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_OrbOfTheArchmage = table.Column<int>(type: "INTEGER", nullable: false),
                    Mage_CharmOfTheWind = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedItems", x => x.SaveSlot);
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    SaveSlot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.ItemId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedItems");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropColumn(
                name: "EquippedAccessory",
                table: "GameStateParameters");

            migrationBuilder.DropColumn(
                name: "EquippedArmor",
                table: "GameStateParameters");

            migrationBuilder.DropColumn(
                name: "EquippedWeapon",
                table: "GameStateParameters");
        }
    }
}
