using RPGTextAdventureWin.Views;

namespace RPGTextAdventureWin;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    private async void OnStartGameClick(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HeroChoosePage));
    }
}