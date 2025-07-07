using RPGTextAdventureWin.Views;

namespace RPGTextAdventureWin;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HeroChoosePage), typeof(HeroChoosePage));
        Routing.RegisterRoute(nameof(NewGameHeroTypeSelectPage), typeof(NewGameHeroTypeSelectPage));
        Routing.RegisterRoute(nameof(ChooseSaveFileTypeSelectPage), typeof(ChooseSaveFileTypeSelectPage));
        Routing.RegisterRoute(nameof(GameLoopMenu), typeof(GameLoopMenu));
        Routing.RegisterRoute(nameof(ShowHeroStatsPage), typeof(ShowHeroStatsPage));
        Routing.RegisterRoute(nameof(BattleMenu), typeof(BattleMenu));
        Routing.RegisterRoute(nameof(ShopMenu), typeof(ShopMenu));
        Routing.RegisterRoute(nameof(BattleInventoryMenu), typeof(BattleInventoryMenu));
        Routing.RegisterRoute(nameof(InventoryMenu), typeof(InventoryMenu));
        Routing.RegisterRoute(nameof(EquipmentSlotMenu), typeof(EquipmentSlotMenu));
    }
}