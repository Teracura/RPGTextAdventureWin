
namespace RPGTextAdventureWin.Views;

public partial class HeroChoosePage : ContentPage
{
    public HeroChoosePage()
    {
        InitializeComponent();
    }

    private async void OnStartNewSaveClick(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewGameHeroTypeSelectPage));
    }

    private async void OnContinueSaveClick(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ChooseSaveFileTypeSelectPage));
    }
}