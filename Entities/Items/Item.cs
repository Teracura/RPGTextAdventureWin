using System.ComponentModel.DataAnnotations;

namespace Entities.Items;

public class Item
{
    public Item()
    {
    }

    public Item(ItemTypes type, string name, string description, int price, string category)
    {
        Type = type;
        Name = name;
        Description = description;
        Price = price;
        EquipmentCategory = category;
    }

    [Key] public int ItemId { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int SaveSlot { get; set; }
    public string EquipmentCategory { get; set; }

    public bool CanBeUsedBy(string heroType)
    {
        var itemName = Type.ToString();
        return itemName.StartsWith("Common_") || itemName.StartsWith($"{heroType}_");
    }
}