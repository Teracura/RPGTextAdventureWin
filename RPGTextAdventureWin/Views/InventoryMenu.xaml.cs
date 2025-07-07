using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Items;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class InventoryMenu : ContentPage
{
    public ObservableCollection<InventoryShowViewModel> InventorySlots { get; set; }
    public GameStateParameters Instance = GameStateParameters.Instance;
    public InventoryMenu()
    {
        InitializeComponent();
        
        InventorySlots = [];
        foreach (var itemPair in Instance.OwnedItemsList.Items)
        {
            if (itemPair.Value <= 0) continue;
            var item = ItemCatalog.GetItemData(itemPair.Key);
            InventorySlots.Add(new InventoryShowViewModel(item.Name,itemPair.Value, item.Description));
        }
        BindingContext = this;
    }

    private async void ReturnToMainMenuClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }
}