using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
using System.Text.Json;
namespace RPG_Assistant.Models;
public class Class {
    private ClassType _name;
    private byte _initialHp;
    private byte _hpPerLevel;
    private byte _manaPerLevel;

    //private List<ClassPower> AvailablePowers;
    //private Dictionary<byte, SpellCircle>? SpellCircleProgression;

    public Class(ClassType name) {
        var raw = ClassDataLoader.GetRaw(name);
        _name = name;
        _initialHp = raw.GetProperty("InitialHp").GetByte();
        _hpPerLevel = raw.GetProperty("HpPerLevel").GetByte();
        _manaPerLevel = raw.GetProperty("ManaPerLevel").GetByte();

    }

    public ClassType Name {
        get => _name;
    }
    public byte InitialHp {
        get => _initialHp;
    }
    public byte HpPerLevel {
        get => _hpPerLevel;
    }
    public byte ManaPerLevel {
        get => _manaPerLevel;
    }
}