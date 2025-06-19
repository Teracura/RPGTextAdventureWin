using MainLogic;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GlobalParameters;

namespace RPGTextAdventureWin.Views;

public partial class NewGameHeroTypeSelectPage : ContentPage
{
    private readonly HeroCreator _heroCreator = new();

    public NewGameHeroTypeSelectPage()
    {
        InitializeComponent();
    }

    private void MageSelected(object? sender, EventArgs e)
    {
        var hero = _heroCreator.CreateNewHero("Mage");
        InitializeHero(hero);
        GoToGameLoopMenu();
    }

    private void ArcherSelected(object? sender, EventArgs e)
    {
        var hero = _heroCreator.CreateNewHero("Archer");
        InitializeHero(hero);
        GoToGameLoopMenu();
    }

    private void WarriorSelected(object? sender, EventArgs e)
    {
        var hero = _heroCreator.CreateNewHero("Warrior");
        InitializeHero(hero);
        GoToGameLoopMenu();
    }

    private async void GoToGameLoopMenu()
    {
            await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private void InitializeHero(IHero hero)
    {
        GameStateParameters.Instance.HeroState.Hero = hero;
        GameStateParameters.Instance.MetaProgressionState.ScaleFactor = 1m;
        GameStateParameters.Instance.DungeonState.NumberOfEnemiesPerDungeon = 5;
        GameManager.Start();
    }
}