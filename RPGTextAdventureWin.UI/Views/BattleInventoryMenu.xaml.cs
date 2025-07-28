using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Items;
using MainLogic.Factories;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class BattleInventoryMenu : ContentPage
{
    public ObservableCollection<InventoryShowViewModel> InventorySlots { get; set; }
    public GameStateParameters Instance = GameStateParameters.Instance;

    public BattleInventoryMenu()
    {
        InitializeComponent();

        InventorySlots = [];
        foreach (var itemPair in Instance.OwnedItemsList.Items)
        {
            if (itemPair.Value <= 0) continue;
            if (!itemPair.Key.ToString().StartsWith("Common_")) continue;
            var item = ItemCatalog.GetItemData(itemPair.Key);

            InventorySlots.Add(new InventoryShowViewModel(item.Name, itemPair.Value, item.Description, "Use",
                new Command(() => UseButtonClicked(item))));
        }

        BindingContext = this;
    }

    private void UseButtonClicked(Item item)
    {
        var effect = ItemEffectFactory.Create(item.Type);
        Debug.WriteLine($"Hero health: {Instance.HeroState.Hero.Hp}");
        Debug.WriteLine($"Hero mana: {Instance.HeroState.Hero.Mp}");
        effect?.Apply(Instance.HeroState.Hero);
        Debug.WriteLine($"Hero health: {Instance.HeroState.Hero.Hp}");
        Debug.WriteLine($"Hero mana: {Instance.HeroState.Hero.Mp}");
        if (--Instance.OwnedItemsList.Items[item.Type] != 0) return;
        InventorySlots.Remove(InventorySlots.First(slot => slot.ItemName == item.Name));
    }

    private async void ReturnToMainMenuClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BattleMenu));
    }
}