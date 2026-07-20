using System.Text.Json;
using RPG_Assistant.Enums;

namespace RPG_Assistant.Models;

public class Weapon : Item {
    private readonly byte[] _damage;
    private readonly DamageType _damageType;
    private readonly byte _marginOfThreat;
    private readonly byte _criticalMultiplier;
    private readonly string _range;

    public Weapon(
        string name, byte space, string description, short price,
        byte[] damage, DamageType damageType, byte marginOfThreat, byte criticalMultiplier, string range)
        : base(name, ItemType.Arma, space, description, price) {
        _damage = damage;
        _damageType = damageType;
        _marginOfThreat = marginOfThreat;
        _criticalMultiplier = criticalMultiplier;
        _range = range;
    }

    public static Weapon CreateFromRaw(string name, JsonElement raw, byte space, string description, short price) {
        byte[] damage = raw.GetProperty("dano").EnumerateArray().Select(d => d.GetByte()).ToArray();
        DamageType damageType = Enum.Parse<DamageType>(raw.GetProperty("tipo de dano").GetString()!);
        byte marginOfThreat = raw.GetProperty("margem de ameaca").GetByte();
        byte criticalMultiplier = raw.GetProperty("multiplicador de critico").GetByte();
        string range = raw.GetProperty("alcance").GetString()!;

        return new Weapon(name, space, description, price, damage, damageType, marginOfThreat, criticalMultiplier, range);
    }

    public byte[] Damage => _damage;
    public DamageType DamageType => _damageType;
    public byte MarginOfThreat => _marginOfThreat;
    public byte CriticalMultiplier => _criticalMultiplier;
    public string Range => _range;
}