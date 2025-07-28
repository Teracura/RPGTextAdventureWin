using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class SpecialAbilitiesMenu : ContentPage
{
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    public ObservableCollection<FourTextLabelsWithButtonViewModel> SpecialSlots { get; set; }

    public SpecialAbilitiesMenu()
    {
        SpecialSlots =
        [
            new FourTextLabelsWithButtonViewModel("Name", "Cost", "Cooldown", "Description", "Example Button",
                null!),
        ];
        var allSpecials =
            SpecialAttackFactory.CreateAllSameClass(Instance.HeroState.Hero.Type!); //null already handled inside
        foreach (var special in allSpecials)
        {
            SpecialSlots.Add(new FourTextLabelsWithButtonViewModel(special.Name, special.ManaCost.ToString(),
                special.turnCooldown.ToString(),
                special.GetDescription(), "Activate", new Command(() => UseSpecialAttack(special))));
        }


        BindingContext = this;
        InitializeComponent();
    }

    private async void UseSpecialAttack(ISpecialAttack attack)
    {
        var gameManager = Instance.GameManager;
        if (gameManager.UseSpecialAttack(attack))
        {
            //Implement a warning system later
        }
        else
        {
            await Shell.Current.GoToAsync("BattleMenu");
        }
    }

    private async void OnBattleMenuButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("BattleMenu");
    }
}