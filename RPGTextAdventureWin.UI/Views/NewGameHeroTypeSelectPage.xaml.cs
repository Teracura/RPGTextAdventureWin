using System.Collections.ObjectModel;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.UI.Models;
using RPGTextAdventureWin.Views;

namespace RPGTextAdventureWin.UI.Views;

public partial class NewGameHeroTypeSelectPage : ContentPage
{
    private readonly HeroCreator _heroCreator = new();
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    public ObservableCollection<OneLabelOneButtonViewModel> NewHeroSlots { get; set; }


    public NewGameHeroTypeSelectPage()
    {
        InitializeComponent();
        NewHeroSlots = [];
        foreach (var heroType in Enum.GetValues<HeroClasses>())
        {
            NewHeroSlots.Add(new OneLabelOneButtonViewModel("Hero class: " + heroType, "Select",
                new Command(() => HeroSelected(heroType))));
        }

        BindingContext = this;
    }


    //TODO: preferably use unified method instead of three separate methods below
    private void HeroSelected(HeroClasses heroType)
    {
        var hero = _heroCreator.CreateNewHero(heroType);
        Instance.ResetState(hero);
        GoToGameLoopMenu();
    }

    private async void GoToGameLoopMenu()
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async void ReturnToStartClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HeroChoosePage));
    }
}