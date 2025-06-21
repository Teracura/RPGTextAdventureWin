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
        ]; //issue to solve: a bought item resets back to non-bought upon saving and loading (and re-opening)

        BindingContext = this;

        _ = LoadStatusesFromDbAsync();
    }

    private async Task SaveSelected(int slotId)
    {
        var context = new AppDataDbContext();
        var dataManager = new GameDataManager(context, Mapper);
        GameManager.Start();


        if (GameStateParameters.Instance.Saving)
        {
            GameStateParameters.Instance.Saving = false;
            try
            {
                await dataManager.SaveGameIntoFileSlot(slotId);
                OutputLabel.Text = "Save successful!";
            }
            catch (Exception e)
            {
                OutputLabel.Text = e.InnerException?.Message ?? e.Message;
            }

            OutputLabel.IsVisible = true;
        }
        else if (!await dataManager.LoadGameFromFileSlot(slotId))
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
        var context = new AppDataDbContext();
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
        await GoToGameLoopMenu();
    }

    private async Task GoToGameLoopMenu()
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }
}