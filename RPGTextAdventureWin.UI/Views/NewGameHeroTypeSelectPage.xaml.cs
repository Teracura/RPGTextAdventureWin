using Entities.Enemies;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic;
using MainLogic.GlobalParameters;

namespace RPGTextAdventureWin.Views;

public partial class NewGameHeroTypeSelectPage : ContentPage
{
    private readonly HeroCreator _heroCreator = new();
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public NewGameHeroTypeSelectPage()
    {
        InitializeComponent();
    }

    //TODO: preferably use unified method instead of three separate methods below
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
        Instance.HeroState.Hero = hero;
        Instance.MetaProgressionState.ScaleFactor = 1m;
        Instance.DungeonState.NumberOfEnemiesPerDungeon =
            5; //default values, don't change unless related to game balance
        ShopManager.GetRandomShopItems(Instance.HeroState.Hero.Type!);
        var gameManager = new GameManager(new EnemyCreator(), new CombatManager(), new Queue<IEnemy>());
        Instance.GameManager = gameManager;
    }

    private async void ReturnToStartClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HeroChoosePage));
    }
}