using System.Text.Json;
using RPG_Assistant.Enums;

namespace RPG_Assistant.Models;

public class Armor : Item {
    private readonly byte _defenseBonus;
    private readonly sbyte _penalty;

    public Armor (
        string name, byte space, string description, short price,
        byte defenseBonus, sbyte penalty) : base(name, ItemType.Armadura, space, description, price) {
        _defenseBonus = defenseBonus;
        _penalty = penalty;
    }

    public static Armor EquipArmor(Armor armor) {
        return new Armor(armor.Name, armor.Space, armor.Description, armor.Price, armor.DefenseBonus, armor.Penalty);
    }

    public static Armor CreateFromRaw(string name, JsonElement raw, byte space, string description, short price) {
        byte defenseBonus = raw.GetProperty("defesa bonus").GetByte();
        sbyte penalty = raw.GetProperty("penalidade").GetSByte();

        return new Armor(name, space, description, price, defenseBonus, penalty);
    }

    public byte DefenseBonus => _defenseBonus;

    public sbyte Penalty => _penalty;
}

