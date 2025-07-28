using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPGTextAdventureWin.Models;

public class ThreeTextLabelsViewModel(string leftSideText = "", string middleText = "", string rightSideText = "")
    : INotifyPropertyChanged
{
    private string _leftSideText = leftSideText;
    private string _middleText = middleText;
    private string _rightSideText = rightSideText;

    public string LeftSideText
    {
        get => _leftSideText;
        set => SetField(ref _leftSideText, value);
    }

    public string MiddleText
    {
        get => _middleText;
        set => SetField(ref _middleText, value);
    }

    public string RightSideText
    {
        get => _rightSideText;
        set => SetField(ref _rightSideText, value);
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