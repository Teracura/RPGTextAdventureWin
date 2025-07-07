using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.Models;

public class InventoryShowViewModel : INotifyPropertyChanged
{
    //constructors
    public InventoryShowViewModel(string itemName,
        int itemCount,
        string itemDescription,
        string itemUseButtonText = "If you are seeing this then something went horribly wrong",
        Command? itemUseCommand = null)
    {
        _itemName = itemName;
        _itemCount = itemCount;
        _itemDescription = itemDescription;
        _itemUseButtonText = itemUseButtonText;
        _itemUseCommand = itemUseCommand;
    }

    public InventoryShowViewModel(string itemName, string itemDescription, string itemUseButtonText,
        Command? itemUseCommand)
    {
        _itemName = itemName;
        _itemDescription = itemDescription;
        _itemUseButtonText = itemUseButtonText;
        _itemUseCommand = itemUseCommand;
        _itemCount = 0; //in this case the count is not needed so it can be any value
    }
    
    private string _itemName;

    public string ItemName
    {
        get => _itemName;
        set => SetField(ref _itemName, value);
    }

    private int _itemCount;

    public int ItemCount
    {
        get => _itemCount;
        set => SetField(ref _itemCount, value);
    }

    private string _itemDescription;

    public string ItemDescription
    {
        get => _itemDescription;
        set => SetField(ref _itemDescription, value);
    }

    private string _itemUseButtonText;

    public string ItemUseButtonText
    {
        get => _itemUseButtonText;
        set => SetField(ref _itemUseButtonText, value);
    }

    private Command? _itemUseCommand;



    public Command? ItemUseCommand
    {
        get => _itemUseCommand;
        set => SetField(ref _itemUseCommand, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}