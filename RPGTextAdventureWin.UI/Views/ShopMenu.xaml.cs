using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainLogic;
using MainLogic.GameLogic;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class ShopMenu : ContentPage
{
    public ObservableCollection<ShopSlotViewModel> ShopSlots { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public ShopMenu()
    {
        InitializeComponent();
        HeroMoney.Text = GameStateParameters.Instance.HeroState.Hero.Money.ToString();
        BuildSlots();
    }

    private void BuildSlots()
    {
        ShopSlots = new ObservableCollection<ShopSlotViewModel>(
            ShopManager.ShopItems.Select((item, purchaseId) =>
            {
                var slot = new ShopSlotViewModel(
                    purchaseId,
                    item.Name,
                    item.Description,
                    item.Price,
                    "Purchase",
                    null!, null!// oooo very scary nulls
                );

                slot.PurchaseCommand = new Command(() => _ = PurchaseButtonClicked(slot));
                slot.Item = item; // rip nulls
                return slot;
            })
        );

        BindingContext = this;
    }

    private async void ReturnToMainMenuClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private async Task PurchaseButtonClicked(ShopSlotViewModel slot)
    {
        var purchasePassed = ShopManager.BuyItem(slot.Item);
        if (!purchasePassed)
        {
            ShopInformationBox.Text = "Invalid purchase: Not enough money.";
        }
        else
        {
            ShopSlots.Remove(slot);
            await MainThread.InvokeOnMainThreadAsync(() =>
                HeroMoney.Text = Instance.HeroState.Hero.Money.ToString()
            );
            ShopInformationBox.Text = "Purchased!";
        }
        ShopInformationBox.IsVisible = true;
    }
}