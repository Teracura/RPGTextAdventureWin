using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainLogic;
using MainLogic.GlobalParameters;
using GameManager = MainLogic.GameLogic.GameManager;

namespace RPGTextAdventureWin.Views;

public partial class GameLoopMenu : ContentPage
{
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    public GameLoopMenu()
    {
        InitializeComponent();
        if (Instance.DungeonState.DungeonCleared)
        {
            Instance.DungeonState.DungeonCleared = false;
            ResultStatus.Text = "Another glorious victory! You may rest now hero.";
            ResultStatus.IsVisible = true;
        }
        else if (Instance.HeroState.IsDefeated)
        {
            Instance.HeroState.IsDefeated = false;
            ResultStatus.Text =
                "Hero? thank god you are awake! you were dragged by us when we saw you fallen in battle";
            ResultStatus.IsVisible = true;
        }
    }

    private async void StartFightClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BattleMenu));
    }

    private async void ShowCharacterStatsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ShowHeroStatsPage));
    }

    private void RestoreStatsClicked(object? sender, EventArgs e)
    {
        Instance.GameManager.RestoreStats();
        ResultStatus.Text = "Stats Restored!";
        ResultStatus.IsVisible = true;
    }

    private async void OpenShopClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ShopMenu));
    }

    private async void ShowEquipmentClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EquipmentSlotMenu));
    }

    private async void ShowInventoryClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(InventoryMenu));
    }

    private async void SaveGameClicked(object? sender, EventArgs e)
    {
        Instance.Saving = true;
        await Shell.Current.GoToAsync(nameof(ChooseSaveFileTypeSelectPage));
    }
}