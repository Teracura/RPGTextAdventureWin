using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enemies;
using Entities.Heroes;
using MainLogic;
using MainLogic.GameLogic;
using MainLogic.GlobalParameters;

namespace RPGTextAdventureWin.Views;

public partial class BattleMenu : ContentPage
{
    public BattleMenu()
    {
        InitializeComponent();
        SubscribeToGameEvents();
        GameManager.InitiateFight();
        UpdateUI();
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
            GameStateParameters.Instance.DungeonState.NumberOfEnemiesPerDungeon; // Assuming you add this to GameStateParameters
        EnemyCounter.Text =
            $"Battle starts! ({GameStateParameters.Instance.DungeonState.NumberOfEnemiesDefeated} Enemies defeated out of {totalEnemies})";

        // Update Hero Info
        IHero currentHero = GameStateParameters.Instance.HeroState.Hero;
        var maxHp = currentHero.CalculateMaxHp();
        HeroName.Text = currentHero.Type!;
        HeroHealth.Text = $"{currentHero.Hp} / {maxHp}";
        HeroMana.Text = $"{currentHero.Mp} / {currentHero.CalculateMaxMp()}";

        var currentEnemy = CombatManager.CurrentEnemy;
        // OR: Make a public static method in GameManager: GameManager.GetCurrentEnemy()
        if (currentEnemy != null)
        {
            EnemyName.Text = currentEnemy.Type;
            EnemyHealth.Text = $"{currentEnemy.Hp} / {currentEnemy.MaxHp}";
            ScaleFactor.Text = $"{GameStateParameters.Instance.MetaProgressionState.ScaleFactor}";
        }
        else
        {
            // Handle case where no enemy is present (e.g., victory, between enemies)
            EnemyName.Text = "No Enemy";
            EnemyHealth.Text = "N/A";
            ScaleFactor.Text = "N/A";
        }
    }

    private void UseItemButtonClicked(object? sender, EventArgs e)
    {
        //TODO: Use item then enemy attacks
    }

    private async void RunAwayButtonClicked(object? sender, EventArgs e)
    {
        //TODO: Enemy attacks three times then run away
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async void AttackButtonClicked(object? sender, EventArgs e)
    {
        if (GameStateParameters.Instance.DungeonState.DungeonCleared || GameStateParameters.Instance.HeroState.IsDefeated)
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