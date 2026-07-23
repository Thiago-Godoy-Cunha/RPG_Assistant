using System.Text.Json;
using RPG_Assistant.Enums;

namespace RPG_Assistant.Models;

public class Shield : Item {
    private readonly byte _defenseBonus;
    private readonly sbyte _penalty;

    public Shield(
        string name, byte space, string description, short price,
        byte defenseBonus, sbyte penalty) : base(name, ItemType.Escudo, space, description, price) {
        _defenseBonus = defenseBonus;
        _penalty = penalty;
    }

    public static Shield EquipShield(Shield shield) {
        return new Shield(shield.Name, shield.Space, shield.Description, shield.Price, shield.DefenseBonus, shield.Penalty);
    }
    public static Shield CreateFromRaw(string name, JsonElement raw, byte space, string description, short price) {
        byte defenseBonus = raw.GetProperty("defesa bonus").GetByte();
        sbyte penalty = raw.GetProperty("penalidade").GetSByte();

        return new Shield(name, space, description, price, defenseBonus, penalty);
    }

    public byte DefenseBonus => _defenseBonus;

    public sbyte Penalty => _penalty;
}

