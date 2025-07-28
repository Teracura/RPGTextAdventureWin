using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.Models;

public class FourTextLabelsWithButtonViewModel(
    string name,
    string manaCost,
    string turnCooldown,
    string description,
    string buttonText,
    Command onButtonClick)
    : INotifyPropertyChanged
{
    private string _name = name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private string _manaCost = manaCost;

    public string ManaCost
    {
        get => _manaCost;
        set
        {
            _manaCost = value;
            OnPropertyChanged();
        }
    }

    private string _turnCooldown = turnCooldown;

    public string TurnCooldown
    {
        get => _turnCooldown;
        set
        {
            _turnCooldown = value;
            OnPropertyChanged();
        }
    }

    private string _description = description;

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }    
    
    private string _buttonText = buttonText;

    public string ButtonText
    {
        get => _buttonText;
        set
        {
            _buttonText = value;
            OnPropertyChanged();
        }
    }

    private Command _onButtonClick = onButtonClick;

    public Command OnButtonClick
    {
        get => _onButtonClick;
        set
        {
            _onButtonClick = value;
            OnPropertyChanged();
        }
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