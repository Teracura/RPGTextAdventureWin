using System.ComponentModel;
using System.Runtime.CompilerServices;
using Entities.Items;

namespace RPGTextAdventureWin.Models;

public class ShopSlotViewModel(
    int purchaseId,
    string name,
    string description,
    int price,
    string buttonText,
    Command purchaseCommand,
    Item item)
    : INotifyPropertyChanged
{
    public int PurchaseId { get; } = purchaseId;
    public string ItemName { get; } = name;
    public string ItemDescription { get; } = description;
    public int ItemPrice { get; } = price;

    private bool _purchased = false;

    public bool Purchased
    {
        get => _purchased;
        set => SetField(ref _purchased, value);
    }

    private string _buttonText = buttonText;

    public string ButtonText
    {
        get => _buttonText;
        set => SetField(ref _buttonText, value);
    }

    private Command _purchaseCommand = purchaseCommand;

    public Command PurchaseCommand
    {
        get => _purchaseCommand;
        set => SetField(ref _purchaseCommand, value);
    }
    
    private Item _item = item;

    public Item Item
    {
        get => _item;
        set => SetField(ref _item, value);
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}