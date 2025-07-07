using System.Collections.ObjectModel;
using Entities.Heroes;
using MainLogic.GameLogic;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class BattleMenu : ContentPage
{
    public ObservableCollection<StatsShowViewModel> StatsSlots { get; set; }

    public BattleMenu()
    {
        InitializeComponent();
        StatsSlots =
        [
            new StatsShowViewModel("Health Points:", "", "Enemy Health:", ""),
            new StatsShowViewModel("Mana Points:", "", "Enemy Scale Factor:", ""),
        ];
        SubscribeToGameEvents();
        GameManager.InitiateFight();
        UpdateUI();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateUI(); // Call a method to refresh all UI elements
    }

    private void SubscribeToGameEvents()
    {
        GameManager.OnGameMessage += OnGameMessageHandler;
        GameManager.OnHeroCombatMessage += OnHeroCombatMessageHandler;
        GameManager.OnEnemyCombatMessage += OnEnemyCombatMessageHandler;
        GameManager.OnGameEnded += OnGameEndedHandler;
        CombatManager.OnHeroAttackMessage += OnHeroCombatMessageHandler;
        CombatManager.OnEnemyAttackMessage += OnEnemyCombatMessageHandler;
    }

    private void OnGameEndedHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread((() =>
            HeroInformationBox.Text = message));
        ShopManager.GetRandomShopItems(GameStateParameters.Instance.HeroState.Hero.Type!, 5);
        UpdateUI();
    }

    private void OnEnemyCombatMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread((() =>
            EnemyInformationBox.Text = message));
        UpdateUI();
    }

    private void OnHeroCombatMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread((() =>
            HeroInformationBox.Text = message));
        UpdateUI();
    }

    private void OnGameMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread((() =>
            BattleStatus.Text = message));
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update Enemy Counter
        int totalEnemies =
            GameStateParameters.Instance.DungeonState
                .NumberOfEnemiesPerDungeon; // Assuming you add this to GameStateParameters
        EnemyCounter.Text =
            $"Battle starts! ({GameStateParameters.Instance.DungeonState.NumberOfEnemiesDefeated} Enemies defeated out of {totalEnemies})";

        // Update Hero Info
        IHero currentHero = GameStateParameters.Instance.HeroState.Hero;
        var maxHp = currentHero.CalculateMaxHp();
        HeroName.Text = currentHero.Type!;
        StatsSlots[0].StatValue1 = $"{currentHero.Hp} / {maxHp}";
        StatsSlots[1].StatValue1 = $"{currentHero.Mp} / {currentHero.CalculateMaxMp()}";

        var currentEnemy = CombatManager.CurrentEnemy;
        // OR: Make a public static method in GameManager: GameManager.GetCurrentEnemy()
        if (currentEnemy != null)
        {
            EnemyName.Text = currentEnemy.Type;
            StatsSlots[0].StatValue2 = $"{currentEnemy.Hp} / {currentEnemy.MaxHp}";
            StatsSlots[1].StatValue2 = $"{GameStateParameters.Instance.MetaProgressionState.ScaleFactor}";
        }
        else
        {
            // Handle case where no enemy is present (e.g., victory, between enemies)
            EnemyName.Text = "No Enemy";
            StatsSlots[0].StatValue2 = "N/A";
            StatsSlots[1].StatValue2 = "N/A";
        }
    }

    private async void UseItemButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BattleInventoryMenu));
    }

    private async void RunAwayButtonClicked(object? sender, EventArgs e)
    {
        //TODO: Enemy attacks three times then run away
        ShopManager.GetRandomShopItems(GameStateParameters.Instance.HeroState.Hero.Type!, 5);
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async void AttackButtonClicked(object? sender, EventArgs e)
    {
        if (GameStateParameters.Instance.DungeonState.DungeonCleared ||
            GameStateParameters.Instance.HeroState.IsDefeated)
        {
            await Shell.Current.GoToAsync(nameof(GameLoopMenu));
        }
        else
        {
            GameManager.Attack();
        }
    }

    private void SpecialAbilityButtonClicked(object? sender, EventArgs e)
    {
        //TODO: Use special ability then enemy attacks
    }
}