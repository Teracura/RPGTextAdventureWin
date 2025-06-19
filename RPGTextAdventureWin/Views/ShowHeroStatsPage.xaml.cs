using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Heroes;
using MainLogic;
using MainLogic.GlobalParameters;

namespace RPGTextAdventureWin.Views;

public partial class ShowHeroStatsPage : ContentPage
{
    public ShowHeroStatsPage()
    {
        InitializeComponent();
        var hero = GameStateParameters.Instance.HeroState.Hero;
        HeroHealth.Text = $"{hero.Hp} / {hero.CalculateMaxHp()}";
        ScaleFactor.Text = GameStateParameters.Instance.MetaProgressionState.ScaleFactor.ToString();
        HeroType.Text = hero.Type!;
        HeroExperiencePoints.Text = $"{hero.Xp} / {HeroUtils.CalculateXpNeededForLevel(hero.Level)}";
        HeroDamage.Text = hero.Dmg.ToString();
        HeroDefencePercentage.Text = $"{hero.DefencePercentage * 100}%";
        HeroManaPoints.Text = $"{hero.Mp} / {hero.CalculateMaxMp()}";
        HeroLevel.Text = hero.Level.ToString();
        HeroMoney.Text = hero.Money.ToString();
        NumberOfDungeonsCleared.Text = GameStateParameters.Instance.MetaProgressionState.GlobalDungeonsCleared.ToString();
        NumberOfEnemiesKilled.Text = GameStateParameters.Instance.MetaProgressionState.GlobalEnemiesKilled.ToString();
        NumberOfTimesDefeated.Text = GameStateParameters.Instance.MetaProgressionState.GlobalTimesDefeated.ToString();
    }

    private async void ReturnToGameLoopMenu(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }
}