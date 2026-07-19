using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
namespace RPG_Assistant.Models;

public class Item {
    private string _name;
    private ItemType _type;
    private byte _space;
    private string _description;
    private short _price;

    public Item(string name) {
        _name = name;
        var raw = ItemDataLoader.GetRaw(ItemType.Arma, name);
    }

    protected Item(string name, ItemType type, byte space, string description, short price) {
        _name = name;
        _type = type; 
        _space = space;
        _description = description;
        _price = price;

    }
}