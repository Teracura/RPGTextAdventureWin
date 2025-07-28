using System.Collections.ObjectModel;
using Entities.Enemies;
using Entities.Heroes;
using MainLogic.AppDataManip;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic;
using MainLogic.GlobalParameters;
using MainLogic.GlobalParameters.BaseEntities;
using Microsoft.EntityFrameworkCore;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class ChooseSaveFileTypeSelectPage : ContentPage
{
    public ObservableCollection<SaveSlotViewModel> SaveSlots { get; set; }
    private static readonly ObjectMapper Mapper = new();
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public ChooseSaveFileTypeSelectPage()
    {
        InitializeComponent();
        if (GameStateParameters.Instance.Saving)
        {
            ReturnToMenu.IsVisible = true;
            ExitToStart.Text = "Exit to start";
        }

        SaveSlots = new ObservableCollection<SaveSlotViewModel>(
            Enumerable.Range(1, 3).Select(slotId =>
            {
                var slot = new SaveSlotViewModel { SlotId = slotId };
                slot.ClickCommand = new Command(() => _ = SaveSelected(slot));
                return slot;
            })
        );

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        OutputLabel.IsVisible = true;
        OutputLabel.Text = "Loading... please wait";

        await Task.Yield();
        await LoadStatusesFromDbAsync();
        OutputLabel.IsVisible = false;
    }

    private async Task SaveSelected(SaveSlotViewModel slot)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "SavingAndLoadingData.db");
        var context = new AppDataDbContext(dbPath);
        var dataManager = new GameDataManager(context, Mapper);
        var gameManager = new GameManager(new EnemyCreator(), new CombatManager(), new Queue<IEnemy>());


        if (GameStateParameters.Instance.Saving)
        {
            try
            {
                await dataManager.SaveGameIntoFileSlot(slot.SlotId);
                OutputLabel.Text = "Save successful!";
                GameStateParameters.Instance.Saving = false;
                slot.Status =
                    $"Type: {Instance.HeroState.Hero.Type}, ScaleFactor: {Instance.MetaProgressionState.ScaleFactor}";
            }
            catch (Exception e)
            {
                OutputLabel.Text = e.InnerException?.Message ?? e.Message;
            }

            OutputLabel.IsVisible = true;
        }
        else if (!await dataManager.LoadGameFromFileSlot(slot.SlotId))
        {
            OutputLabel.Text = "No save file found!";
            OutputLabel.IsVisible = true;
        }
        else
        {
            await GoToGameLoopMenu();
        }
    }

    private async Task LoadStatusesFromDbAsync()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "SavingAndLoadingData.db");
        var context = new AppDataDbContext(dbPath);

        foreach (var slot in SaveSlots)
        {
            var heroBase = await context.Heroes.FindAsync(slot.SlotId);
            var gameStateBase = await context.GameStateParameters.FindAsync(slot.SlotId);

            if (heroBase != null && gameStateBase != null)
            {
                slot.Status = $"Type: {heroBase.Type}, ScaleFactor: {gameStateBase.ScaleFactor}";
            }
            else
            {
                slot.Status = "Empty";
            }
        }
    }

    private async void ReturnToMainMenu(object? sender, EventArgs e)
    {
        await GoToGameLoopMenu();
    }

    private async Task GoToGameLoopMenu()
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async void ReturnToStartClicked(object? sender, EventArgs e)
    {
        Instance.Saving = false;
        await Shell.Current.GoToAsync(nameof(HeroChoosePage));
    }
}