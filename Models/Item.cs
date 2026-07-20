using System.Text.Json;
using System.Linq;
using RPG_Assistant.Enums;
using RPG_Assistant.Loading;

namespace RPG_Assistant.Models;

public class Item {
    private readonly string _name;
    private readonly ItemType _type;
    private readonly byte _space;
    private readonly string _description;
    private readonly short _price;

    internal Item(string name, ItemType type, byte space, string description, short price) {
        _name = name;
        _type = type;
        _space = space;
        _description = description;
        _price = price;
    }

    public static Item Create(string name) {
        (ItemType type, JsonElement raw) = ItemDataLoader.GetRaw(name);

        byte space = raw.GetProperty("espacos").GetByte();
        string description = raw.GetProperty("descricao").GetString()!;
        short price = raw.GetProperty("preco").GetInt16();

        return type switch {
            ItemType.Arma => Weapon.CreateFromRaw(name, raw, space, description, price),
            ItemType.Armadura => Armor.CreateFromRaw(name, raw, space, description, price),
            ItemType.Escudo => Shield.CreateFromRaw(name, raw, space, description, price),
            ItemType.Municao => Ammo.CreateFromRaw(name, raw, space, description, price),
            _ => new Item(name, type, space, description, price)
        };
    }

    public string Name => _name;
    public ItemType Type => _type;
    public byte Space => _space;
    public string Description => _description;
    public short Price => _price;
}