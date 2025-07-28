using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.UI.Models;

public class OneLabelOneButtonViewModel(string labelText, string buttonText, Command onButtonClicked)
    : INotifyPropertyChanged
{
    private string _labelText = labelText;

    public string LabelText
    {
        get => _labelText;
        set => SetField(ref _labelText, value);
    }

    private string _buttonText = buttonText;

    public string ButtonText
    {
        get => _buttonText;
        set => SetField(ref _buttonText, value);
    }

    private Command _onButtonClicked = onButtonClicked;

    public Command OnButtonClicked
    {
        get => _onButtonClicked;
        set => SetField(ref _onButtonClicked, value);
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