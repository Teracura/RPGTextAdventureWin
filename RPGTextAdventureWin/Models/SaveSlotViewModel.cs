using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.Models;

public sealed class SaveSlotViewModel : INotifyPropertyChanged
{
    public int SlotId { get; set; }
    public string DisplayText => $"Save file {SlotId}";
    private string _status = "Empty";
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }

    public Command ClickCommand { get; set; }
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