using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.Models;

public class StatsShowViewModel(
    string statName1,
    string statValue1,
    string statName2,
    string statValue2)
    : INotifyPropertyChanged
{
    public string _statName1 = statName1;

    public string StatName1
    {
        get => _statName1;
        set => SetField(ref _statName1, value);
    }

    private string _statValue1 = statValue1;

    public string StatValue1
    {
        get => _statValue1;
        set => SetField(ref _statValue1, value);
    }

    private string _statName2 = statName2;

    public string StatName2
    {
        get => _statName2;
        set => SetField(ref _statName2, value);
    }

    private string _statValue2 = statValue2;

    public string StatValue2
    {
        get => _statValue2;
        set => SetField(ref _statValue2, value);
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