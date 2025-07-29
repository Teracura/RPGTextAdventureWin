using RPGTextAdventureWin.UI.Views;
using RPGTextAdventureWin.Views;

namespace RPGTextAdventureWin;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        ChangeThemeLogos();

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
        Routing.RegisterRoute(nameof(SpecialAbilitiesMenu), typeof(SpecialAbilitiesMenu));
    }

    private void ChangeThemeLogos()
    {
        if (Application.Current == null) return;

        var effectiveTheme = (Application.Current.UserAppTheme != AppTheme.Unspecified)
            ? Application.Current.UserAppTheme
            : AppInfo.RequestedTheme;

        ThemeButton.IconImageSource = effectiveTheme switch
        {
            AppTheme.Light => "moon.png",
            AppTheme.Dark => "sun.png",
            _ => "dotnet_bot.png"
        };
    }


    private void OnToggleThemeClicked(object sender, EventArgs e)
    {
        if (Application.Current == null) return;
        var current = (Application.Current.UserAppTheme != AppTheme.Unspecified)
            ? Application.Current.UserAppTheme
            : AppInfo.RequestedTheme;


        Application.Current.UserAppTheme = current switch
        {
            AppTheme.Light => AppTheme.Dark,
            AppTheme.Dark => AppTheme.Light,
            _ => AppTheme.Dark
        };

        ChangeThemeLogos();
    }
}