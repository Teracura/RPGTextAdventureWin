using System.Collections.ObjectModel;
using Entities.Heroes;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class ShowHeroStatsPage : ContentPage
{
    public ObservableCollection<StatsShowViewModel> StatsSlots { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public ShowHeroStatsPage()
    {
        InitializeComponent();
        StatsSlots =
        [
            new StatsShowViewModel("Health Points:", "", "ScaleFactor:", ""),
            new StatsShowViewModel("Damage Points:", "", "Mana Points:", ""),
            new StatsShowViewModel("Level:", "", "Defence percentage:", ""),
            new StatsShowViewModel("Hero Type:", "", "Number of times defeated:", ""),
            new StatsShowViewModel("Hero Money:", "", "Number of enemies killed:", ""),
            new StatsShowViewModel("Hero Experience Points:", "", "Number of Dungeons cleared:", "")
        ];

        var hero = Instance.HeroState.Hero;
        StatsSlots[0].StatValue1 = $"{hero.Hp} / {hero.CalculateMaxHp()}";
        StatsSlots[0].StatValue2 = Instance.MetaProgressionState.ScaleFactor.ToString();
        StatsSlots[1].StatValue1 = hero.Dmg.ToString();
        StatsSlots[1].StatValue2 = $"{hero.Mp} / {hero.CalculateMaxMp()}";
        StatsSlots[2].StatValue2 = $"{hero.DefencePercentage * 100}%";
        StatsSlots[2].StatValue1 = hero.Level.ToString();
        StatsSlots[3].StatValue1 = hero.Type!;
        StatsSlots[3].StatValue2 = Instance.MetaProgressionState.GlobalTimesDefeated.ToString();
        StatsSlots[4].StatValue1 = hero.Money.ToString();
        StatsSlots[4].StatValue2 = Instance.MetaProgressionState.GlobalEnemiesKilled.ToString();
        StatsSlots[5].StatValue1 = $"{hero.Xp} / {HeroUtils.CalculateXpNeededForLevel(hero.Level)}";
        StatsSlots[5].StatValue2 =
            Instance.MetaProgressionState.GlobalDungeonsCleared.ToString();

        BindingContext = this;
    }

    private async void ReturnToGameLoopMenu(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }
}