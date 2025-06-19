using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainLogic;
using MainLogic.GlobalParameters;

namespace RPGTextAdventureWin.Views;

public partial class ShopMenu : ContentPage
{
    public ShopMenu()
    {
        InitializeComponent();
        HeroMoney.Text = GameStateParameters.Instance.HeroState.Hero.Money.ToString();
    }

    private async void ReturnToMainMenuClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private void PurchaseButton1Clicked(object? sender, EventArgs e)
    {
    }

    private void PurchaseButton2Clicked(object? sender, EventArgs e)
    {
    }

    private void PurchaseButton3Clicked(object? sender, EventArgs e)
    {
    }

    private void PurchaseButton4Clicked(object? sender, EventArgs e)
    {
    }

    private void PurchaseButton5Clicked(object? sender, EventArgs e)
    {
    }
}