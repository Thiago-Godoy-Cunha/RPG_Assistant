using RPG_Assistant.Enums;
using System.Text.Json;
namespace RPG_Assistant.Models;
public class Class {
    private ClassType _name;
    private byte _initialHp;
    private byte _hpPerLevel;
    private byte _manaPerLevel;
    //private List<ClassPower> AvailablePowers;
    //private Dictionary<byte, SpellCircle>? SpellCircleProgression;

    public Class(string name) {
        _name = Enum.Parse<ClassType>(name);

        string jsonString = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\classes.json");
        using JsonDocument doc = JsonDocument.Parse(jsonString);
        JsonElement listaClasses = doc.RootElement.GetProperty("classes");

        bool encontrou = false;
        foreach (JsonElement classe in listaClasses.EnumerateArray()) {
            string nomeNoJson = classe.GetProperty("Nome").GetString();

            if (nomeNoJson != null && nomeNoJson.Equals(name.ToString(), StringComparison.OrdinalIgnoreCase)) {
                _initialHp = classe.GetProperty("InitialHp").GetByte();
                _hpPerLevel = classe.GetProperty("HpPerLevel").GetByte();
                _manaPerLevel = classe.GetProperty("ManaPerLevel").GetByte();

                encontrou = true;
                break;
            }
        }
    }

    public ClassType Name {
        get => _name;
        set => _name = value;
    }
    public byte InitialHp {
        get => _initialHp;
        set => _initialHp = value;
    }
    public byte HpPerLevel {
        get => _hpPerLevel;
        set => _hpPerLevel = value;
    }
    public byte ManaPerLevel {
        get => _manaPerLevel;
        set => _manaPerLevel = value;
    }
}