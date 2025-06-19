namespace RPGTextAdventureWin.Models;

public class SaveSlotViewModel
{
    public int SlotId { get; set; }
    public string DisplayText => $"Save file {SlotId}";
    public string Status { get; set; } = "Empty";

    public Command ClickCommand { get; set; }
}