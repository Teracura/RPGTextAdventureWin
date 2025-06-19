using System.Collections.ObjectModel;
using MainLogic;
using MainLogic.AppDataManip;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class ChooseSaveFileTypeSelectPage : ContentPage
{
    private static readonly AppDataDbContextFactory Factory = new();
    public ObservableCollection<SaveSlotViewModel> SaveSlots { get; set; }
    private static readonly ObjectMapper Mapper = new();

    public ChooseSaveFileTypeSelectPage()
    {
        InitializeComponent();
        if (GameStateParameters.Instance.Saving)
        {
            ReturnToMenu.IsVisible = true;
        }

        SaveSlots =
        [
            new SaveSlotViewModel { SlotId = 1, ClickCommand = new Command(() => _ = SaveSelected(1)) },
            new SaveSlotViewModel { SlotId = 2, ClickCommand = new Command(() => _ = SaveSelected(2)) },
            new SaveSlotViewModel { SlotId = 3, ClickCommand = new Command(() => _ = SaveSelected(3)) }
        ];

        BindingContext = this;
        
        _ = LoadStatusesFromDbAsync();
    }

    private async Task SaveSelected(int slotId)
    {
        await using var context = Factory.CreateDbContext([]);
        var dataManager = new GameDataManager(context, Mapper);
        GameManager.Start();


        if (GameStateParameters.Instance.Saving)
        {
            OutputLabel.Text = "Saving... Please don't quit the game";
            await MakeOutputLabelVisibleAsync(3);
            GameStateParameters.Instance.Saving = false;
            if (await dataManager.SaveGameIntoFileSlot(slotId))
            {
                OutputLabel.Text = "Save successful!";
                await MakeOutputLabelVisibleAsync(3);
            }
        }
        else if (!await dataManager.LoadGameFromFileSlot(slotId))
        {
            OutputLabel.Text = "No save file found!";
            await MakeOutputLabelVisibleAsync(3);
        }
        else
        {
            GoToGameLoopMenu();
        }
    }

    private async Task LoadStatusesFromDbAsync()
    {
        await using var context = Factory.CreateDbContext([]);
        var dataManager = new GameDataManager(context, Mapper);

        for (var i = 1; i <= SaveSlots.Count; i++)
        {
            var slot = SaveSlots[i - 1];
            var heroBase = await context.Heroes.FindAsync(i);
            var gameStateBase = await context.GameStateParameters.FindAsync(i);

            if (heroBase != null && gameStateBase != null)
            {
                slot.Status = $"Type: {heroBase.Type}, Type: {heroBase.Type}, ScaleFactor: {gameStateBase.ScaleFactor}";
            }
            else
            {
                slot.Status = "Empty";
            }
        }
    }

    private async void ReturnToMainMenu(object? sender, EventArgs e)
    {
        GoToGameLoopMenu();
    }

    private async void GoToGameLoopMenu()
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async Task MakeOutputLabelVisibleAsync(decimal timeSeconds)
    {
        OutputLabel.IsVisible = true;
        await Task.Delay((int)(timeSeconds * 1000));
        OutputLabel.IsVisible = false;
    }
}