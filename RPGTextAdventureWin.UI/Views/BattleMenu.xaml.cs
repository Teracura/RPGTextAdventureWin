using System.Collections.ObjectModel;
using Entities.Heroes;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class BattleMenu : ContentPage
{
    public ObservableCollection<StatsShowViewModel> StatsSlots { get; set; }
    public ObservableCollection<ThreeTextLabelsViewModel> InformationBoxes { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    private bool _heroMessageReceived;
    private bool _enemyMessageReceived;
    private bool _gameMessageReceived;


    public BattleMenu()
    {
        InitializeComponent();
        StatsSlots =
        [
            new StatsShowViewModel("Health Points:", "", "Enemy Health:", ""),
            new StatsShowViewModel("Mana Points:", "", "Enemy Scale Factor:", ""),
        ];
        InformationBoxes = [];
        
        for (var i = 0; i < 5; i++)
        {
            InformationBoxes.Add(new ThreeTextLabelsViewModel());
        }

        SubscribeToGameEvents();
        Instance.GameManager.InitiateFight();
        UpdateUi();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateUi(); // Call a method to refresh all UI elements
    }

    private void SubscribeToGameEvents()
    {
        EventManager.GameMessageNotification += OnGameMessageHandler;
        EventManager.HeroSideNotification += OnHeroCombatMessageHandler;
        EventManager.EnemySideNotification += OnEnemyCombatMessageHandler;
    }

    private void OnHeroCombatMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AddToInformationBox(message, MessageSides.Left);
            _heroMessageReceived = true;
            TryUpdateUiOncePerAction();
        });
    }

    private void OnEnemyCombatMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AddToInformationBox(message, MessageSides.Right);
            _enemyMessageReceived = true;
            TryUpdateUiOncePerAction();
        });
    }

    private void OnGameMessageHandler(string message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AddToInformationBox(message, MessageSides.Center);
            _gameMessageReceived = true;
            TryUpdateUiOncePerAction();
        });
    }


    private void AddToInformationBox(string message, MessageSides side)
    {
        // Try to find the first label where the relevant side is empty
        foreach (var label in InformationBoxes)
        {
            switch (side)
            {
                case MessageSides.Left:
                    if (string.IsNullOrWhiteSpace(label.LeftSideText))
                    {
                        label.LeftSideText = message;
                        return;
                    }

                    break;
                case MessageSides.Right:
                    if (string.IsNullOrWhiteSpace(label.RightSideText))
                    {
                        label.RightSideText = message;
                        return;
                    }

                    break;
                case MessageSides.Center:
                    if (string.IsNullOrWhiteSpace(label.MiddleText))
                    {
                        label.MiddleText = message;
                        return;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        // If no empty slot was found, remove the oldest and add a new one
        InformationBoxes.RemoveAt(0);
        var newLabel = new ThreeTextLabelsViewModel();
        switch (side)
        {
            case MessageSides.Left:
                newLabel.LeftSideText = message;
                break;
            case MessageSides.Right:
                newLabel.RightSideText = message;
                break;
            case MessageSides.Center:
                newLabel.MiddleText = message;
                break;
        }

        InformationBoxes.Add(newLabel);
    }


    private void UpdateUi()
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
        EnemyName.Text = currentEnemy.Type;
        StatsSlots[0].StatValue2 = $"{currentEnemy.Hp} / {currentEnemy.MaxHp}";
        StatsSlots[1].StatValue2 = $"{GameStateParameters.Instance.MetaProgressionState.ScaleFactor}";
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
            Instance.GameManager.Attack();
        }
    }

    private void TryUpdateUiOncePerAction()
    {
        if ((!_heroMessageReceived || !_enemyMessageReceived) && !_gameMessageReceived) return;
        UpdateUi();

        // Reset for next combat action
        _heroMessageReceived = false;
        _enemyMessageReceived = false;
        _gameMessageReceived = false;
    }


    private async void SpecialAbilityButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SpecialAbilitiesMenu");
    }
}