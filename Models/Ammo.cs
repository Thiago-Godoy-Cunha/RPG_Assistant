using System.Text.Json;
using RPG_Assistant.Enums;

namespace RPG_Assistant.Models;

public class Ammo : Item {
    private readonly short _quantity;

    public Ammo(
        string name, byte space, string description, short price,
        short quantity) : base(name, ItemType.Municao, space, description, price) {
        _quantity = quantity;
    }

    public static Ammo CreateFromRaw(string name, JsonElement raw, byte space, string description, short price) {
        byte quantity = raw.GetProperty("quantidade").GetByte();

        return new Ammo(name, space, description, price, quantity);
    }
    public short Quantity => _quantity;

}

