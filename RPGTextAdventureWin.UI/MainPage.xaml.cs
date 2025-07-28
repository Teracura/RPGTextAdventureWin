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
        //TODO: Separate UI from Desktop and Android
        await Shell.Current.GoToAsync(nameof(HeroChoosePage)); //should be windows and mac only (maybe later on support linux with a third party)
    }
}